
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-27 Create
//
//////////////////////////////////////////////////////////////////////////

namespace FRPaint
{
    // This command don't use the state machine.
    class DrSelectCmd : PtCommand
    {
        public DrSelectCmd()
            : base("SelectCmd", false)
        {            
        }

        protected override EventTarget GetCurrentTraget()
        {
            return m_SelTarget;
        }

        protected override bool OnActive()
        {
            m_SelTarget = new SelectEventTarget();

            SetStatus("Select");
            SetCursorShape("SelectArrow.cur");

            return true;
        }

        public override void OnResume() 
        {
            SetStatus("Select");
            SetCursorShape("SelectArrow.cur");
        }

        public override void OnEvent(EventContext EventCtx)
        {
            m_SelTarget.OnEvent(EventCtx);
        }

        private SelectEventTarget m_SelTarget;
    };
}
