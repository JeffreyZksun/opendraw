
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-30 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Windows.Forms;
using FRContainer;

namespace FRPaint
{
    public class PtxToolBar
    {
        public PtxToolBar()
        {
            m_ToolBar = new ToolBar();
            m_ToolBar.ButtonClick += new ToolBarButtonClickEventHandler(
               this.OnToolBarClick);

            m_ButtonList = new FRList<PtxToolButton>();

            m_ToolBar.TextAlign = ToolBarTextAlign.Right;
            m_ToolBar.AutoSize = false;
            m_ToolBar.Height = 25;
            m_ToolBar.ButtonSize = new System.Drawing.Size(30, 20);
        }

        private void OnToolBarClick(
                         System.Object sender,
                         ToolBarButtonClickEventArgs e)
        {
            m_ButtonList[m_ToolBar.Buttons.IndexOf(e.Button)].OnClick();
        }

        public ToolBar GetToolBar()
        {
            return m_ToolBar;
        }

        public void AddButtonToToolBar(PtCommand cmd)
        {
            if (null == cmd) return;
            PtxToolButton button = new PtxToolButton(cmd);
            m_ToolBar.Buttons.Add(button.Button);
            m_ButtonList.Add(button);
        }

        private ToolBar m_ToolBar;
        private FRList<PtxToolButton> m_ButtonList;
    }
}
