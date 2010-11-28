
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
    public class GLxDisplayText : GLxDisplayItem
    {
        public GLxDisplayText(OpenGLContext context)
        {
            m_Context = context;
        }

        public override void Draw()
        {
            dbase = GL.glGenLists(256);														// Storage For 96 Characters
            wglUseFontBitmaps(m_Context.GetNativeGDI(), 0, 256, dbase);

            GL.glColor3f(0, 0, 1);
            GL.glRasterPos2f(0, 0);// Position The Text On The Screen
            PrintText("Draw text with OpenGL");
        }

        private static void PrintText(string text)
        {										// Custom GL "Print" Routine
            GL.glPushAttrib(GL.GL_LIST_BIT);													// Pushes The Display List Bits
            GL.glListBase(dbase);														// Sets The Base Character to 32
            GL.glCallLists(text.Length, GL.GL_UNSIGNED_SHORT, text);							// Draws The Display List Text
            GL.glPopAttrib();																// Pops The Display List Bits
        }

        private OpenGLContext m_Context;
        private static uint dbase;

        // --- Externs ---
        [DllImport("opengl32.dll")]
        private static extern void wglUseFontBitmaps(IntPtr hdc, uint first, uint count, uint listBase);

    }
}