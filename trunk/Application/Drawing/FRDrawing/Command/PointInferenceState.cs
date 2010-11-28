//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
// History:
//  2009-3-27 Create
//
//////////////////////////////////////////////////////////////////////////

using FRPaint;
using FRMath;

namespace FRPaint
{
    public class PointInferenceState : LeafState
    {
        #region Constructor
        public PointInferenceState()
        {
            m_Target = new PointInferenceTarget();
        }
        #endregion

        #region State event - override
        public override EventTarget OnAvtive()
        {
            m_Target.SetParentState(this);

            return m_Target;
        }

        public override void OnTerminate()
        {
            //PointInferenceTarget target = (PointInferenceTarget)m_Target;
            if (GetInferPoint() != null)
            {
                m_CmdExeCtx.StateMgr.AddItem(new PositionDataItem(
                    GetInferPoint()));
            }
        }
        #endregion

        public GePoint GetInferPoint()
        {
            PointInferenceTarget Target = (PointInferenceTarget)m_Target;
            if (Target != null)
                return Target.Point;

            return null;
        }
    }
}
