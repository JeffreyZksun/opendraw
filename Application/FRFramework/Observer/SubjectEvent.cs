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

namespace FRPaint
{
    public enum EventType
    {
        eUnkonwn = 0,
        eUpdate
    }

    public class SubjectEvent
    {
        public SubjectEvent(Object subject, EventType vt)
        {
            m_Subject = subject;
            m_EventType = vt;
        }

        public Object GetSubject()
        {
            return m_Subject;
        }

        public EventType GetEventType()
        {
            return m_EventType;
        }

        private Object m_Subject;
        private EventType m_EventType;
    }
}
