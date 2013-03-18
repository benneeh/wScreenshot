/*using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using UtilitiesLib.Native;
using System.ComponentModel;

namespace UtilitiesLib.Hooks.Classes
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class HotKey
    {
        #region Declarations
        private Keys m_HotKey = Keys.None;
        private string m_Name = string.Empty;
        private short m_AtomID = 0;
        public event EventHandler<HotKeyEventArgs> HotKeyEvent;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Keys Key
        {
            get { return this.m_HotKey; }
            set { this.m_HotKey = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        internal short AtomID
        {
            get { return this.m_AtomID; }
            set { this.m_AtomID = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public HotKey() : this(Keys.None, string.Empty) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_HotKey"></param>
        /// <param name="_Name"></param>
        public HotKey(Keys _HotKey, string _Name)
        {
            this.m_HotKey = _HotKey;
            this.m_Name = _Name;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        internal void OnHotKey(Control control)
        {
            if (this.HotKeyEvent != null) this.HotKeyEvent(control, new HotKeyEventArgs(this.m_HotKey, this.m_Name));
        }
        #endregion

    }
}
*/
