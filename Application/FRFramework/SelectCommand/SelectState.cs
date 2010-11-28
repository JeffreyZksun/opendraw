//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
// History:
//  2009-3-27 Create
//
//////////////////////////////////////////////////////////////////////////

namespace FRPaint
{
    public class SelectState : LeafState
    {
        #region Constructor
        public SelectState()
        {
            m_Target = new SelectEventTarget();
        }
        #endregion

        #region State event - override
        public override EventTarget OnAvtive()
        {
            return m_Target;
        }

        public override void OnTerminate()
        {
        }
        #endregion

    }
}
