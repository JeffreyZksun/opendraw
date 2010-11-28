
//////////////////////////////////////////////////////////////////////////
//
// Create and manage the command state machine.
// Organize the states diagram as a tree.
// It is command level. 
// The state machine is for drawing command.
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using FRContainer;

namespace FRPaint
{
    public class StateManager
    {

        #region enum StateResult
        // It indicate the result of the state machine
        public enum StateResult
        {
            eDone = 0,
            eContinue
        }
        #endregion

        #region Constructor
        public StateManager(StateCommand cmd, CommandState entryState)
        {
            m_CmdExeCtx = new CommandExecutionContext(cmd, this);
            m_EntryState = entryState;
            m_StateList = new FRList<CommandState>();
            m_DataItemList = new FRList<DataItem>();
        }
        #endregion

        #region Active / Terminate state machine
        public bool Active()
        {
            m_CurrentTarget = PushState(m_EntryState);
            if (null == m_CurrentTarget)
                return false;
            
            return true;
        }

        public void Terminate()
        {
            while (CurrentState != null)
            {
                CurrentState.Terminate();

                m_StateList.Remove(CurrentState);
            }
        }

        #endregion

        #region State control. Need more test.
        private EventTarget PushState(CommandState state)
        {
            if (state == null) return null;

            m_StateList.Add(state);

            EventTarget target = state.Avtive(m_CmdExeCtx);
            if (target == null)
            {
                target = NextState();
            }

            return target;
        }

        private EventTarget NextState()
        {
            CommandState state = CurrentState;

            if (state == null) return null;

            CommandState NextState = state.Next();

            if (NextState != null)
                return PushState(NextState);
            else
                return TerminateCurrentState();

        }

        private EventTarget TerminateCurrentState()
        {
            if (CurrentState != null)
            {
                CurrentState.Terminate();

                m_StateList.Remove(CurrentState);

                return NextState();
            }

            return null;
        }
        #endregion

        #region Event anwser - OnEvent
        public StateResult OnEvent(EventContext EventCtx)
        {
            if (null == EventCtx) return StateResult.eContinue;

            EventTarget.EventResult EventRet = ProcessEvent(EventCtx);

            StateResult StateRet = AdjustStateMachine(EventRet);

            return StateRet;
        }
       
        #endregion

        #region SaveData
        // OPT - We should add a class to save data and 
        // answer the state request.
        public void AddItem(DataItem item)
        {
            m_DataItemList.Add(item);
        }
        #endregion

        #region Private methods

        // OPT - this function should be removed.
        private EventTarget.EventResult ProcessEvent(EventContext EventCtx)
        {
            if (null == EventCtx) return EventTarget.EventResult.eContinue;

            EventTarget.EventResult EventRet = EventTarget.EventResult.eContinue;

            EventRet = m_CurrentTarget.OnEvent(EventCtx);     

            return EventRet;
        }

        private StateResult AdjustStateMachine(EventTarget.EventResult EventRet)
        {
            StateResult StateRet = StateResult.eContinue;
            switch (EventRet)
            {
                case EventTarget.EventResult.eContinue:
                    StateRet = StateResult.eContinue;
                    break;
                case EventTarget.EventResult.eDone:
                    m_CurrentTarget = TerminateCurrentState();
                    if (m_CurrentTarget != null)
                        StateRet = StateResult.eContinue;
                    else
                        StateRet = StateResult.eDone;

                    //m_CmdExeCtx.Command.GenGraphics();

                    break;
            }

            return StateRet;
        }
        #endregion

        #region Attributes Current State, Target
        public CommandState CurrentState
        {
            get
            {
                if (m_StateList.Count > 0)
                    return m_StateList[m_StateList.Count - 1];
                else
                    return null;
            }
        }

        public EventTarget CurrentTarget
        {
            get
            {
                return m_CurrentTarget;
            }
        }

        public FRList<DataItem> DataItems
        {
            get
            {
                return m_DataItemList;
            }
        }
        #endregion

        #region Data

        private CommandExecutionContext m_CmdExeCtx;
        private EventTarget m_CurrentTarget;
        private CommandState m_EntryState;
        private FRList<CommandState> m_StateList;

        // save the data collected by the state machine.
        private FRList<DataItem> m_DataItemList;
        #endregion
    };
}
