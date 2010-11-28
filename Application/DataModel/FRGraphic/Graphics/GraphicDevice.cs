
//////////////////////////////////////////////////////////////////////////
//
// The coordinate in this class are device coordinate.
//  This class will finish the final drawing tasks.
// 
// Author: Sun Zhongkui
//
// History:
//  2008-2-17 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using FRMath;
using System.Drawing;
using FRContainer;
using System.Diagnostics;

namespace FRGraphic
{
    public abstract class GraphicDevice
    {
        public GraphicDevice()
        {
            m_SettingData = new GraphicSettingData();

            SetColor(Color.Black);
        }

        #region Draw Graphic
        public abstract void DrawMesh(FRList<int> connectivity
            , FRList<GePoint> points, FRList<UnitVector> normals
            , FRList<Color> colors);

        public abstract void DrawLine(
            GePoint sPoint, GePoint ePoint);


        public abstract void DrawCircle(
            GePoint cneterPoint, float fRadius);


        public abstract void DrawPoint(
            GePoint point);


        public abstract void FillEllipse(
            GeRectangle rect);


        public abstract void DrawArc(GeRectangle rect
            , float startAngle, float endAngle);

        public abstract void DrawString(string text
            , GePoint position);

        public abstract GeRectangle GetRange(String str, Font font);
        #endregion

        #region Set attributes
        public abstract void SetColor(Color color);
        public abstract void SetLineWidth(float width);
        public abstract void SetPointSize(float size);
        public abstract void SetFont(Font font);
        #endregion

        #region Graphic Attribute stack
        public enum AttributeType
        {
            eColor,
            eLineStyle,
            ePointSize,
            eFont
        }

        public void Push(AttributeType type)
        {
            // Save the setting type
            m_SettingData.m_AttributeTypeStack.Push(type);

            // Save the current settings
            switch (type)
            {
                case AttributeType.eColor:
                    m_SettingData.m_ColorStatck.Push(m_SettingData.m_Color);
                    break;

                case AttributeType.eLineStyle:
                    m_SettingData.m_LineWidthStack.Push(m_SettingData.m_LineWidth);
                    break;

                case AttributeType.ePointSize:
                    m_SettingData.m_PointSizeStack.Push(m_SettingData.m_PointSize);
                    break;

                case AttributeType.eFont:
                    m_SettingData.m_FontStack.Push(m_SettingData.m_Font);
                    break;

                default:
                    Debug.Assert(false, "Not handled attribute");
                    break;
            }
        }

        public void Pop()
        {
            AttributeType type = m_SettingData.m_AttributeTypeStack.Pop();
            // Apply the last setting on the top of the stack.
            switch (type)
            {
                case AttributeType.eColor:
                    SetColor(m_SettingData.m_ColorStatck.Pop());
                    break;

                case AttributeType.eLineStyle:
                    SetLineWidth(m_SettingData.m_LineWidthStack.Pop());
                    break;

                case AttributeType.ePointSize:
                    SetPointSize(m_SettingData.m_PointSizeStack.Pop());
                    break;

                case AttributeType.eFont:
                    SetFont(m_SettingData.m_FontStack.Pop());
                    break;

                default:
                    Debug.Assert(false, "Not handled attribute");
                    break;
            }

        }

        #endregion

        #region Data cache current graphic settings
        protected GraphicSettingData m_SettingData;
        #endregion
    }
}
