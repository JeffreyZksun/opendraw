
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-8-3 Create
//
//////////////////////////////////////////////////////////////////////////

using FRContainer;
using FRMath;
using System.Drawing;

namespace FRGraphic
{
    public class DisplayItemMesh : DisplayItem
    {
        public DisplayItemMesh()
        {

        }

        #region Override class DisplayItem
        protected override void OnDraw(GraphicContext gpCtx, Matrix44 parentTrans)
        {
            if (gpCtx != null
                && m_Connectivity != null && m_Connectivity.Count > 2
                && m_Points != null && m_Points.Count > 2)
            {
                FRList<GePoint> worldPoints = MathUtil.GetTransformedPoints(m_Points, parentTrans);
                
                GraphicDeviceProxy Device =
                    gpCtx.CreateGraphicDeviceProxy();
                Device.DrawMesh(m_Connectivity, worldPoints, m_Normals, m_Colors);
            }
        }


        protected override bool OnQualify(SelectContext selectCtx, Matrix44 parentTrans)
        {
            return false;
        }
        #endregion

        #region Attribute
        public FRList<int> Connectivity
        {
            set { m_Connectivity = value; }
        }

        public FRList<GePoint> Points
        {
            set { m_Points = value; }
        }
        public FRList<UnitVector> Normals
        {
            set { m_Normals = value; }
        }

        public FRList<Color> Colors
        {
            set { m_Colors = value; }
        }


        #endregion

        #region Data
        // Record the index of the points.
        // Indicate the collection of them.
        // Every 3 points indicate a triangle.
        private FRList<int> m_Connectivity;

        // All the points of the mesh.
        private FRList<GePoint> m_Points;

        // The normal of every point.
        private FRList<UnitVector> m_Normals;

        // The color of every point.
        private FRList<Color> m_Colors;
        #endregion
    }
}
