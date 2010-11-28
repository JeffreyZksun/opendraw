
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

// FRD - Define the context required in the state machine.

namespace FRPaint
{
    public class CommandExecutionContext
    {
        public CommandExecutionContext(StateCommand cmd
            ,StateManager StateMgr)
        {
            m_cmd = cmd;
            m_StateMgr = StateMgr;
        }

        #region Attribute - Command, StateMgr
        public StateCommand Command
        {
            get 
            {
                return m_cmd;
            }
        }
        public StateManager StateMgr
        {
            get
            {
                return m_StateMgr;
            }
        }
        #endregion

        private readonly StateCommand m_cmd;
        private readonly StateManager m_StateMgr;
    }
}
