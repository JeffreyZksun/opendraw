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
    public class UnitVectorAdaptor
    {
        #region Constructors
        public UnitVectorAdaptor(double xx, double yy, double zz)
        {
            m_VectorProxy = MathAdaptorFactory.Instance.CreateVectorAdaptor(xx, yy, zz);
            Unitize();
        }
        public UnitVectorAdaptor(VectorAdaptor src)
        {
            m_VectorProxy = src.Clone();
            Unitize();
        }

        private void Unitize()
        {
            if (!m_VectorProxy.IsZero())
            {
                double dDenominator = m_VectorProxy.Length();
                m_VectorProxy.X = m_VectorProxy.X / dDenominator;
                m_VectorProxy.Y = m_VectorProxy.Y / dDenominator;
                m_VectorProxy.Z = m_VectorProxy.Z / dDenominator;
            }
            else
            {
                m_VectorProxy.X = 1.0;
                m_VectorProxy.Y = 0.0;
                m_VectorProxy.Z = 0.0;
            }

        }
        #endregion

        #region Implementation
        public UnitVectorAdaptor Clone()
        {
            UnitVectorAdaptor NewVector = new UnitVectorAdaptor(m_VectorProxy);
            return NewVector;
        }    
    
        public double Dot(VectorAdaptor B)
        {
            return m_VectorProxy.Dot(B);
        }

        public bool IsPerpendicularTo(VectorAdaptor TargetVector)
        {
            return m_VectorProxy.IsPerpendicularTo(TargetVector);
        }

        public bool IsPerpendicularTo(UnitVectorAdaptor TargetVector)
        {
            return m_VectorProxy.IsPerpendicularTo(TargetVector.m_VectorProxy);
        }

        public VectorAdaptor Cross(VectorAdaptor B)
        {
            return m_VectorProxy.Cross(B);
        }

         public bool IsParallelTo(VectorAdaptor TargetVector)
         {
             return m_VectorProxy.IsParallelTo(TargetVector);
         }
        public bool IsParallelTo(UnitVectorAdaptor TargetVector)
        {
            return m_VectorProxy.IsParallelTo(TargetVector.m_VectorProxy);
        }

        public double Length()
        {
            return m_VectorProxy.Length();
        }

        public double AngleTo(UnitVectorAdaptor B)
         {
             return m_VectorProxy.AngleTo(B.m_VectorProxy);
         }

        #endregion

        #region Data Access, the data cann't be changed
        public double X
        {
            get { return m_VectorProxy.X; }
            set { }
        }
        public double Y
        {
            get { return m_VectorProxy.Y; }
            set { }
        }
        public double Z
        {
            get { return m_VectorProxy.Z; }
            set {}
        }

        public double this[uint i]
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
            set { ;}
        }

        public VectorAdaptor VectorAdaptor
        {
            get { return m_VectorProxy; }
        }
        #endregion

        private VectorAdaptor m_VectorProxy;

        #region Override Object
        public override string ToString()
        {
            return "[" + X + "," + Y + "," + Z + "]";
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            if (!(obj is UnitVectorAdaptor)) return false;

            UnitVectorAdaptor rhs = (UnitVectorAdaptor)obj;
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
