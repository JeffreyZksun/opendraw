
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-27 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;

using FRMath;
using FRContainer;
using FRGraphic;

namespace FRPaint
{
    public class SelectEventTarget : EventTarget
    {
        public SelectEventTarget()
        {
            m_bIsDragging = false;
            m_IsMiddleButtonDown = false;
        }

        #region Event - override
        protected override EventResult OnMouseMove(EventContext Context)
        {
            // Drag and translation the view.
            if(true == m_IsMiddleButtonDown)
            {
                PtApp.GetActiveView().GetTranslationManipulator().Drag(Context);
                SetCursorShape(PtApp.GetActiveView().GetTranslationManipulator().GetCursorShape());
                return EventResult.eContinue;
            }

            if (true == m_bIsDragging)
            {
                SelectionSet SelectedSet = PtApp.ActiveView.SelectionMgr.GetSelectedSelectionSet();

                // We just support drag a selection
                FRList<Selection> Selections = SelectedSet.Selections;
                if (Selections.Count == 1)
                {
                    if (Selections[0].GetManipulator() != null)
                    {
                        Selections[0].GetManipulator().Drag(Context);
                        SetCursorShape(Selections[0].GetManipulator().GetCursorShape());
                    }
                }
            }
            else
            {
                // We do preselect when the mouse move but not drag.
                PtApp.GetActiveView().UpdateView();

                GePoint mousPoint = Context.MouseWorldPoint;
                PtSelectContext sltCtx = new PtSelectContext();
                sltCtx.WorldPoint = mousPoint;

                // Set selection set.
                SelectionSet PreSet = PtApp.ActiveView.SelectionMgr.GetPreviewSelectionSet();
                PreSet.Clear();
                sltCtx.CurrentSelectionSet = PreSet;

                PreSelect(sltCtx);

                //bool bAfterSelect = PreSet.Empty();
               
                //// We just update the preview when need.
                //if (!bAfterSelect)
                //    PtApp.ActiveView.UpdateView();

                // We just support drag a selection
                FRList<Selection> Selections = PreSet.Selections;
                if (Selections.Count == 1)
                {
                    if (Selections[0].GetManipulator() != null)
                    {
                        SetCursorShape(Selections[0].GetManipulator().GetCursorShape());
                    }
                    else
                    {
                        SetCursorShape("SelectArrow.cur");
                    }
                }
                else
                {
                    SetCursorShape("SelectArrow.cur");
                }
            }   

            return EventResult.eContinue;
        }

        protected override EventResult OnLeftButtionDown(EventContext Context) 
        {
            m_bIsDragging = true;
            // manipulator start drag

            // Move the selections from preview set to selected set.
            SelectionSet SelectedSet = PtApp.ActiveView.SelectionMgr.GetSelectedSelectionSet();
            SelectedSet.Clear();
            SelectedSet.Union(PtApp.ActiveView.SelectionMgr.GetPreviewSelectionSet());
            PtApp.ActiveView.SelectionMgr.GetPreviewSelectionSet().Clear();

            // We just support drag a selection
            FRList<Selection> Selections = SelectedSet.Selections;
            if(Selections.Count == 1)
            {
                if (Selections[0].GetManipulator() != null)
                {
                    SetCursorShape(Selections[0].GetManipulator().GetCursorShape());
                    Selections[0].GetManipulator().StartDrag(Context);
                }
            }

            return EventResult.eContinue; 
        }
        protected override EventResult OnLeftButtionUp(EventContext Context) 
        {
            m_bIsDragging = false;

            // End Drag
            SelectionSet SelectedSet = PtApp.ActiveView.SelectionMgr.GetSelectedSelectionSet();

            // We just support drag a selection
            FRList<Selection> Selections = SelectedSet.Selections;
            if (Selections.Count == 1)
            {
                if (Selections[0].GetManipulator() != null)
                {
                    Selections[0].GetManipulator().EndDrag(Context);
                }
            }

            SetCursorShape("SelectArrow.cur");

            return EventResult.eContinue; 
        }

        protected override EventResult OnLeftDoubleClick(EventContext Context)
        {
            // We just support drag a selection
            SelectionSet SelectedSet = PtApp.ActiveView.SelectionMgr.GetSelectedSelectionSet();
            FRList<Selection> Selections = SelectedSet.Selections;
            if (Selections.Count > 0 && Selections[0].GetManipulator() != null)
                Selections[0].GetManipulator().DoubClick();

            return EventResult.eContinue;
        }

        protected override EventResult OnMiddleButtionDown(EventContext Context) 
        {
            m_IsMiddleButtonDown = true;
            PtApp.GetActiveView().GetTranslationManipulator().StartDrag(Context);

            return EventResult.eContinue; 
        }
        protected override EventResult OnMiddleButtionUp(EventContext Context) 
        {
            m_IsMiddleButtonDown = false;

            SetCursorShape("SelectArrow.cur");

            return EventResult.eContinue; 
        }

        protected override EventResult OnMouseWheel(EventContext Context) 
        {
            PtApp.GetActiveView().GetTranslationManipulator().MouseWheel(Context);
            
            return EventResult.eContinue; 
        }

       

        #endregion

        public void PreSelect(PtSelectContext sltCtx)
        {
#if DEBUG// Test Performance
            DateTime StartTime = DateTime.Now;
            Console.WriteLine("Start time -" + StartTime.ToString());
#endif// End Test

            PtApp.ActiveView.Select(sltCtx);

#if DEBUG// Test Performance
            DateTime EndTime = DateTime.Now;
            Console.WriteLine("End time -" + EndTime.ToString());
            TimeSpan Span = EndTime - StartTime;
            Console.WriteLine("Use time -" + Span.ToString());
#endif // End Test  

            FRList<Selection> Selections =
                sltCtx.CurrentSelectionSet.Selections;
            if (!Selections.Empty())
            {
                //PtApp.ActiveView.UpdateView();
                
                Debug.WriteLine("Preselect successfully.");
            }
        }



        // The both variables will indicate whether it is dragging
        // LButton down -> m_bIsDragging = true
        // MouseMove && m_bIsDragging == true -> is dragging
        // LButton up -> m_bIsDragging = false
        private bool m_bIsDragging;
        private bool m_IsMiddleButtonDown;
        
    };
}
