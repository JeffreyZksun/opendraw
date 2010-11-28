
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
    public class GLxDisplaySphere : GLxDisplayQuadric
    {
        protected override void OnDraw()
        {
            base.OnDraw();
            GL.glColor3f(0, 0, 1);
            GL.glTranslatef(200, 200, 0);
            GL.gluSphere(glQuadric, radius, slices, stacks);
            GL.glTranslatef(-200, -200, 0);
        }

        //	Sphere data.
        double radius = 80.0;
        int slices = 16;
        int stacks = 20;
    }
}