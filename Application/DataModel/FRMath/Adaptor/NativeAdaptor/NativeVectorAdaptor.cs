//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2009-4-24 Create
//
//////////////////////////////////////////////////////////////////////////

namespace FRMath.Adaptor
{
    class NativeVectorAdaptor : VectorAdaptor
    {
        #region Constructors
        public NativeVectorAdaptor(double xx, double yy, double zz)
        {
            x = xx;
            y = yy;
            z = zz;
        }

        public NativeVectorAdaptor()
        {
            x = 1.0;
            y = 0.0;
            z = 0.0;
        }
        #endregion

        #region Override operator

        #endregion       

        #region Attributes
        public override double X
        {
            get { return x; }
            set { x = value; }
        }
        public override double Y
        {
            get { return y; }
            set { y = value; }
        }
        public override double Z
        {
            get { return z; }
            set { z = value; }
        }
        #endregion

        #region Data
        protected double x;
        protected double y;
        protected double z;

        #endregion
    }
}
