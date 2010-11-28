
//////////////////////////////////////////////////////////////////////////
//
//
// Classes - CircleSelection, CircleSelectionCreator
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
    public class CircleSelection : Selection
    {
        public CircleSelection(GraphicNode node)
            : base(node)
        {

        }
    }

    public class CircleSelectionCreator : SelectionCreator
    {
        public CircleSelectionCreator(GraphicNode Node)
            :base (Node)
        {

        }
        protected override Selection GetSelection()
        {
            CircleSelection Slt = new CircleSelection(m_Node);
            return Slt;
        }

        // Support the operation for selection
        protected override Manipulator OnItemSelection(DisplayItemCircle SelectedItem)
        {
            try
            {
                CircleNode Node = (CircleNode)m_Node;
                return new CircleChangeManipulator(Node);
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
                CircleNode Node = (CircleNode)m_Node;
                return new CircleMoveManipulator(Node);
            }
            catch
            {
                return null;
            }
        }
    };
}
