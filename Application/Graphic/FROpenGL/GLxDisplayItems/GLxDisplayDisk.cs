
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
    public class GLxDisplayDisk : GLxDisplayQuadric
    {
        #region Draw routines
        protected override void OnDraw()
        {
            base.OnDraw();

            int slices = (int)(m_dSweepAngle / 10 + 1) * (int)(m_dOuterRadius / 200 + 1);
            int loops = (int)(m_dOuterRadius / 6);
            GL.gluPartialDisk(glQuadric, m_dInnerRadius, m_dOuterRadius,
                        slices, loops, m_dStartAngle, m_dSweepAngle);
        }

        #endregion

        #region Modify routines and attributes
        public double InnerRadius
        {
            get { return m_dInnerRadius; }
            set
            {
                if (value > -1e-6)
                {
                    if (value < 1e-6)
                        m_dInnerRadius = 0;
                    else
                        m_dInnerRadius = value;

                    m_modified = true;
                }
            }
        }

        public double OuterRadius
        {
            get { return m_dOuterRadius; }
            set
            {
                if (value > 1e-6)
                {
                    m_dOuterRadius = value;
                    m_modified = true;
                }
            }
        }

        public double StartAngle
        {
            get { return m_dStartAngle; }
            set
            {
                m_dStartAngle = value;
                m_modified = true;
            }
        }

        public double SweepAngle
        {
            get { return m_dSweepAngle; }
            set
            {
                m_dSweepAngle = value;
                m_modified = true;
            }
        }
        #endregion

        #region Data
        private double m_dInnerRadius = 10.0;
        private double m_dOuterRadius = 20.0;
        private double m_dStartAngle = 0.0; // In degree
        private double m_dSweepAngle = 180.0; // In degree
        #endregion
    }
}