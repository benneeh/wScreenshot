using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using wScreenshot.Interop;
using wScreenshot.Native;
using wScreenshot.Properties;
using Enum = wScreenshot.Native.Enum;

namespace wScreenshot.Dialog
{
    /// <summary>
    ///     Prompts the user to select a folder.
    /// </summary>
    /// <remarks>
    ///     This class will use the Vista style Select Folder dialog if possible, or the regular FolderBrowserDialog
    ///     if it is not. Note that the Vista style dialog is very different, so using this class without testing
    ///     in both Vista and older Windows versions is not recommended.
    /// </remarks>
    /// <threadsafety instance="false" static="true" />
    [DefaultEvent("HelpRequest"),
     Designer(
         "System.Windows.Forms.Design.FolderBrowserDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
         ), DefaultProperty("SelectedPath"), Description("Prompts the user to select a folder.")]
    public sealed class FolderBrowserDialog
    {
        private string _description;
        private string _selectedPath;

        /// <summary>
        ///     Creates a new instance of the <see cref="FolderBrowserDialog" /> class.
        /// </summary>
        public FolderBrowserDialog()
        {
            Reset();
        }

        #region Public Properties

        /// <summary>
        ///     Gets a value that indicates whether the current OS supports Vista-style common file dialogs.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> on Windows Vista or newer operating systems; otherwise, <see langword="false" />.
        /// </value>
        [Browsable(false)]
        public static bool IsVistaFolderDialogSupported
        {
            get
            {
                return Environment.OSVersion.Platform == PlatformID.Win32NT &&
                       Environment.OSVersion.Version >= new Version(6, 0, 6000);
            }
        }

        /// <summary>
        ///     Gets or sets the descriptive text displayed above the tree view control in the dialog box, or below the list view
        ///     control
        ///     in the Vista style dialog.
        /// </summary>
        /// <value>
        ///     The description to display. The default is an empty string ("").
        /// </value>
        [Category("Folder Browsing"), DefaultValue(""), Localizable(true), Browsable(true),
         Description(
             "The descriptive text displayed above the tree view control in the dialog box, or below the list view control in the Vista style dialog."
             )]
        public string Description
        {
            get { return _description ?? string.Empty; }
            set { _description = value; }
        }

