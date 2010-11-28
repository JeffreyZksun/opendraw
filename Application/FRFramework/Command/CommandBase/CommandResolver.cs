
//////////////////////////////////////////////////////////////////////////
//
// FRD - It'll manage the active commands. Process the command nest.
// All the commands's start and commit should be controlled by this class.
//
// OnCommand - The command ask for starting. We need process the 
//      command nesting here.
// OnDone - Ask for commit the current active command.
// OnCancel - Ask for terminate the current active command.
// OnEvent - All the events is sent to command by way of here.
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-23 Create
//
//////////////////////////////////////////////////////////////////////////

using FRContainer;

namespace FRPaint
{
    public class CommandResolver
    {
        public CommandResolver()
        {
            m_CommandList = new FRList<PtCommand>();

            m_SelectCmd = PtApp.Get().GetCommand("SelectCmd");
            if (m_SelectCmd != null) m_SelectCmd.OnCommand();
        }

        // There are two kinds of command
        // 1.	Database command, It will change the database. Such as create line, circle.
        // 2.	Non-database command, It won¡¯t change the database. Such as view commands.
        // 
        // Currently, we only support 2-depth command nested. It means there are 
        // two commands in the stack at most. Their combination is [db cmd, non-db cmd].
        // 
        // The following work flow is used to process the nested commands.
        // 1.	If the command stack isn¡¯t empty.
        //      A)	If the new command is database command, terminate all the commands, 
        //          including all the database command and non-database command,  in the stack. 
        //          Such as [Line (, Pan)] ->Circle => [Circle].
        //      B)	If the new command is non-database command, terminate all the 
        //          non-database commands in the stack if there is. The database command will be left 
        //          if there is. Such as [(Line ,) Pan] ->Zoom =>[(Line,) Zoom].
        // 
        // 2.	Active this command and add it to the stack.
        //
        //
        // The entry of all the commands
        public void OnCommand(PtCommand Command)
        {
            if(!m_CommandList.Empty())
            {
                if (Command.IsDatabaseCommand)
                    TerminateAllStackCommands();
                else
                    TerminateNonDatabaseCommands();
            }            

            // Start the command
            bool bSuc = Command.OnCommand();
            if (!bSuc)
                Command.Terminate();
            else
                m_CommandList.Add(Command);
        }

        public void OnCancel()
        {
            if (!m_CommandList.Empty())
            {
                ActiveCommand.Terminate();
                m_CommandList.PopBack();
                ActiveCommand.OnResume();
            }
        }
        public void OnDone()
        {
            if (!m_CommandList.Empty())
            {
                ActiveCommand.Commit();
                m_CommandList.PopBack();
                ActiveCommand.OnResume();
            }
        }

        public void OnEvent(EventContext Ctx)
        {
            ActiveCommand.OnEvent(Ctx);
        }

        public PtCommand ActiveCommand
        {
            get 
            {
                if (!m_CommandList.Empty())
                    return m_CommandList.Back();
                else
                    return m_SelectCmd;
            }
        }

        #region Process nested commands

        // Terminate all the commands in the stack.
        private void TerminateAllStackCommands()
        {
            while (!m_CommandList.Empty())
            {
                PtCommand TopCommand = m_CommandList.Back();
                TopCommand.Terminate();
                m_CommandList.PopBack();
            }
        }

        // Terminate all the non-database commands on the top of the stack.
        private void TerminateNonDatabaseCommands()
        {
            while (!m_CommandList.Empty())
            {
                PtCommand TopCommand = m_CommandList.Back();
                if (TopCommand.IsDatabaseCommand) break;

                TopCommand.Terminate();
                m_CommandList.PopBack();
            }
        }
        #endregion

        private FRList<PtCommand> m_CommandList;

        private PtCommand m_SelectCmd;
    }
}
