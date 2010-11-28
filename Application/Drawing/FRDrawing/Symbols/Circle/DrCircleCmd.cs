
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-27 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;

using FRMath;
using FRPaint.Fragment;

namespace FRPaint
{
    class DrCircleCmd : DMStateCommand
    {
        public DrCircleCmd()
            : base("DrCircleCmd", "&Circle")
        { }

        // Every class with more than one state should 
        // declare this enum for state machine
        enum CmdState
        {
            eStart,
            eCenterPoint,
            eRaduis
        }

        protected override bool OnActive()
        {

            m_CurrentState = CmdState.eStart;           


            return base.OnActive();
        }

        public override CommandState GetNextState()
        {
            switch (m_CurrentState)
            {
                case CmdState.eStart:
                    m_CurrentState = CmdState.eCenterPoint;

                    return new PointInferenceState();
                    
                case CmdState.eCenterPoint:
                    if (m_StateMgr.DataItems.Count > 0)
                    {
                        PositionDataItem Pos = (PositionDataItem)m_StateMgr.DataItems.Back();
                        m_Instance.CenterPoint = Pos.Point;

                        m_CurrentState = CmdState.eRaduis;
                    }  

                    return new PointInferenceState();

                default:
                    if (m_StateMgr.DataItems.Count > 1)
                    {
                        PositionDataItem Pos = (PositionDataItem)m_StateMgr.DataItems.Back();
                        m_Instance.SetRaduis(Pos.Point);

                        m_CurrentState = CmdState.eRaduis;
                    } 
                    else
                    {
                        Debug.Assert(false);

                        return new PointInferenceState();
                    }

                    return null;
            }

        }

        // For updating real time.
        public override void UpdateGraphics(GePoint MousePosition)
        {
            if(m_CurrentState == CmdState.eCenterPoint)
            {
                m_Instance.SetCenter(MousePosition);
                m_Instance.Compute();
            }
            else if (m_CurrentState == CmdState.eRaduis)
            {
                m_Instance.SetRaduis(MousePosition);
                m_Instance.Compute();
            }
        }

        protected override bool OnExcute()
        {
            SetStatus("Draw a circle");

            if (!base.OnExcute())
                return false;

            return true;
        }

        protected override GraphicNode GetNode()
        {
            DataFragment fragment = PtApp.ActiveDocument.Database.GetFragment();
            m_Instance = new CircleInstance(fragment, new PointState(fragment));
            CircleNode Node = new CircleNode(m_Instance);

            return Node;
        }



        private CircleInstance m_Instance;
        private CmdState m_CurrentState;
    };
}
