/********************************************************************************
 Copyright (C) 2012 Hugh Bailey <obs.jim@gmail.com>

 This program is free software; you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation; either version 2 of the License, or
 (at your option) any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program; if not, write to the Free Software
 Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307, USA.
********************************************************************************/


#include "GraphicsCapture.h"


bool GraphicsCaptureSource::Init(XElement *data)
{
    this->data = data;
    capture = NULL;

    Log(TEXT("Using graphics capture"));
    return true;
}

GraphicsCaptureSource::~GraphicsCaptureSource()
{
    if(warningID)
    {
        API->RemoveStreamInfo(warningID);
        warningID = 0;
    }

    EndScene(); //should never actually need to be called, but doing it anyway just to be safe
}

bool GetCaptureInfo(CaptureInfo &ci, DWORD processID)
{
    HANDLE hFileMap = OpenFileMapping(FILE_MAP_ALL_ACCESS, FALSE, String() << INFO_MEMORY << int(processID));
    if(hFileMap == NULL)
    {
        AppWarning(TEXT("GetCaptureInfo: Could not open file mapping"));
        return false;
    }

    CaptureInfo *infoIn;
    infoIn = (CaptureInfo*)MapViewOfFile(hFileMap, FILE_MAP_ALL_ACCESS, 0, 0, sizeof(CaptureInfo));
    if(!infoIn)
    {
        AppWarning(TEXT("GetCaptureInfo: Could not map view of file"));
        return false;
    }

    mcpy(&ci, infoIn, sizeof(CaptureInfo));

    if(infoIn)
        UnmapViewOfFile(infoIn);

    if(hFileMap)
        CloseHandle(hFileMap);

    return true;
}

void GraphicsCaptureSource::NewCapture()
{
    if(capture)
    {
        capture->Destroy();
        delete capture;
        capture = NULL;
    }

    if(!hSignalRestart)
    {
        hSignalRestart = GetEvent(String() << RESTART_CAPTURE_EVENT << int(targetProcessID));
        if(!hSignalRestart)
        {
            RUNONCE AppWarning(TEXT("GraphicsCaptureSource::NewCapture:  Could not create restart event"));
            return;
        }
    }

    hSignalEnd = GetEvent(String() << END_CAPTURE_EVENT << int(targetProcessID));
    if(!hSignalEnd)
    {
        RUNONCE AppWarning(TEXT("GraphicsCaptureSource::NewCapture:  Could not create end event"));
        return;
    }

    hSignalReady = GetEvent(String() << CAPTURE_READY_EVENT << int(targetProcessID));
    if(!hSignalReady)
    {
        RUNONCE AppWarning(TEXT("GraphicsCaptureSource::NewCapture:  Could not create ready event"));
        return;
    }

    hSignalExit = GetEvent(String() << APP_EXIT_EVENT << int(targetProcessID));
    if(!hSignalExit)
    {
        RUNONCE AppWarning(TEXT("GraphicsCaptureSource::NewCapture:  Could not create exit event"));
        return;
    }

    CaptureInfo info;
    if(!GetCaptureInfo(info, targetProcessID))
        return;

    bFlip = info.bFlip != 0;

    hwndCapture = (HWND)info.hwndCapture;

    if(info.captureType == CAPTURETYPE_MEMORY)
        capture = new MemoryCapture;
    else if(info.captureType == CAPTURETYPE_SHAREDTEX)
        capture = new SharedTexCapture;
    else
    {
        API->LeaveSceneMutex();

        RUNONCE AppWarning(TEXT("GraphicsCaptureSource::NewCapture: wtf, bad data from the target process"));
        return;
    }

    if(!capture->Init(info))
    {
        capture->Destroy();
        delete capture;
        capture = NULL;
    }
}

