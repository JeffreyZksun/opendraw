//////////////////////////////////////////////////////////////////////////
//
// 
//
// Author: Sun Zhongkui
//
// History:
//  2009-6-10 Create
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace FRGraphic
{
    class PointSizeAttribute : GraphicAttribute
    {
        public PointSizeAttribute(float size)
            : base(GraphicDevice.AttributeType.eLineStyle)
        {
            m_Size = size;
        }

        #region graphic attribute
        public override void Apply(GraphicDevice device)
        {
            device.Push(GetType());
            device.SetPointSize(m_Size);
        }
        #endregion

        private float m_Size;
    }
}
