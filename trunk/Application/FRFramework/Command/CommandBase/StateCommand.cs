
//////////////////////////////////////////////////////////////////////////
//
// The state command is only responsible for the initial state.
// For other states, we can query from the initial state. OnNext();
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using FRMath;

// FRD - State commands, this command will maintain a state machine

namespace FRPaint
{
      abstract public class StateCommand : PtCommand
    {
          public StateCommand(string InternalName, string DisplayName)
            : base(InternalName, DisplayName, true)
        {
            m_StateMgr = null;
        }

        #region override the command event

        // Initialize and start the state machine.
        // Generally, the derived class doesn't need to override this function.
        // If it is overridden,it must be called by the derived class.
        protected override bool OnActive()
        {
            if (m_StateMgr == null)
                m_StateMgr = new StateManager(this, GetInitialState());

            bool bSuc = m_StateMgr.Active();
            if (!bSuc)
                return false;

            return true;
        }

        protected override void OnFinish()
        {
            m_StateMgr.Terminate();
        }

         public override void OnEvent(EventContext EventCtx)
        {
            StateManager.StateResult StateRet = m_StateMgr.OnEvent(EventCtx);

            if (StateManager.StateResult.eDone == StateRet)
                PtApp.Get().CmdResolver.OnDone();
        }

        #endregion

        #region State Machine

          protected abstract CommandState GetInitialState();


          protected override EventTarget GetCurrentTraget()
          {
              if (m_StateMgr != null)
                  return m_StateMgr.CurrentTarget;
              else
                  return null;
          }
        #endregion      

        protected StateManager m_StateMgr;
    }
   
}

