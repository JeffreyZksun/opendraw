//////////////////////////////////////////////////////////////////////////
//        _  _     _              _     _  _
//       | Xn |   |   Sx  0  0  Tx |   | Xo | 
//       |    |   |                |   |    |
//       | Yn |   |   0  Sy  0  Ty |   | Yo |
//       |    | = |                | * |    |
//       | Zn |   |   0  0  Sz  Tz |   | Zo |
//       |    |   |                |   |    |
//       | 1  |   |_  0  0   0  S _|   |_1 _|
//       |_  _|
//
// Xo - Old point, Xn - New point. 
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
    public class Matrix44
    {

        #region constructors
        public Matrix44()
        {
            m_Proxy = MathAdaptorFactory.Instance.CreateMatrix44Adaptor();
        }

        //private Matrix44(Matrix2d src)
        //{
        //    Debug.Assert(src.RowCount == 4 && src.ColumnCount == 4);
        //    if (src.RowCount == 4 && src.ColumnCount == 4)
        //        m_Matrix2d = src;
        //}
        #endregion

        #region Attributes
        public double this[int row, int column]
        {
            get
            {
                return _Proxy[row, column];

            }
            set
            {
                _Proxy[row, column] = value;
            }
        }

        #endregion

        #region transfrom
        // Translation
        public Matrix44 SetTranslation(Vector distance)
        {
            Debug.Assert(distance != null);
            _Proxy.SetTranslation(distance._Proxy);
            return this;
        }

        // Rotate the angle is radian
        public Matrix44 SetRotate(double AngleThita, UnitVector axis, GePoint basePoint)
        {
            Debug.Assert(axis != null && basePoint != null);
            if (!(axis != null && basePoint != null)) return this;
            _Proxy.SetRotate(AngleThita, axis._Proxy, basePoint._Proxy);
            return this;
        }

        public Matrix44 SetRotate(UnitVector fromDirection
            , UnitVector toDirection, GePoint basePoint)
        {
            Debug.Assert(fromDirection != null && toDirection != null
                && basePoint != null);
            if (!(fromDirection != null && toDirection != null
                && basePoint != null)) return this;
            _Proxy.SetRotate(fromDirection._Proxy, toDirection._Proxy, basePoint._Proxy);
            
            return this;
        }

        // Scale
        public Matrix44 SetScaling(double allScale, GePoint basePoint)
        {
            Debug.Assert(allScale > 0 && basePoint != null);
            if (!(allScale > 0 && basePoint != null)) return this;
            _Proxy.SetScaling(allScale, basePoint._Proxy);
            return this;
        }

        public Matrix44 SetScaling(double allScale)
        {
            Debug.Assert(allScale > 0);
            _Proxy.SetScaling(allScale);
            return this;
        }

        public double GetAllScaling()
        {
            return _Proxy.GetAllScaling();
        }

        // Project  SetProject(...)

        // Mirror SetMirror(...) GePoint, GeLine, GePlane

        #endregion

        public void LeftMultiply(Matrix44 rhs)
        {
            if (null == rhs) return;
            Matrix44 newMatrix = rhs * this;
            m_Proxy = newMatrix._Proxy;       
        }

        #region Clone
        public Matrix44 Clone()
        {
            return new Matrix44(_Proxy.Clone());
        }
        #endregion

        #region operator *

        public static GePoint operator *(Matrix44 trans, GePoint point)
        {
            return new GePoint( trans._Proxy * point._Proxy );
        }

        public static Matrix44 operator *(Matrix44 lhs, Matrix44 rhs)
        {
            if (null == lhs || null == rhs) return null;
            return new Matrix44(lhs._Proxy * rhs._Proxy);
        }
        #endregion

        #region Attributes
        public static Matrix44 Identity
        {
            get
            {
                Matrix44 tran = new Matrix44();
                return tran;
            }
        }

        public Matrix44 Inverse
        {
            get
            {
                return new Matrix44(_Proxy.Inverse);
            }
        }
        #endregion

        #region override Object
        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            if (!(obj is Matrix44)) return false;

            Matrix44 rhs = (Matrix44)obj;

            return _Proxy.Equals(rhs._Proxy);
        }

        public override string ToString()
        {
            return _Proxy.ToString();
        }

        public override int GetHashCode()
        {
            return _Proxy.GetHashCode();
        }
        #endregion

        #region Proxy

        public Matrix44(Matrix44Adaptor proxy)
        {
            m_Proxy = proxy;
        }
        public Matrix44Adaptor _Proxy
        {
            get { return m_Proxy; }
        }
        private Matrix44Adaptor m_Proxy;
        #endregion
    }
}
