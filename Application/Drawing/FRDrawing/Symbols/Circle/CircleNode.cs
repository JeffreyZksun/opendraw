
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-27 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;

using FRMath;
using FRContainer;
using FRGraphic;
using FRPaint.Fragment;

namespace FRPaint
{
    public class CircleNode : GraphicNode
    {
        public CircleNode(SymbolConstraint instance)
            : base(instance)
        {
        }

        protected override SelectionCreator GetCreator()
        {
            CircleSelectionCreator Creator = new CircleSelectionCreator(this);
            return Creator;
        }
    };    
}
