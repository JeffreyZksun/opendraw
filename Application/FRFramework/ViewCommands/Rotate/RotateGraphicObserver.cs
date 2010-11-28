
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2009-3-28 Create
//
//////////////////////////////////////////////////////////////////////////

using FRGraphic;
using FRMath;

namespace FRPaint
{
    class RotateGraphicObserver : ViewPaintObserver
    {
        public RotateGraphicObserver(double width, double height)
        {
            m_ViewWidth = width;
            m_ViewHeight = height;
            m_ItemList = null;
        }
        public override void OnPaintDrawing(DeviceContext DeviceCtx) 
        {
            if (null == DeviceCtx) return;

            GraphicContext gpCtx = new DrawGraphicContext(DeviceCtx
                , PtApp.Get().ColorScheme.PreviewCurveColor, Matrix44.Identity);

            GetDisplayList().Draw(gpCtx, Matrix44.Identity);
        }

        private DisplayItemList GetDisplayList()
        {
            if (m_ItemList != null)
                return m_ItemList;

            m_ItemList = new DisplayItemList();

            double radius = MathUtil.Min(m_ViewWidth / 3, m_ViewHeight / 3);
            GePoint center = new GePoint(m_ViewWidth / 2, m_ViewHeight / 2);
            DisplayItemBuilder.GenDisplayItemCircle(m_ItemList , center, radius);

            return m_ItemList;
        }

        // Save the actual size of the view control.
        private double m_ViewWidth;
        private double m_ViewHeight;
        private DisplayItemList m_ItemList = new DisplayItemList();
    }
}
