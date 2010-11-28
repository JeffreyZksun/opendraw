

//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using FRContainer;

// Define the command class
// Commands are application level
// Command sequence - OnCommand-Active-Execute-Terminate/Commit

// OnEvent - Answer the event when the command is executing.
//          The event is the power to force the command state machine.

// OnCommand - The entry of the command when it is clicked.
// OnActive - Active the command when the clicked.
// OnExcute - Execute the command if it is active successfully.
// OnResume - The command is changed to the current command in the 
//              executing command stack.

// There are two exits when finish the command.
// OnTerminate - The command is cancelled.
// OnCommit - The command finished successfully.

namespace FRPaint
{
    // Command internal name

    public abstract class PtCommand
    {
        public PtCommand(string InternalName, bool IsDatabaseCommand)
        {
            Initialize(InternalName, "DefaultName");

            m_IsDatabaseCommand = IsDatabaseCommand;
        }

        public PtCommand(string InternalName, string DisplayName
            , bool IsDatabaseCommand)
        {
            Initialize(InternalName, DisplayName);

            m_IsDatabaseCommand = IsDatabaseCommand;
       }

        private void Initialize(string InternalName, string DisplayName)
        {
            m_InternalName = string.Copy(InternalName);
            m_DisplayName = string.Copy(DisplayName);
            m_CommandObservers = new FRList<ICommandObserver>();
        }

        public string GetInternalName()
        {
            return m_InternalName;
        }

        #region Status information and cursor shape
        public void SetStatus(string strStatus)
        {
            if (GetCurrentTraget() != null)
            {
                GetCurrentTraget().SetStatus(strStatus);
            }
        }
        public void SetCursorShape(string CursorResource)
        {
            if(GetCurrentTraget() != null)
            {
                GetCurrentTraget().SetCursorShape(CursorResource);
            }
        }

        // Get the current target for this command
        protected virtual EventTarget GetCurrentTraget() { return null; }
        #endregion

        #region Entry and exit of the command

        // The entry of the command.
        public bool OnCommand()
        {
            NoticeObservers(true);

            if (Active())
            {
                return Excute();
            }

            return false;
        }


        public void Terminate()
        {
            OnTerminate();

            Fish();
        }

        public void Commit()
        {
            OnCommit();

            Fish();
        }

        private void Fish()
        {
            OnFinish();
            NoticeObservers(false);
        }

        // 
        protected bool Active()
        {
            return OnActive();
        }

        // 
        protected bool Excute()
        {
            return OnExcute();
        }
        #endregion

        #region Command event
        public virtual void OnEvent(EventContext EventCtx) { }

        protected virtual bool OnActive() { return true; }
        protected virtual bool OnExcute() { return true; }
        protected virtual void OnTerminate() { }
        protected virtual void OnCommit(){}
        protected virtual void OnFinish() { }
        public virtual void OnResume(){}
        #endregion

        public string DisplayName
        {
            get { return m_DisplayName; }
        }

        public bool IsDatabaseCommand
        {
            get { return m_IsDatabaseCommand; }
        }

        #region Command observer
        private void NoticeObservers(bool bStart)
        {
            foreach (ICommandObserver observer in m_CommandObservers)
            {
                if (bStart)
                    observer.OnStart();
                else
                    observer.OnFinish();
            }
        }

        public void AddObserver(ICommandObserver observer)
        {
            if (observer != null)
                m_CommandObservers.Add(observer);
        }
        #endregion

        private string m_InternalName;
        private string m_DisplayName;

        private FRList<ICommandObserver> m_CommandObservers;

        // There are two kinds of command
        // 1.	Database command, It will change the database. Such as create line, circle.
        // 2.	Non-database command, It won¡¯t change the database. Such as view commands.
        private bool m_IsDatabaseCommand;
    };

}
