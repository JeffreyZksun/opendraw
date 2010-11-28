//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2009-4-24 Create
//
//////////////////////////////////////////////////////////////////////////
using System.Diagnostics;

namespace FRMath.Adaptor
{
    public abstract class PlaneAdaptor
    {
        #region Implementation

        public bool Contains(PointAdaptor point)
        {
            if (null == point) return false;

            // Ax + By + Cz = D;
            double ret = Normal.X * point.X
                + Normal.Y * point.Y
                + Normal.Z * point.Z;

            return MathUtil.IsTwoDoubleEqual(ret, ConstNumber);
        }

        // The shortest distance of point to plane.
        public double DistanceTo(PointAdaptor point)
        {
            Debug.Assert(point != null);
            double distance = Normal.Dot(point - Point);

            return distance;
        }

        public bool Contains(LineAdaptor line)
        {
            if (null == line) return false;

            if (Contains(line.BasePoint)
                && Normal.IsPerpendicularTo(line.Direction))
                return true;

            return false;
        }
        #endregion


        #region Clone
        public PlaneAdaptor Clone()
        {
            return MathAdaptorFactory.Instance.CreatePlaneAdaptor(Point, Normal);
        }
        #endregion


        #region Abstract functions
        public abstract PointAdaptor ClosestPointTo(PointAdaptor point);
        #endregion


        #region Operator

        #endregion

        #region Attributes

        public abstract UnitVectorAdaptor Normal
        {
            get;
        }
        public abstract PointAdaptor Point
        {
            get ;
        }

        // OPT - This name should be replaced
        // Ax + By + Cz = D;
        public abstract double ConstNumber
        {
            get;
        }

        #endregion
    }
}
