
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
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using FRFramework.Extension;

// Define the application document view class
// It's the root of the application.
// From here, we can index all the required information.

// Application maintain the documents and views

namespace FRPaint
{ 

    // Singleton
    public class PtApp
    {
        // Singleton
        private PtApp()
        {
            m_Form = null;
            m_StatusBar = null;
            m_Doc = new PtDocument();
            m_View = null;
            m_CmdResolver = null;

            m_ColorScheme = new PtColorScheme();
        }

        static public PtApp Get()
        {
            if (null == m_App)
                m_App = new PtApp();

            return m_App;
        }

        public void Initialize(Form form, FROpenGL.OpenGLSheet PicBox
            , StatusStrip statusBar)
        {
            Debug.Assert(form != null && PicBox != null && statusBar != null);

            // We should set the form first.
            m_Form = form;
            m_StatusBar = statusBar;

            PtView activeView = new PtView(PicBox);
            PtApp.SetActiveView(activeView);

            CommandManger.Get().Initialize();

            m_CmdResolver = new CommandResolver();

            FWComponetManager.Instance.LoadComponents();

            PtxCommandUIManager.InitializeCommandUI(m_Form);
        }
       

        public void SetStatus(string strStatus)
        {
            if (m_StatusBar != null)
            {
                m_StatusBar.Items[0].Text = strStatus;
            }
        }

        // OPT - The method of getting cursor should be optimized
        public Cursor CreateCursor(string CursorResource)
        {
            string ResourcePath = "FRFramework.Resource.CursorShapes.";
            // Get the assembly containing the resource
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream stream = asm.GetManifestResourceStream(ResourcePath + CursorResource);
            Cursor newCursor = new Cursor(stream);

            return newCursor;
        }

        #region View Document 
        static public PtDocument GetActiveDocument()
        {
            return Instance.m_Doc;
        }

        static public PtView GetActiveView()
        {
            return Instance.m_View;
        }

        static public void SetActiveView(PtView view)
        {
            Instance.m_View = view;
        }
        #endregion


        #region Active command handle
        public PtCommand GetCommand(String InternalName)
        {
            return CommandManger.Get().GetCommand(InternalName);
        }

        public void OnCommand(String InternalName)
        {
            PtCommand Command = CommandManger.Get().GetCommand(InternalName);
            if (Command != null) m_CmdResolver.OnCommand(Command);
        }
        public void OnEvent(EventContext Ctx)
        {
            m_CmdResolver.OnEvent(Ctx);
        }

        public PtCommand ActiveCommand
        {
            get { return m_CmdResolver.ActiveCommand; }
        }
        #endregion


        #region Attributes
        public static PtApp Instance 
        {
            get { return Get(); }
        }
        public CommandResolver CmdResolver
        {
            get { return m_CmdResolver; }
        }

        public static PtDocument ActiveDocument
        {
            get { return Instance.m_Doc; }
        }

        public static PtView ActiveView
        {
            get { return Instance.m_View; }
        }

        public PtColorScheme ColorScheme
        {
            get { return m_ColorScheme; }
        }
        #endregion

        #region Data
        static private PtApp m_App;

        // UI
        Form m_Form;
        StatusStrip m_StatusBar;

        // Single document
        private PtDocument m_Doc;
        private PtView m_View;

        // Handle active command.
        private CommandResolver m_CmdResolver;

        // Manage all the colors
        private PtColorScheme m_ColorScheme;
        #endregion
    }


}
