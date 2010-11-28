//////////////////////////////////////////////////////////////////////////
//
//
// Classes - DrawGraphicDeviceProxy
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

namespace FRGraphic
{
    public class DrawGraphicDeviceProxy : GraphicDeviceProxy
    {
        public DrawGraphicDeviceProxy(GraphicContext GpCtx)
            : base(GpCtx)
        {

        }

        #region Different drawing format - point
        public override void DrawPoint(GePoint point)
        { }
        #endregion
    };
}
