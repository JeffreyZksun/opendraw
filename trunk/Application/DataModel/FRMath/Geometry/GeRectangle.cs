//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-23 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;

namespace FRMath
{
    public class GeRectangle : Geometry
    {
        public GeRectangle()
        {
            m_MinPoint = new GePoint(0, 0, 0);
            m_MaxPoint = new GePoint(0, 0, 0);
        }

        public GeRectangle(GeRectangle src)
        {
            if(src != null)
            {
                m_MinPoint = new GePoint(src.MinPoint);
                m_MaxPoint = new GePoint(src.m_MaxPoint);
            }
        }

        public GeRectangle(GePoint Point1, GePoint Point2)
        {

            Debug.Assert(Point1 != null && Point2 != null);

            GePoint tmpP1 = null;
            if (Point1 != null)
                tmpP1 = (GePoint)Point1.Clone();
            else
                tmpP1 = GePoint.kOrigin;

            GePoint tmpP2 = null;
            if (Point2 != null)
                tmpP2 = (GePoint)Point2.Clone();
            else
                tmpP2 = GePoint.kOrigin;

            m_MinPoint = new GePoint(
                MathUtil.Min(tmpP1.X, tmpP2.X),
                MathUtil.Min(tmpP1.Y, tmpP2.Y));
            m_MaxPoint = new GePoint(
                MathUtil.Max(tmpP1.X, tmpP2.X),
                MathUtil.Max(tmpP1.Y, tmpP2.Y));
        }

        public void MoveTo(GePoint Target)
        {
            m_MaxPoint = m_MaxPoint + (Target - m_MinPoint);
            m_MinPoint = (GePoint)Target.Clone();
        }

        public GePoint MinPoint
        { get { return m_MinPoint; } }

        public GePoint MaxPoint
        { get { return m_MaxPoint; } }

        public double Height // Y axis direction
        {
            get { return m_MaxPoint.Y - m_MinPoint.Y; }
        }
        public double Width // X axis direction
        {
            get { return m_MaxPoint.X - m_MinPoint.X; }
        }

        private GePoint m_MinPoint;
        private GePoint m_MaxPoint;
    }
}
