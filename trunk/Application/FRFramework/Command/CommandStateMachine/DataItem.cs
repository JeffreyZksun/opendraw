

//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using FRMath;
using System.Diagnostics;

// FRD - Save the data information collected by the state machine
namespace FRPaint
{
    public class DataItem
    {
    }

    public class PositionDataItem: DataItem
    {
        public PositionDataItem(GePoint point)
        {
            Debug.Assert(point != null);
            if(point != null)
            {
                m_point = (GePoint)point.Clone();
            }            
        }

        public GePoint Point 
        {
            get
            {
                return m_point;
            }
        }

        private GePoint m_point;
    };
}
