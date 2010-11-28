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
    class NativePointAdaptor : PointAdaptor
    {
        #region Constructors
        public NativePointAdaptor()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public NativePointAdaptor(double xx, double yy)
        {
            x = xx;
            y = yy;
            z = 0.0;
        }

        public NativePointAdaptor(double xx, double yy, double zz)
        {
            x = xx;
            y = yy;
            z = zz;
        }

        public NativePointAdaptor(PointAdaptor src)
        {
            x = src.X;
            y = src.Y;
            z = src.Z;
        }
        #endregion

        #region Override

        // The shortest distance of two points.
        public override double DistanceTo(PointAdaptor pointProxy)
        {
            NativePointAdaptor point = pointProxy as NativePointAdaptor;
            if (point == null) return 0.0;

            double dist = System.Math.Sqrt(
                (x - point.x) * (x - point.x)
                + (y - point.y) * (y - point.y)
                + (z - point.z) * (z - point.z));

            return dist;
        }

        public override PointAdaptor Transform(Matrix44Adaptor transProxy)
        {
            Debug.Assert(transProxy != null);
            if (null == transProxy) return this;

            PointAdaptor point =  transProxy * this;
            Assign(point);

            return this;
        }

        public override bool IsEqualTo(PointAdaptor pointProxy)
        {
            NativePointAdaptor point = pointProxy as NativePointAdaptor;
            if (point == null) return false;

            return (MathUtil.IsTwoDoubleEqual(x, point.x)
                && MathUtil.IsTwoDoubleEqual(y, point.y)
                && MathUtil.IsTwoDoubleEqual(z, point.z));
        }

        public override void Move(VectorAdaptor distance)
        {
            if (null == distance) return;

            x = x + distance.X;
            y = y + distance.Y;
            z = z + distance.Z;
        }
        #endregion

        #region Private Helper
        private void Assign(PointAdaptor point)
        {
            Debug.Assert(point != null);
            if (null == point) return;

            this.X = point.X;
            this.Y = point.Y;
            this.Z = point.Z;
        }
        #endregion

        #region Operator


        #endregion
                
        #region Attributes
        public override float X
        {
            get { return (float)x; }
            set { x = value; }
        }
        public override float Y
        {
            get { return (float)y; }
            set { y = value; }
        }
        public override float Z
        {
            get { return (float)z; }
            set { z = value; }
        }

        #endregion

        #region Data
        private double x;
        private double y;
        private double z;
        #endregion
    }
}
