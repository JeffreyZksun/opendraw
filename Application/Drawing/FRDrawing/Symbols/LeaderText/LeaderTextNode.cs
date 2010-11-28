
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-12-11 Create
//
//////////////////////////////////////////////////////////////////////////

using FRContainer;
using FRMath;
using FRText;
using System.Diagnostics;
using FRGraphic;
using FRPaint.Fragment;

namespace FRPaint
{
    public class LeaderTextNode : GraphicNode
    {
        public LeaderTextNode(SymbolConstraint instance)
            : base(instance)
        {
        }

        protected override SelectionCreator GetCreator()
        {
            //TextSelectionCreator Creator = new TextSelectionCreator(this);
            //return Creator;
            return null;
        }
    };

    
}
