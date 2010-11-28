using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using AppInterface;

namespace Script
{
	/// <summary>
	/// Summary description for ScriptEditor.
	/// </summary>
	public class ScriptEditor : System.Windows.Forms.Form
    {
        #region Controls

        internal System.Windows.Forms.TextBox txtScript;
        internal System.Windows.Forms.Button btnCancel;
        private ToolStrip toolStrip1;
        private ToolStripButton btnCompile;
        private ToolStripButton btnRun;
        internal ListView lvwErrors;
        internal ColumnHeader columnHeader1;
        internal ColumnHeader columnHeader2;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        #endregion

        #region Costructor
        public ScriptEditor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
        }
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptEditor));
            this.txtScript = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnCompile = new System.Windows.Forms.ToolStripButton();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.lvwErrors = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtScript
            // 
            this.txtScript.AcceptsReturn = true;
            this.txtScript.AcceptsTab = true;
            this.txtScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScript.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScript.Location = new System.Drawing.Point(5, 39);
            this.txtScript.Multiline = true;
            this.txtScript.Name = "txtScript";
            this.txtScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtScript.Size = new System.Drawing.Size(787, 273);
            this.txtScript.TabIndex = 3;
            this.txtScript.WordWrap = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(703, 340);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 51);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Close";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCompile,
            this.btnRun});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(804, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCompile
            // 
            this.btnCompile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCompile.Image = ((System.Drawing.Image)(resources.GetObject("btnCompile.Image")));
            this.btnCompile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.Size = new System.Drawing.Size(51, 22);
            this.btnCompile.Text = "Compile";
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
            // 
            // btnRun
            // 
            this.btnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRun.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.Image")));
            this.btnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(27, 22);
            this.btnRun.Text = "Run";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lvwErrors
            // 
            this.lvwErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvwErrors.FullRowSelect = true;
            this.lvwErrors.GridLines = true;
            this.lvwErrors.Location = new System.Drawing.Point(5, 319);
            this.lvwErrors.MultiSelect = false;
            this.lvwErrors.Name = "lvwErrors";
            this.lvwErrors.Size = new System.Drawing.Size(671, 118);
            this.lvwErrors.TabIndex = 9;
            this.lvwErrors.UseCompatibleStateImageBehavior = false;
            this.lvwErrors.View = System.Windows.Forms.View.Details;
            this.lvwErrors.ItemActivate += new System.EventHandler(this.lvwErrors_ItemActivate);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Error";
            this.columnHeader1.Width = 602;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Line";
            this.columnHeader2.Width = 65;
            // 
            // ScriptEditor
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(804, 449);
            this.Controls.Add(this.lvwErrors);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtScript);
            this.Controls.Add(this.btnCancel);
            this.Name = "ScriptEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Script";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        #region Attributes
        public String ScriptSource
		{
			get { return txtScript.Text; }
			set
			{
				txtScript.Text = value;
				txtScript.SelectionLength = 0;
			}
        }
        #endregion

        #region Compile Run
        private void btnCompile_Click(object sender, EventArgs e)
        {
            CompilerResults results;

            Cursor = Cursors.WaitCursor;

            // Find reference
            String reference = GetReference();

            // Compile script
            lvwErrors.Items.Clear();
            ScriptCompiler m_Compiler = new ScriptCompiler();
            //results = Scripting.CompileScript(ScriptSource, reference, Scripting.Languages.VB);
            results = m_Compiler.CompileScript(ScriptSource, reference);

            if (results.Errors.Count == 0)
            {
                MessageBox.Show("Compile successfully");
            }
            else
            {
                ListViewItem l;

                // Add each error as a listview item with its line number
                foreach (CompilerError err in results.Errors)
                {
                    l = new ListViewItem(err.ErrorText);
                    l.SubItems.Add(err.Line.ToString());
                    lvwErrors.Items.Add(l);
                }

                MessageBox.Show("Compile failed with " + results.Errors.Count.ToString() + " errors.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            Cursor = Cursors.Default;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {

            // Find reference
            String reference = GetReference();

            ScriptCompiler compiler = new ScriptCompiler();

            CompilerResults results;
            results = compiler.CompileScript(ScriptSource, reference);

            if (results.Errors.Count == 0)
            {
                IScript _compiledScript = (IScript)compiler.FindInterface(results.CompiledAssembly, "IScript");
                if (_compiledScript != null)
                    _compiledScript.Main(); // Run the transcript
                else
                    MessageBox.Show("No script");

            }
            else
            {
                MessageBox.Show("Compile failed with " + results.Errors.Count.ToString() + " errors.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        #region private function
        private String GetReference()
        {
            String reference;
            reference = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            if (!reference.EndsWith(@"\"))
                reference += @"\";
            reference += "AppInterface.dll";

            return reference;
        }

        private void lvwErrors_ItemActivate(object sender, System.EventArgs e)
        {
            int l = Convert.ToInt32(lvwErrors.SelectedItems[0].SubItems[1].Text);
            int i, pos;

            if (l != 0)
            {
                i = 1;
                pos = 0;
                while (i < l)
                {
                    pos = txtScript.Text.IndexOf(Environment.NewLine, pos + 1);
                    i++;
                }
                txtScript.SelectionStart = pos;
                txtScript.SelectionLength = txtScript.Text.IndexOf(Environment.NewLine, pos + 1) - pos;
            }

            txtScript.Focus();
        }
        #endregion        
    }
}