void GraphicsCaptureSource::EndCapture()
{
    if(capture)
    {
        capture->Destroy();
        delete capture;
        capture = NULL;
    }

    if(hSignalRestart)
        CloseHandle(hSignalRestart);
    if(hSignalEnd)
        CloseHandle(hSignalEnd);
    if(hSignalReady)
        CloseHandle(hSignalReady);
    if(hSignalExit)
        CloseHandle(hSignalExit);

    hSignalRestart = hSignalEnd = hSignalReady = hSignalExit = NULL;

    bErrorAcquiring = false;

    bCapturing = false;
    captureCheckInterval = -1.0f;
    hwndCapture = NULL;
    targetProcessID = 0;

    if(warningID)
    {
        API->RemoveStreamInfo(warningID);
        warningID = 0;
    }
}

void GraphicsCaptureSource::Preprocess()
{
    if(hSignalExit && WaitForSingleObject(hSignalExit, 0) == WAIT_OBJECT_0)
    {
        EndCapture();
    }

    if(bCapturing && !hSignalReady && targetProcessID)
        hSignalReady = GetEvent(String() << CAPTURE_READY_EVENT << int(targetProcessID));

    if(hSignalReady && (WaitForSingleObject(hSignalReady, 0) == WAIT_OBJECT_0))
        NewCapture();
}

void GraphicsCaptureSource::BeginScene()
{
    if(bCapturing)
        return;

    strWindowClass = data->GetString(TEXT("windowClass"));
    if(strWindowClass.IsEmpty())
        return;

    bStretch = data->GetInt(TEXT("stretchImage")) != 0;
    bIgnoreAspect = data->GetInt(TEXT("ignoreAspect")) != 0;
    bCaptureMouse = data->GetInt(TEXT("captureMouse"), 1) != 0;

    gamma = data->GetInt(TEXT("gamma"), 100);

    if(bCaptureMouse && data->GetInt(TEXT("invertMouse")))
        invertShader = CreatePixelShaderFromFile(TEXT("shaders\\InvertTexture.pShader"));

    drawShader = CreatePixelShaderFromFile(TEXT("shaders\\DrawTexture_ColorAdjust.pShader"));

    AttemptCapture();
}

void GraphicsCaptureSource::AttemptCapture()
{
    hwndTarget = FindWindow(strWindowClass, NULL);
    if(hwndTarget)
    {
        GetWindowThreadProcessId(hwndTarget, &targetProcessID);
        if(!targetProcessID)
        {
            AppWarning(TEXT("GraphicsCaptureSource::BeginScene: GetWindowThreadProcessId failed, GetLastError = %u"), GetLastError());
            bErrorAcquiring = true;
            return;
        }
    }
    else
    {
        if(!warningID)
            warningID = API->AddStreamInfo(Str("Sources.SoftwareCaptureSource.WindowNotFound"), StreamInfoPriority_High);

        bCapturing = false;

        return;
    }

    if(warningID)
    {
        API->RemoveStreamInfo(warningID);
        warningID = 0;
    }

    //-------------------------------------------
    // see if we already hooked the process.  if not, inject DLL

    HANDLE hProcess = OpenProcess(PROCESS_ALL_ACCESS, FALSE, targetProcessID);
    if(hProcess)
    {
        hwndCapture = hwndTarget;

        hSignalRestart = OpenEvent(EVENT_ALL_ACCESS, FALSE, String() << RESTART_CAPTURE_EVENT << int(targetProcessID));
        if(hSignalRestart)
        {
            SetEvent(hSignalRestart);
            bCapturing = true;
            captureWaitCount = 0;
        }
        else
        {
            String strDLLPath;
            DWORD dwDirSize = GetCurrentDirectory(0, NULL);
            strDLLPath.SetLength(dwDirSize);
            GetCurrentDirectory(dwDirSize, strDLLPath);

            strDLLPath << TEXT("\\plugins\\GraphicsCapture");

            BOOL b32bit = TRUE;
            if(Is64BitWindows())
                IsWow64Process(hProcess, &b32bit);

            String strHelper = strDLLPath;
            strHelper << ((b32bit) ? TEXT("\\injectHelper.exe") : TEXT("\\injectHelper64.exe"));

            String strCommandLine;
            strCommandLine << TEXT("\"") << strHelper << TEXT("\" ") << UIntString(targetProcessID);

            //---------------------------------------

            PROCESS_INFORMATION pi;
            STARTUPINFO si;

            zero(&pi, sizeof(pi));
            zero(&si, sizeof(si));
            si.cb = sizeof(si);

            if(CreateProcess(strHelper, strCommandLine, NULL, NULL, FALSE, 0, NULL, strDLLPath, &si, &pi))
            {
                int exitCode = 0;

                WaitForSingleObject(pi.hProcess, INFINITE);
                GetExitCodeProcess(pi.hProcess, (DWORD*)&exitCode);
                CloseHandle(pi.hThread);
                CloseHandle(pi.hProcess);

                if(exitCode == 0)
                {
                    captureWaitCount = 0;
                    bCapturing = true;
                }
                else
                {
                    AppWarning(TEXT("GraphicsCaptureSource::BeginScene: Failed to inject library, error code = %d"), exitCode);
                    bErrorAcquiring = true;
                }
            }
            else
            {
                AppWarning(TEXT("GraphicsCaptureSource::BeginScene: Could not create inject helper, GetLastError = %u"), GetLastError());
                bErrorAcquiring = true;
            }
        }

        CloseHandle(hProcess);
        hProcess = NULL;
    }
    else
    {
        AppWarning(TEXT("GraphicsCaptureSource::BeginScene: OpenProcess failed, GetLastError = %u"), GetLastError());
        bErrorAcquiring = true;
    }
}

