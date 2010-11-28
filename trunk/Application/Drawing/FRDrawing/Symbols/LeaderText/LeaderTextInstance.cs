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
using FRText;
using FRContainer;

namespace FRPaint
{
    public class LeaderTextInstance : SymbolConstraint
    {
        public LeaderTextInstance(DataFragment fragment, PointState attachPoint)
            : base(fragment, new LeaderTextGraphicState(fragment))
        {
            m_AttachPoint = attachPoint;
            m_InternalPoints = new FRList<GePoint>();
            m_Text = new RichString(PtApp.ActiveDocument.GetFontManager());
        }

        #region Override for the compute pipeline
        public override void Compute()
        {
            LeaderTextGraphicState symState = GetSymbolState() as LeaderTextGraphicState;

            symState.AttachPoint = m_AttachPoint.Point;
            symState.LeaderVertexes = m_InternalPoints;
            symState.TextString = m_Text;

            ObserverManager.Instance.SendEvent(new SubjectEvent(this, EventType.eUpdate));
        }
        #endregion

        public void AddLastPointBefore(GePoint point)
        {
            if (point != null)
            {
                if (m_InternalPoints.Empty())
                {
                    m_AttachPoint.Point = point;
                    m_InternalPoints.Add(point);
                }
                else
                    m_InternalPoints.Insert(m_InternalPoints.Count - 1, point);
            }
        }

        public void SetLastPoint(GePoint point)
        {
            if (point != null)
            {
                if (m_InternalPoints.Empty())
                    m_AttachPoint.Point = point;
                else
                    m_InternalPoints[m_InternalPoints.Count - 1] = point;
            }
        }

        // N/A maybe we should clone the str here.
        public void SetText(RichString str)
        {
            m_Text = str;
        }

        public RichString GetText()
        {
            return m_Text;
        }

        private PointState m_AttachPoint;
        // Save the points between the arrow head and the text
        private FRList<GePoint> m_InternalPoints;
        private RichString m_Text;
    };
}
