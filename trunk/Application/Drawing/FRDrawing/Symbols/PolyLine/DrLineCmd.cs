
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-27 Create
//
//////////////////////////////////////////////////////////////////////////

using FRMath;
using FRPaint.Fragment;

namespace FRPaint
{
    class DrLineCmd : DMStateCommand
    {
        // OPT - We can use a file to manage all the internal names. 
        public DrLineCmd()
            : base("DrLineCmd", "&Line")
        {
            m_Instance = null;
        }

        public override CommandState GetNextState()
        {
            if(!m_StateMgr.DataItems.Empty())
            {
                PositionDataItem Pos = (PositionDataItem)m_StateMgr.DataItems.Back();
                m_Instance.AddLastPointBefore(Pos.Point);
            }

            return new PointInferenceState();
        }

        protected override bool OnActive()
        {
            return base.OnActive();
        }

        protected override bool OnExcute()
        {
            SetStatus("Draw Line");

            if(!base.OnExcute())
                return false;

            return true;
        }

        protected override GraphicNode GetNode()
        {
            DataFragment fragment = PtApp.ActiveDocument.Database.GetFragment();
            m_Instance = new PolyLineInstance(fragment);
            m_Instance.SetLastPoint(new GePoint(0, 0, 0));
            PolyLineNode Node = new PolyLineNode(m_Instance);

            return Node;
        }

        // For updating real time.
        public override void UpdateGraphics(GePoint MousePosition)
        {
            m_Instance.SetLastPoint(MousePosition);
            m_Instance.Compute();
        }

        protected override void OnCommit()
        {
            m_Instance.RemoveLastPoint();

            base.OnCommit();
        }



        private PolyLineInstance m_Instance;
    };
}
