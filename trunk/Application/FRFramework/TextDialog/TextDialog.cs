using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FRPaint
{
    public partial class TextDialog : Form
    {
        public TextDialog(TextDialogData data)
        {
            InitializeComponent();

            m_Data = data;
            m_bInitializing = true;
        }

        private void TextDialog_Load(object sender, EventArgs e)
        {
            m_bInitializing = true;

            for (int i = 0; i < m_Data.m_FontNameList.Count; i++)
            {
                FontNameComboBox.Items.Add(m_Data.m_FontNameList[i]);
            }
            int FontIndex = FontNameComboBox.FindString(m_Data.m_FontName);
            if (!(FontIndex > 0 && FontIndex < FontNameComboBox.Items.Count))
                FontIndex = -1;
            FontNameComboBox.SelectedIndex = FontIndex;

            for (int i = 0; i < m_Data.m_SizeList.Count; i++)
            {
                SizeComboBox.Items.Add(m_Data.m_SizeList[i]);
            }
            int SizeIndex = SizeComboBox.FindString(m_Data.m_FontSize);
            if (!(SizeIndex > 0 && SizeIndex < SizeComboBox.Items.Count))
                SizeIndex = -1;
            SizeComboBox.SelectedIndex = SizeIndex;

            BoldChk.Checked = m_Data.m_bBold;
            ItalicChk.Checked = m_Data.m_bItalic;
            UnderlineChk.Checked = m_Data.m_bUnderline;

            TextRichBox.Text = m_Data.m_Text;

            ChangeTextFont();

            m_bInitializing = false;
        }

        

        // Change the font of the text
        // We only support the text with same font.
        private void ChangeTextFont()
        {
            Font newFont = m_Data.GetFont();

            if (newFont != null)
                TextRichBox.Font = newFont;
        }

        #region Event
        private void BoldChk_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_bInitializing)
            {
                if (m_Data.m_bBold != BoldChk.Checked)
                {
                    m_Data.m_bBold = BoldChk.Checked;
                    ChangeTextFont();
                }
            }                
        }

        private void ItalicChk_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_bInitializing)
            {
                if (m_Data.m_bItalic != ItalicChk.Checked)
                {
                    m_Data.m_bItalic = ItalicChk.Checked;
                    ChangeTextFont();
                }
            } 
        }

        private void UnderlineChk_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_bInitializing)
            {
                if (m_Data.m_bUnderline != UnderlineChk.Checked)
                {
                    m_Data.m_bUnderline = UnderlineChk.Checked;
                    ChangeTextFont();
                }
            } 
        }

        private void FontNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_bInitializing)
            {
                if (m_Data.m_FontName != FontNameComboBox.Text)
                {
                    m_Data.m_FontName = FontNameComboBox.Text;
                    ChangeTextFont();
                }
            } 
        }

        private void SizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_bInitializing)
            {
                if (m_Data.m_FontSize != SizeComboBox.Text)
                {
                    m_Data.m_FontSize = SizeComboBox.Text;
                    ChangeTextFont();
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_Data.m_Text = TextRichBox.Text;
        }
        #endregion

        #region Data

        private TextDialogData m_Data;
        private bool m_bInitializing;
        #endregion


        
    };
}