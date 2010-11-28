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

using FRPaint;
using FRMath;
using System.Diagnostics;

namespace FRPaint
{
    public class PointInferenceTarget : EventTarget
    {
        public PointInferenceTarget()
        {
            m_MousePoint = null;
        }


        #region Mouse event
        protected override EventResult OnMouseMove(EventContext Context)
        {
            Debug.Assert(Context != null);
            if (null == Context) return EventResult.eContinue;

            DMStateCommand cmd = m_ParentState.CommandExeCtx.Command as DMStateCommand;

            if (cmd != null)
                cmd.UpdateGraphics(Context.MouseWorldPoint);

            PtApp.GetActiveView().UpdateView();

            //SnapContext snapCtx = new SnapContext();
            //snapCtx.InitalizeSnapContext(Context.m_MousePoint);

            //SnapMatcher macher = new SnapMatcher(snapCtx);
            //PositionIntent itent = new PositionIntent();
            //macher.Match(Context.m_MousePoint, itent);

            //if (itent.IsValid())
            //    SetCursorShape(itent.GetCursorShape());
            //else
            //    SetCursorShape("Drawing.cur");

            return EventResult.eContinue;
        }

        protected override EventResult OnLeftButtionClick(EventContext Context)
        {
            m_MousePoint = Context.MouseWorldPoint;

            return EventResult.eDone;
        }

        protected override EventResult OnLeftDoubleClick(EventContext Context)
        {
            m_MousePoint = Context.MouseWorldPoint;

            DMStateCommand cmd = m_ParentState.CommandExeCtx.Command as DMStateCommand;

            if (cmd != null)
                cmd.ShowDialog();

            return EventResult.eDone;
        }

        #endregion

        public GePoint Point
        {
            get
            {
                return m_MousePoint;
            }
        }

        private GePoint m_MousePoint;
    };
}
