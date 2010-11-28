//////////////////////////////////////////////////////////////////////////
//
//
// Classes - GraphicDeviceProxy
// 
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using FRMath;

// It will finish the coordinate transform and the 
// color of display lists.
// Convert the world coordinates to the device coordinates.
// All the display item coordinates should be converted before drawing.
//

using System.Drawing;
using FRContainer;

namespace FRGraphic
{
    public abstract class GraphicDeviceProxy
    {
        public GraphicDeviceProxy(GraphicContext GpCtx)
        {
            m_GraphicContext = GpCtx;
            m_Device = GpCtx.GetGraphicDevice();

            m_WorldToDevice = GpCtx.WorldToDevice;
        }

        #region Draw graphic - Line Point Circle

        public virtual void DrawMesh(FRList<int> connectivity
            , FRList<GePoint> points, FRList<UnitVector> normals
            , FRList<Color> colors)
        {
            // We should keep the sequence of the points.
            FRList<GePoint> worldPoints = new FRList<GePoint>();
            foreach (GePoint point in points)
            {
                worldPoints.Add(GetDevicePoint(point));
            }

            m_Device.DrawMesh(connectivity, worldPoints
                , normals, colors);
        }

        public virtual void DrawLine(GePoint sPoint, GePoint ePoint)
        {
            m_Device.DrawLine(GetDevicePoint(sPoint)
                , GetDevicePoint(ePoint));
        }

        public virtual void DrawCircle(
            GePoint centerPoint, float fRadius)
        {
            float raduis = (float)GetWorldToDeviceScale() * fRadius;

            m_Device.DrawCircle(GetDevicePoint(centerPoint)
                , raduis);
        }

        public virtual void DrawPoint(GePoint point)
        {
            m_Device.DrawPoint(GetDevicePoint(point));
        }

        public virtual void DrawText(String text, GePoint position, Font font)
        {
            position = GetDevicePoint(position);
            m_Device.DrawString(text, position);
        }

        protected GePoint GetDevicePoint(GePoint worldPoint)
        {
            Debug.Assert(m_WorldToDevice != null);

            return m_WorldToDevice * worldPoint;
        }

        public double GetWorldToDeviceScale()
        {
            return m_WorldToDevice.GetAllScaling();
        }
        #endregion
        
        protected GraphicContext m_GraphicContext;
        protected GraphicDevice m_Device;

        private Matrix44 m_WorldToDevice;
    }
}
