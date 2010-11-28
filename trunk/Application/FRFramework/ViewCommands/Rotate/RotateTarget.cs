
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
// a.	Architecture: It is a state command, use the target to trace the mouse behavior, and update the view transform. Like manipulator.
// c.	Workflow: Invoke command, show the orbit graphic, change the mouse shape. View observer.
// d.	Workflow: Target. Left button down->drag->change the view transformation->update the view-> end drag/ button up.
// e.	Algorithm: Use 1/3 of the diameter of the circle as the benchmark to get the rotate angle. Rotate based on the axis on the screen plane and upright to the mouse movement direction. 
// 
// History:
//  2009-3-27 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using FRMath;

namespace FRPaint
{
    class RotateTarget : EventTarget
    {
        public RotateTarget()
        {
            m_bIsDragging = false;
        }
        protected override EventResult OnMouseMove(EventContext Ctx)
        {
            Debug.Assert(Ctx != null);
            if (null == Ctx) return EventResult.eContinue;

            GePoint currentPoint = Ctx.MouseWorldPoint; //Ctx.MouseWorldPoint;
            if (m_bIsDragging
                && currentPoint != null 
                && !currentPoint.IsEqualTo(m_LastPoint))
            {
                Matrix44 trans = Matrix44.Identity;
                Vector dir = new Vector(m_LastPoint, currentPoint);
                Vector rotateAxis = UnitVector.kZAxis.Vector.Cross(dir);
                
                PtApp.ActiveView.DeviceToWorldMatrix.SetRotate(
                    30 * FRMath.MathUtil.PI / 180
                    , rotateAxis.UnitVector, PtApp.ActiveView.ViewCenter);

                m_LastPoint = Ctx.MouseWorldPoint;

                PtApp.ActiveView.UpdateView();
            }
            return EventResult.eContinue;
        }

        protected override EventResult OnLeftButtionDown(EventContext Ctx) 
        {
            m_LastPoint = Ctx.MouseWorldPoint;
            m_bIsDragging = true;
            return EventResult.eContinue;
        }

        protected override EventResult OnLeftButtionUp(EventContext Ctx) 
        {
            m_bIsDragging = false;
            return EventResult.eContinue; 
        }

        // Save the old device point. We use device point to handle the 
        // view transform.
        private GePoint m_LastPoint;

        private bool m_bIsDragging;
    }
}
