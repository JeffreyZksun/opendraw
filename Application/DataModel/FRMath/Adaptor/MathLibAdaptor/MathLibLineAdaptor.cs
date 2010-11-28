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
    class MathLibLineAdaptor : LineAdaptor
    {


        #region Abstract functions

        public override PointAdaptor GetIntersection(PlaneAdaptor planeProxy)
        {
            Debug.Assert(false, "NO IMP");
            return null;
        }

        #endregion


        #region Operator

        #endregion

        #region Attributes
        public override PointAdaptor BasePoint
        {
            get
            {
                Debug.Assert(false, "NO IMP");
                return null;
            }
        }
        public override UnitVectorAdaptor Direction
        {
            get
            {
                Debug.Assert(false, "NO IMP");
                return null;
            }
        }
        #endregion
    }
}
