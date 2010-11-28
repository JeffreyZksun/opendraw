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
    class MathLibPointAdaptor : PointAdaptor
    {
        #region Abstract functions

        public override double DistanceTo(PointAdaptor pointProxy)
        {
            Debug.Assert(false, "NO IMP");
            return 0;
        }


        public override PointAdaptor Transform(Matrix44Adaptor transProxy)
        {
            Debug.Assert(false, "NO IMP");
            return null;
        }

        public override bool IsEqualTo(PointAdaptor pointProxy)
        {
            Debug.Assert(false, "NO IMP");
            return false;
        }

        public override void Move(VectorAdaptor distance)
        {
            Debug.Assert(false, "NO IMP");
        }

        #endregion 

        #region Attribute
        public override float X
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
        public override float Y
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
        public override float Z
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
        #endregion
    }
}
