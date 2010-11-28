
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
    class FRFont
    {
        public FRFont(FRFontTemplate FontTemplate)
        {
            Debug.Assert(FontTemplate != null);

            m_FontTemplate = FontTemplate;

            FontStyle TheStyle = FontStyle.Regular;
            if (m_FontTemplate.m_bBold)
                TheStyle |= FontStyle.Bold;
            if (m_FontTemplate.m_bItalic)
                TheStyle |= FontStyle.Italic;
            if (m_FontTemplate.m_bUnderLine)
                TheStyle |= FontStyle.Underline;
            try
            {
                m_Font = new Font(m_FontTemplate.m_FontName, m_FontTemplate.m_Size, TheStyle);
            }
            catch
            {
                Debug.Assert(false, "Create font failed");
            }
        }

        public FRFontTemplate m_FontTemplate;
        public Font m_Font;
    };
}
