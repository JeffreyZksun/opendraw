//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-27 Create
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using FRPaint.Fragment;
using FRMath;
using FRGraphic;

namespace FRPaint
{
    public class CircleGraphicState: SymbolGraphicState
    {
        public CircleGraphicState(DataFragment fragment)
            : base(fragment)
        {
            m_CenterPoint = new GePoint();
            m_dRadius = 10;
        }

        public override void GenerateGraphics(DisplayItemList DLList)
        {
            if (null == DLList) return;

            if (m_dRadius < 0)
                return;

            //DisplayItemList DisplayList = new DisplayItemList();

            DisplayItemCircle Circle = new DisplayItemCircle(
                m_CenterPoint, m_dRadius);
            DLList.AddItem(Circle);

            DisplayItemPoint CenterPoint = new DisplayItemPoint(m_CenterPoint);
            DLList.AddItem(CenterPoint);
        }

        #region Attribute
        public GePoint CenterPoint
        {
            set {m_CenterPoint = (GePoint)value.Clone();}
        }

        public double Radius
        {
            set { m_dRadius = value; }
        }
        #endregion

        #region Data
        //private GePoint m_CenterPoint;
        private GePoint m_CenterPoint;
        private double m_dRadius;
        #endregion
    }
}
