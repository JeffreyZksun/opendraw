
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
    // Save the font information
    class FRFontTemplate
    {
        public FRFontTemplate(string FontName, float fSize
            , bool bBold, bool bItalic, bool bUnderLine)
        {
            m_FontName = FontName;
            m_Size = fSize;
            m_bBold = bBold;
            m_bItalic = bItalic;
            m_bUnderLine = bUnderLine;
        }

        #region override operator ==, !=
        public static bool operator ==(FRFontTemplate lhs, FRFontTemplate rhs)
        {
            if (!(lhs is FRFontTemplate)) return false;
            if (!(rhs is FRFontTemplate)) return false;

            // Why should we process the empty object?
            // Need more test for this.
            if (lhs.m_FontName != rhs.m_FontName)
                return false;
            if (!MathUtil.IsTwoDoubleEqual(lhs.m_Size, rhs.m_Size))
                return false;
            if (lhs.m_bBold != rhs.m_bBold)
                return false;
            if (lhs.m_bItalic != rhs.m_bItalic)
                return false;
            if (lhs.m_bUnderLine != rhs.m_bUnderLine)
                return false;

            return true;
        }

        public static bool operator !=(FRFontTemplate lhs, FRFontTemplate rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is FRFontTemplate)
                return this == (FRFontTemplate)obj;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return 1;
        }
        #endregion

        public string m_FontName;
        public float m_Size;
        public bool m_bBold;
        public bool m_bItalic;
        public bool m_bUnderLine;
    };
}
