
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

namespace FRPaint
{
    public abstract class Manipulator
    {
        public Manipulator()
        {
            m_DefaultCursorShape = "SelectArrow.cur";
            m_CursorShape = m_DefaultCursorShape;
        }

        public virtual void StartDrag(EventContext Ctx){}
        public virtual void Drag(EventContext Ctx) { }
        public virtual void EndDrag(EventContext Ctx) { }
        public virtual void DoubClick() { }
        public virtual void MouseWheel(EventContext Ctx) { }

        protected void UpdateView()
        {
            PtView view = PtApp.GetActiveView();
            view.UpdateView();
        }

        public string GetCursorShape()
        {
            return m_CursorShape;
        }        

        protected string m_CursorShape;
        protected string m_DefaultCursorShape;
    }
}
