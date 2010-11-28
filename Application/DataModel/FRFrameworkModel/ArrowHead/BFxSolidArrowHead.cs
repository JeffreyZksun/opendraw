
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
    public class BFxSolidArrowHead : BFxArrowHead
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

            FRList<GePoint> meshPoints = new FRList<GePoint>();
            meshPoints.Add(Base);
            meshPoints.Add(End);
            meshPoints.Add(Top);
            meshPoints.Add(Bottom);

            // Use two triangles
            FRList<int> connectivity = new FRList<int>();
            connectivity.Add(0);
            connectivity.Add(1);
            connectivity.Add(2);

            connectivity.Add(0);
            connectivity.Add(1);
            connectivity.Add(3);

            DisplayItemBuilder.GenDisplayItemLines(DLList, PointList);
            DisplayItemBuilder.GenDisplayItemMesh(DLList
                , connectivity, meshPoints, null, null);
            DisplayItemBuilder.GenDisplayItemPoint(DLList, Base);
            DisplayItemBuilder.GenDisplayItemPoint(DLList, End);
        }
    };
}

