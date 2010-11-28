
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

using FRMath;
using System.Diagnostics;
using System.Drawing;
using System;
using System.Collections.Generic;
using CsGL.OpenGL;
using FRGraphic;
using FRContainer;

namespace FROpenGL.Graphic
{
    class OpenGLDevice : GraphicDevice
    {
        public OpenGLDevice(OpenGLGraphics gp, OpenGLContext context)
        {
            m_OpenGL = gp;
            m_Context = context;            
        }

        #region Draw Graphics

        public override void DrawMesh(FRList<int> connectivity
            , FRList<GePoint> points, FRList<UnitVector> normals
            , FRList<Color> colors)
        {
            m_OpenGL.DrawMesh(connectivity, points, normals, colors);
        }
        public override void DrawLine(
            GePoint sPoint, GePoint ePoint)
        {
            m_OpenGL.DrawLine(sPoint, ePoint);
        }

        public override void DrawCircle(
            GePoint cneterPoint, float fRadius)
        {
            // Add 1 to justify the circle  size.
            // When we draw a point with the bound circle,
            // the bound circle should be greater than the point size.
            m_OpenGL.DrawCircle(cneterPoint, fRadius + 1);
        }

        public override void DrawPoint(
            GePoint point)
        {
            m_OpenGL.DrawPoint(point);
        }

        public override void FillEllipse(
            GeRectangle rect)
        {
            Debug.Assert(false, "NO IMP");
        }


        public override void DrawArc(GeRectangle rect
            , float startAngle, float endAngle)
        {
            Debug.Assert(false, "NO IMP");
        }

        public override void DrawString(string text
            , GePoint position)
        {
            m_OpenGL.DrawString(text, position, m_Context);
        }
        #endregion 
        
        // This routine should be update by using OpenGL
        public override GeRectangle GetRange(String str, Font font)
        {
            Debug.Assert(false, "NO IMP");
            // Measure string.
            SizeF stringSize = new SizeF();

            GeRectangle rec = new GeRectangle(new GePoint(0, 0)
                , new GePoint(stringSize.Width, stringSize.Height));
            return rec;
        }

        #region Set attributes
        public override void SetColor(Color color)
        {
            m_SettingData.m_Color = color;

            GL.glColor4ub(m_SettingData.m_Color.R, m_SettingData.m_Color.G
                , m_SettingData.m_Color.B, m_SettingData.m_Color.A);
        }

        public override void SetLineWidth(float width)
        {
            m_SettingData.m_LineWidth = width;

            GL.glLineWidth(m_SettingData.m_LineWidth);
        }

        public override void SetPointSize(float size)
        {
            m_SettingData.m_PointSize = size;

            GL.glPointSize(m_SettingData.m_PointSize);
            GL.glEnable(GL.GL_POINT_SMOOTH);
        }

        public override void SetFont(Font font)
        {
            m_SettingData.m_Font = font;
        }
        
        #endregion

        #region Data
        private OpenGLGraphics m_OpenGL;
        private OpenGLContext m_Context;
        #endregion
    }
}
