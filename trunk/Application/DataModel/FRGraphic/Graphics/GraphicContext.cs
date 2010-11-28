
//////////////////////////////////////////////////////////////////////////
//
//
//  GraphicContext - save display settings, such as color...
//  GraphicDeviceProxy - Transform coordinate from world to device. 
//                      Apply preview and selection attributes.
//  GraphicDevice - Do the real draw tasks.
//
//Classes - GraphicContext,SelectionGraphicContext,DrawGraphicContext
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Drawing;
using FRMath;

// Platform dependence 
// GraphicContext: Color

// Define the context information when do some action.
// These classes help the related classes only maintain the 
// necessary and platform-independent information.

// DP: Template Method [GraphicContext::CreateGraphicDevice()]

namespace FRGraphic
{

    public abstract class GraphicContext
    {
        protected GraphicContext(DeviceContext deviceContext
            , Color DrawingColor, Matrix44 WorldToDevice)
        {
            m_DeviceContext = deviceContext;
            m_DrawingColor = DrawingColor;

            m_fLinwWidth = 1;

            m_WorldToDevice = WorldToDevice;
        }

        public Matrix44 WorldToDevice
        {
            get { return m_WorldToDevice; }
        }

        public abstract GraphicDeviceProxy CreateGraphicDeviceProxy();

        //public Graphics m_GDIGraphics;
        public Color m_DrawingColor; // The default color when draw.
        public float m_fLinwWidth;

        private DeviceContext m_DeviceContext;
        private Matrix44 m_WorldToDevice;

        #region Added for OpenGL

        public GraphicDevice GetGraphicDevice()
        {
            return m_DeviceContext.GraphicDevice;
        }
        #endregion
    };

    public class SelectionGraphicContext:GraphicContext
    {
        public SelectionGraphicContext(DeviceContext deviceContext, Color PreviewPointColor,
            Color PreviewPointCircleColor, Color PreviewCurveColor, Matrix44 WorldToDevice)
            : base(deviceContext, PreviewCurveColor, WorldToDevice)
        {
            m_PointPenColor = PreviewPointCircleColor;
            m_PointBrushColor = PreviewPointColor;
            m_fPointViewRadius = 4;
        }

        public override GraphicDeviceProxy CreateGraphicDeviceProxy()
        {
            return new SelectionGraphicDeviceProxy(this);
        }

        public float m_fPointViewRadius;
        public Color m_PointPenColor;
        public Color m_PointBrushColor;
    };

    public class DrawGraphicContext : GraphicContext
    {
        public DrawGraphicContext(DeviceContext deviceContext
            , Color ForeColor, Matrix44 WorldToDevice)
            : base(deviceContext, ForeColor, WorldToDevice)
        {

        }
        public override GraphicDeviceProxy CreateGraphicDeviceProxy()
        {
            return new DrawGraphicDeviceProxy(this);
        }
    }; 
}
