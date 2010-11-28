//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2008-10-16 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Windows.Forms;
using FRPaint;
using FRMath;

namespace FRPart
{
    class PTExtrudeCmd : StateCommand
    {
        public PTExtrudeCmd()
            : base("PTExtrudeCmd", "&Extrude")
        { }

        #region State machine callbacks.

        protected override CommandState GetInitialState()
        {
            return new SelectState();
        }

        #endregion

        #region Command events
        protected override bool OnExcute()
        {
            SetStatus("Extrude");


            return base.OnExcute();
        }

        #endregion

        private ExtrusionInstance m_Instance;
    };
}
