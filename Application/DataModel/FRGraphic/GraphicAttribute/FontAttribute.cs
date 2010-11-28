//////////////////////////////////////////////////////////////////////////
//
// 
//
// Author: Sun Zhongkui
//
// History:
//  2009-6-9 Create
//
//////////////////////////////////////////////////////////////////////////
using System.Drawing;

namespace FRGraphic
{
    class FontAttribute : GraphicAttribute
    {
        public FontAttribute(Font font)
            : base(GraphicDevice.AttributeType.eFont)
        {
            m_Font = font;
        }

        #region graphic attribute
        public override void Apply(GraphicDevice device)
        {
            device.Push(GetType());
            device.SetFont(m_Font);
        }
        #endregion

        private Font m_Font;
    }
}
