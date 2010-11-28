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
using FRContainer;
using System.Diagnostics;

namespace FRPaint
{
    public class ObserverManager
    {
        public ObserverManager()
        {
            m_SubjectObserverListMap = new Dictionary<object, FRList<ObserverWrapper>>();
        }

        #region Standalone
        static public ObserverManager Instance
        {
            get
            {
                if (m_ObserverMgr == null)
                    m_ObserverMgr = new ObserverManager();

                return m_ObserverMgr;
            }
        }

        static private ObserverManager m_ObserverMgr = null;
        #endregion

        #region Subject, observer manage
        public void AddObserver(Object subject, IObserver observer, EventType vt)
        {
            // Find observer list of the subject, if no, add a new item.
            FRList<ObserverWrapper> observerList = null;
            if (m_SubjectObserverListMap.ContainsKey(subject)) // compare the reference
            {
                observerList = m_SubjectObserverListMap[subject];
            }
            else
            {
                observerList = new FRList<ObserverWrapper>();
            }

            // If the observer to this subject exists, add the interested event type.
            foreach (ObserverWrapper tmpItem in observerList)
            {
                if (tmpItem.GetObserver() == observer) // compare the reference
                {
                    tmpItem.AddInsterestedEvent(vt);
                    return;
                }
            }

            // Add the observer
            ObserverWrapper newWrapper = new ObserverWrapper(observer, vt);
            observerList.Add(newWrapper);
            m_SubjectObserverListMap.Add(subject, observerList);
        }
        #endregion

        #region send event
        public void SendEvent(SubjectEvent v)
        {
            // Find observer list of this subject.
            if (m_SubjectObserverListMap.ContainsKey(v.GetSubject())) // compare the reference
            {

                // Send the event to all the observers
                FRList<ObserverWrapper> observerList = m_SubjectObserverListMap[v.GetSubject()];
                foreach(ObserverWrapper obsWrapper in observerList)
                {
                    obsWrapper.OnEvent(v);
                }
            }
        }
        #endregion

        // subject (*)--------(*) observer               
        private Dictionary<Object, FRList<ObserverWrapper>> m_SubjectObserverListMap;
    }
}