void GraphicsCaptureSource::EndScene()
{
    if(capture)
    {
        capture->Destroy();
        delete capture;
        capture = NULL;
    }

    if(invertShader)
    {
        delete invertShader;
        invertShader = NULL;
    }

    if(drawShader)
    {
        delete drawShader;
        drawShader = NULL;
    }

    if(cursorTexture)
    {
        delete cursorTexture;
        cursorTexture = NULL;
    }

    if(!bCapturing)
    {
        return;
    }

    bCapturing = false;

    SetEvent(hSignalEnd);
    EndCapture();
}

void GraphicsCaptureSource::Tick(float fSeconds)
{
    if(bCapturing && !capture)
    {
        if(++captureWaitCount >= API->GetMaxFPS())
        {
            bCapturing = false;
        }
    }

    if(!bCapturing && !bErrorAcquiring)
    {
        captureCheckInterval += fSeconds;
        if(captureCheckInterval >= 3.0f)
        {
            AttemptCapture();
            captureCheckInterval = 0.0f;
        }
    }
    else
    {
        if(!IsWindow(hwndCapture))
            EndCapture();
    }
}

inline double round(double val)
{
    if(!_isnan(val) || !_finite(val))
        return val;

    if(val > 0.0f)
        return floor(val+0.5);
    else
        return floor(val-0.5);
}

void RoundVect2(Vect2 &v)
{
    v.x = float(round(v.x));
    v.y = float(round(v.y));
}

