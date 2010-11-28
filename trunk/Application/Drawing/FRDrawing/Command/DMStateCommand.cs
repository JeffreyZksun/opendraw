//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
// History:
//  2009-3-27 Create
//
//////////////////////////////////////////////////////////////////////////

using FRPaint;
using FRMath;

namespace FRPaint
{
    abstract class DMStateCommand : StateCommand
    {
        public DMStateCommand(string InternalName, string DisplayName)
            : base(InternalName, DisplayName)
        {
            m_Node = null;
        }

        #region override the command event

        protected override bool OnExcute()
        {
            m_Node = GetNode();
            if (m_Node != null)
            {
                PtApp.ActiveView.TransientObjectObserver.AddNode(m_Node);
            }

            SetCursorShape("Drawing.cur");

            return true;
        }

        protected override void OnTerminate()
        {
            if(m_Node != null)
            {
                PtApp.ActiveView.TransientObjectObserver.RemoveNode(m_Node);
            }
        }

        protected override void OnCommit()
        {
            if (m_Node != null)
            {
                PtApp.ActiveView.TransientObjectObserver.RemoveNode(m_Node);

                PtDocument doc = PtApp.GetActiveDocument();
                doc.AddNode(m_Node);
                m_Node.GetInstance().Compute();
            }
        }
        #endregion

        protected override CommandState GetInitialState()
        {
            return new CommandAskState();
        }

        // This function will be called 
        // when the state machine try to 
        // get the next state.
        public virtual CommandState GetNextState()
        {
            return null;
        }

         // For updating real time.
         // It is called by target class when move the mouse
         public abstract void UpdateGraphics(GePoint MousePosition);
          
         // It was called by target class when d-click
         public virtual void ShowDialog(){}

         protected abstract GraphicNode GetNode();
       
        protected GraphicNode m_Node;
    }
}
