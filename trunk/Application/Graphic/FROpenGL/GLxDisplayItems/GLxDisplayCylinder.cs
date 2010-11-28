
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
    public class GLxDisplayCylinder : GLxDisplayQuadric
    {
        protected override void OnDraw()
        {
            base.OnDraw();
            GL.glColor3f(1, 0, 0);
            GL.glRotatef(45f, 1, 0, 0);
            GL.gluCylinder(glQuadric, baseRadius, topRadius, height, slices, stacks);
            GL.glRotatef(-45f, 1, 0, 0);
        }

        //	Sphere data.
        protected double baseRadius = 5;
        protected double topRadius = 10.0;
        protected double height = 10.0;
        protected int slices = 16;
        protected int stacks = 20;
    }
}
