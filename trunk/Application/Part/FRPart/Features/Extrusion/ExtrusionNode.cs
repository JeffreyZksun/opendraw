//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2008-10-16 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using FRGraphic;
using FRPaint;

namespace FRPart
{
    public class ExtrusionNode : GraphicNode
    {
        public ExtrusionNode(SymbolConstraint instance)
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

