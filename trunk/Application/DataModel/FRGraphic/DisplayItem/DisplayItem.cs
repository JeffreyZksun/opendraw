
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using FRContainer;
using FRMath;
using System.Diagnostics;

// Define all the display items
// Only these items can be shown on the view.

// DP: Composite [DisplayItemXXX]
//     Template Method [DisplayItem::OnQualify()]

namespace FRGraphic
{
    public abstract class DisplayItem
    {

        // This class can't be created  
        protected DisplayItem()
        {
            m_AttributeStack = null;
        }

        public void Draw(GraphicContext gpCtx, Matrix44 parentTrans)
        {
            ApplyAttributes(gpCtx.GetGraphicDevice());

            if (gpCtx != null)
                OnDraw(gpCtx, parentTrans);

            UnApplyAttributes(gpCtx.GetGraphicDevice());
        }

        // DP: Template Method
        public void Qualify(SelectContext selectCtx, Matrix44 parentTrans)
        {
            //if (null == selectCtx || selectCtx.IsHitted) return;
            if (null == selectCtx) return;

            if(OnQualify(selectCtx, parentTrans))
            {

            }
        }

        #region Graphic attribute
        public void AddAttribute(GraphicAttribute attribute)
        {
            if (m_AttributeStack == null)
                m_AttributeStack = new GraphicAttributeStack();

            m_AttributeStack.AddAttribute(attribute);
        }

        public void RemoveAttribute(GraphicAttribute attribute)
        {
            if (m_AttributeStack != null)
            {
                m_AttributeStack.RemoveAttribute(attribute);

                // Release the stack if there is no attribute.
                if (m_AttributeStack.Count == 0)
                    m_AttributeStack = null;
            } 
        }

        private void ApplyAttributes(GraphicDevice device)
        {
            if (m_AttributeStack != null)
                m_AttributeStack.ApplyAttributes(device);
        }
        private void UnApplyAttributes(GraphicDevice device)
        {
            if (m_AttributeStack != null)
                m_AttributeStack.UnApplyAttributes(device);
        }
        #endregion

        #region pure virtual function
        protected abstract void OnDraw(GraphicContext gpCtx, Matrix44 parentTrans);
        protected abstract bool OnQualify(SelectContext selectCtx, Matrix44 parentTrans);
        #endregion

        private GraphicAttributeStack m_AttributeStack;
    };
}
