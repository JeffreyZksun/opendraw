
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-30 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Windows.Forms;

namespace FRPaint
{
    class PtxContextMenu
    {
        public PtxContextMenu()
        {
            m_ContextMenu = new ContextMenu();
            m_ContextMenu.Popup += new System.EventHandler(this.OnContextMenuPopup);
        }

        private void AddCommand(int index, PtCommand cmd)
        {
            if(cmd != null)
            {
                PtxMenuItem item = new PtxMenuItem(cmd);
                m_ContextMenu.MenuItems.Add(item.GetMenuItem());
             }
        }

        // When pop up the context menu, we should clear all the items first.
        // Then calculate and add the new items.
        private void OnContextMenuPopup(object sender, System.EventArgs e)
        {
            m_ContextMenu.MenuItems.Clear();

            // We just show the command Done & Cancel for the database command.
            if( PtApp.Get().ActiveCommand.IsDatabaseCommand)
            {
                AddCommand(0, PtApp.Get().GetCommand("Done"));
                AddCommand(0, PtApp.Get().GetCommand("Cancel"));
            }
        }

        public ContextMenu GetContextMenu()
        {
            return m_ContextMenu;
        }

        private ContextMenu m_ContextMenu;
    }
}
