
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
    // It contains the relationship of the command and button UI.
    public class PtxToolButton : ICommandObserver
    {
        public PtxToolButton(PtCommand cmd)
        {
            m_Command = cmd;
            m_Button = new ToolBarButton();
            m_Button.Text = m_Command.DisplayName;
            m_Command.AddObserver(this);
        }

        public void OnStart()
        {
            m_Button.Pushed = true;
        }
        public void OnFinish()
        {
            m_Button.Pushed = false;
        }

        public ToolBarButton Button
        {
            get { return m_Button; }
        }

        public void OnClick()
        {
            //m_Command.OnCommand();
            // OPT - The command work flow should be changed.
            PtApp.Get().OnCommand(m_Command.GetInternalName()); ;
        }

        private ToolBarButton m_Button;
        private PtCommand m_Command;
    }
}
