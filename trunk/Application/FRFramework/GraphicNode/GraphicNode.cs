
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;

using FRContainer;
using FRMath;
using FRGraphic;

// Define the concrete symbol node.
// Maintain the relationship of the data and view.

namespace FRPaint
{
    abstract public class GraphicNode : IObserver
    {
        protected GraphicNode(SymbolConstraint instance)
        {
            m_Instance = instance;

            // Attach the observer to sync the display list.
            //m_Instance.Attach(this);
            m_ItemList = new DisplayItemList();
            m_Visible = true;
            m_CanBeSelected = true;

            // Observe the instance change.
            ObserverManager.Instance.AddObserver(instance, this, EventType.eUpdate);
        }

        public void OnEvent(SubjectEvent v)
        {
            if(v.GetEventType() == EventType.eUpdate)
            {
                m_ItemList.RemoveAll();
                m_Instance.GetSymbolState().GenerateGraphics(m_ItemList);
            }
            else
            {
                Debug.Assert(false, "Unkown event.");
            }
        }

        // For showing the graphics
        public void Draw(GraphicContext gpCtx)
        {
            Debug.Assert(gpCtx != null && m_Visible 
                && m_Instance != null);

            if (!Visible) return;

            if (gpCtx != null && m_Visible && m_ItemList != null)
                m_ItemList.Draw(gpCtx, null);
        }

        public DisplayItemList DisplayList
        {
            get {  return m_ItemList;}
            set { m_ItemList = value; }
        }

        // For selecting
        public void Qualify(PtSelectContext selectCtx)
        {
            Debug.Assert(selectCtx != null);
            if (null == selectCtx) return;

            if (!m_CanBeSelected) return;

            SelectionCreator Creator = GetCreator();
            if (null == Creator) return;            

            if (m_ItemList != null)
            {
                m_ItemList.Qualify(selectCtx, null);

                //SelectionCreator Creator = selectCtx.Creator;
                if (selectCtx.IsHitted && selectCtx.CurrentSelectionSet != null)
                {
                    
                    Creator.CreateSelection(selectCtx, selectCtx.GetHittedItem());
                }
            }
        }

        public bool IsValid()
        {
            return m_ItemList != null;
        }

        public SymbolConstraint GetInstance()
        {
            return m_Instance;
        }

        public bool Visible
        {
            get { return m_Visible; }
            set { m_Visible = value; }
        }

        public bool CanBeSelected
        {
            get { return m_CanBeSelected; }
            set { m_CanBeSelected = value; }
        }

        protected virtual SelectionCreator GetCreator(){ return null;}

        protected DisplayItemList m_ItemList;
        protected SymbolConstraint m_Instance;

        // Whether show the node in view
        private bool m_Visible;

        // Whether this node can be selected.
        private bool m_CanBeSelected;
    }
}
