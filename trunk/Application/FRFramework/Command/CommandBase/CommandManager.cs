
//////////////////////////////////////////////////////////////////////////
//
//  Manage the command.
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-23 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using FRContainer;
using System.Collections;
using System.Diagnostics;

namespace FRPaint
{
    // Singleton
    public class CommandManger
    {
        #region Constructors
        private CommandManger()
        {
            m_Commands = new Hashtable(51,0.5f);
            m_CommandGroups = new Hashtable(5, 0.5f);
        }

        static public CommandManger Get()
        {
            return m_CmdManager;
        }
        #endregion

        public void Initialize()
        {
            AddCommand(new DrSelectCmd());

            CommandGroup standard = AddCommandGroup(
                "FRxStandard", "Standard");

            CommandGroup contextMenu = AddCommandGroup(
                "FRxContextMenu", "ContextMenu");

            // Add the necessary context command here.
            // Maybe it should be moved to other place.
            contextMenu.AddCommand(new PtxContextDoneCmd());
            contextMenu.AddCommand(new PtxContextCancelCmd());

            // Add view commands
            standard.AddCommand(new DrViewRotateCmd());
        }

        #region Commands manage - Get Add
        public PtCommand GetCommand(String InternalName)
        {
            return m_Commands[InternalName] as PtCommand;
        }

        public bool ExistsCommand(String InternalName)
        {
            return m_Commands[InternalName] != null;
        }

        public void AddCommand(PtCommand command)
        {
            // We don't want to add two commands with the same internal name.
            bool bExist = m_Commands[command.GetInternalName()] != null;
            Debug.Assert(!bExist
                , "There already exist the command with the same internal name ["
                + command.GetInternalName() + "]");
            if (!bExist)
                m_Commands.Add(command.GetInternalName(), command);
        }

        public Hashtable Commands
        {
            get { return m_Commands; }
        }
        #endregion

        #region Command group manage - Get Add

        public CommandGroup GetCommandGroup(String InternalName)
        {
            return m_CommandGroups[InternalName] as CommandGroup;
        }
        public bool ExitsCommandGroup(String InternalName)
        {
            return m_CommandGroups[InternalName] != null;
        }

        // If there exists, return the existing one.
        public CommandGroup AddCommandGroup(String InternalName, String DisplayName)
        {
            CommandGroup group = GetCommandGroup(InternalName);
            if(group == null)
            {
                group = new CommandGroup(InternalName, DisplayName, this);
                m_CommandGroups.Add(group.InternalName, group);
            }
            return group;
        }

        #endregion

        #region Data

        static private CommandManger m_CmdManager = new CommandManger();

        // We use hashtable to save the commands to improve the access performance.
        private Hashtable m_Commands;

        private Hashtable m_CommandGroups;

        #endregion

    };
}
