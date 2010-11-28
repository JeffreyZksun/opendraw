
//////////////////////////////////////////////////////////////////////////
//
// The Entry of the Drawing component.
//
// Author: Sun Zhongkui
//
// History:
//  2008-8-15 Create
//
//////////////////////////////////////////////////////////////////////////

using FRFramework.Extension;
using System.Diagnostics;
using FRPaint.Fragment;

// Indicate this assembly in an extension of the framework.
[assembly: FRExtension]

namespace FRPaint
{
    [FRComponentEntry("Drawing", Company = "FR")]
    public class DRComponent: FWComponent
    {
        public override bool OnInitialize()
        {
            CommandGroup standard = CommandManger.Get().GetCommandGroup(
                "FRxStandard");
            Debug.Assert(standard != null, "No required group! Maybe you need to create it yourself");

            if(standard != null)
            {
                standard.AddCommand(new DrLineCmd());
                standard.AddCommand(new DrCircleCmd());
                standard.AddCommand(new DrTextCmd());
                standard.AddCommand(new DrLeaderTextCmd());
            }

            return true;
        }
    }
}
