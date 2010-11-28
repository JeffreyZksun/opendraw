//////////////////////////////////////////////////////////////////////////
//
// This factory is responsible for return the correct AcGe proxy.
//
// Author: Sun Zhongkui
//
// History:
//  2009-4-25 Create
//
//////////////////////////////////////////////////////////////////////////


namespace FRMath.Adaptor 
{
    class MathLibAdaptorFactory : MathAdaptorFactory
    {
        #region Get Proxy
        public override LineAdaptor CreateLineAdaptor(PointAdaptor point, UnitVectorAdaptor direction)
        {
            return new MathLibLineAdaptor();
        }

        public override Matrix44Adaptor CreateMatrix44Adaptor()
        {
            return new MathLibMatrix44Adaptor();
        }

        public override PlaneAdaptor CreatePlaneAdaptor(PointAdaptor point, UnitVectorAdaptor Normal)
        {
            return new MathLibPlaneAdaptor();
        }

        public override PointAdaptor CreatePlaneAdaptor(double xx, double yy, double zz)
        {
            return new MathLibPointAdaptor();
        }

        public override VectorAdaptor CreateVectorAdaptor(double xx, double yy, double zz)
        {
            return new MathLibVectorAdaptor();
        }
        #endregion
    }
}
