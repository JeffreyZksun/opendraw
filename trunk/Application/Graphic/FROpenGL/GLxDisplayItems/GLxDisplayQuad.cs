
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
    public class GLxDisplayQuad : GLxDisplayItem
    {
        protected override void OnDraw()
        {
            //gl.Translate(3.0f, 0.0f, 0.0f);					// Move Right 3 Units
            GL.glColor3f(0.8f, 0.3f, 0.7f);
            GL.glBegin(GL.GL_QUADS);						// Drawing Using Quads
            GL.glVertex3f(0.5f, -0.9f, 0.0f);				// Top Left
            GL.glVertex3f(0.5f, 0.9f, 0.0f);				// Top Right
            GL.glVertex3f(0.9f, 0.9f, 0.0f);				// Bottom Right
            GL.glVertex3f(0.9f, -0.9f, 0.0f);				// Bottom Left
            GL.glEnd();
        }
    }
}