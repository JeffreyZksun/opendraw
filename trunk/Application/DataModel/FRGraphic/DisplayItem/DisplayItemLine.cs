
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

namespace FRGraphic
{
    public class DisplayItemLine : DisplayItem
    {
        public DisplayItemLine(GePoint sPoint
            , GePoint ePoint)
        {
            //Debug.Assert(sPoint != null && ePoint != null && !sPoint.IsEqualTo(ePoint));
            // There are many places, when create a DLLine, 
            // it doesn't check whether the two points are equal.
            // If we add the check code when create the DLLinw, 
            // the code will look long.
            // So we allow the line segment geometry is null here.
            // It is a invalid DLLine.
            if (sPoint != null && ePoint != null && !sPoint.IsEqualTo(ePoint))
                m_LineSeg = new GeLineSeg(sPoint, ePoint);
            else
                m_LineSeg = null;
        }

        #region Override class DisplayItem
        protected override void OnDraw(GraphicContext gpCtx, Matrix44 parentTrans)
        {
            if (gpCtx != null && m_LineSeg != null)
            {
                GeLineSeg worldLineSeg = MathUtil.GetTransformedLineSeg(m_LineSeg, parentTrans);

                GraphicDeviceProxy Device =
                    gpCtx.CreateGraphicDeviceProxy();
                Device.DrawLine(worldLineSeg.StartPoint, worldLineSeg.EndPoint);
            }
        }

        // OPT - Performance. It used too many time.
        protected override bool OnQualify(SelectContext selectCtx, Matrix44 parentTrans)
        {
            if (null == selectCtx) return false;
            if (null == m_LineSeg) return false;

            GeLineSeg worldLineSeg = MathUtil.GetTransformedLineSeg(m_LineSeg, parentTrans);

            GePoint Point = worldLineSeg.ClosestPointTo(selectCtx.WorldPoint);

            double dDiff = Point.DistanceTo(selectCtx.WorldPoint);

            if (dDiff < selectCtx.SelectTolerance)
            {
                selectCtx.AddItem(dDiff, this);
                return true;
            }

            return false;

        }
        #endregion

        #region Data
        private GeLineSeg m_LineSeg;
        #endregion
    };
}