        /// <summary>
        ///     Gets or sets the root folder where the browsing starts from. This property has no effect if the Vista style
        ///     dialog is used.
        /// </summary>
        /// <value>
        ///     One of the <see cref="System.Environment.SpecialFolder" /> values. The default is Desktop.
        /// </value>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException">
        ///     The value assigned is not one of the
        ///     <see cref="System.Environment.SpecialFolder" /> values.
        /// </exception>
        [Localizable(false),
         Description(
             "The root folder where the browsing starts from. This property has no effect if the Vista style dialog is used."
             ), Category("Folder Browsing"), Browsable(true),
         DefaultValue(typeof (Environment.SpecialFolder), "Desktop")]
        public Environment.SpecialFolder RootFolder
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the path selected by the user.
        /// </summary>
        /// <value>
        ///     The path of the folder first selected in the dialog box or the last folder selected by the user. The default is an
        ///     empty string ("").
        /// </value>
        [Browsable(true),
         Editor(
             "System.Windows.Forms.Design.SelectedPathEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
             typeof (UITypeEditor)), Description("The path selected by the user."), DefaultValue(""), Localizable(true),
         Category("Folder Browsing")]
        public string SelectedPath
        {
            get { return _selectedPath ?? string.Empty; }
            set { _selectedPath = value; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the New Folder button appears in the folder browser dialog box. This
        ///     property has no effect if the Vista style dialog is used; in that case, the New Folder button is always shown.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if the New Folder button is shown in the dialog box; otherwise, <see langword="false" />.
        ///     The default is <see langword="true" />.
        /// </value>
        [Browsable(true), Localizable(false),
         Description(
             "A value indicating whether the New Folder button appears in the folder browser dialog box. This property has no effect if the Vista style dialog is used; in that case, the New Folder button is always shown."
             ), DefaultValue(true), Category("Folder Browsing")]
        public bool ShowNewFolderButton
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value that indicates whether to use the value of the <see cref="Description" /> property
        ///     as the dialog title for Vista style dialogs. This property has no effect on old style dialogs.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> to indicate that the value of the <see cref="Description" /> property is used as dialog
        ///     title; <see langword="false" />
        ///     to indicate the value is added as additional text to the dialog. The default is <see langword="false" />.
        /// </value>
        [Category("Folder Browsing"), DefaultValue(false),
         Description(
             "A value that indicates whether to use the value of the Description property as the dialog title for Vista style dialogs. This property has no effect on old style dialogs."
             )]
        public bool UseDescriptionForTitle
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Resets all properties to their default values.
        /// </summary>
        public void Reset()
        {
            _description = string.Empty;
            UseDescriptionForTitle = false;
            _selectedPath = string.Empty;
            RootFolder = Environment.SpecialFolder.Desktop;
            ShowNewFolderButton = true;
        }

        /// <summary>
        ///     Displays the folder browser dialog.
        /// </summary>
        /// <returns>If the user clicks the OK button, <see langword="true" /> is returned; otherwise, <see langword="false" />.</returns>
        public bool? ShowDialog()
        {
            return ShowDialog(null);
        }

        /// <summary>
        ///     Displays the folder browser dialog.
        /// </summary>
        /// <param name="owner">Handle to the window that owns the dialog.</param>
        /// <returns>If the user clicks the OK button, <see langword="true" /> is returned; otherwise, <see langword="false" />.</returns>
        public bool? ShowDialog(Window owner)
        {
            IntPtr ownerHandle = owner == null ? User32.GetActiveWindow() : new WindowInteropHelper(owner).Handle;
            return IsVistaFolderDialogSupported ? RunDialog(ownerHandle) : RunDialogDownlevel(ownerHandle);
        }

        #endregion Public Methods

        #region Private Methods

        private bool RunDialog(IntPtr owner)
        {
            IFileDialog dialog = null;
            try
            {
                dialog = new NativeFileOpenDialog();
                SetDialogProperties(dialog);
                int result = dialog.Show(owner);
                if (result < 0)
                {
                    if ((uint) result == (uint) HRESULT.ERROR_CANCELLED)
                        return false;
                    else
                        throw Marshal.GetExceptionForHR(result);
                }
                GetResult(dialog);
                return true;
            }
            finally
            {
                if (dialog != null)
                    Marshal.FinalReleaseComObject(dialog);
            }
        }

        private bool RunDialogDownlevel(IntPtr owner)
        {
            IntPtr rootItemIdList = IntPtr.Zero;
            IntPtr resultItemIdList = IntPtr.Zero;
            if (Shell32.SHGetSpecialFolderLocation(owner, RootFolder, ref rootItemIdList) != 0)
            {
                if (Shell32.SHGetSpecialFolderLocation(owner, 0, ref rootItemIdList) != 0)
                {
                    throw new InvalidOperationException(Resources.FolderBrowserDialogNoRootFolder);
                }
            }
            try
            {
                var info = new Struct.BROWSEINFO();
                info.hwndOwner = owner;
                info.lpfn = BrowseCallbackProc;
                info.lpszTitle = Description;
                info.pidlRoot = rootItemIdList;
                info.pszDisplayName = new string('\0', 260);
                info.ulFlags = Enum.BrowseInfoFlags.NewDialogStyle | Enum.BrowseInfoFlags.ReturnOnlyFsDirs;
                if (!ShowNewFolderButton)
                    info.ulFlags |= Enum.BrowseInfoFlags.NoNewFolderButton;
                resultItemIdList = Shell32.SHBrowseForFolder(ref info);
                if (resultItemIdList != IntPtr.Zero)
                {
                    var path = new StringBuilder(260);
                    Shell32.SHGetPathFromIDList(resultItemIdList, path);
                    SelectedPath = path.ToString();
                    return true;
                }
                else
                    return false;
            }
            finally
            {
                if (rootItemIdList != null)
                {
                    IMalloc malloc = Shell32.SHGetMalloc();
                    malloc.Free(rootItemIdList);
                    Marshal.ReleaseComObject(malloc);
                }
                if (resultItemIdList != null)
                {
                    Marshal.FreeCoTaskMem(resultItemIdList);
                }
            }
        }

        private void SetDialogProperties(IFileDialog dialog)
        {
            // Description
            if (!string.IsNullOrEmpty(_description))
            {
                if (UseDescriptionForTitle)
                {
                    dialog.SetTitle(_description);
                }
                else
                {
                    var customize = (IFileDialogCustomize) dialog;
                    customize.AddText(0, _description);
                }
            }

            dialog.SetOptions(Enum.FOS.FOS_PICKFOLDERS | Enum.FOS.FOS_FORCEFILESYSTEM | Enum.FOS.FOS_FILEMUSTEXIST);

            if (!string.IsNullOrEmpty(_selectedPath))
            {
                string parent = Path.GetDirectoryName(_selectedPath);
                if (parent == null || !Directory.Exists(parent))
                {
                    dialog.SetFileName(_selectedPath);
                }
                else
                {
                    string folder = Path.GetFileName(_selectedPath);
                    dialog.SetFolder(Shell32.CreateItemFromParsingName(parent));
                    dialog.SetFileName(folder);
                }
            }
        }

        private void GetResult(IFileDialog dialog)
        {
            IShellItem item;
            dialog.GetResult(out item);
            item.GetDisplayName(Enum.SIGDN.SIGDN_FILESYSPATH, out _selectedPath);
        }

        private int BrowseCallbackProc(IntPtr hwnd, Enum.FolderBrowserDialogMessage msg, IntPtr lParam, IntPtr wParam)
        {
            switch (msg)
            {
                case Enum.FolderBrowserDialogMessage.Initialized:
                    if (SelectedPath.Length != 0)
                        User32.SendMessage(hwnd, Enum.FolderBrowserDialogMessage.SetSelection, new IntPtr(1),
                            SelectedPath);
                    break;

                case Enum.FolderBrowserDialogMessage.SelChanged:
                    if (lParam != IntPtr.Zero)
                    {
                        var path = new StringBuilder(260);
                        bool validPath = Shell32.SHGetPathFromIDList(lParam, path);
                        User32.SendMessage(hwnd, Enum.FolderBrowserDialogMessage.EnableOk, IntPtr.Zero,
                            validPath ? new IntPtr(1) : IntPtr.Zero);
                    }
                    break;
            }
            return 0;
        }

        #endregion Private Methods
    }
}