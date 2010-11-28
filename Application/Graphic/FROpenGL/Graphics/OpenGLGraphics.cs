
//////////////////////////////////////////////////////////////////////////
//
// Draw the graphics with OpengGL.
// 
// Author: Sun Zhongkui
//
// History:
//  2008-2-17 Create
//
//////////////////////////////////////////////////////////////////////////

using FRMath;
using System.Diagnostics;
using CsGL.OpenGL;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using FRGraphic;
using System.Drawing;
using FRContainer;

namespace FROpenGL.Graphic
{
    public class OpenGLGraphics
    {
        public void DrawMesh(FRList<int> connectivity
            , FRList<GePoint> points, FRList<UnitVector> normals
            , FRList<Color> colors)
        {
            Debug.Assert(connectivity != null && points != null);
            if (null == connectivity || null == points) return;

            if (connectivity.Count < 3 || points.Count < 3) return;

            // Use the triangles to draw mesh
            // Top - Bottom Left - Bottom Right
            GL.glBegin(GL.GL_TRIANGLES);						// Drawing Using Triangles
            foreach(int index in connectivity)
            {
                // Color
                if (colors != null && index < colors.Count)
                    GL.glColor4ub(colors[index].R
                        , colors[index].G, colors[index].B
                        , colors[index].A);

                // Normal
                if (normals != null && index < normals.Count)
                    GL.glNormal3d(normals[index].X
                        , normals[index].Y, normals[index].Z);

                // Vertex
                if (index < points.Count)
                    GL.glVertex3d(points[index].X
                        , points[index].Y, points[index].Z);
            }
            GL.glEnd();
        }

        public void DrawLine(GePoint start, GePoint end)
        {
            Debug.Assert(start != null && end != null);
            if (null == start || null == end) return;

            //Draw Line
            GL.glBegin(GL.GL_LINES);
            GL.glVertex3d(start.X, start.Y, start.Z);
            GL.glVertex3d(end.X, end.Y, end.Z);
            GL.glEnd();

        }

        public void DrawLines(List<GePoint> points)
        {
            Debug.Assert(points != null);
            if (null == points) return;
            if (points.Count < 2) return;

            //Draw Lines
            GL.glBegin(GL.GL_LINE_STRIP);
            foreach(GePoint point in points)
                GL.glVertex3d(point.X, point.Y, point.Z);
            GL.glEnd();
        }
            
        // Draw a circle on X-Y parallel plane.
        // We should add routine for drawing circle on any plane.
        public void DrawCircle(GePoint center, double radius)
        {
            Debug.Assert(center != null);
            if (null == center) return;
            if (radius < MathUtil.kTolerance) return;

            //	Start drawing a line loop.
            GL.glBegin(GL.GL_LINE_LOOP);

            //	Make a circle of points.
            int pieceNumber = ((int)(radius / 20) + 1) * 12;
            for (int i = 0; i < pieceNumber; i++)
            {
                double angle = 2 * MathUtil.PI * i / pieceNumber;

                //	Draw the point.
                GL.glVertex3d(center.X + radius * System.Math.Cos(angle)
                    , center.Y + radius * System.Math.Sin(angle)
                    , center.Z);
            }
            GL.glEnd();
        }

        // Fill a circle on X-Y plane
        // We should add routine for drawing circle on any plane.
        public void FillCircle(GePoint center, double radius)
        {
            GLUquadric glQuadric = GL.gluNewQuadric();
            //	Set the quadric properties.
            GL.gluQuadricDrawStyle(glQuadric, GL.GLU_FILL);
            GL.gluQuadricOrientation(glQuadric, GL.GLU_OUTSIDE);
            GL.gluQuadricNormals(glQuadric, GL.GLU_SMOOTH);
            GL.gluQuadricTexture(glQuadric, (byte)1);

            GL.glPushMatrix();
            GL.glLoadIdentity();
            GL.glTranslatef(center.X, center.Y, center.Z);

            // Draw disk.
            GL.gluPartialDisk(glQuadric, 0, radius,
                        12, 10, 0, 360);
            GL.glPopMatrix();

            GL.gluDeleteQuadric(glQuadric);
        }


        public void DrawString(string text
            , GePoint position, OpenGLContext context)
        {
            uint dbase = GL.glGenLists(256);
            wglUseFontBitmaps(context.GetNativeGDI(), 0, 256, dbase);

            GL.glRasterPos2f(position.X, position.Y);// Position The Text On The Screen

            GL.glPushAttrib(GL.GL_LIST_BIT);													// Pushes The Display List Bits
            GL.glListBase(dbase);														// Sets The Base Character to 32
            GL.glCallLists(text.Length, GL.GL_UNSIGNED_SHORT, text);							// Draws The Display List Text
            GL.glPopAttrib();
        }

        public void DrawArc(GePoint center, double radius, Vector normal)
        {
            //TBD
            Debug.Assert(false, "NOIMP");
        }

        public void DrawPoint(GePoint point)
        {
            //	Begin drawing point.
            GL.glBegin(GL.GL_POINTS);
            GL.glVertex3d(point.X, point.Y, point.Z);
            GL.glEnd();
        }              

        // --- Externs ---
        [DllImport("opengl32.dll")]
        private static extern void wglUseFontBitmaps(IntPtr hdc, uint first, uint count, uint listBase);
	
    }
}
