
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using FRContainer;
using FRMath;

namespace FRGraphic
{
    public class DisplayItemCircle : DisplayItem
    {
        public DisplayItemCircle(GePoint CenterPoint
            , double dRadius)
        {
            m_CenterPoint = CenterPoint;
            m_dRadius = dRadius;
        }

        #region Override class DisplayItem
        protected override void OnDraw(GraphicContext gpCtx, Matrix44 parentTrans)
        {
            if (gpCtx != null)
            {
                GePoint worldCenterPoint = MathUtil.GetTransformedPoint(m_CenterPoint, parentTrans);
                
                GraphicDeviceProxy Device =
                    gpCtx.CreateGraphicDeviceProxy();
                Device.DrawCircle(worldCenterPoint, Convert.ToSingle(m_dRadius));
            }
        }

        protected override bool OnQualify(SelectContext selectCtx, Matrix44 parentTrans)
        {
            if (selectCtx == null) return false;

            GePoint worldCenterPoint = MathUtil.GetTransformedPoint(m_CenterPoint, parentTrans);

            double dist = worldCenterPoint.DistanceTo(selectCtx.WorldPoint);

            // Get the plus distance
            double dDiff = dist - m_dRadius;
            if (dDiff < 0) dDiff = -dDiff;

            if (dDiff < selectCtx.SelectTolerance)
            {
                selectCtx.AddItem(dDiff, this);
                return true;
            }

            return false;
        }
        #endregion

        #region Data
        private GePoint m_CenterPoint;
        private double m_dRadius;
        #endregion
    };
}
