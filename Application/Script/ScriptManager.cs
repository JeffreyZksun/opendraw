using System;
using System.Collections.Generic;
using System.Text;
using AppInterface;
using System.IO;
using System.Windows.Forms;
using System.CodeDom.Compiler;

namespace Script
{
    public class ScriptManager
    {
        #region Constructor
        public ScriptManager(IApplication app)
        {
            m_App = app;
        }
        #endregion

        #region Edit Run
        public void Edit(String fileName)
        {
            ScriptEditor frmEditor = new ScriptEditor();

            // Set the source code
            String sourceCode = GetSourceCode(fileName);

            frmEditor.ScriptSource = sourceCode;
            if (frmEditor.ShowDialog() == DialogResult.Cancel)
                return;
        }

        public void Run(String fileName)
        {
            // Set the source code
            String sourceCode = GetSourceCode(fileName);
            // Find reference
            String reference = GetReference();

            ScriptCompiler compiler = new ScriptCompiler();

            CompilerResults results;
            results = compiler.CompileScript(sourceCode, reference);

            if (results.Errors.Count == 0)
            {
                IScript _compiledScript = (IScript)compiler.FindInterface(results.CompiledAssembly, "IScript");
                _compiledScript.Main(); // Run the transcript
               
            }
            else
            {
                MessageBox.Show("Compile failed with " + results.Errors.Count.ToString() + " errors.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        #region private function
        private String GetSourceCode(String fileName)
        {
            String sourceCode = null;
            if (File.Exists(fileName))
            {
                StreamReader st = new StreamReader(fileName);
                sourceCode = st.ReadToEnd();
            }

            return sourceCode;
        }

        private String GetReference()
        {
             String reference;
            reference = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            if (!reference.EndsWith(@"\"))
                reference += @"\";
            reference += "AppInterface.dll";

            return reference;
        }
        #endregion

        private IApplication m_App;
    }
}
