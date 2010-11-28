//////////////////////////////////////////////////////////////////////////
//
// Save all the data about graphic settings
// 
// Author: Sun Zhongkui
//
// History:
//  2009-6-10 Create
//
//////////////////////////////////////////////////////////////////////////
using System.Drawing;
using System.Collections.Generic;

namespace FRGraphic
{
    public class GraphicSettingData
    {

        public GraphicSettingData()
        {
            m_AttributeTypeStack = new Stack<GraphicDevice.AttributeType>();
            m_ColorStatck = new Stack<Color>();
            m_LineWidthStack = new Stack<float>();
            m_PointSizeStack = new Stack<float>();
            m_FontStack = new Stack<Font>();

            m_Color = Color.Black;
            m_LineWidth = 1.0f;
            m_PointSize = 1.0f;
            m_Font = null;
        }

        #region Data - graphic setting stack
        public Stack<GraphicDevice.AttributeType> m_AttributeTypeStack;
        public Stack<Color> m_ColorStatck;
        public Stack<float> m_LineWidthStack;
        public Stack<float> m_PointSizeStack;
        public Stack<Font> m_FontStack;
        #endregion

        #region Data cache current graphic settings
        public Color m_Color;
        public float m_LineWidth;
        public float m_PointSize;
        public Font m_Font;
        #endregion
    }
}
