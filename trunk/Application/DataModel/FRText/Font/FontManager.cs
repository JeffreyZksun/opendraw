
//////////////////////////////////////////////////////////////////////////
//
// Manage all the fonts used by the application
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Drawing;
using System.Drawing.Text;
using FRContainer;
using System.Diagnostics;
using FRMath;

namespace FRText
{
    public class FontManager
    {
        public FontManager()
        {
            m_FontList = new FRList<FRFont>();
            m_ApplicationDefaultFontID = -1;

            AddDefaultFont();
        }

        // The font id is the index of the FRFont in the list.
        public int GetFontID(string FontName, float fSize
            , bool bBold, bool bItalic, bool bUnderLine)
        {
            Debug.Assert(FontName.Trim().Length != 0 && fSize > 0);
            if (FontName.Trim().Length == 0 || fSize < 0) return -1;

            FRFontTemplate FontTemplate = new FRFontTemplate(FontName
                , fSize, bBold, bItalic, bUnderLine);

            int ID = GetFontID(FontTemplate);

            if(ID == -1)
            {
                ID = AddFont(FontTemplate);
            }

            return ID;
        }

        public Font GetFont(string FontName, float fSize
            , bool bBold, bool bItalic, bool bUnderLine)
        {
            int ID = GetFontID(FontName, fSize, bBold, bItalic, bUnderLine);

            return GetFont(ID);
        }

        public Font GetFont(int FontID)
        {
            //Debug.Assert(FontID > -1 && FontID < m_FontList.Count);
            if (!(FontID > -1 && FontID < m_FontList.Count)) return GetApplicationDefaultFont();

            return m_FontList[FontID].m_Font;
        }

        public Font GetApplicationDefaultFont()
        {
            Debug.Assert(m_ApplicationDefaultFontID != -1);
            if (-1 == m_ApplicationDefaultFontID) return null;

            return m_FontList[m_ApplicationDefaultFontID].m_Font;
        }

        public int GetApplicationDefaultFontID()
        {
            return m_ApplicationDefaultFontID;
        }

        private int GetFontID(FRFontTemplate FontTemplate)
        {
            Debug.Assert(FontTemplate != null);

            for (int i = 0; i < m_FontList.Count; i++)
            {
                if (FontTemplate == m_FontList[i].m_FontTemplate)
                    return i;
            }

            return -1;
        }

        private int AddFont(FRFontTemplate FontTemplate)
        {
            Debug.Assert(FontTemplate != null);

            FRFont NewFont = new FRFont(FontTemplate);
            m_FontList.Add(NewFont);

            return m_FontList.Count - 1;
        }

        private void AddDefaultFont()
        {
            Debug.Assert(FontNameList.Count > 0);
            if (FontNameList.Count < 0) return;

            FRFontTemplate FontTemplate = new FRFontTemplate(
                FontNameList[0]
                , 7f, false, false, false);

            m_ApplicationDefaultFontID = AddFont(FontTemplate);
        }

        public FRList<string> FontNameList
        {
            get
            {
                FRList<string> FontNames = new FRList<string>();

                // Get the installed fonts from the system
                InstalledFontCollection MyFont = new InstalledFontCollection();
                FontFamily[] MyFontFamilies = MyFont.Families;
                int Count = MyFontFamilies.Length;
                for (int i = 0; i < Count; i++)
                {
                    string FontName = MyFontFamilies[i].Name;
                    FontNames.Add(FontName);                    
                }

                return FontNames;
            }
        }

        private FRList<FRFont> m_FontList;
        // The default font used by the applicaiton.
        private int m_ApplicationDefaultFontID;
    }
}
