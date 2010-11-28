
//////////////////////////////////////////////////////////////////////////
//
// Define all the shared data.
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-28 Create
//
//////////////////////////////////////////////////////////////////////////

namespace FRPaint
{
    public class PtDataScheme
    {
        public PtDataScheme()
        {
            m_DoublesComapreTolerance = 1e-7;
            m_SelectTolerance = 5;
            m_SnapTolerance = 5;
            m_ReleaseDependencyTolerance = m_SnapTolerance * 1.5;
            m_MinZoomScale = 0.1;
            m_MaxZoomScale = 10;
        }

        #region Attributes
        public double SelectTolerance
        {
            get { return m_SelectTolerance; }
        }

        public double SnapTolerance
        {
            get { return m_SnapTolerance; }
        }

        public double DoublesComapreTolerance
        {
            get { return m_DoublesComapreTolerance; }
        }

        public double ReleaseDependencyTolerance
        {
            get { return m_ReleaseDependencyTolerance; }
        }

        public double MinZoomScale
        {
            get { return m_MinZoomScale; }
        }

        public double MaxZoomScale
        {
            get { return m_MaxZoomScale; }
        }
        #endregion

        private double m_SnapTolerance;
        // Disconnect the dependency when drag a node.
        private double m_ReleaseDependencyTolerance;
        
        // TBD - we should move all the data defined in other places to this class.
        private double m_SelectTolerance;
        private double m_DoublesComapreTolerance;

        private double m_MinZoomScale;
        private double m_MaxZoomScale;
    }
}
