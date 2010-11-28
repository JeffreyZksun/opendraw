
//////////////////////////////////////////////////////////////////////////
//
// Draw the graphics with GDI.
// 
// Author: Sun Zhongkui
//
// History:
//  2008-2-17 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using FRMath;
using System.Diagnostics;
using FRGraphic;
using FRContainer;

// The data in this class use the device coordinate

namespace FROpenGL.Graphic
{
    public class GDIDevice : GraphicDevice
    {
        #region Constructors
        public GDIDevice(Graphics gp)
        {
            Debug.Assert(gp != null);
            m_Graphics = gp;
        }
        #endregion

        #region Draw Graphics

        public override void DrawMesh(FRList<int> connectivity
            , FRList<GePoint> points, FRList<UnitVector> normals
            , FRList<Color> colors)
        {
            Debug.Assert(false, "No IMP");
        }
        public override void DrawLine(
            GePoint sPoint, GePoint ePoint)
        {
            Pen pen = GetPen();
            m_Graphics.DrawLine(pen, sPoint.X, sPoint.Y,
                ePoint.X, ePoint.Y);
        }

        public override void DrawCircle(
            GePoint cneterPoint, float fRadius)
        {
            Pen pen = GetPen();

            RectangleF rect = new RectangleF(cneterPoint.X - fRadius, cneterPoint.Y - fRadius
                , fRadius * 2, fRadius * 2);
            m_Graphics.DrawArc(pen, rect, 0, 360);
        }

        public override void DrawPoint(
            GePoint point)
        {
            float fRadius = m_SettingData.m_PointSize / 2;        

            RectangleF rect = new RectangleF(point.X - fRadius, point.Y - fRadius
                , fRadius * 2, fRadius * 2);
            FillEllipse(rect);
        }

        public override void FillEllipse(
            GeRectangle rect)
        {
            RectangleF rc = new RectangleF((float)rect.MinPoint.X,
                (float)rect.MinPoint.Y, (float)rect.Width,
                (float)rect.Height);
            FillEllipse(rc);
        }

        public override void DrawArc(GeRectangle rect
            , float startAngle, float endAngle)
        {
            RectangleF rc = new RectangleF((float)rect.MinPoint.X,
                (float)rect.MinPoint.Y, (float)rect.Width,
                (float)rect.Height);

            Pen pen = GetPen();

            m_Graphics.DrawArc(pen, rc, startAngle, endAngle);
        }


        public override void DrawString(string text
            , GePoint position)
        {
            Brush brush = GetBrush();

            m_Graphics.DrawString(text, m_SettingData.m_Font
                , brush, position.X, position.Y);
            
        }

        public override GeRectangle GetRange(String str, Font font)
        {
            // Measure string.
            SizeF stringSize = m_Graphics.MeasureString(
                   str, font);

            GeRectangle rec = new GeRectangle(new GePoint(0, 0)
                , new GePoint(stringSize.Width, stringSize.Height));
            return rec;
        }
        #endregion

        #region Private helper
        private void FillEllipse(
            RectangleF rect)
        {
            Brush brush = GetBrush();
            m_Graphics.FillEllipse(brush, rect);
        }
        #endregion

        #region Get pen brush
        private Pen GetPen()
        {
            Pen ThePen = new Pen(m_SettingData.m_Color, m_SettingData.m_LineWidth);
            return ThePen;
        }

        private Brush GetBrush()
        {
            SolidBrush TheBrush = new SolidBrush(m_SettingData.m_Color);
            return TheBrush;
        }
        #endregion       
        
        #region Set attributes
        public override void SetColor(Color color)
        {
            m_SettingData.m_Color = color;
        }

        public override void SetLineWidth(float width)
        {
            m_SettingData.m_LineWidth = width;
        }

        public override void SetPointSize(float size)
        {
            m_SettingData.m_PointSize = size;
        }

        public override void SetFont(Font font)
        {
            m_SettingData.m_Font = font;
        }

        #endregion

        #region Data
        private Graphics m_Graphics;
        #endregion
    }
}
