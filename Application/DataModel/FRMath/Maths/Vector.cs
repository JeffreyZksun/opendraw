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
    public class Vector 
    {
        #region Constructors
        public Vector(double xx, double yy, double zz)
        {
            m_Proxy = MathAdaptorFactory.Instance.CreateVectorAdaptor
            (xx, yy, zz);
        }

        public Vector(GePoint sp, GePoint ep)
        {
            m_Proxy = MathAdaptorFactory.Instance.CreateVectorAdaptor
            (sp._Proxy, ep._Proxy);
        }

        public Vector()
        {
            m_Proxy = MathAdaptorFactory.Instance.CreateVectorAdaptor
            (1.0,0,0);
        }
        #endregion

        public void Copy(Vector src)
        {
            if (null == src) return;

            m_Proxy = src.m_Proxy.Clone();
        }

        public double Length()
        {
            return m_Proxy.Length();
        }

        public bool IsZero()
        {
            return m_Proxy.IsZero();
        }

        // Get the angle of the two vector [0, PI]
        // a.b = |a||b| cos t 
        public double AngleTo(Vector B)
        {
            if (null == B) return -1;
            return m_Proxy.AngleTo(B._Proxy);
        }

        public bool IsSameDirectionTo(Vector TargetVector)
        {
            if (null == TargetVector) return false;
            return m_Proxy.IsSameDirectionTo(TargetVector._Proxy);
        }

        public bool IsParallelTo(Vector TargetVector)
        {
            if (null == TargetVector) return false;
            return _Proxy.IsParallelTo(TargetVector._Proxy);
        }

        // a . b = a1*b1 + a2*b2 + a3*b3
        public double Dot(Vector B)
        {
            if (null == B) return -1;
            return _Proxy.Dot(B._Proxy);
        }

        //Suppose we have a = a1 i + a2 j + a3 k and b = b1 i + b2 j + b3 k.
        //a x b = (a1 i + a2 j + a3 k) x (b1 i + b2 j + b3 k). 
        // a x b = (a2b3 - a3b2) i + (a3b1 - a1b3) j + (a1b2 - a2b1) k
        public Vector Cross(Vector B)
        {
            if (null == B) return null;
            return new Vector(_Proxy.Cross(B._Proxy));
        }

        public bool IsPerpendicularTo(Vector TargetVector)
        {
            if (null == TargetVector) return false;
            return _Proxy.IsPerpendicularTo(TargetVector._Proxy);
        }

        public Vector ProjectTo(Vector TargetVector)
        {
            // TBD
            Debug.Assert(false, " NO IMP");
            return null;
        }

        public virtual Vector Clone()
        {
            Vector NewVector = new Vector(_Proxy.Clone());
            return NewVector;
        }        

        #region Override operator
        public static Vector operator -(Vector vector)
        {
            return new Vector(-vector._Proxy);
        }

        public static Vector operator *(Vector vector,
        double length)
        {
            return new Vector(vector._Proxy * length);
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
            if (!(obj is Vector)) return false;
            Vector rhs = (Vector)obj;            
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

        #region Data access
        public virtual double X
        {
            get { return _Proxy.X; }
            set { _Proxy.X = value; }
        }
        public virtual double Y
        {
            get { return _Proxy.Y; }
            set { _Proxy.Y = value; }
        }
        public virtual double Z
        {
            get { return _Proxy.Z; }
            set { _Proxy.Z = value; }
        }

        public virtual double this[uint i]
        {
            get
            {
                return _Proxy[i];
            }
            set
            {
                _Proxy[i] = value;
            }

        }

        public UnitVector UnitVector
        {
            get { return new UnitVector(X, Y, Z); }
        }
        #endregion

        #region Proxy
        public Vector(VectorAdaptor proxy)
        {
            m_Proxy = proxy;
        }
        public VectorAdaptor _Proxy
        {
            get { return m_Proxy; }
        }
        private VectorAdaptor m_Proxy;
        #endregion
    }

}