void GraphicsCaptureSource::Render(const Vect2 &pos, const Vect2 &size)
{
    if(capture)
    {
        Shader *lastShader = GetCurrentPixelShader();

        float fGamma = float(-(gamma-100) + 100) * 0.01f;

        LoadPixelShader(drawShader);
        HANDLE hGamma = drawShader->GetParameterByName(TEXT("gamma"));
        if(hGamma)
            drawShader->SetFloat(hGamma, fGamma);

        //----------------------------------------------------------
        // capture mouse

        bMouseCaptured = false;
        if(bCaptureMouse)
        {
            CURSORINFO ci;
            zero(&ci, sizeof(ci));
            ci.cbSize = sizeof(ci);

            if(GetCursorInfo(&ci) && hwndCapture)
            {
                mcpy(&cursorPos, &ci.ptScreenPos, sizeof(cursorPos));

                ScreenToClient(hwndCapture, &cursorPos);

                if(ci.flags & CURSOR_SHOWING)
                {
                    if(ci.hCursor == hCurrentCursor)
                        bMouseCaptured = true;
                    else
                    {
                        HICON hIcon = CopyIcon(ci.hCursor);
                        hCurrentCursor = ci.hCursor;

                        delete cursorTexture;
                        cursorTexture = NULL;

                        if(hIcon)
                        {
                            ICONINFO ii;
                            if(GetIconInfo(hIcon, &ii))
                            {
                                xHotspot = int(ii.xHotspot);
                                yHotspot = int(ii.yHotspot);

                                UINT size;
                                LPBYTE lpData = GetCursorData(hIcon, ii, size);
                                if(lpData)
                                {
                                    cursorTexture = CreateTexture(size, size, GS_BGRA, lpData, FALSE);
                                    if(cursorTexture)
                                        bMouseCaptured = true;

                                    Free(lpData);
                                }

                                DeleteObject(ii.hbmColor);
                                DeleteObject(ii.hbmMask);
                            }

                            DestroyIcon(hIcon);
                        }
                    }
                }
            }
        }

        //----------------------------------------------------------
        // game texture

        Texture *tex = capture->LockTexture();

        Vect2 texPos = Vect2(0.0f, 0.0f);
        Vect2 texStretch = Vect2(1.0f, 1.0f);

        if(tex)
        {
            Vect2 texSize = Vect2(float(tex->Width()), float(tex->Height()));
            Vect2 totalSize = API->GetBaseSize();

            Vect2 center = totalSize*0.5f;

            BlendFunction(GS_BLEND_ONE, GS_BLEND_ZERO);

            if(bStretch)
            {
                if(bIgnoreAspect)
                    texStretch *= totalSize;
                else
                {
                    float xDif = fabsf(totalSize.x-texSize.x);
                    float yDif = fabsf(totalSize.y-texSize.y);
                    float multiplyVal = (xDif < yDif) ? (totalSize.x/texSize.x) : (totalSize.y/texSize.y);
                        
                    texStretch *= texSize*multiplyVal;
                    texPos = center-(texStretch*0.5f);
                }
            }
            else
            {
                texStretch *= texSize;
                texPos = center-(texStretch*0.5f);
            }

            Vect2 sizeAdjust = size/totalSize;
            texPos *= sizeAdjust;
            texPos += pos;
            texStretch *= sizeAdjust;

            RoundVect2(texPos);
            RoundVect2(texSize);

            if(bFlip)
                DrawSprite(tex, 0xFFFFFFFF, texPos.x, texPos.y+texStretch.y, texPos.x+texStretch.x, texPos.y);
            else
                DrawSprite(tex, 0xFFFFFFFF, texPos.x, texPos.y, texPos.x+texStretch.x, texPos.y+texStretch.y);

            capture->UnlockTexture();

            BlendFunction(GS_BLEND_SRCALPHA, GS_BLEND_INVSRCALPHA);

            //----------------------------------------------------------
            // draw mouse

            if(bMouseCaptured && cursorTexture)
            {
                Vect2 newCursorPos  = Vect2(float(cursorPos.x-xHotspot), float(cursorPos.y-xHotspot));
                Vect2 newCursorSize = Vect2(float(cursorTexture->Width()), float(cursorTexture->Height()));

                newCursorPos  /= texSize;
                newCursorSize /= texSize;

                newCursorPos *= texStretch;
                newCursorPos += texPos;

                newCursorSize *= texStretch;

                bool bInvertCursor = false;
                if(invertShader)
                {
                    if(bInvertCursor = ((GetAsyncKeyState(VK_LBUTTON) & 0x8000) != 0 || (GetAsyncKeyState(VK_RBUTTON) & 0x8000) != 0))
                        LoadPixelShader(invertShader);
                }

                DrawSprite(cursorTexture, 0xFFFFFFFF, newCursorPos.x, newCursorPos.y+newCursorSize.y, newCursorPos.x+newCursorSize.x, newCursorPos.y);
            }
        }

        if(lastShader)
            LoadPixelShader(lastShader);
    }
}

Vect2 GraphicsCaptureSource::GetSize() const
{
    return API->GetBaseSize();
}

void GraphicsCaptureSource::UpdateSettings()
{
    EndScene();
    BeginScene();
}

void GraphicsCaptureSource::SetInt(CTSTR lpName, int iVal)
{
    if(scmpi(lpName, TEXT("gamma")) == 0)
    {
        gamma = iVal;
        if(gamma < 50)        gamma = 50;
        else if(gamma > 175)  gamma = 175;
    }
}
