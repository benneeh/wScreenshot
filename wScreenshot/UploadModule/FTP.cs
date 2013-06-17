namespace wScreenshot.UploadModule
{
    internal class FTP
    {
        //private void upload()
        //{
        //    while (uploadBuffer.Count > 0)
        //    {
        //        try
        //        {
        //            this.Invoke((MethodInvoker)delegate { Clipboard.SetData(DataFormats.StringFormat, System.Web.HttpUtility.UrlPathEncode((options.FtpDomain + uploadBuffer[0].fileName.Substring(0, uploadBuffer[0].fileName.LastIndexOf('/') + 1) + uploadBuffer[0].fileName.Substring(1 + uploadBuffer[0].fileName.LastIndexOf('/'))))); });
        //            string domain, ftp;
        //            ftp = options.FtpAddress + uploadBuffer[0].fileName;
        //            domain = options.FtpDomain;

        //            FtpWebRequest request = options.getFtpWebRequest(WebRequestMethods.Ftp.UploadFile, true, false, ftp);
        //            Stream uls = null;
        //            try
        //            {
        //                uls = request.GetRequestStream();
        //            }
        //            catch (Exception e)
        //            {
        //                string ftpurl = ftp.Substring(0, ftp.Substring(6).IndexOf('/') + 7);
        //                string temp = ftp.Replace(ftpurl, "");
        //                string folders = temp.Substring(0, temp.LastIndexOf('/'));

        //                //string folders = ftp.Substring(ftp.Substring(6).IndexOf('/') + 7,ftp.LastIndexOf('/'));
        //                string[] dirar = folders.Split('/');
        //                foreach (string dir in dirar)
        //                {
        //                    if (dir != "")
        //                    {
        //                        try
        //                        {
        //                            ftpurl += dir + '/';

        //                            request = options.getFtpWebRequest(WebRequestMethods.Ftp.PrintWorkingDirectory, true, false, ftpurl);

        //                            string s = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            request = options.getFtpWebRequest(WebRequestMethods.Ftp.MakeDirectory, true, false, ftpurl);
        //                            string s = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        //                        }
        //                    }
        //                }
        //                continue;
        //            }

        //            MemoryStream ms = new MemoryStream();
        //            try
        //            {
        //                save(ms, (Bitmap)uploadBuffer[0].b, uploadBuffer[0].imageFormat);

        //                //().Save(ms, uploadBuffer[0].imageFormat);
        //            }
        //            catch (Exception)
        //            {
        //                Bitmap b = ((Bitmap)uploadBuffer[0].b);
        //                Bitmap nb = new Bitmap(b.Width, b.Height);
        //                BitmapData nbd = nb.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
        //                BitmapData bd = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        //                unsafe
        //                {
        //                    int* startBn = (int*)nbd.Scan0;
        //                    int* startB = (int*)bd.Scan0;
        //                    for (int y = 0; y < bd.Height; y++)
        //                    {
        //                        for (int x = 0; x < bd.Width; x++)
        //                        {
        //                            startBn[x + y * bd.Width] = startB[x + y * bd.Width];
        //                        }
        //                    }
        //                    b.UnlockBits(bd);
        //                    nb.UnlockBits(nbd);
        //                }
        //                ms = new MemoryStream();
        //                save(ms, nb, uploadBuffer[0].imageFormat);

        //                //nb.Save(ms, uploadBuffer[0].imageFormat);
        //                nb.Dispose();
        //            }
        //            ms.Position = 0;
        //            byte[] file = ms.ToArray();

        //            int proz = 0;
        //            int sendAtOneTime = 4096;
        //            int sent = 0;
        //            for (int i = 0; sent < file.Length; i++)
        //            {
        //                uls.Write(file, sent, (file.Length - sent < sendAtOneTime) ? file.Length - sent : sendAtOneTime);
        //                sent += (file.Length - sent < sendAtOneTime) ? file.Length - sent : sendAtOneTime;
        //                proz = (int)(((double)sent / (double)file.Length) * 100.0);
        //                if (proz != bt_upload.Value) bt_upload.Value = proz;
        //            }
        //            uls.Close();
        //            upLoadLink(domain + System.Web.HttpUtility.UrlEncode(uploadBuffer[0].fileName));
        //        }
        //        catch (Exception ee)
        //        {
        //            getFilename(-1, null);
        //            uploadComplete();
        //            if (ExceptionHelper.Handle(ee))
        //            {
        //                throw;
        //            }
        //        }
        //        uploadBuffer.RemoveAt(0);
        //    }
        //}
    }
}