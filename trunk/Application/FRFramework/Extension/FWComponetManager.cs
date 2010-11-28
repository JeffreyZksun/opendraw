
//////////////////////////////////////////////////////////////////////////
//
// Initialize all the extension components.
//  Get all the dlls.
//  Load assembly from the dll.
//  Initialize the entry class by using of the specified attributes.
//
// Author: Sun Zhongkui
//
// History:
//  2008-8-15 Create
//
//////////////////////////////////////////////////////////////////////////

using FRContainer;
using System.Reflection;
using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace FRFramework.Extension
{
    public class FWComponetManager
    {

        #region Constructors
        private FWComponetManager()
        {
            m_ComponentList = new FRList<FWComponent>();
            m_ComponentMgr = null;
        }

        // Singleton
        public static FWComponetManager Get()
        {
            if (null == m_ComponentMgr)
                m_ComponentMgr = new FWComponetManager();

            return m_ComponentMgr;
        }

        public static FWComponetManager Instance
        {
            get
            {
                if (null == m_ComponentMgr)
                    m_ComponentMgr = new FWComponetManager();

                return m_ComponentMgr;
            }
        }
        #endregion

        #region Load Components
        public void LoadComponents()
        {
            // We get all the dlls in the application path as the 
            // candidate assemblies. But if there are too many dlls,
            // the performance should be considered. Now, it isn't a 
            // big deal.
            FRList<String> ComponentAssembies = GetCandidateAssemblies();

            LoadComponents(ComponentAssembies);
        }

        private FRList<String> GetCandidateAssemblies()
        {
            FRList<String> ComponentAssembies = new FRList<string>();
            String AppPath = System.Environment.CurrentDirectory;

            String[] filelist = System.IO.Directory.GetFiles(AppPath);
            foreach (String file in filelist)
            {
                // We only get the dll files as the candidate assemblies.
                if (file.EndsWith(".dll"))
                    ComponentAssembies.Add(file);
            }

            return ComponentAssembies;
        }

        private void LoadComponents(FRList<String> ComponentAssembies)
        {
            foreach (String assemblyPath in ComponentAssembies)
            {
                FWComponent component = LoadComponent(assemblyPath);
                if (component != null)
                    m_ComponentList.Add(component);
            }
        }

        private FWComponent LoadComponent(String assemblyPath)
        {
            try
            {
                Assembly ass = Assembly.LoadFrom(assemblyPath);

                // Check whether it is an extension assembly.
                Attribute att = Attribute.GetCustomAttribute(ass, typeof(FRExtensionAttribute));
                if (att == null)
                    return null;

                // Loop to get the entry.
                Type[] types = ass.GetExportedTypes();
                foreach (Type type in types)
                {
                    // We limit there only one entry in the component.
                    if (type.GetCustomAttributes(typeof(FRComponentEntryAttribute), false).Length == 1)
                    {
                        // Create the component entry and initialize the component.
                        FWComponent com = ass.CreateInstance(type.FullName) as FWComponent;
                        if (com != null)
                            com.OnInitialize();

                        return com;
                    }
                }
            }
            catch
            {
                // When load the native dll, the exception will be thrown.
                // So it is anticipative, and we don't pop the asset dialog here.
                //Debug.Assert(false, "Component load failed - " + assemblyPath);
            } 

            return null;
        }
        #endregion

        #region Data
        private FRList<FWComponent> m_ComponentList;
        static private FWComponetManager m_ComponentMgr;
        #endregion
    }
}
