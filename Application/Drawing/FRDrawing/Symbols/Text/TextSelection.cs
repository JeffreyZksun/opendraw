
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-22 Create
//
//////////////////////////////////////////////////////////////////////////

using FRGraphic;

namespace FRPaint
{

    public class TextSelection : Selection
    {
        public TextSelection(GraphicNode node)
            : base(node)
        {

        }
    }

    public class TextSelectionCreator : SelectionCreator
    {
        public TextSelectionCreator(GraphicNode Node)
            : base(Node)
        {

        }
        protected override Selection GetSelection()
        {
            return new TextSelection(m_Node);
        }

        protected override Manipulator OnItemSelection(DisplayItemText SelectedItem) 
        {
            try
            {
                return new TextManipulator((TextInstance)m_Node.GetInstance());
            }
            catch
            {
                return null;
            }     
        }

    }
}
