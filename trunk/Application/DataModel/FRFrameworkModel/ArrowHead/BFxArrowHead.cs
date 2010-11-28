
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
    public abstract class BFxArrowHead
    {
        public BFxArrowHead()
        {
            m_Height = 6;
            m_Width = 20;
        }

        public abstract void GetDisplayList(DisplayItemList DLList, Matrix44 trans);

        protected double m_Height; //[Save in other place]
        protected double m_Width; //[Save in other place]
    }
}
