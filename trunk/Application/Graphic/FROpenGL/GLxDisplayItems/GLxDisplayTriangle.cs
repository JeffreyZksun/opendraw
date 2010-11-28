
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
    public class GLxDisplayTriangle : GLxDisplayItem
    {
        protected override void OnDraw()
        {
            GL.glBegin(GL.GL_TRIANGLES);						// Drawing Using Triangles
            GL.glColor3f(1, 0, 0);
            GL.glVertex3f(-3.0f, 1.0f, 0.0f);				// Top
            GL.glColor3f(0, 1, 0);
            GL.glVertex3f(-4.0f, -1.0f, 0.0f);				// Bottom Left
            GL.glColor3f(0, 0, 1);
            GL.glVertex3f(-2.0f, -1.0f, 0.0f);				// Bottom Right
            GL.glEnd();
        }
    }
}