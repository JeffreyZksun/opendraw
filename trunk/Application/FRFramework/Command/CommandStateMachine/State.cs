

//////////////////////////////////////////////////////////////////////////
//
// Define the states for the state machine
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using FRContainer;
using FRMath;

namespace FRPaint
{
    public abstract class CommandState
    {
        public CommandState()
        {
            m_CmdExeCtx = null;
        }

        #region State event - active, next, terminate
        public EventTarget Avtive(CommandExecutionContext CmdExeCtx)
        {
            m_CmdExeCtx = CmdExeCtx;

            return OnAvtive();
        }
        public CommandState Next()
        {
            return OnNext();
        }

        public void Terminate()
        {
            OnTerminate();
        }

        public abstract EventTarget OnAvtive();
        public abstract CommandState OnNext();
        public abstract void OnTerminate();

        #endregion

        public CommandExecutionContext CommandExeCtx
        {
            get { return m_CmdExeCtx; }
        }

        protected CommandExecutionContext m_CmdExeCtx;
    }

    // It has no child state, it can return a target
    public abstract class LeafState : CommandState
    {
        #region Constructor
        public LeafState()
        {
            m_Target = null;
        }
        #endregion

        #region State event - override
        public override EventTarget OnAvtive()
        {
            // N/A
            return m_Target;
        }

        public override CommandState OnNext()
        {
            // N/A
            return null;
        }
        public override void OnTerminate()
        {
            // N/A
        }
        #endregion

        protected void SetTarget(EventTarget target)
        {
            m_Target = target;
            if(m_Target != null)
                m_Target.SetParentState(this);
        }

        #region Data

        protected EventTarget m_Target;
        #endregion

    }

    // It is responsible for the states change between its child states.
    // It has no target.
    public class NodeState : CommandState
    {

        #region State event - override
        public override EventTarget OnAvtive()
        {
            // N/A
            return null;
        }

        public override CommandState OnNext()
        {
            // N/A
            return null;
        }
        public override void OnTerminate()
        {

        }
        #endregion
    }
}
