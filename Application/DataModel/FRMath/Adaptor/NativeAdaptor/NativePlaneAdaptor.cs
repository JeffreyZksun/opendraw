//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2009-4-24 Create
//
//////////////////////////////////////////////////////////////////////////
using System.Diagnostics;
using System;

namespace FRMath.Adaptor
{
    class NativePlaneAdaptor : PlaneAdaptor
    {
        #region Constructors
        public NativePlaneAdaptor(PointAdaptor point, UnitVectorAdaptor Normal)
        {
            if (point != null)
                m_Point = (PointAdaptor)point.Clone();
            else
                m_Point = new NativePointAdaptor(0,0,0);

            if (Normal != null)
                m_Normal = Normal.Clone();
            else
                m_Normal = new UnitVectorAdaptor(1,0,0);
        }
        #endregion


        #region Override
        public override PointAdaptor ClosestPointTo(PointAdaptor point)
        {
            // TBD
            Debug.Assert(false, "NO IMP!");
            return null;
        }

        #endregion

        #region Attributes
        public override UnitVectorAdaptor Normal
        {
            get { return m_Normal; }
        }
        public override PointAdaptor Point
        {
            get { return m_Point; }
        }
        // OPT - This name should be replaced
        // Ax + By + Cz = D;
        public override double ConstNumber
        {
            get
            {
                return (m_Normal.X * m_Point.X
                    + Normal.Y * m_Point.Y
                    + m_Normal.Z * m_Point.Z);
            }
        }
 
        #endregion

        #region Data
        private UnitVectorAdaptor m_Normal;
        private PointAdaptor m_Point;
        #endregion
    }
}
