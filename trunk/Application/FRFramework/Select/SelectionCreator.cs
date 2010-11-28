

//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-27 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using FRGraphic;

// Create the selection and corresponding manipulator
namespace FRPaint
{
    public abstract class SelectionCreator
    {
        protected SelectionCreator(GraphicNode Node)
        {
            m_Node = Node;
            m_SelectTolerance = -1;
        }
        public void CreateSelection(PtSelectContext SltCtx, DisplayItem SelectedItem)
        {
            Debug.Assert(SltCtx != null && SelectedItem != null);
            if (SltCtx.CurrentSelectionSet != null && SelectedItem != null)
            {
                m_SelectTolerance = SltCtx.SelectTolerance;
                Selection Slt = GetSelection();
                Slt.SetManipulator(GetManipulator(SelectedItem));
                SltCtx.CurrentSelectionSet.AddSelection(Slt);
            }
        }

        protected virtual Manipulator OnItemSelection(DisplayItemCircle SelectedItem) { return null; }
        protected virtual Manipulator OnItemSelection(DisplayItemLine SelectedItem) { return null; }
        protected virtual Manipulator OnItemSelection(DisplayItemPoint SelectedItem) { return null; }
        protected virtual Manipulator OnItemSelection(DisplayItemText SelectedItem) { return null; }

        protected abstract Selection GetSelection();
        protected Manipulator GetManipulator(DisplayItem SelectedItem) 
        {
            Debug.Assert(SelectedItem != null);
            if (null == SelectedItem) return null;


            if (SelectedItem is DisplayItemCircle)
            {
                return OnItemSelection((DisplayItemCircle)SelectedItem);
            }
            else if (SelectedItem is DisplayItemLine)
            {
                return OnItemSelection((DisplayItemLine)SelectedItem);
            }
            else if (SelectedItem is DisplayItemPoint)
            {
                return OnItemSelection((DisplayItemPoint)SelectedItem);
            }
            else if (SelectedItem is DisplayItemText)
            {
                return OnItemSelection((DisplayItemText)SelectedItem);
            }

           return null;   
        }

        protected GraphicNode m_Node;
        protected double m_SelectTolerance;
    }
}
