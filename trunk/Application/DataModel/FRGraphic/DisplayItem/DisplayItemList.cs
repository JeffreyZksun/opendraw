
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

namespace FRGraphic
{
    public class DisplayItemList : DisplayItem
    {
        public DisplayItemList()
        {
            m_ItemList = new FRList<DisplayItem>();
            m_Transformation = null;
        }

        public void AddItem(DisplayItem newItem)
        {
            if (newItem != null)
                m_ItemList.Add(newItem);
        }

        public void RemoveAll()
        {
            m_ItemList.RemoveAll();
        }

        #region Override class DisplayItem
        protected override void OnDraw(GraphicContext gpCtx, Matrix44 parentTrans)
        {
            for (int i = 0; i < m_ItemList.Count; i++)
                m_ItemList[i].Draw(gpCtx, GetChildrenTransformation(parentTrans));
        }

        protected override bool OnQualify(SelectContext selectCtx, Matrix44 parentTrans)
        {
            foreach (DisplayItem item in m_ItemList)
                item.Qualify(selectCtx, GetChildrenTransformation(parentTrans));

            return false;
        }

        private Matrix44 GetChildrenTransformation(Matrix44 parentTrans)
        {
            Matrix44 childTrans = null;
            if (parentTrans != null && m_Transformation != null)
            {
                childTrans = m_Transformation * parentTrans;
            }
            else if (parentTrans != null) // m_Transformation == null
            {
                childTrans = parentTrans;
            }
            else // trans == null
            {
                childTrans = m_Transformation;
            }

            return childTrans;
        }
        #endregion

        #region Attribute

        public Matrix44 Transformation
        {
            get { return m_Transformation; }
            set { m_Transformation = value; }
        }
        #endregion

        #region Data
        private FRList<DisplayItem> m_ItemList;

        // All the items in this list will be affected by this transformation.
        private Matrix44 m_Transformation; 
        #endregion
    };
}
