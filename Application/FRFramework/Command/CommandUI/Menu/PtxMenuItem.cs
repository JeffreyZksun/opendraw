
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-12-03 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Windows.Forms;

namespace FRPaint
{
    class PtxMenuItem
    {
        public PtxMenuItem(string DisplayName)
        {
            m_MenuItem = new MenuItem(DisplayName);
            m_Command = null;
        }

        public PtxMenuItem(PtCommand cmd)
        {
            m_Command = cmd;
            m_MenuItem = new MenuItem(m_Command.DisplayName);
            m_MenuItem.Click += new System.EventHandler(OnClick);
        }

        private void OnClick(object sender, System.EventArgs e)
        {
            if (m_Command != null)
                PtApp.Get().OnCommand(m_Command.GetInternalName());
        }

        public MenuItem GetMenuItem()
        {
            return m_MenuItem;
        }

        public void AddSubMenu(PtxMenuItem SubMenu)
        {
            m_MenuItem.MenuItems.Add(SubMenu.GetMenuItem());
        }

        private MenuItem m_MenuItem;
        private PtCommand m_Command;
    };
}
