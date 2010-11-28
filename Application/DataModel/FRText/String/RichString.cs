
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using System.Drawing;
using FRMath;

namespace FRText
{
    public class RichString
    {
        public RichString(FontManager fontManager)
        {
            // This sentence should be removed.
            m_String = "";

            m_FontID = -1;

            m_FontManager = fontManager;
        }

        public string TextString
        {
            get { return m_String; }
            set { m_String = value; }
        }
        public int FontID
        {
            get { return m_FontID; }
            set
            {
                Debug.Assert(value > -1);

                if (value > -1)
                    m_FontID = value;
            }
        }

        public Font GetFont()
        {
            return m_FontManager.GetFont(m_FontID);
        }

        public string GetString()
        {
            return m_String;
        }
                
        public bool Empty
        {
            get { return m_String.Length == 0; }
        }

        private string m_String;
        private int m_FontID;

        private FontManager m_FontManager;
    }
}
