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
    class MathLibMatrix44Adaptor : Matrix44Adaptor
    {
        #region Clone
        public override Matrix44Adaptor Clone()
        {
            Debug.Assert(false, "NO IMP");
            return null;
        }
        #endregion

        #region Abstract functions
        // Translation
        public override Matrix44Adaptor SetTranslation(VectorAdaptor distance)
        {
            Debug.Assert(false, "NO IMP");
            return null;
        }

        // Rotate the angle is radian
        public override Matrix44Adaptor SetRotate(double AngleThita, UnitVectorAdaptor axis, PointAdaptor basePoint)
        {
            Debug.Assert(false, "NO IMP");
            return null;
        }


        // Scale
        public override Matrix44Adaptor SetScaling(double allScale, PointAdaptor basePoint)
        {
            Debug.Assert(false, "NO IMP");
            return null;
        }
        public override Matrix44Adaptor SetScaling(double allScale)
        {
            Debug.Assert(false, "NO IMP");
            return null;
        }

        public override double GetAllScaling()
        {
            Debug.Assert(false, "NO IMP");
            return 0;
        }

        #endregion


        #region Operator
        
        protected override PointAdaptor OperatorMultiply(Matrix44Adaptor trans, PointAdaptor point)
        {
            Debug.Assert(false, "NO IMP");
            return null;
        }

        protected override Matrix44Adaptor OperatorMultiply(Matrix44Adaptor lhs, Matrix44Adaptor rhs)
        {
            Debug.Assert(false, "NO IMP");
            return null;
        }       
        #endregion

        #region Attributes

        public override double this[int row, int column]
        {
            get
            {
                Debug.Assert(false, "NO IMP");
                return 0;
            }
            set
            {
                Debug.Assert(false, "NO IMP");
            }
        }

        public override Matrix44Adaptor Inverse
        {
            get
            {
                Debug.Assert(false, "NO IMP");
                return null;
            }
        }

        #endregion

        #region Override Object - Should be implemented in children class.

        public override bool Equals(object obj)  
        {
            Debug.Assert(false, "NO IMP");
                return false;
        }
        public override string ToString()
        {
            Debug.Assert(false, "NO IMP");
            return null;
        }
        public override int GetHashCode()
        {
            Debug.Assert(false, "NO IMP");
            return 0;
        }
        #endregion
    }
}
