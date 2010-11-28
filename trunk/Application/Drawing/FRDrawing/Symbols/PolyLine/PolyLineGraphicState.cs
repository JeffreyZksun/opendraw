//////////////////////////////////////////////////////////////////////////
//
//
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-27 Create
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using FRPaint.Fragment;
using FRMath;
using FRContainer;
using FRGraphic;
using System.Diagnostics;

namespace FRPaint
{
    class PolyLineGraphicState : SymbolGraphicState
    {
        public PolyLineGraphicState(DataFragment fragment)
            : base(fragment)
        {
            m_Points = new FRList<GePoint>();
        }

        public override void GenerateGraphics(DisplayItemList DLList)
        {
            Debug.Assert(DLList != null);
            if (null == DLList) return;

            if (m_Points.Count < 2) return;

            DisplayItemBuilder.GenDisplayItemPoints(DLList, m_Points);
            DisplayItemBuilder.GenDisplayItemLines(DLList, m_Points);
        }

        public FRList<GePoint> LineVertexes
        {
            set { m_Points = value; }
        }


        private FRList<GePoint> m_Points;
    }
}
