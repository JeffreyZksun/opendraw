//////////////////////////////////////////////////////////////////////////
//
//
// Author: Sun Zhongkui
//
// History:
//  2009-6-9 Create
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace FRPaint
{
    class ObserverWrapper
    {
        public ObserverWrapper(IObserver observer, EventType vt)
        {
            m_Observer = observer;
            m_InterestedEventType = vt;
        }
        public void OnEvent(SubjectEvent v)
        {
            // If observer interest in the event type, notify it.
            if(IsInterestedIn(v.GetEventType()))
            {
                m_Observer.OnEvent(v);
            }
        }
        public bool IsInterestedIn(EventType vt)
        {
            // To do - Currently, we only support one event type.
            return m_InterestedEventType == vt;
        }

        public IObserver GetObserver()
        {
            return m_Observer;
        }

        public void AddInsterestedEvent(EventType interestedEventType)
        {
            Debug.Assert(false, "NO IMP");
        }

        private IObserver m_Observer;
        private EventType m_InterestedEventType;
    }
}
