//////////////////////////////////////////////////////////////////////////
//
//
// Author: Sun Zhongkui
//
// History:
//  2009-6-7 Create
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using FRPaint.Fragment;
using FRPaint;
using FRGraphic;
using FRMath;

namespace FRUML.TimeLine
{
    class UmlTimeLineConstraint: SymbolConstraint
    {
        public UmlTimeLineConstraint(DataFragment fragment)
            : base(fragment, new UmlTimeLineGraphicState(fragment))
        {
        }

        public void SetCenterPoint(GePoint centerPoint)
        {
            UmlTimeLineGraphicState symState = GetSymbolState() as UmlTimeLineGraphicState;
            if (symState != null)
                symState.SetCenterPoint(centerPoint);
        }

        #region Override for the compute pipeline
        public override void Compute()
        {
            ObserverManager.Instance.SendEvent(new SubjectEvent(this, EventType.eUpdate));
        }
        #endregion
    }
}
