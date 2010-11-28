
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
    public class GLxDisplayPoint : GLxDisplayItem
    {
        #region Constructors
        public GLxDisplayPoint(double x, double y, double z)
        {
            m_x = x;
            m_y = y;
            m_z = z;
        }
        #endregion

        #region Draw routines
        protected override void OnDraw()
        {
            GL.glPointSize(m_PointSize);
            GL.glEnable(GL.GL_POINT_SMOOTH);

            //	Begin drawing point.
            GL.glBegin(GL.GL_POINTS);
            GL.glVertex3d(m_x, m_y, m_z);
            GL.glEnd();
        }
        #endregion

        #region Modify routines and attributes
        public float Size
        {
            get { return m_PointSize; }
            set
            {
                if (value > 1e-6)
                {
                    m_PointSize = value;
                    m_modified = true;
                }
            }
        }
        #endregion

        #region Data
        private double m_x = 0;
        private double m_y = 0;
        private double m_z = 0;

        private float m_PointSize = 10;
        #endregion
    }
}