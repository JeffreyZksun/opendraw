//////////////////////////////////////////////////////////////////////////
//
// This class maintains the organization architecture of the commands
// It saves the internal names of the commands. 
//
// When add a command with internal name, it will check whether there
// is this command in command manager.
// If add a command directly, this command will be added to the command
// manager if there is no.
//
// Usually a tool bar will show a set of commands in a group.
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using FRContainer;

namespace FRPaint
{
    public class CommandGroup
    {
        public CommandGroup(String InternalName, String DisplayName
            , CommandManger CommandManager)
        {
            m_InternalName = InternalName.Clone() as String;
            m_DisplayName = DisplayName.Clone() as String;
            m_Commands = new FRList<String>();
            m_CommandManager = CommandManager;
        }

        public void AddCommand(PtCommand command)
        {
            if(!m_CommandManager.ExistsCommand(command.GetInternalName()))
            {
                m_CommandManager.AddCommand(command);
            }

            m_Commands.Add(command.GetInternalName());
        }

        public  FRList<String> Commands
        {
            get { return m_Commands; }
        }

        public String InternalName
        {
            get { return m_InternalName; }
        }

        private String m_InternalName; // The access key of this group
        private String m_DisplayName; // It will be used as the access name of the tool bar.
        private FRList<String> m_Commands;
        private CommandManger m_CommandManager;
    }
}
