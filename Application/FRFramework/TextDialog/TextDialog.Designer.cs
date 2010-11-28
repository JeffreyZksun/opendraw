namespace FRPaint
{
    partial class TextDialog
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FontNameComboBox = new System.Windows.Forms.ComboBox();
            this.SizeComboBox = new System.Windows.Forms.ComboBox();
            this.BoldChk = new System.Windows.Forms.CheckBox();
            this.ItalicChk = new System.Windows.Forms.CheckBox();
            this.UnderlineChk = new System.Windows.Forms.CheckBox();
            this.TextRichBox = new System.Windows.Forms.RichTextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Font";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Size";
            // 
            // FontNameComboBox
            // 
            this.FontNameComboBox.FormattingEnabled = true;
            this.FontNameComboBox.Location = new System.Drawing.Point(11, 40);
            this.FontNameComboBox.Name = "FontNameComboBox";
            this.FontNameComboBox.Size = new System.Drawing.Size(121, 20);
            this.FontNameComboBox.TabIndex = 2;
            this.FontNameComboBox.SelectedIndexChanged += new System.EventHandler(this.FontNameComboBox_SelectedIndexChanged);
            // 
            // SizeComboBox
            // 
            this.SizeComboBox.FormattingEnabled = true;
            this.SizeComboBox.Location = new System.Drawing.Point(150, 40);
            this.SizeComboBox.Name = "SizeComboBox";
            this.SizeComboBox.Size = new System.Drawing.Size(121, 20);
            this.SizeComboBox.TabIndex = 3;
            this.SizeComboBox.SelectedIndexChanged += new System.EventHandler(this.SizeComboBox_SelectedIndexChanged);
            // 
            // BoldChk
            // 
            this.BoldChk.Appearance = System.Windows.Forms.Appearance.Button;
            this.BoldChk.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BoldChk.Location = new System.Drawing.Point(299, 40);
            this.BoldChk.Name = "BoldChk";
            this.BoldChk.Size = new System.Drawing.Size(27, 24);
            this.BoldChk.TabIndex = 4;
            this.BoldChk.Text = "B";
            this.BoldChk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BoldChk.UseVisualStyleBackColor = true;
            this.BoldChk.CheckedChanged += new System.EventHandler(this.BoldChk_CheckedChanged);
            // 
            // ItalicChk
            // 
            this.ItalicChk.Appearance = System.Windows.Forms.Appearance.Button;
            this.ItalicChk.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ItalicChk.Location = new System.Drawing.Point(332, 40);
            this.ItalicChk.Name = "ItalicChk";
            this.ItalicChk.Size = new System.Drawing.Size(27, 24);
            this.ItalicChk.TabIndex = 5;
            this.ItalicChk.Text = "I";
            this.ItalicChk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ItalicChk.UseVisualStyleBackColor = true;
            this.ItalicChk.CheckedChanged += new System.EventHandler(this.ItalicChk_CheckedChanged);
            // 
            // UnderlineChk
            // 
            this.UnderlineChk.Appearance = System.Windows.Forms.Appearance.Button;
            this.UnderlineChk.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UnderlineChk.Location = new System.Drawing.Point(365, 40);
            this.UnderlineChk.Name = "UnderlineChk";
            this.UnderlineChk.Size = new System.Drawing.Size(27, 24);
            this.UnderlineChk.TabIndex = 6;
            this.UnderlineChk.Text = "U";
            this.UnderlineChk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UnderlineChk.UseVisualStyleBackColor = true;
            this.UnderlineChk.CheckedChanged += new System.EventHandler(this.UnderlineChk_CheckedChanged);
            // 
            // TextRichBox
            // 
            this.TextRichBox.Location = new System.Drawing.Point(11, 77);
            this.TextRichBox.Name = "TextRichBox";
            this.TextRichBox.Size = new System.Drawing.Size(381, 102);
            this.TextRichBox.TabIndex = 7;
            this.TextRichBox.Text = "";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(206, 194);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(299, 194);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // TextDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 226);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.TextRichBox);
            this.Controls.Add(this.UnderlineChk);
            this.Controls.Add(this.ItalicChk);
            this.Controls.Add(this.BoldChk);
            this.Controls.Add(this.SizeComboBox);
            this.Controls.Add(this.FontNameComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "TextDialog";
            this.Text = "TextDialog";
            this.Load += new System.EventHandler(this.TextDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox FontNameComboBox;
        private System.Windows.Forms.ComboBox SizeComboBox;
        private System.Windows.Forms.CheckBox BoldChk;
        private System.Windows.Forms.CheckBox ItalicChk;
        private System.Windows.Forms.CheckBox UnderlineChk;
        private System.Windows.Forms.RichTextBox TextRichBox;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}