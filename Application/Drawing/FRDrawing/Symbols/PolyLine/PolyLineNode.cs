
//////////////////////////////////////////////////////////////////////////
//
//
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-27 Create
//
//////////////////////////////////////////////////////////////////////////

using FRMath;
using FRContainer;
using System.Diagnostics;
using FRGraphic;
using FRPaint.Fragment;

namespace FRPaint
{
    public class PolyLineNode : GraphicNode
    {
        public PolyLineNode(SymbolConstraint instance)
            : base(instance)
        {
        }

        protected override SelectionCreator GetCreator()
        {
            PolyLineSelectionCreator Creator = new PolyLineSelectionCreator(this);
            return Creator;
        }
    };    
}
