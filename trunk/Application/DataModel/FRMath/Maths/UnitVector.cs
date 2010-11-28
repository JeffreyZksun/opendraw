//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-23 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using FRMath.Adaptor;

namespace FRMath
{
    // Need unit vector derive from vector?
    public class UnitVector
    {
        #region Constructors
        public UnitVector(double xx, double yy, double zz)
        {
            m_Proxy = MathAdaptorFactory.Instance.CreateUnitVectorAdaptor(xx, yy, zz);
        }
        public UnitVector(Vector src)
        {
            m_Proxy = MathAdaptorFactory.Instance.CreateUnitVectorAdaptor(
                src.X, src.Y, src.Z);
        }
        
        #endregion

        public UnitVector Clone()
        {
            UnitVector NewVector = new UnitVector(m_Proxy.Clone());
            return NewVector;
        }  

        #region X, Y, Z axix.
        public static UnitVector kXAxis
        {
            get { return new UnitVector(1, 0, 0); }
        }

        public static UnitVector kYAxis
        {
            get { return new UnitVector(0, 1, 0); }
        }
        public static UnitVector kZAxis
        {
            get { return new UnitVector(0, 0, 1); }
        }
        #endregion

        #region Data Access, the data cann't be changed
        public double X
        {
            get { return _Proxy.X; }
        }
        public double Y
        {
            get { return _Proxy.Y; }
        }
        public double Z
        {
            get { return _Proxy.Z; }
        }

        public double this[uint i]
        {
            get { return _Proxy[i]; }
        }

        public Vector Vector
        {
            get { return new Vector(X, Y, Z); }
        }
        #endregion

        #region Proxy
        public UnitVector(UnitVectorAdaptor proxy)
        {
            m_Proxy = proxy;
        }
        public UnitVectorAdaptor _Proxy
        {
            get { return m_Proxy; }
        }
        private UnitVectorAdaptor m_Proxy;
        #endregion

        #region override Object
        public override string ToString()
        {
            return _Proxy.ToString();
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            if (!(obj is UnitVector)) return false;
            UnitVector rhs = (UnitVector)obj;
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
    };
}
