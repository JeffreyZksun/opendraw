//////////////////////////////////////////////////////////////////////////
//
// The base class for the graphic attribute.
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
    public class GraphicAttribute
    {
        public GraphicAttribute(GraphicDevice.AttributeType type)
        {
            m_Type = type;
        }

        public GraphicDevice.AttributeType GetType()
        {
            return m_Type;
        }

        #region graphic attribute
        public virtual void Apply(GraphicDevice device)
        {
            device.Push(m_Type);
        }

        public virtual void UnApply(GraphicDevice device)
        {
            device.Pop();
        }
        #endregion

        private GraphicDevice.AttributeType m_Type;
    }
}
