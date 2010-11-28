
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-12-13 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using FRMath;
using FRContainer;
using FRGraphic;

namespace FRPaint
{
    class BFxEmptyArrowHead : BFxArrowHead
    {

        public override void GetDisplayList(DisplayItemList DLList
            , Matrix44 trans)
        {
            Debug.Assert(DLList != null && trans != null);
            if (null == DLList || null == trans) return;

            GePoint Base = new GePoint(0, 0, 0);
            GePoint End = new GePoint(m_Width, 0, 0);
            GePoint Top = new GePoint(m_Width, m_Height / 2, 0);
            GePoint Bottom = new GePoint(m_Width, -m_Height / 2, 0);
            Base.Transform(trans);
            End.Transform(trans);
            Top.Transform(trans);
            Bottom.Transform(trans);

            FRList<GePoint> PointList = new FRList<GePoint>();
            PointList.Add(Base);
            PointList.Add(Top);
            PointList.Add(Bottom);
            PointList.Add(Base);

            DisplayItemBuilder.GenDisplayItemLines(DLList, PointList);
            DisplayItemBuilder.GenDisplayItemPoint(DLList, Base);
            DisplayItemBuilder.GenDisplayItemPoint(DLList, End);
        }
    };
}
