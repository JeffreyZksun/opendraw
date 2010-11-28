//////////////////////////////////////////////////////////////////////////
//
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
using FRPaint;
using FRPaint.Fragment;
using FRGraphic;
using FRMath;

namespace FRUML.TimeLine
{
    class UmlTimeLineGraphicState : SymbolGraphicState
    {
        public UmlTimeLineGraphicState(DataFragment fragment)
            : base(fragment)
        {
            m_Height = 40.0;
            m_Length = 200.0;
            m_BasePoint = new GePoint(50.0, 20.0, 0.0);
            m_Direction = UnitVector.kXAxis;
        }

        public override void GenerateGraphics(DisplayItemList DLList)
        {

            if (null == DLList) return;

            GePoint leftButtom = m_BasePoint + UnitVector.kYAxis.Vector * m_Height * (-0.5);
            GePoint leftTop = m_BasePoint + UnitVector.kYAxis.Vector * m_Height * 0.5;
            GePoint rightButtom = leftButtom + m_Direction.Vector * m_Length;
            GePoint rightTop = leftTop + UnitVector.kXAxis.Vector * m_Length;

            //DisplayItemList DisplayList = new DisplayItemList();

            DisplayItemBuilder.GenDisplayItemLine(DLList, leftButtom, leftTop);
            DisplayItemBuilder.GenDisplayItemLine(DLList, leftTop, rightTop);
            DisplayItemBuilder.GenDisplayItemLine(DLList, rightTop, rightButtom);
            DisplayItemBuilder.GenDisplayItemLine(DLList, rightButtom, leftButtom);

        }

        public void SetCenterPoint(GePoint centerPoint)
        {
            m_BasePoint = centerPoint + m_Direction.Vector * ((-m_Length) / 2);
        }
        
        
        private double m_Height;
        private double m_Length;
        private GePoint m_BasePoint; // The left center point
        private UnitVector m_Direction;
    }
}
