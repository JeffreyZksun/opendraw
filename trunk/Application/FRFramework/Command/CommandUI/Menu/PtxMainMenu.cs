
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
    class PtxMainMenu
    {
        public PtxMainMenu()
        {
            m_MainMenu = new MainMenu();
        }

        public void AddMenuItem(PtxMenuItem item)
        {
            m_MainMenu.MenuItems.Add(item.GetMenuItem());
        }

        public MainMenu GetMainMenu()
        {
            return m_MainMenu;
        }


        private MainMenu m_MainMenu;
    }

    
}
