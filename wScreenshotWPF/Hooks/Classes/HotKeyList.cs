/*using System;
using System.Collections.ObjectModel;
using System.Text;
using UtilitiesLib.Native;
using System.Windows.Forms;
using System.ComponentModel;

namespace UtilitiesLib.Hooks.Classes
{
    public class HotKeyList : Collection<HotKey>
    {
        #region Declarations
        private IntPtr m_hWnd;
        #endregion

        #region constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_hWnd"></param>
        public HotKeyList(IntPtr _hWnd)
        {
            this.m_hWnd = _hWnd;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HotKey[] ToArray()
        {
            HotKey[] array = new HotKey[this.Count];
            base.CopyTo(array, 0);
            return array;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_AtomID"></param>
        /// <returns></returns>
        public HotKey[] GetHotKeysByAtomID(short _AtomID)
        {
            return Array.FindAll<HotKey>(this.ToArray(), delegate(HotKey _hotKey) { return _hotKey.AtomID == _AtomID; });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_AtomID"></param>
        /// <returns></returns>
        public bool Exists(string _Name)
        {
            return Array.Exists<HotKey>(this.ToArray(), delegate(HotKey _hotKey) { return _hotKey.Name == _Name; });
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void ClearItems()
        {
            Array.ForEach<HotKey>(this.ToArray(), delegate(HotKey _hotKey) { this.RemoveHotKey(_hotKey); });
            base.ClearItems();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, HotKey item)
        {
            if (!this.Exists(item.Name))
            {
                this.AddHotKey(item);
                base.InsertItem(index, item);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        protected override void RemoveItem(int index)
        {
            this.RemoveHotKey(this[index]);
            base.RemoveItem(index);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hotKey"></param>
        private void AddHotKey(HotKey hotKey)
        {
            hotKey.AtomID = Win32.GlobalAddAtom(hotKey.Name);
            uint nModifier = (uint)Win32.MODKEY.MOD_NONE;

            Keys nKey = hotKey.Key;
            if ((uint)(hotKey.Key & Keys.Alt) == (uint)Keys.Alt)
            {
                nModifier += (int)Win32.MODKEY.MOD_ALT;
                nKey ^= Keys.Alt;
            }

            if ((uint)(hotKey.Key & Keys.Control) == (uint)Keys.Control)
            {
                nModifier += (int)Win32.MODKEY.MOD_CONTROL;
                nKey ^= Keys.Control;
            }

            if ((uint)(hotKey.Key & Keys.Shift) == (uint)Keys.Shift)
            {
                nModifier += (uint)Win32.MODKEY.MOD_SHIFT;
                nKey ^= Keys.Shift;
            }

            Win32.RegisterHotKey(this.m_hWnd, hotKey.AtomID, nModifier, (uint) nKey);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hotKey"></param>
        private void RemoveHotKey(HotKey hotKey)
        {
            Win32.UnregisterHotKey(this.m_hWnd, hotKey.AtomID);
            Win32.GlobalDeleteAtom(hotKey.AtomID);
        }
        #endregion
    }
}
*/