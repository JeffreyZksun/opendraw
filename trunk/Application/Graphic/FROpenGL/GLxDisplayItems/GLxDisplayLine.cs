
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
    public class GLxDisplayLine : GLxDisplayItem
    {
        #region construtors
        public GLxDisplayLine(double x1, double y1, double z1
            , double x2, double y2, double z2)
        {
            m_x1 = x1;
            m_y1 = y1;
            m_z1 = z1;

            m_x2 = x2;
            m_y2 = y2;
            m_z2 = z2;
        }

        public GLxDisplayLine() { }
        #endregion

        #region Draw routines
        protected override void OnDraw()
        {
            GL.glLineWidth(m_LineWidth);

            GL.glBegin(GL.GL_LINES);
            GL.glVertex3d(m_x1, m_y1, m_z1);
            GL.glVertex3d(m_x2, m_y2, m_z2);
            GL.glEnd();
        }
        #endregion

        #region Modify routines and attributes
        public void SetStartPoint(double x, double y, double z)
        {
            m_x1 = x;
            m_y1 = y;
            m_z1 = z;

            m_modified = true;
        }

        public void SetEndPoint(double x, double y, double z)
        {
            m_x2 = x;
            m_y2 = y;
            m_z2 = z;

            m_modified = true;
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
        private double m_x1 = 0;
        private double m_y1 = 0;
        private double m_z1 = 0;

        private double m_x2 = 0;
        private double m_y2 = 0;
        private double m_z2 = 0;

        private float m_LineWidth = 2;
        #endregion
    }
}