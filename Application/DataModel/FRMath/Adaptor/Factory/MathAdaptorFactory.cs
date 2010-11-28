//////////////////////////////////////////////////////////////////////////
//
// This factory is responsible for return the correct math proxy.
//
// Author: Sun Zhongkui
//
// History:
//  2009-4-24 Create
//
//////////////////////////////////////////////////////////////////////////

namespace FRMath.Adaptor
{
    abstract class  MathAdaptorFactory
    {

        #region Attribute
        public static MathAdaptorFactory Instance
        {
            get { return m_Factory; }
        }
        #endregion

        #region Data
        private static MathAdaptorFactory m_Factory = new NativeAdaptorFactory();
        #endregion

        #region Get Proxy
        public abstract LineAdaptor CreateLineAdaptor(PointAdaptor point, UnitVectorAdaptor direction);

        public abstract Matrix44Adaptor CreateMatrix44Adaptor();

        public abstract PlaneAdaptor CreatePlaneAdaptor(PointAdaptor point, UnitVectorAdaptor Normal);

        public abstract PointAdaptor CreatePlaneAdaptor(double xx, double yy, double zz);

        public abstract VectorAdaptor CreateVectorAdaptor(double xx, double yy, double zz);

        #endregion

        #region Implementation
        public UnitVectorAdaptor CreateUnitVectorAdaptor(double xx, double yy, double zz)
        {
            return new UnitVectorAdaptor(xx, yy, zz);
        }

        public VectorAdaptor CreateVectorAdaptor(PointAdaptor sp, PointAdaptor ep)
        {
            if (null == sp || null == ep)
            {
                return null;
            }
            else
            {
                return CreateVectorAdaptor(ep.X - sp.X
                , ep.Y - sp.Y, ep.Z - sp.Z);
            }
        }
        #endregion
    }
}
