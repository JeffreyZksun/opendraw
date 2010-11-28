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
using FRMath;

namespace FRPaint
{
    public class SpacePointSelectTarget : EventTarget        
    {
        public SpacePointSelectTarget(SpacePointSelectState parentState)
            :base(parentState)
        {

        }

        protected override EventResult OnMouseMove(EventContext Context)
        {
            SpacePointSelectState state = m_ParentState as SpacePointSelectState;
            if (state != null)
            {
                GePoint point = Context.MouseWorldPoint;
                state.OnMouseMove(point);
            }
            return EventResult.eContinue;
        }

        protected override EventResult OnLeftButtionClick(EventContext Context) 
        {
            SpacePointSelectState state = m_ParentState as SpacePointSelectState;
            if (state != null)
            {
                GePoint point = Context.MouseWorldPoint;
                state.OnLeftButtionClick(point);
            }
            return EventResult.eDone; 
        }

        protected override EventResult OnLeftDoubleClick(EventContext Context) 
        {
            SpacePointSelectState state = m_ParentState as SpacePointSelectState;
            if(state != null)
            {
                GePoint point = Context.MouseWorldPoint;
                state.OnLeftDoubleClick(point);
            }
            return EventResult.eDone; 
        }

    }
}
