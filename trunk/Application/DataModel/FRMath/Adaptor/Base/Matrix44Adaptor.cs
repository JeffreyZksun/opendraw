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
    public abstract class Matrix44Adaptor
    {
        #region Implementation

        public Matrix44Adaptor SetRotate(UnitVectorAdaptor fromDirection
    , UnitVectorAdaptor toDirection, PointAdaptor basePoint)
        {
            Debug.Assert(fromDirection != null && toDirection != null
                && basePoint != null);
            if (!(fromDirection != null && toDirection != null
                && basePoint != null)) return this;

            double angle = fromDirection.AngleTo(toDirection);
            VectorAdaptor axis = fromDirection.VectorAdaptor.Cross(toDirection.VectorAdaptor);

            SetRotate(angle, axis.UnitVectorAdaptor, basePoint);

            return this;
        }

        #endregion


        #region Clone
        public abstract Matrix44Adaptor Clone();
        #endregion


        #region Abstract functions
         // Translation
        public abstract Matrix44Adaptor SetTranslation(VectorAdaptor distance);

        // Rotate the angle is radian
        public abstract Matrix44Adaptor SetRotate(double AngleThita, UnitVectorAdaptor axis, PointAdaptor basePoint);

        
        // Scale
        public abstract Matrix44Adaptor SetScaling(double allScale, PointAdaptor basePoint);
        public abstract Matrix44Adaptor SetScaling(double allScale);

        public abstract double GetAllScaling();

        #endregion


        #region Operator

        protected abstract PointAdaptor OperatorMultiply(Matrix44Adaptor trans, PointAdaptor point);
        protected abstract Matrix44Adaptor OperatorMultiply(Matrix44Adaptor lhs, Matrix44Adaptor rhs);
        
        public static PointAdaptor operator *(Matrix44Adaptor trans, PointAdaptor point)
        {
            return trans.OperatorMultiply(trans, point);
        }

        public static Matrix44Adaptor operator *(Matrix44Adaptor lhs, Matrix44Adaptor rhs)
        {
            return lhs.OperatorMultiply(lhs, rhs);
        }


        #endregion

        #region Attributes

        public abstract double this[int row, int column]
        {
            get;
            set;
        }

        public abstract Matrix44Adaptor Inverse
        {
            get;
        }

        #endregion

        #region Override Object - Should be implemented in children class.

        //public override bool Equals(object obj)       
        //public override string ToString()
        //public override int GetHashCode()
        #endregion
    }
}
