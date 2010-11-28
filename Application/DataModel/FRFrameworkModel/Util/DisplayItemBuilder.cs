

//////////////////////////////////////////////////////////////////////////
//
// All the display items should be created in this builder.
//
// Author: Sun Zhongkui
//
// History:
//  2008-8-2 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using FRGraphic;
using FRText;
using FRMath;
using FRContainer;
using System.Diagnostics;
using System.Drawing;

namespace FRPaint
{
    public class DisplayItemBuilder
    {
        public static DisplayItemText GenDisplayItemText(DisplayItemList list, RichString str, GePoint position, GeRectangle rec)
        {
            //GeRectangle rec = PtApp.ActiveView.GetRichStringRange(str);

            DisplayItemText DLText = new DisplayItemText(str.GetString(), position,
                str.GetFont());
            DLText.Range = rec;

            if (list != null)
                list.AddItem(DLText);

            return DLText;
        }
        public static DisplayItemCircle GenDisplayItemCircle(DisplayItemList list, GePoint center, double radius)
        {
            DisplayItemCircle Circle = new DisplayItemCircle(
                center, radius);
            if (list != null)
                list.AddItem(Circle);

            return Circle;
        }

        public static DisplayItemPoint GenDisplayItemPoint(DisplayItemList list, GePoint position)
        {   
            DisplayItemPoint DLPoint = new DisplayItemPoint(position);
            if (list != null)
                list.AddItem(DLPoint);

            return DLPoint;
        }

        public static void GenDisplayItemPoints(DisplayItemList DLList, FRList<GePoint> points)
        {
            Debug.Assert(DLList != null && points != null);
            if (null == DLList || null == points) return;


            for (int i = 0; i < points.Count; i++)
            {
                GePoint point = points[i];
                DisplayItemPoint DLPoint = new DisplayItemPoint(point);
                DLList.AddItem(DLPoint);
            }
        }

        public static void GenDisplayItemLine(DisplayItemList DLList
            , GePoint startPoint, GePoint endPoint)
        {
            Debug.Assert(DLList != null && startPoint != null && endPoint != null);
            if (null == DLList || null == startPoint || null == endPoint) return;

            if (!startPoint.IsEqualTo(endPoint))
            {
                DisplayItemLine dpLine = new DisplayItemLine(startPoint, endPoint);
                DLList.AddItem(dpLine);
            }
        }

        public static void GenDisplayItemLines(DisplayItemList DLList, FRList<GePoint> points)
        {
            Debug.Assert(DLList != null && points != null);
            if (null == DLList || null == points) return;

            if (points.Count < 2) return;

            for (int i = 0; i < points.Count - 1; i++)
            {
                GePoint sp = points[i];
                GePoint ep = points[i + 1];

                GenDisplayItemLine(DLList, sp, ep);
            }
        }

        public static void GenDisplayItemLines(DisplayItemList DLList, GeRectangle rec)
        {
            Debug.Assert(DLList != null && rec != null);
            if (null == DLList || null == rec) return;

            GePoint MinPoint = rec.MinPoint;
            GePoint MaxPoint = rec.MaxPoint;
            GePoint Corner1 = new GePoint(MinPoint.X, MaxPoint.Y);
            GePoint Corner2 = new GePoint(MaxPoint.X, MinPoint.Y);

            // Add the bounding of the rectangular.
            GenDisplayItemLine(DLList, Corner1, MinPoint);
            GenDisplayItemLine(DLList, MinPoint, Corner2);
            GenDisplayItemLine(DLList, Corner2, MaxPoint);
            GenDisplayItemLine(DLList, MaxPoint, Corner1);
            DLList.AddItem(new DisplayItemPoint(MinPoint));
            DLList.AddItem(new DisplayItemPoint(MaxPoint));
            DLList.AddItem(new DisplayItemPoint(Corner1));
            DLList.AddItem(new DisplayItemPoint(Corner2));
        }

        public static void GenDisplayItemMesh(DisplayItemList DLList, FRList<int> connectivity
    , FRList<GePoint> points, FRList<UnitVector> normals
    , FRList<Color> colors)
        {
            Debug.Assert(DLList != null && connectivity != null && points != null);
            if (null == DLList || null == connectivity || null == points) return;

            if (connectivity.Count < 3 || points.Count < 3) return;

            DisplayItemMesh mesh = new DisplayItemMesh();

            mesh.Connectivity = connectivity;
            mesh.Points = points;
            mesh.Normals = normals;
            mesh.Colors = colors;

            DLList.AddItem(mesh);
        }
    }
}
