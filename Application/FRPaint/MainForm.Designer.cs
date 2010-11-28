namespace FRPaint
{
    partial class MainFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.drawBox = new FROpenGL.OpenGLSheet();
            this.StatusLabel = new System.Windows.Forms.StatusStrip();
            this.cmdStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.SketchSheets = new System.Windows.Forms.TabControl();
            this.Sheet1 = new System.Windows.Forms.TabPage();
            this.Sheet2 = new System.Windows.Forms.TabPage();
            //((System.ComponentModel.ISupportInitialize)(this.drawBox)).BeginInit();
            this.StatusLabel.SuspendLayout();
            this.SketchSheets.SuspendLayout();
            this.Sheet1.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawBox
            // 
            this.drawBox.BackColor = System.Drawing.Color.Beige;
            this.drawBox.Location = new System.Drawing.Point(40, 23);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(228, 105);
            this.drawBox.TabIndex = 2;
            this.drawBox.TabStop = false;
            //this.drawBox.DoubleClick += new System.EventHandler(this.drawBox_DoubleClick);
            //this.drawBox.Click += new System.EventHandler(this.drawBox_Click);
            //this.drawBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseDown);
            //this.drawBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseMove);
            this.drawBox.Paint += new System.Windows.Forms.PaintEventHandler(this.drawBox_Paint);
            //this.drawBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseUp);
            // 
            // StatusLabel
            // 
            this.StatusLabel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdStatus});
            this.StatusLabel.Location = new System.Drawing.Point(0, 346);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(517, 22);
            this.StatusLabel.TabIndex = 4;
            this.StatusLabel.Text = "statusStrip1";
            // 
            // cmdStatus
            // 
            this.cmdStatus.Name = "cmdStatus";
            this.cmdStatus.Size = new System.Drawing.Size(137, 17);
            this.cmdStatus.Text = "Copyright Sun Zhongkui";
            // 
            // SketchSheets
            // 
            this.SketchSheets.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.SketchSheets.Controls.Add(this.Sheet1);
            this.SketchSheets.Controls.Add(this.Sheet2);
            this.SketchSheets.Location = new System.Drawing.Point(12, 68);
            this.SketchSheets.Name = "SketchSheets";
            this.SketchSheets.SelectedIndex = 0;
            this.SketchSheets.Size = new System.Drawing.Size(307, 177);
            this.SketchSheets.TabIndex = 6;
            // 
            // Sheet1
            // 
            this.Sheet1.BackColor = System.Drawing.Color.Black;
            this.Sheet1.Controls.Add(this.drawBox);
            this.Sheet1.Location = new System.Drawing.Point(4, 4);
            this.Sheet1.Name = "Sheet1";
            this.Sheet1.Padding = new System.Windows.Forms.Padding(3);
            this.Sheet1.Size = new System.Drawing.Size(299, 152);
            this.Sheet1.TabIndex = 0;
            this.Sheet1.Text = "Sheet1";
            // 
            // Sheet2
            // 
            this.Sheet2.BackColor = System.Drawing.Color.Black;
            this.Sheet2.Location = new System.Drawing.Point(4, 4);
            this.Sheet2.Name = "Sheet2";
            this.Sheet2.Padding = new System.Windows.Forms.Padding(3);
            this.Sheet2.Size = new System.Drawing.Size(299, 152);
            this.Sheet2.TabIndex = 1;
            this.Sheet2.Text = "Sheet2";
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 368);
            this.Controls.Add(this.SketchSheets);
            this.Controls.Add(this.StatusLabel);
            this.Name = "MainFrame";
            this.Text = "FRPaint";
            this.SizeChanged += new System.EventHandler(this.MainFrame_SizeChanged);
            this.Load += new System.EventHandler(this.MainFrame_Load);
            //((System.ComponentModel.ISupportInitialize)(this.drawBox)).EndInit();
            this.StatusLabel.ResumeLayout(false);
            this.StatusLabel.PerformLayout();
            this.SketchSheets.ResumeLayout(false);
            this.Sheet1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FROpenGL.OpenGLSheet drawBox;
        private System.Windows.Forms.StatusStrip StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel cmdStatus;
        private System.Windows.Forms.TabControl SketchSheets;
        private System.Windows.Forms.TabPage Sheet1;
        private System.Windows.Forms.TabPage Sheet2;
    }
}

