//////////////////////////////////////////////////////////////////////////
//
// This state will ask the command to get the next state.
//
// Author: Sun Zhongkui
// History:
//  2009-3-27 Create
//
//////////////////////////////////////////////////////////////////////////


namespace FRPaint
{
    // It is the entry state of the state machine
    // It saves all the data.
    class CommandAskState : NodeState
    {
         #region Constructor
        public CommandAskState()
        {
          
        }
        #endregion

        #region State event - override

        public override CommandState OnNext()
        {
            DMStateCommand cmd = m_CmdExeCtx.Command as DMStateCommand;

            return cmd != null ? cmd.GetNextState() : null;
        }
        #endregion
    }
}
