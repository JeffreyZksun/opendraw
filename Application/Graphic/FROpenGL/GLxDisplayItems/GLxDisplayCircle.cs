
//////////////////////////////////////////////////////////////////////////
//
// Draw the graphics with OpenGL. 
// 
// Author: Sun Zhongkui
//
// History:
//  2008-2-17 Create
//
//////////////////////////////////////////////////////////////////////////

using CsGL.OpenGL;
using System;
using System.Runtime.InteropServices;

// Define the graphics drawn by OpenGL.
// It wrapped the drawing detail.

namespace FROpenGL.GLxDisplayItems
{
    public class GLxDisplayCircle : GLxDisplayItem
    {
        #region Draw routines
        protected override void OnDraw()
        {
            GL.glLineWidth(m_LineWidth);

            //	Start drawing a line loop.
            GL.glBegin(GL.GL_LINE_LOOP);

            //	Make a circle of points.
            int pieceNumber = ((int)(m_dRadius / 20) + 1) * 24;
            for (int i = 0; i < pieceNumber; i++)
            {
                double angle = 2 * System.Math.PI * i / pieceNumber;

                //	Draw the point.
                GL.glVertex3d(m_dCenterX + System.Math.Cos(angle) * m_dRadius,
                    m_dCenterY + System.Math.Sin(angle) * m_dRadius, 0);
            }

            //	End the line drawing.
            GL.glEnd();
        }
        #endregion

        #region Modify routines and attributes
        public void SetCenter(double x, double y, double z)
        {
            m_dCenterX = x;
            m_dCenterY = y;
            m_dCenterZ = z;

            m_modified = true;
        }
        public double Raduis
        {
            get { return m_dRadius; }
            set
            {
                if (value > -1e-6)
                {
                    if (value < 1e-6)
                        m_dRadius = 0;
                    else
                        m_dRadius = value;

                    m_modified = true;
                }
            }
        }

        public float Width
        {
            get { return m_LineWidth; }
            set
            {
                if (value > 0)
                {
                    m_LineWidth = value;
                    m_modified = true;
                }
            }
        }
        #endregion

        #region Data
        private double m_dRadius = 20;
        private double m_dCenterX = 0;
        private double m_dCenterY = 0;
        private double m_dCenterZ = 0;

        private float m_LineWidth = 2;
        #endregion

    }
}
