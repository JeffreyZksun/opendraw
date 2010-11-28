//////////////////////////////////////////////////////////////////////////
//
//
// Classes - PolyLineSelection, PolyLineSelectionCreator
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-27 Create
//
//////////////////////////////////////////////////////////////////////////
using FRGraphic;

namespace FRPaint
{
    public class PolyLineSelection : Selection
    {
        public PolyLineSelection(GraphicNode node)
            : base(node)
        {

        }
    }

    public class PolyLineSelectionCreator: SelectionCreator
    {
        public PolyLineSelectionCreator(GraphicNode Node)
            :base (Node)
        {

        }
        protected override Selection GetSelection()
        {
            return new PolyLineSelection(m_Node);
        }

        // Support the operation for selection
        protected override Manipulator OnItemSelection(DisplayItemLine SelectedItem)
        {
            try
            {
                PolyLineNode Node = (PolyLineNode)m_Node;
                return new MoveLineManipulator(Node);
            }
            catch
            {
                return null;
            }
        }

        protected override Manipulator OnItemSelection(DisplayItemPoint SelectedItem)
        {
            try
            {
                PolyLineNode Node = (PolyLineNode)m_Node;
                
                PolyLineInstance instance =  (PolyLineInstance)m_Node.GetInstance();
                int index = instance.GetPointIndex(SelectedItem.Point, m_SelectTolerance);

                return new MovePointManipulator(Node, index);
            }
            catch
            {
                return null;
            }
        }
    }
}
