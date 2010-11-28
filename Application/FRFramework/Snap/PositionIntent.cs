
//////////////////////////////////////////////////////////////////////////
//
// It saves the snapped information, which is a subset data of snap context.
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-28 Create
//
//////////////////////////////////////////////////////////////////////////

using FRMath;
using System.Diagnostics;

namespace FRPaint
{
    public class PositionIntent
    {
        public PositionIntent()
        {
            m_GeometryConst = null;
            m_Point = null;
        }

        public void SetGeometryConstraint(SymbolGeometryConstraint GeoConst)
        {
            m_GeometryConst = GeoConst;
        }

        public SymbolGeometryConstraint GetGeometryConstraint()
        {
            return m_GeometryConst;
        }

        public string GetCursorShape()
        {
            if (m_GeometryConst is SymbolPointConstraint)
                return "SnapPoint.cur";
            else if (m_GeometryConst is SymbolLineConstraint)
                return "SnapPointOnLine.cur";
            else
                return "SelectArrow.cur";
        }

        public bool IsValid()
        {
            return m_GeometryConst != null;
        }

        public void SetPoint(GePoint point)
        {
            m_Point = (GePoint)point.Clone();
        }

        public GePoint GetPoint()
        {
            return m_Point;
        }

        private SymbolGeometryConstraint m_GeometryConst;
        private GePoint m_Point;
    }
}
