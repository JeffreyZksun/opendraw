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
    public abstract class PointAdaptor
    {
        #region Implementation
        // The shortest distance of point to plane.
        public double DistanceTo(PlaneAdaptor planeProxy)
        {
            Debug.Assert(planeProxy != null);

            return planeProxy.DistanceTo(this);
        }

        public bool IsOn(PlaneAdaptor Plane)
        {
            if (null == Plane) return false;

            return Plane.Contains(this);
        }

        public bool IsOn(LineAdaptor line)
        {
            if (null == line) return false;

            return line.Contains(this);
        }
        #endregion

        #region Clone
        public PointAdaptor Clone()
        {
            return MathAdaptorFactory.Instance.CreatePlaneAdaptor(X, Y, Z);
        }       

        #endregion


        #region Abstract functions

        public abstract double DistanceTo(PointAdaptor pointProxy);


        public abstract PointAdaptor Transform(Matrix44Adaptor transProxy);

        public abstract bool IsEqualTo(PointAdaptor pointProxy);

        public abstract void Move(VectorAdaptor distance);

        #endregion


        #region Operator

        public static VectorAdaptor operator -(PointAdaptor ep,
            PointAdaptor sp)
        {
            return MathAdaptorFactory.Instance.CreateVectorAdaptor(
                ep.X - sp.X, ep.Y - sp.Y, ep.Z - sp.Z);
        }

        public static PointAdaptor operator +(PointAdaptor sp,
            VectorAdaptor distance)
        {
            return MathAdaptorFactory.Instance.CreatePlaneAdaptor(sp.X + distance.X
                , sp.Y + distance.Y, sp.Z + distance.Z);
        }

        #endregion

        #region Attribute
        public abstract float X
        {
            get;
            set;
        }
        public abstract float Y
        {
            get;
            set;
        }
        public abstract float Z
        {
            get;
            set;
        }
        #endregion

        #region Override Object
        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            if (!(obj is PointAdaptor)) return false;

            PointAdaptor rhs = (PointAdaptor)obj;

            if (!MathUtil.IsTwoDoubleEqual(X, rhs.X)
                || !MathUtil.IsTwoDoubleEqual(Y, rhs.Y)
                || !MathUtil.IsTwoDoubleEqual(Z, rhs.Z))
                return false;

            return true;
        }

        public override string ToString()
        {
            return "(" + X + "," + Y + "," + Z + ")";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        #endregion
    }
}
