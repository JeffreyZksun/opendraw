//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2009-4-24 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;

namespace FRMath.Adaptor
{
    public abstract class VectorAdaptor
    {

        #region Implementation
        public double Length()
        {
            return System.Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public bool IsZero()
        {
            return MathUtil.IsTwoDoubleEqual(Length(), 0);
        }

        // Get the angle of the two vector [0, PI]
        // a.b = |a||b| cos t 
        public double AngleTo(VectorAdaptor B)
        {
            if (null == B) return -1;

            if (this.IsZero() || B.IsZero()) return 0;

            double dCosValue = this.Dot(B) / (this.Length() * B.Length());
            if (dCosValue > 1.0) dCosValue = 1.0;
            if (dCosValue < -1.0) dCosValue = -1.0;

            return System.Math.Acos(dCosValue);
        }

        public bool IsSameDirectionTo(VectorAdaptor TargetVector)
        {
            if (null == TargetVector) return false;

            double dAngle = AngleTo(TargetVector);

            return MathUtil.IsTwoDoubleEqual(
                dAngle, 0);
        }

        public bool IsParallelTo(VectorAdaptor TargetVector)
        {
            if (null == TargetVector) return false;

            double dAngle = AngleTo(TargetVector);

            return (MathUtil.IsTwoDoubleEqual(
                dAngle, MathUtil.PI)
                || MathUtil.IsTwoDoubleEqual(
                dAngle, 0));
        }

        // a . b = a1*b1 + a2*b2 + a3*b3
        public double Dot(VectorAdaptor B)
        {
            if (null == B) return -1;

            VectorAdaptor A = this;

            double ret = A[1] * B[1]
            + A[2] * B[2]
            + A[3] * B[3];

            return ret;
        }

        //Suppose we have a = a1 i + a2 j + a3 k and b = b1 i + b2 j + b3 k.
        //a x b = (a1 i + a2 j + a3 k) x (b1 i + b2 j + b3 k). 
        // a x b = (a2b3 - a3b2) i + (a3b1 - a1b3) j + (a1b2 - a2b1) k
        public VectorAdaptor Cross(VectorAdaptor B)
        {
            if (null == B) return null;
            VectorAdaptor A = this;

            double i = A[2] * B[3] - A[3] * B[2];
            double j = A[3] * B[1] - A[1] * B[3];
            double k = A[1] * B[2] - A[2] * B[1];

            return MathAdaptorFactory.Instance.CreateVectorAdaptor(i, j, k);
        }

        public bool IsPerpendicularTo(VectorAdaptor TargetVector)
        {
            if (null == TargetVector) return false;

            double dAngle = AngleTo(TargetVector);

            return MathUtil.IsTwoDoubleEqual(
                dAngle, MathUtil.PI / 2);
        }
        #endregion

        #region Clone
        public VectorAdaptor Clone()
        {
            return MathAdaptorFactory.Instance.CreateVectorAdaptor(X, Y, Z);
        }
        #endregion

        #region Abstract functions
        

        #endregion


        #region Operator

        public static VectorAdaptor operator -(VectorAdaptor vec)
        {
            return MathAdaptorFactory.Instance.CreateVectorAdaptor(
                -vec.X, -vec.Y, -vec.Z);
        }

        public static VectorAdaptor operator *(VectorAdaptor vec,
        double length)
        {
            return MathAdaptorFactory.Instance.CreateVectorAdaptor(vec.X * length,
                vec.Y * length, vec.Z * length);
        }
        #endregion

        #region Attributes
        public abstract double X
        {
            get;
            set;
        }
        public abstract double Y
        {
            get;
            set;
        }
        public abstract double Z
        {
            get;
            set;
        }

        // 1--3? Why not 0--2?
        public virtual double this[uint i]
        {
            get
            {
                switch (i)
                {
                    case 1: return X;
                    case 2: return Y;
                    case 3: return Z;
                    default:
                        {
                            Debug.Assert(false, "ERROR INDEX.");
                            return X;
                        }
                }
            }
            set
            {
                switch (i)
                {
                    case 1:
                        X = value;
                        break;
                    case 2:
                        Y = value;
                        break;
                    case 3:
                        Z = value;
                        break;
                    default:
                        Debug.Assert(false, "ERROR INDEX.");
                        break;
                }
            }
        }

        public UnitVectorAdaptor UnitVectorAdaptor
        {
            get
            {
                return MathAdaptorFactory.Instance.CreateUnitVectorAdaptor(X, Y, Z);
            }
        }
        #endregion

        #region Override Object
        public override string ToString()
        {
            return "[" + X + "," + Y + "," + Z + "]";
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            if (!(obj is VectorAdaptor)) return false;

            VectorAdaptor rhs = (VectorAdaptor)obj;
            if (!MathUtil.IsTwoDoubleEqual(this.X, rhs.X)
                || !MathUtil.IsTwoDoubleEqual(this.Y, rhs.Y)
                || !MathUtil.IsTwoDoubleEqual(this.Z, rhs.Z))
                return false;

            return true;
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
            return ToString().GetHashCode();
        }
        #endregion
    }
}
