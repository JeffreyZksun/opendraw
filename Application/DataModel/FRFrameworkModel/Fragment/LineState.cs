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
    public class LineState : State
    {
        public LineState(DataFragment fragment)
            : base(fragment)
        {
            m_Line = new GeLine(new GePoint(), UnitVector.kXAxis);
        }

        public GeLine Line
        {
             get { return m_Line; }
            set
            {
                if (value != null)
                {
                    m_Line = (GeLine)value.Clone();
                }
            }
        }

        private GeLine m_Line;
    }
}
