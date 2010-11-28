
//////////////////////////////////////////////////////////////////////////
//
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
    public abstract class GeometryVisitor
    {
        public GeometryVisitor(SnapContext ctx, GePoint point)
        {
            m_SnapContext = ctx;
            m_ReferencePoint = (GePoint)point.Clone();
        }

        public abstract void Visit(SymbolGeometryConstraint GeProxy);

        protected SnapContext m_SnapContext;
        protected GePoint m_ReferencePoint;
    }

    public class GePointVisitor : GeometryVisitor
    {
         public GePointVisitor(SnapContext ctx, GePoint point)
             : base(ctx, point)
        {  }

        public override void Visit(SymbolGeometryConstraint GeConst)
        {
            Debug.Assert(GeConst != null);
            if (null == GeConst) return;

            if (GeConst is SymbolPointConstraint)
            {
                SymbolPointConstraint PointConst = (SymbolPointConstraint)GeConst;
                double dist = m_ReferencePoint.DistanceTo(
                    PointConst.GetPointState().Point);

                if (dist < m_SnapContext.SnapTolerance)
                {
                    m_SnapContext.SnappedGeometryConstraint = PointConst;
                    m_SnapContext.SnappedPoint = PointConst.GetPointState().Point;
                }
            }
       
        }
    };

    public class GeLineVisitor : GeometryVisitor
    {
         public GeLineVisitor(SnapContext ctx, GePoint point)
             : base(ctx, point)
        {  }

        public override void Visit(SymbolGeometryConstraint GeProxy)
        {

        }
    };
}
