
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-22 Create
//
//////////////////////////////////////////////////////////////////////////

using FRContainer;
using System;
using System.Drawing.Text;
using System.Drawing;
using System.Diagnostics;
using FRText;

namespace FRPaint
{
    public class TextDialogData
    {
        // It will be initialized by specific data.
        public TextDialogData()
        {
            m_FontManager = PtApp.ActiveDocument.GetFontManager();
            Debug.Assert(m_FontManager != null);
            m_FontNameList = m_FontManager.FontNameList;

            // OPT - these data should be owned by other class.
            m_SizeList = new FRList<string>();
            m_SizeList.Add("5 mm");
            m_SizeList.Add("10 mm");
            m_SizeList.Add("15 mm");
            m_SizeList.Add("20 mm");

            m_FontName = "ËÎÌו";
            m_FontSize = "10 mm";
            m_bBold = false;
            m_bItalic = true;
            m_bUnderline = true;
            m_Text = "";
        }

        // Get the font based on the current data
        public Font GetFont()
        {
            int ID = GetFontID();
            Debug.Assert(ID > -1);

            return m_FontManager.GetFont(ID);
        }

        // Get the font based on the current data
        public int GetFontID()
        {
            DimensionParser parser = new DimensionParser();
            double dSize = 1;
            bool bRet = parser.GetDimension(m_FontSize, ref dSize);
            if (!bRet)
                dSize = 10;

            return m_FontManager.GetFontID(
                    m_FontName, (float)dSize, m_bBold, m_bItalic, m_bUnderline);

        }

        public FRList<string> m_FontNameList;
        public FRList<string> m_SizeList;

        public bool m_bBold;
        public bool m_bItalic;
        public bool m_bUnderline;
        public string m_Text;

        public string m_FontName;
        public string m_FontSize;

        private FontManager m_FontManager;
    }
}
