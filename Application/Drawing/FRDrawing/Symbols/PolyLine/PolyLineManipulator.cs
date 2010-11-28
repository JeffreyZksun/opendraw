
//////////////////////////////////////////////////////////////////////////
//
//
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-29 Create
//
//////////////////////////////////////////////////////////////////////////

using FRMath;
using System.Diagnostics;

namespace FRPaint
{
    public class PolyLineManipulator : Manipulator
    {
        public PolyLineManipulator(PolyLineNode node)
         {
             m_Node = node;
             m_Instance = m_Node.GetInstance() as PolyLineInstance;

             m_DragStartPoint = null;
             m_DragStartPoint = null;

             m_CursorShape = "Move.cur";
         }

        public override void StartDrag(EventContext Ctx)
        {
            Debug.Assert(Ctx != null);
            if (null == Ctx) return;

            m_DragStartPoint = (GePoint)Ctx.MouseWorldPoint.Clone();
            m_DragLastPoint = (GePoint)Ctx.MouseWorldPoint.Clone();
        }

        protected PolyLineNode m_Node;
        protected PolyLineInstance m_Instance;

        protected GePoint m_DragStartPoint;
        protected GePoint m_DragLastPoint;
    }

    public class MovePointManipulator : PolyLineManipulator
    {
        public MovePointManipulator(PolyLineNode node, int index)
            : base(node)
        {
            m_index = index;
        }

        public override void Drag(EventContext Ctx)
        {
            Debug.Assert(Ctx != null && m_DragLastPoint != null);
            if (null == Ctx || null == m_DragLastPoint) return;

            GePoint CurrentPoint = Ctx.MouseWorldPoint;

            Vector distance = CurrentPoint - m_DragLastPoint;

            m_Instance.MovePoints(distance, m_index);

            m_DragLastPoint = (GePoint)CurrentPoint.Clone();

            m_Instance.Compute();
            UpdateView();
        }

        private int m_index;
    };

    public class MoveLineManipulator : PolyLineManipulator
    {
        public MoveLineManipulator(PolyLineNode node)
            : base(node)
        {

        }

        public override void Drag(EventContext Ctx)
        {
            GePoint CurrentPoint = Ctx.MouseWorldPoint;

            Vector distance = CurrentPoint - m_DragLastPoint;
            m_Instance.MovePoints(distance, -1);

            m_DragLastPoint = (GePoint)CurrentPoint.Clone();

            m_Instance.Compute();
            UpdateView();
        }
    };
}
