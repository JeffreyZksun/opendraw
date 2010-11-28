
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
    public class GLxDisplayRectangular : GLxDisplayItem
    {
        protected override void OnDraw()
        {
            GL.glColor3f(0.5f, 0.5f, 0.5f);
            GL.glRectf(-0.9f, -0.9f, -0.5f, 0.9f);
        }
    }
}