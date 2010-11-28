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
    class MathLibVectorAdaptor : VectorAdaptor
    {
        #region Attributes
        public override double X
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
        public override double Y
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
        public override double Z
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
