//////////////////////////////////////////////////////////////////////////
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
using FRMath;

namespace FRPaint
{
    public class PointState : State
    {
        public PointState(DataFragment fragment)
            : base(fragment)
        {
            m_Point = new GePoint(0, 0, 0);
        }

        public GePoint Point
        {
            get { return m_Point; }
            set
            {
                if (value != null)
                {
                    m_Point = (GePoint)value.Clone();
                }
            }
        }

        private GePoint m_Point;
    }
}
