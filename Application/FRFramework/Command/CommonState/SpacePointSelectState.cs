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
    public class SpacePointSelectState : LeafState
    {
        public SpacePointSelectState()
        {
            m_Target = new SpacePointSelectTarget(this);
        }

        public virtual void OnMouseMove(GePoint point)
        {
           
        }

        public virtual void OnLeftButtionClick(GePoint point)
        {

        }

        public virtual void OnLeftDoubleClick(GePoint point)
        {
            
        }

    }
}
