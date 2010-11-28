
//////////////////////////////////////////////////////////////////////////
//
// Define all the colors used in the application.
// OPT - the color should be change in the windows UI.
// 
// Author: Sun Zhongkui
//
// History:
//  2007-10-31 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Drawing;

namespace FRPaint
{
    public class PtColorScheme
    {
        public PtColorScheme()
        {
            m_Drawing = Color.Black; // Blue->Black
            m_PreviewPoint = Color.LightGreen;
            m_PreviewPointCircle = Color.Black;
            m_PreviewCurve = Color.Red;
            m_SelectedPoint = Color.LightGreen;
            m_SelectedPointCircle = Color.Black;
            m_SelectedCurve = Color.Blue;
        }

        #region Attributes - can just get now
        public Color DrawingColor
        {
            get { return m_Drawing; }
        }
        public Color PreviewPointColor
        {
            get { return m_PreviewPoint; }
        }
        public Color PreviewPointCircleColor
        {
            get { return m_PreviewPointCircle; }
        }
        public Color PreviewCurveColor
        {
            get { return m_PreviewCurve; }
        }
        public Color SelectedPointColor
        {
            get { return m_SelectedPoint; }
        }
        public Color SelectedPointCircleColor
        {
            get { return m_SelectedPointCircle; }
        }
        public Color SelectedCurveColor
        {
            get { return m_SelectedCurve; }
        }
        #endregion

        private Color m_Drawing; // The color of the graphic on the view
        private Color m_PreviewPoint;// The point color when preview.
        private Color m_PreviewPointCircle;// The color of the circle around the point when preview.
        private Color m_PreviewCurve;// The curve color when preview.
        private Color m_SelectedPoint;
        private Color m_SelectedPointCircle;
        private Color m_SelectedCurve;
    }
}
