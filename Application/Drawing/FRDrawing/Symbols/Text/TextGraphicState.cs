//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using FRPaint.Fragment;
using FRMath;
using FRGraphic;
using FRText;
using System.Diagnostics;

namespace FRPaint
{
    class TextGraphicState: SymbolGraphicState
    {
        public TextGraphicState(DataFragment fragment)
            : base(fragment)
        {
            m_String = new RichString(PtApp.ActiveDocument.GetFontManager());
        }

        public override void GenerateGraphics(DisplayItemList DLList)
        {
            Debug.Assert(DLList != null);
            if (null == DLList) return;

            DisplayItemText DLText =
                DisplayItemBuilder.GenDisplayItemText(DLList, m_String, m_Position
                , PtApp.ActiveView.GetRichStringRange(m_String));

            GeRectangle rec = DLText.Range;
            if (rec != null)
            {
                rec.MoveTo(m_Position);

                GePoint MinPoint = rec.MinPoint;
                GePoint MaxPoint = rec.MaxPoint;
                GePoint Corner1 = new GePoint(MinPoint.X, MaxPoint.Y);
                GePoint Corner2 = new GePoint(MaxPoint.X, MinPoint.Y);

                DisplayItemBuilder.GenDisplayItemPoint(DLList, MinPoint);
                DisplayItemBuilder.GenDisplayItemPoint(DLList, MaxPoint);
                DisplayItemBuilder.GenDisplayItemPoint(DLList, Corner1);
                DisplayItemBuilder.GenDisplayItemPoint(DLList, Corner2);
            }
        }

        public GePoint Position
        {
            set { m_Position = (GePoint)value.Clone(); }
        }

        public RichString TextString
        {
            set { m_String = value; }
        }

        private RichString m_String;
        private GePoint m_Position;
    }
}
