
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
    class GraphicAttributeStack
    {
        public GraphicAttributeStack()
        {
            m_GraphicAttributes = new List<GraphicAttribute>();
        }

        public void AddAttribute(GraphicAttribute attribute)
        {
            // Remove the existing one.
            foreach(GraphicAttribute item in m_GraphicAttributes)
            {
                // Compare by type, other than by reference
                if(item.GetType() == attribute.GetType())
                {
                    m_GraphicAttributes.Remove(item);
                    break;
                }
            }

            m_GraphicAttributes.Add(attribute);
        }

        public void RemoveAttribute(GraphicAttribute attribute)
        {
            foreach (GraphicAttribute item in m_GraphicAttributes)
            {
                // Compare by type, other than by reference
                if (item.GetType() == attribute.GetType())
                {
                    m_GraphicAttributes.Remove(item);
                    break;
                }
            }
        }

        public void ApplyAttributes(GraphicDevice device)
        {
            // Loop all the GraphicAttribute from first to end
            for (int i = 0; i < m_GraphicAttributes.Count; ++i)
            {
                GraphicAttribute attr = m_GraphicAttributes[i];
                attr.Apply(device);
            }
        }

        public void UnApplyAttributes(GraphicDevice device)
        {
            // Loop all the GraphicAttribute from first to end
            for (int i = m_GraphicAttributes.Count - 1; i >= 0; ++i)
            {
                GraphicAttribute attr = m_GraphicAttributes[i];
                attr.UnApply(device);
            }
        }

        public int Count
        {
            get { return m_GraphicAttributes.Count; }
        }

        private List<GraphicAttribute> m_GraphicAttributes;
    }
}
