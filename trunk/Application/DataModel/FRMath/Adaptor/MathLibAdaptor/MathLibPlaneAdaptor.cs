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
    class MathLibPlaneAdaptor : PlaneAdaptor
    {
        #region Abstract functions
        public override PointAdaptor ClosestPointTo(PointAdaptor point)
        {
            Debug.Assert(false, "NO IMP");
            return null;
        }
        #endregion


        #region Operator

        #endregion

        #region Attributes

        public override UnitVectorAdaptor Normal
        {
            get
            {
                Debug.Assert(false, "NO IMP");
                return null;
            }
        }
        public override PointAdaptor Point
        {
            get
            {
                Debug.Assert(false, "NO IMP");
                return null;
            }
        }

        // OPT - This name should be replaced
        // Ax + By + Cz = D;
        public override double ConstNumber
        {
            get
            {
                Debug.Assert(false, "NO IMP");
                return 0;
            }
        }

        #endregion
    }
}
