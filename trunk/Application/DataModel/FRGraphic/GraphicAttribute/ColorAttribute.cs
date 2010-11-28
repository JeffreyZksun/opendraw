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
    class ColorAttribute: GraphicAttribute
    {
        public ColorAttribute(Color color)
            : base(GraphicDevice.AttributeType.eColor)
        {
            m_Color = color;
        }

        #region graphic attribute
        public override void Apply(GraphicDevice device)
        {
            device.Push(GetType());
            device.SetColor(m_Color);
        }
        #endregion

        private Color m_Color;
    }
}
