using System;
using System.CodeDom.Compiler;

namespace Script
{
    public class ScriptCompiler
    {
        public ScriptCompiler()
        {
            m_ScriptLanguage = Language.CSharp;
        }

        private Language m_ScriptLanguage;

        public Language ScriptLanguage
        {
            get { return m_ScriptLanguage; }
            set { m_ScriptLanguage = value; }
        }

        public enum Language
        {
            VB,
            CSharp
        }
        /// <summary>
        /// Compile the source code and return the result.
        /// </summary>
        /// <param name="Source">The source code</param>
        /// <param name="Reference">The references</param>
        /// <returns>Compile results</returns>
        public CompilerResults CompileScript(string Source, string Reference)
        {
            CodeDomProvider provider = null;

            switch (ScriptLanguage)
            {
                case Language.VB:
                    provider = new Microsoft.VisualBasic.VBCodeProvider();
                    break;
                case Language.CSharp:
                    provider = new Microsoft.CSharp.CSharpCodeProvider();
                    break;
            }

            return CompileScript(Source, Reference, provider);
        }

        private CompilerResults CompileScript(string Source, string Reference, CodeDomProvider Provider)
        {
            ICodeCompiler compiler = Provider.CreateCompiler();
            CompilerParameters parms = new CompilerParameters();
            CompilerResults results;

            // Configure parameters
            parms.GenerateExecutable = false;
            parms.GenerateInMemory = true;
            parms.IncludeDebugInformation = false;
            if (Reference != null && Reference.Length != 0)
                parms.ReferencedAssemblies.Add(Reference);
            parms.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parms.ReferencedAssemblies.Add("System.dll");

            // Compile
            results = compiler.CompileAssemblyFromSource(parms, Source);

            return results;
        }


        /// <summary>
        /// Create a IScript instance in the passed in assembly.
        /// </summary>
        /// <param name="DLL">In this project, the dll is a temporary one in memory,
        /// not saved in the disk</param>
        /// <param name="InterfaceName">The name of the script interface class. 
        /// It's "IScript" for project</param>
        /// <returns></returns>
        public object FindInterface(System.Reflection.Assembly DLL, string InterfaceName)
        {
            // Loop through types looking for one that implements the given interface
            foreach (Type t in DLL.GetTypes())
            {
                if (t.GetInterface(InterfaceName, true) != null)
                    return DLL.CreateInstance(t.FullName);
            }

            return null;
        }

    }
}
