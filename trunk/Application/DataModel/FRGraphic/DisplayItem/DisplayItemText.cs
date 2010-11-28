
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using FRMath;
using System.Diagnostics;
using System.Drawing;

namespace FRGraphic
{
    public class DisplayItemText : DisplayItem
    {
        public DisplayItemText(String str, GePoint Position, Font font)
        {
            m_String = str;
            m_Position = Position;
            m_Font = font;
            m_Range = new GeRectangle();
        }

        #region Override class DisplayItem
        protected override void OnDraw(GraphicContext gpCtx, Matrix44 parentTrans)
        {
            if (gpCtx != null)
            {
                GePoint worldPosition = MathUtil.GetTransformedPoint(m_Position, parentTrans);

                GraphicDeviceProxy Device =
                    gpCtx.CreateGraphicDeviceProxy();
                Device.DrawText(m_String, worldPosition, m_Font);
            }
        }

        protected override bool OnQualify(SelectContext selectCtx, Matrix44 parentTrans)
        {
            if (selectCtx == null) return false;

            GeRectangle Rec = Range;

            GePoint worldPosition = MathUtil.GetTransformedPoint(m_Position, parentTrans);

            Rec.MoveTo(worldPosition);

            if (selectCtx.WorldPoint.X > (Rec.MinPoint.X - selectCtx.SelectTolerance)
                && selectCtx.WorldPoint.X < (Rec.MaxPoint.X + selectCtx.SelectTolerance)
                && selectCtx.WorldPoint.Y > (Rec.MinPoint.Y - selectCtx.SelectTolerance)
                && selectCtx.WorldPoint.Y < (Rec.MaxPoint.Y + selectCtx.SelectTolerance))
            {
                selectCtx.AddItem(0, this);
                return true;
            }

            return false;
        }
        #endregion

        #region Attribute
        // We create a new rectangle, so its value can't be change 
        // outside this object.
        public GeRectangle Range
        {
            get { return new GeRectangle(m_Range); }
            set 
            {
                if (value != null)
                {
                    m_Range = new GeRectangle(value);
                    m_Range.MoveTo(new GePoint(0, 0, 0));
                }
            }
        }
        #endregion

        #region data
        private String m_String;
        private GePoint m_Position;
        private Font m_Font;

        // The min point of the range in origin.
        private GeRectangle m_Range;
        #endregion
    };
}
