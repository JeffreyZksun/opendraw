
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using FRMath.Adaptor;

namespace FRMath
{
    public class GeLine : Geometry
    {
        #region Constructors
        public GeLine(GePoint sp, GePoint ep)
        {
            Debug.Assert(sp != null && ep != null);

            VectorAdaptor dir = ep._Proxy - sp._Proxy;
            m_Proxy = MathAdaptorFactory.Instance.CreateLineAdaptor(
                sp._Proxy, dir.UnitVectorAdaptor);
        }

        public GeLine(GePoint point, UnitVector direction)
        {
            Debug.Assert(point != null && direction != null);

            m_Proxy = MathAdaptorFactory.Instance.CreateLineAdaptor(point._Proxy, direction._Proxy);

        }
        #endregion

        #region Distance

        // The shortest distance of point to line.
        public double DistanceTo(GePoint point)
        {
            Debug.Assert(point != null);

            return _Proxy.DistanceTo(point._Proxy);
        }

        // The shortest distance of two lines.
        public double DistanceTo(GeLine lineB)
        {
            Debug.Assert(lineB != null);
            return _Proxy.DistanceTo(lineB._Proxy);            
        }

        // The shortest distance of line to plane.
        public double DistanceTo(GePlane plane)
        {
            Debug.Assert(plane != null);
            return _Proxy.DistanceTo(plane._Proxy);
        }

        #endregion

        #region functions
        public GePoint ClosestPointTo(GePoint point)
        {
            if (null == point) return null;
            PointAdaptor px = _Proxy.ClosestPointTo(point._Proxy);
            return new GePoint(px);
        }

        public GePlane GetPerpendicularPlane(GePoint point)
        {
            if (null == point) return null;
            GePlane Plane = new GePlane(
                _Proxy.GetPerpendicularPlane(point._Proxy));
            return Plane;
        }

        public GePoint GetIntersection(GePlane Plane)
        {
            if (null == Plane) return null;
            return new GePoint(_Proxy.GetIntersection(Plane._Proxy));
        }
        #endregion

        #region Gemotry relationship
        public bool IsOn(GePlane Plane)
        {
            if (null == Plane) return false;

            return _Proxy.IsOn(Plane._Proxy);
        }

        public bool IsParallelTo(GePlane Plane)
        {
            if (null == Plane) return false;

            return _Proxy.IsParallelTo(Plane._Proxy);
        }

        public bool IsParallelTo(GeLine lineB)
        {
            if (null == lineB) return false;

            return _Proxy.IsParallelTo(lineB._Proxy);
        }

        public bool Contains(GePoint point)
        {
            if (null == point) return false;
            return _Proxy.Contains(point._Proxy);
        }

        #endregion

        #region Attributes
        public GePoint BasePoint
        {
            get { return new GePoint(_Proxy.BasePoint); }
        }
        public UnitVector Direction
        {
            get { return new UnitVector(_Proxy.Direction); }
        }

        #endregion

        #region override Object
        public override string ToString()
        {
            return _Proxy.ToString();
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            if (!(obj is GeLine)) return false;
            GeLine rhs = (GeLine)obj;
            return _Proxy.Equals(rhs._Proxy);
        }
        // The hash table key requires that if A.Equals(B),
        // A.GetHashCode must be equal to B.GetHashCode.
        // So we'd better override routines Equals and GetHashCode
        // together to meet that principle.
        //
        // The hash table will work with high efficiency, if the return
        // value of this routine is distributed averagely.
        //
        // Use string to get the hash code, we can get the symmetrical value.
        // But the routine ToString will affect the hash table performance.
        public override int GetHashCode()
        {
            return _Proxy.GetHashCode();
        }
        #endregion

        #region Proxy
        public LineAdaptor _Proxy
        {
            get { return m_Proxy; }
        }
        private LineAdaptor m_Proxy;
        #endregion
    }
}
