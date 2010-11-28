
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
    public class GePlane : Geometry
    {
        #region Constructors
        public GePlane(GePoint point, UnitVector Normal)
        {
            Debug.Assert(point != null && Normal != null);
            m_Proxy = MathAdaptorFactory.Instance.CreatePlaneAdaptor(point._Proxy, Normal._Proxy);
        }
        #endregion

        public GePoint ClosestPointTo(GePoint point)
        {
            return new GePoint(_Proxy.ClosestPointTo(point._Proxy));
        }
        #region Distance
        // The shortest distance of point to plane.
        public double DistanceTo(GePoint point)
        {
            Debug.Assert(point != null);       
            return _Proxy.DistanceTo(point._Proxy);
        }
        #endregion


        #region Geometry Relationship

        public bool Contains(GePoint point)
        {
            if (null == point) return false;
            return _Proxy.Contains(point._Proxy);
        }

        public bool Contains(GeLine line)
        {
            if (null == line) return false;
            return _Proxy.Contains(line._Proxy);
        }

        #endregion

        #region Attributes
        public UnitVector Normal
        {
            get { return new UnitVector(_Proxy.Normal); }
        }
        public GePoint Point
        {
            get { return new GePoint(_Proxy.Point); }
        }
        // OPT - This name should be replaced
        // Ax + By + Cz = D;
        public double ConstNumber
        {
            get
            {
                return _Proxy.ConstNumber;
            }
        }

        public GePlane XYPlane
        {
            get
            {
                return new GePlane(
                new GePoint(0, 0, 0), new UnitVector(0, 0, 1));
            }
        }
        public GePlane YZPlane
        {
            get
            {
                return new GePlane(
                new GePoint(0, 0, 0), new UnitVector(1, 0, 0));
            }
        }
        public GePlane XZPlane
        {
            get
            {
                return new GePlane(
                new GePoint(0, 0, 0), new UnitVector(0, 1, 0));
            }
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
            if (!(obj is GePlane)) return false;
            GePlane rhs = (GePlane)obj;
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
        public GePlane(PlaneAdaptor proxy)
        {
            m_Proxy = proxy;
        }
        public PlaneAdaptor _Proxy
        {
            get { return m_Proxy; }
        }
        private PlaneAdaptor m_Proxy;
        #endregion
    }
}
