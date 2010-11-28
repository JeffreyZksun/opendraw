
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using FRMath;
using System.Diagnostics;

// These classes process all the mouse and keyboard events on the view
// Such as mouse move, click, double click, drag and so on

namespace FRPaint
{
    // Define the context of the event
    public class EventContext
    {
        public enum EventType
        {
            eUnknown = 0,
            eMouseMove,
            eLeftButtonUp,
            eLeftButtonDown,
            eLeftButtionClick,
            eLeftDoubleClick,
            eRightButtonClick,            
            eRightDoubleClick,
            eMiddleButtonDown,
            eMiddleButtonUp,
            eMouseWheel
            
        }

        public enum ButtonType
        {
            eNone,
            eLeft,
            eMiddle,
            eRight
        }

        public EventContext()
        {
            m_EventType = EventType.eUnknown;
            m_ButtonType = ButtonType.eNone;
            m_Delta = 0;
        }

        public EventType Type
        {
            get { return m_EventType; }
            set { m_EventType = value; }
        }

        public ButtonType GetButtonType()
        {
            return m_ButtonType;
        }

        public void SetButtonType(ButtonType type)
        {
            m_ButtonType = type;
        }

        public int Delta
        {
            get { return m_Delta; }
            set { m_Delta = value; }
        }

        public GePoint MouseWorldPoint
        {
            get { return m_MouseWorldPoint; }
        }

        public GePoint MouseDevicePoint
        {
            get { return m_MouseDevicePoint; }
        }

        public void SetMouseDevicePoint(GePoint devicePoint)
        {
            Debug.Assert(devicePoint != null);
            if (devicePoint != null)
            {
                m_MouseDevicePoint = devicePoint;
                m_MouseWorldPoint = PtApp.ActiveView.DeviceToWorld(devicePoint);
            }
        }

        private GePoint m_MouseWorldPoint;
        private GePoint m_MouseDevicePoint;

        private EventType m_EventType;
        private ButtonType m_ButtonType;
        private int m_Delta;
    };

    public abstract class EventTarget
    {

        // It indicate the result of the target
        public enum EventResult
        {
            eDone = 0,
            eContinue
        }

        public EventTarget()
        {
            m_ParentState = null;
        }

        public EventTarget(LeafState parentState)
        {
            m_ParentState = parentState;
        }

        #region Status information and cursor shape
        public void SetStatus(string strStatus)
        {
            PtApp.Instance.SetStatus(strStatus);
        }
        public void SetCursorShape(string CursorResource)
        {
            PtApp.ActiveView.SetCursorShape(CursorResource);
        }
        #endregion
        
        #region Mouse event
        public EventResult OnEvent(EventContext EventCtx)
        {
            if (null == EventCtx) return EventResult.eContinue;
            EventResult EventRet = EventResult.eContinue;

            switch (EventCtx.Type)
            {
                case EventContext.EventType.eMouseMove:
                    EventRet = OnMouseMove(EventCtx);
                    break;
                case EventContext.EventType.eLeftButtionClick:
                    EventRet = OnLeftButtionClick(EventCtx);
                    break;
                case EventContext.EventType.eLeftButtonDown:
                    EventRet = OnLeftButtionDown(EventCtx);
                    break;
                case EventContext.EventType.eLeftButtonUp:
                    EventRet = OnLeftButtionUp(EventCtx);
                    break;
                case EventContext.EventType.eLeftDoubleClick:
                    EventRet = OnLeftDoubleClick(EventCtx);
                    break;
                case EventContext.EventType.eMiddleButtonDown:
                    EventRet = OnMiddleButtionDown(EventCtx);
                    break;
                case EventContext.EventType.eMiddleButtonUp:
                    EventRet = OnMiddleButtionUp(EventCtx);
                    break;
                case EventContext.EventType.eMouseWheel:
                    EventRet = OnMouseWheel(EventCtx);
                    break;

            }

            return EventRet;
        }

        protected abstract EventResult OnMouseMove(EventContext Context);
        protected virtual EventResult OnLeftButtionClick(EventContext Context) { return EventResult.eContinue; }
        protected virtual EventResult OnLeftButtionDown(EventContext Context) { return EventResult.eContinue; }
        protected virtual EventResult OnLeftButtionUp(EventContext Context) { return EventResult.eContinue; }
        protected virtual EventResult OnLeftDoubleClick(EventContext Context) { return EventResult.eContinue; }
        protected virtual EventResult OnMiddleButtionDown(EventContext Context) { return EventResult.eContinue; }
        protected virtual EventResult OnMiddleButtionUp(EventContext Context) { return EventResult.eContinue; }
        protected virtual EventResult OnMouseWheel(EventContext Context) { return EventResult.eContinue; }
       
        
        #endregion

        public void SetParentState(LeafState state)
        {
            m_ParentState = state;
        }

        protected LeafState m_ParentState;
    } 
    
}
