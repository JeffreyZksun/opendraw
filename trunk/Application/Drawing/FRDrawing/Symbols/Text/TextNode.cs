
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-22 Create
//
//////////////////////////////////////////////////////////////////////////

using FRMath;
using System.Diagnostics;
using FRGraphic;
using FRText;
using FRPaint.Fragment;

namespace FRPaint
{
    public class TextNode : GraphicNode
    {
        public TextNode(SymbolConstraint instance)
            : base(instance)
        {
        }

        protected override SelectionCreator GetCreator()
        {
            TextSelectionCreator Creator = new TextSelectionCreator(this);
            return Creator;
        }
    };

    
}
