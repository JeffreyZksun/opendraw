//////////////////////////////////////////////////////////////////////////
//
//
// Classes - SelectionGraphicDeviceProxy
//          Transform coordinate from world to device. 
//          Apply preview and selection attributes.
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
using System.Drawing;
using FRContainer;

namespace FRGraphic
{
    public class SelectionGraphicDeviceProxy : GraphicDeviceProxy
    {
        public SelectionGraphicDeviceProxy(GraphicContext GpCtx)
            : base(GpCtx)
        {
        }

        #region Different drawing format - point
        public override void DrawPoint(GePoint point)
        {
            SelectionGraphicContext GpCtx = (SelectionGraphicContext)m_GraphicContext;
            if (GpCtx == null) return;

            point = GetDevicePoint(point);

            // Apply the point attribute
            PointSizeAttribute sizeAtt = new PointSizeAttribute(GpCtx.m_fPointViewRadius * 2);
            ColorAttribute colAtt = new ColorAttribute(GpCtx.m_PointBrushColor);
            sizeAtt.Apply(m_Device);
            colAtt.Apply(m_Device);

            m_Device.DrawPoint(point);

            colAtt.UnApply(m_Device);
            sizeAtt.UnApply(m_Device);

            LineStyleAttribute linAtt = new LineStyleAttribute(2.0f);
            ColorAttribute linColAtt = new ColorAttribute(GpCtx.m_PointPenColor);
            linAtt.Apply(m_Device);
            linColAtt.Apply(m_Device);

            m_Device.DrawCircle(point, GpCtx.m_fPointViewRadius);

            linColAtt.UnApply(m_Device);
            linAtt.UnApply(m_Device);
        }

        public override void DrawMesh(FRList<int> connectivity
            , FRList<GePoint> points, FRList<UnitVector> normals
            , FRList<Color> colors)
        {
            if (m_GraphicContext == null) return;
            ColorAttribute colAtt = new ColorAttribute(m_GraphicContext.m_DrawingColor);
            colAtt.Apply(m_Device);

           base.DrawMesh(connectivity, points, normals, colors);

           colAtt.UnApply(m_Device);
        }

        public override void DrawLine(GePoint sPoint, GePoint ePoint)
        {
            if (m_GraphicContext == null) return;
            ColorAttribute colAtt = new ColorAttribute(m_GraphicContext.m_DrawingColor);
            colAtt.Apply(m_Device);

            base.DrawLine(sPoint, ePoint);

            colAtt.UnApply(m_Device);
        }

        public override void DrawCircle(
            GePoint centerPoint, float fRadius)
        {
            if (m_GraphicContext == null) return;
            ColorAttribute colAtt = new ColorAttribute(m_GraphicContext.m_DrawingColor);
            colAtt.Apply(m_Device);

            base.DrawCircle(centerPoint, fRadius);

            colAtt.UnApply(m_Device);
        }

        public override void DrawText(String text, GePoint position, Font font)
        {
            if (m_GraphicContext == null) return;
            ColorAttribute colAtt = new ColorAttribute(m_GraphicContext.m_DrawingColor);
            colAtt.Apply(m_Device);

            base.DrawText(text, position, font);

            colAtt.UnApply(m_Device);
        }
        #endregion

    };
}
