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
using System;
using System.Collections.Generic;
using System.Text;

namespace FRGraphic
{
    public class LineStyleAttribute : GraphicAttribute
    {
        public LineStyleAttribute(float width)
            : base(GraphicDevice.AttributeType.eLineStyle)
        {
            m_Width = width;
        }

        #region graphic attribute
        public override void Apply(GraphicDevice device)
        {
            device.Push(GetType());
            device.SetLineWidth(m_Width);
        }
        #endregion

        private float m_Width;
    }
}
