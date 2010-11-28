
//////////////////////////////////////////////////////////////////////////
//
// This project is a wrapper to the CSGL.
// URL: https://sourceforge.net/projects/csgl/
//
// 1. Do some setting for the OpenGL.
// 2. Transform the coordination of the control. 
// The origin is changed to the left-bottom corner.
// Override the event in the UDC. 
// Intercept and capture the mouse event, 
// change the event arguments. 
// 3. Provide a OpenGLDraw event.
//
// Author: Sun Zhongkui
//
// History:
//  2008-1-31 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using CsGL.OpenGL;
using FROpenGL.Graphic;
using FRGraphic;

namespace FROpenGL
{

    public class OpenGLSheet : OpenGLControl
    {
        #region Graphic Device
        public enum GraphicDeviceType
        {
            GDI,
            OPENGL,
            DIRECTX
        }

        public GraphicDevice GetGraphicDevice()
        {
            if(m_DeviceType == GraphicDeviceType.OPENGL)
            {
                return new OpenGLDevice(CreateOpenGLGraphics(), Context);
            }
            else
            {
                return new GDIDevice(CreateGraphics());
            }
        }

        public GraphicDeviceType DeviceType
        {
            get { return m_DeviceType; }
            set { m_DeviceType = value; }
        }
        #endregion

        #region  Overrides

        #region 1. Do some setting for the OpenGL.
        protected override void InitGLContext()
        {
            //GL.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
            SetOpenGLClearColor();

            GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glShadeModel(GL.GL_FLAT);
            GL.glDepthRange(0.0f, 1.0f);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);  

            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();
            // The coordination of the OpenGL is change along with the view size.
            // Define the near and far clip plane.
            // The graphics whose z-coordinate isn't in this range [-80, 79] won't be drawn.

            if (m_bFixedViewpoint)
            {
                double widthFactor = (double)Size.Width / (double)Size.Height;
                if (widthFactor < 1e-6) 
                    widthFactor = 1;
                m_dViewpointWidth = (int)(widthFactor * m_dViewpointHeight);
                GL.glOrtho(0.0f, m_dViewpointWidth
                    , 0.0f, m_dViewpointHeight, -79f, 80f);
                //GL.glViewport(0, 0, (int)(widthFactor * Size.Height), Size.Height);
            }
            else
            {
                GL.glOrtho(0.0f, Size.Width, 0.0f, Size.Height, -79f, 80f);
                //GL.glViewport(0, 0, Size.Width, Size.Height);
            }

            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();

            return;
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            SetOpenGLClearColor();
        } 

        private void SetOpenGLClearColor()
        {
            float red = BackColor.R / 255f;
            float green = BackColor.G / 255f;
            float blue = BackColor.B / 255f;
            float alpha = BackColor.A / 255f;

            GL.glClearColor(red, green, blue, alpha);
        }
        #endregion

        #region 2. Transform the coordination.
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(GetTransformedMouseArgs(e));
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(GetTransformedMouseArgs(e));
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(GetTransformedMouseArgs(e));
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(GetTransformedMouseArgs(e));
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(GetTransformedMouseArgs(e));
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(GetTransformedMouseArgs(e));
        }

        // 1. Transform the origin of the coordination from left-top to left-bottom.
        // 2. Change the pixel coordination of the UDC to the 
        // drawing coordination of the OpenGL viewpoit.
        private MouseEventArgs GetTransformedMouseArgs(MouseEventArgs e)
        {
            if(m_DeviceType == GraphicDeviceType.OPENGL)
            {
                Int32 DrawingX = e.X * OpenGLDrawingSize.Width / Size.Width;
                Int32 DrawingY = OpenGLDrawingSize.Height
                    - e.Y * OpenGLDrawingSize.Height / Size.Height;

                MouseEventArgs GLe = new MouseEventArgs(
                  e.Button, e.Clicks, DrawingX, DrawingY, e.Delta);

                return GLe;
            }
            else
            {
                return e;
            }
        }
        #endregion

        #region 3. Provide a DrawingRender event.

        // The OnPaint event is prevented in this control.
        protected override void OnPaint(PaintEventArgs pevent)
        {
            if(m_DeviceType == GraphicDeviceType.OPENGL)
            {
                OpenGLPaint(pevent);
            }
            else
            {
                GDIPaint(pevent);
            }            
        }

        private void OpenGLPaint(PaintEventArgs pevent)
        {
            Context.Grab();

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT); // Clear Screen And Depth Buffer
            GL.glLoadIdentity();

            if (DrawingRender != null)
                DrawingRender(this, pevent);

            GL.glFlush();

            GL.glFinish();
            SwapBuffer();
            OpenGLException.Assert();
        }

        private void GDIPaint(PaintEventArgs pevent)
        {
            CreateGraphics().Clear(BackColor);

            if (DrawingRender != null)
                DrawingRender(this, pevent);
        }

        public event PaintEventHandler DrawingRender;  

        #endregion

        #endregion


        public OpenGLGraphics CreateOpenGLGraphics()
        {
            return new OpenGLGraphics();
        }
      

        public bool FixedViewpoint
        {
            get { return m_bFixedViewpoint; }
            set { m_bFixedViewpoint = value; }
        }

        public Size OpenGLDrawingSize
        {
            get
            {
                if (m_bFixedViewpoint)
                    return new Size(m_dViewpointWidth, m_dViewpointHeight);
                else
                    return Size;
            }
        }

        #region Data
        // Indicate whether the viewpoint is changed when the size of the control changes.
        private bool m_bFixedViewpoint = false;
        // This value is used when m_bFixedViewpoint = true;
        private int m_dViewpointHeight = 300;
        private int m_dViewpointWidth = 300;

        private GraphicDeviceType m_DeviceType = GraphicDeviceType.OPENGL;
        #endregion

    }
}
