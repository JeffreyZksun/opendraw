
//////////////////////////////////////////////////////////////////////////
//
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-28 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using FRMath;

namespace FRPaint
{
    public class SnapMatcher
    {
        public SnapMatcher(SnapContext snapCtx)
        {
            Debug.Assert(snapCtx != null);
            m_SnapContext = snapCtx;
        }

        public void Match(GePoint point, PositionIntent itent)
        {
            Debug.Assert(point != null && itent != null && m_SnapContext != null);
            if (null == point || null == itent) return;           

            // Point match
            m_SnapContext.Accept(new GePointVisitor(m_SnapContext, point));
            if(m_SnapContext.HasResult())
            {
                itent.SetGeometryConstraint(m_SnapContext.SnappedGeometryConstraint);
                itent.SetPoint(m_SnapContext.SnappedPoint);
                return;
            }

            // Line match


            return;
        }

        private SnapContext m_SnapContext;
    }
}
