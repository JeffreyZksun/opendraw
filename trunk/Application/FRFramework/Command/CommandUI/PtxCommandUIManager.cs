
//////////////////////////////////////////////////////////////////////////
//
// This class is responsible to maintain the UIs of the commad.
// Such as button, menu
//
// Author: Sun Zhongkui
//
// History:
//  2007-12-03 Create
//
//////////////////////////////////////////////////////////////////////////
using System.Windows.Forms;
using System.Diagnostics;
using System;
using System.Collections;

namespace FRPaint
{
    class PtxCommandUIManager
    {

        public static void InitializeCommandUI(Form form)
        {
            Debug.Assert(form != null);
            if (null == form) return;

            form.ContextMenu = InitializseContextMenu().GetContextMenu();
            form.Controls.Add(InitializeToolBar().GetToolBar());
            form.Menu = InitializeMainMenu().GetMainMenu();
        }

        public static PtxContextMenu InitializseContextMenu()
        {
            // Initialize context menu.
            PtxContextMenu ctx = new PtxContextMenu();

            return ctx;
        }

        public static PtxToolBar InitializeToolBar()
        {
            // Initialize the tool bar
            // Create and initialize the ToolBar and ToolBarButton controls.
            PtxToolBar m_ToolBar = new PtxToolBar();
            CommandGroup standard = CommandManger.Get().GetCommandGroup(
                "FRxStandard");
            Debug.Assert(standard != null, "No required group! Maybe you need to create it yourself");

            // We just show the standard bar here
            if (standard != null)
            {
                foreach (String cmdName in standard.Commands)
                {
                    PtCommand cmd = CommandManger.Get().GetCommand(cmdName);
                    m_ToolBar.AddButtonToToolBar(cmd);
                }
            }
            
            return m_ToolBar;
        }

        public static PtxMainMenu InitializeMainMenu()
        {
            // Create an empty MainMenu.
            PtxMainMenu mainMenu = new PtxMainMenu();

            PtxMenuItem menuFile = new PtxMenuItem("&File");
            //PtxMenuItem menuDone = new PtxMenuItem(m_DoneCmd);
            //menuFile.AddSubMenu(menuDone);

            // Add MenuItem objects to the MainMenu.
            mainMenu.AddMenuItem(menuFile);

            return mainMenu;
        }
    }
}
