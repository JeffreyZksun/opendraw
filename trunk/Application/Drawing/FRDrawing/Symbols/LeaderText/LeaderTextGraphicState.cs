//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-12-11 Create
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using FRPaint.Fragment;
using FRMath;
using FRContainer;
using FRText;
using FRGraphic;
using System.Diagnostics;

namespace FRPaint
{
    class LeaderTextGraphicState: SymbolGraphicState
    {
        public LeaderTextGraphicState(DataFragment fragment)
            : base(fragment)
        {
            m_AttachPoint = new GePoint();
            m_InternalPoints = new FRList<GePoint>();
            m_Text = new RichString(PtApp.ActiveDocument.GetFontManager());

        }

         public override void GenerateGraphics(DisplayItemList DLList)
         {
             Debug.Assert(DLList != null);
             if (null == DLList) return;

             // It must exist two point at least.
             if (m_AttachPoint == null) return;
             if (m_InternalPoints.Empty()) return;


             // Attach point to the first internal point
             GePoint p1 = m_AttachPoint;
             GePoint p2 = m_InternalPoints[0];
             // If the first two points are equal, we needn't continue to calculate.
             if (p1.IsEqualTo(p2)) return;


             Matrix44 trans = new Matrix44();
             trans.SetTranslation(p1 - GePoint.kOrigin);
             //trans.SetRotate(MathFactory.Math.PI, UnitVector.kZAxis, GePoint.kOrigin);
             Vector direction = p2 - p1;
             trans.SetRotate(UnitVector.kXAxis, direction.UnitVector, GePoint.kOrigin);
             //trans.SetRotate(UnitVector.kXAxis, UnitVector.kYAxis, p2);

             // Arrowhead
             BFxSolidArrowHead arrow = new BFxSolidArrowHead();
             arrow.GetDisplayList(DLList, trans);

             DisplayItemBuilder.GenDisplayItemLine(DLList, p1, p2);

             DisplayItemBuilder.GenDisplayItemPoint(DLList, p1);

             DisplayItemBuilder.GenDisplayItemLines(DLList, m_InternalPoints);
             DisplayItemBuilder.GenDisplayItemPoints(DLList, m_InternalPoints);
                         

             // Text
             if (!m_Text.Empty)
             {
                 GeRectangle rec = PtApp.ActiveView.GetRichStringRange(m_Text);

                 if (rec != null)
                 {
                     Vector offset = new Vector(0, -rec.Height / 2, 0);
                     GePoint basePoint = m_InternalPoints[m_InternalPoints.Count - 1] + offset;
                     DisplayItemText DLText = new DisplayItemText(m_Text.GetString(), basePoint, PtApp.ActiveDocument.GetFontManager().GetFont(m_Text.FontID));
                     DLList.AddItem(DLText);

                     rec.MoveTo(basePoint);

                     DisplayItemBuilder.GenDisplayItemLines(DLList, rec);
                 }
             }
         }

        #region Attribute
        public GePoint AttachPoint
        {
            set { m_AttachPoint = (GePoint) value.Clone(); }
        }

        public RichString TextString
        {
            set { m_Text = value; }
        }

        public FRList<GePoint> LeaderVertexes
        {
            set { m_InternalPoints = value; }
        }
        #endregion

         #region Data
         private GePoint m_AttachPoint;
        // Save the points between the arrow head and the text
        private FRList<GePoint> m_InternalPoints;
        private RichString m_Text;
        #endregion
    }
}
