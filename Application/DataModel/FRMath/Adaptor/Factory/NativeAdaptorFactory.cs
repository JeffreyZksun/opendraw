//////////////////////////////////////////////////////////////////////////
//
// This factory is responsible for return the correct native proxy.
//
// Author: Sun Zhongkui
//
// History:
//  2009-4-25 Create
//
//////////////////////////////////////////////////////////////////////////


namespace FRMath.Adaptor
{
    class NativeAdaptorFactory : MathAdaptorFactory
    {
        #region Get Proxy
        public override LineAdaptor CreateLineAdaptor(PointAdaptor point, UnitVectorAdaptor direction)
        {
            return new NativeLineAdaptor(point, direction);
        }

        public override Matrix44Adaptor CreateMatrix44Adaptor()
        {
            return new NativeMatrix44Adaptor();
        }

        public override PlaneAdaptor CreatePlaneAdaptor(PointAdaptor point, UnitVectorAdaptor Normal)
        {
            return new NativePlaneAdaptor(point, Normal);
        }

        public override PointAdaptor CreatePlaneAdaptor(double xx, double yy, double zz)
        {
            return new NativePointAdaptor(xx, yy, zz);
        }

        public override VectorAdaptor CreateVectorAdaptor(double xx, double yy, double zz)
        {
            return new NativeVectorAdaptor(xx, yy, zz);
        }
        
        #endregion
    }
}
