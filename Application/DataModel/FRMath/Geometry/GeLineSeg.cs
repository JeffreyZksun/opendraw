
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

namespace FRMath
{
    public class GeLineSeg : Geometry
    {
        #region Constructors
        public GeLineSeg(GePoint sp, GePoint ep)
        {
            if (sp != null)
                m_StartPoint = (GePoint)sp.Clone();
            else
                m_StartPoint = GePoint.kOrigin;

            if (ep != null && !m_StartPoint.IsEqualTo(ep))
                m_EndPoint = (GePoint)ep.Clone();
            else
                m_EndPoint = m_StartPoint + UnitVector.kXAxis.Vector;
        }
        #endregion

        public GePoint ClosestPointTo(GePoint point)
        {
            if (null == point) return null;

            GeLine Line = new GeLine(m_StartPoint, m_EndPoint);

            GePoint ClosePoint = Line.ClosestPointTo(point);

            if(Contains(ClosePoint))
            {
                return ClosePoint;
            }
            else
            {
                double P2S = point.DistanceTo(m_StartPoint);
                double P2E = point.DistanceTo(m_EndPoint);

                if (P2S < P2E)
                    return (GePoint)m_StartPoint.Clone();
                else
                    return (GePoint)m_EndPoint.Clone();
            }
        }

        public bool Contains(GePoint point)
        {
            if (null == point) return false;

            GeLine Line = new GeLine(m_StartPoint, m_EndPoint);
            if (null == Line) return false;

            GePoint ClosestPoint = Line.ClosestPointTo(point);
            if (null == ClosestPoint || !ClosestPoint.IsEqualTo(point))
                return false;

            Vector C2S = new Vector(ClosestPoint, m_StartPoint);
            Vector C2E = new Vector(ClosestPoint, m_EndPoint);

            double angle = C2S.AngleTo(C2E);
            //bool bRet = C2S.AngleTo(C2E);

            if ( MathUtil.IsTwoDoubleEqual(angle
                , MathUtil.PI)) 
            {
                return true;
            }

            return false;
        }

        #region Attributes
        public GePoint StartPoint
        {
            get { return m_StartPoint; }
        }

        public GePoint EndPoint
        {
            get { return m_EndPoint; }
        }

        #endregion


        private GePoint m_StartPoint;
        private GePoint m_EndPoint;
    }
}
