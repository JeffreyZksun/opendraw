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
using FRModeling;
using FRMath;
using FRPaint.Fragment;

namespace FRPart
{
    public class ExtrusionInstance : SymbolConstraint
    {
        public ExtrusionInstance(DataFragment fragment)
            : base(fragment, new ExtrusionGraphicState(fragment))
        {
        }        
    };
}

