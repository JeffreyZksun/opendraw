
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
    public class DisplayItemPoint : DisplayItem
    {
        public DisplayItemPoint(GePoint point)
        {
            m_Point = (GePoint)point.Clone();
        }

        #region Override class DisplayItem
        protected override void OnDraw(GraphicContext gpCtx, Matrix44 parentTrans)
        {
            if (gpCtx != null)
            {
                GePoint worldPoint = MathUtil.GetTransformedPoint(m_Point, parentTrans);

                GraphicDeviceProxy Device =
                    gpCtx.CreateGraphicDeviceProxy();

                Device.DrawPoint(worldPoint);
            }
        }

        protected override bool OnQualify(SelectContext selectCtx, Matrix44 parentTrans)
        {
            if (selectCtx == null) return false;

            GePoint worldPoint = MathUtil.GetTransformedPoint(m_Point, parentTrans);

            double dDiff = worldPoint.DistanceTo(selectCtx.WorldPoint);

            if (dDiff < selectCtx.SelectTolerance)
            {
                selectCtx.AddItem(dDiff, this);
                return true;
            }

            return false;

        }
        #endregion

        public GePoint Point
        {
            get { return m_Point; }
        }
        #region Data
        private GePoint m_Point;
        #endregion
    };
}
