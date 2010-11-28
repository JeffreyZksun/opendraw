
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-30 Create
//
//////////////////////////////////////////////////////////////////////////

using FRMath;
using System.Diagnostics;
using FRContainer;
using FRPaint.Fragment;

namespace FRPaint
{
     public class CircleManipulator : Manipulator
     {
         public CircleManipulator(CircleNode node)
         {
             m_Node = node;
             m_Instance = m_Node.GetInstance() as CircleInstance;
         }

         protected CircleNode m_Node;
         protected CircleInstance m_Instance;         
     };

    // change the circle when drag the circle edge.
    public class CircleChangeManipulator : CircleManipulator
    {
        public CircleChangeManipulator(CircleNode node)
            : base(node)
        {
            m_CursorShape = "Resize.cur";
        }

        public override void Drag(EventContext Ctx)
        {
            Debug.Assert(Ctx != null);
            if (null == Ctx) return;

            GePoint CurrentPoint = Ctx.MouseWorldPoint;
            GePoint CircleCenter = m_Instance.CenterPoint;

            double NewRadius = CircleCenter.DistanceTo(CurrentPoint);

            m_Instance.SetRaduis(CurrentPoint);
            
            m_Instance.Compute();
            UpdateView();
        }

    }

    // Move the node when drag the center point.
    public class CircleMoveManipulator : CircleManipulator
    {
        public CircleMoveManipulator(CircleNode node)
            : base(node)
        {
            m_DragStartPoint = null;
            m_DragLastPoint = null;

            m_CursorShape = "Move.cur";
        }

        public override void StartDrag(EventContext Ctx)
        {
            Debug.Assert(Ctx != null);
            if (null == Ctx) return;

            m_DragStartPoint = (GePoint)Ctx.MouseWorldPoint.Clone();
            m_DragLastPoint = (GePoint)Ctx.MouseWorldPoint.Clone();

            m_Node.CanBeSelected = false;

            m_Intent = null;
        }

        public override void Drag(EventContext Ctx) 
        {
            Debug.Assert(Ctx != null && m_DragLastPoint != null);
            if (null == Ctx || null == m_DragLastPoint) return;

            GePoint CurrentPoint = Ctx.MouseWorldPoint;

            SnapContext snapCtx = SnapContext.InitalizeSnapContextFromPreSelection(CurrentPoint);

            SnapMatcher macher = new SnapMatcher(snapCtx);
            m_Intent = new PositionIntent();
            m_Intent.SetPoint(CurrentPoint);
            macher.Match(CurrentPoint, m_Intent);           

            GePoint NewCenter = null;
            if (m_Intent.IsValid())
            {
                m_CursorShape = m_Intent.GetCursorShape();

                NewCenter = m_Intent.GetPoint();
            }
            else
            {
                m_CursorShape = "Move.cur";

                GePoint OldCenter = m_Instance.CenterPoint;
                NewCenter = OldCenter
                    + (CurrentPoint - m_DragLastPoint);
            }

            m_Instance.SetCenter(NewCenter);

            m_DragLastPoint = (GePoint)CurrentPoint.Clone();

            m_Instance.Compute();
            UpdateView();
        }

        public override void EndDrag(EventContext Ctx) 
        {

            SymbolConstraint symConst = null;
            int pointIndex = 0;
            if (m_Intent != null && m_Intent.IsValid())
            {
                if (m_Intent.GetGeometryConstraint() != null)
                {
                    SymbolPointConstraint pointConst = m_Intent.GetGeometryConstraint() as SymbolPointConstraint;

                    if (pointConst != null)
                    {
                        symConst = pointConst.GetSymbolConstraint();
                        if (symConst != null)
                        {
                            if (m_Instance.GetFragment().IsFirstPriorToSecond(m_Instance, symConst))
                            {
                                symConst = null; // There is circle if we create forward.
                            }
                        }
                        
                        // We don't need to delete the temporary symbol point constraint
                        // Suppose the GM can collect it.
                    }
                }
            }

            // Change the data model in request
            CircleCenterMoveRequest req = new CircleCenterMoveRequest(
                m_Instance,
                m_Instance.GetCenterPoint().Point,
                symConst,
                pointIndex);

            req.Execute();

            m_Node.CanBeSelected = true;            
        }

        private GePoint m_DragStartPoint;
        private GePoint m_DragLastPoint;
        private PositionIntent m_Intent;
    };
}
