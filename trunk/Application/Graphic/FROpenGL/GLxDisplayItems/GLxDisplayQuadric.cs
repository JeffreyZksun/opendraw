
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
    public class GLxDisplayQuadric : GLxDisplayItem
    {
        #region Constructors
        public GLxDisplayQuadric()
        {
            glQuadric = GL.gluNewQuadric();
        }
        #endregion

        #region Destroy override
        /// <summary>
        /// Call this function to destroy the OpenGL quadric object that represents this
        /// quadric.
        /// </summary>
        /// <param name="gl"></param>
        public virtual void Destroy(OpenGL gl)
        {
            GL.gluDeleteQuadric(glQuadric);
        }
        #endregion

        #region Draw routines
        protected override void OnDraw()
        {
            //	Set the quadric properties.
            GL.gluQuadricDrawStyle(glQuadric, GL.GLU_FILL);
            GL.gluQuadricOrientation(glQuadric, GL.GLU_OUTSIDE);
            GL.gluQuadricNormals(glQuadric, GL.GLU_SMOOTH);
            GL.gluQuadricTexture(glQuadric, textureCoords ? (byte)1 : (byte)0);
        }

        protected internal override bool DoPreDraw()
        {
            if (!base.DoPreDraw()) return false;

            // transform
            GL.glPushMatrix();
            GL.glTranslated(m_dCenterX, m_dCenterY, m_dCenterZ);

            return true;
        }

        protected internal override void DoPostDraw()
        {
            base.DoPostDraw();

            GL.glPopMatrix();
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

        #endregion

        #region Data
        /// <summary>
        /// This is the pointer to the opengl quadric object.
        /// </summary>
        protected GLUquadric glQuadric;

        protected bool textureCoords = true;

        private double m_dCenterX = 0;
        private double m_dCenterY = 0;
        private double m_dCenterZ = 0;
        #endregion
    }
}