/*using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using UtilitiesLib.Native;
using System.ComponentModel;

namespace UtilitiesLib.Hooks.Classes
{
    public class HotKeyUtil : NativeWindow
    {
        #region Declarations
        private HotKeyList m_HotKeyList;
        private Control m_Control;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(typeof(HotKeyList))]
        public HotKeyList HotKeyList
        {
            get { return this.m_HotKeyList; }
            set { this.m_HotKeyList = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public HotKeyUtil(Control control)
        {
            this.m_Control = control;
            base.AssignHandle(control.Handle);
            this.m_HotKeyList = new HotKeyList(control.Handle);
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)Win32.WindowsMessages.WM_HOTKEY:
                    {
                        this.WmHotKey(ref m);
                        break;
                    }
                default:
                    {
                        base.WndProc(ref m);
                        break;
                    }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        private void WmHotKey(ref Message m)
        {
            base.WndProc(ref m);

            HotKey[] hotKeys = this.m_HotKeyList.GetHotKeysByAtomID((short)m.WParam.ToInt32());
            foreach (HotKey _hk in hotKeys) _hk.OnHotKey(this.m_Control);
        }
        #endregion
    }
}
*/