
//////////////////////////////////////////////////////////////////////////
//
// The Entry of the Script component.
//
// Author: Sun Zhongkui
//
// History:
//  2009-12-3 Create
//
//////////////////////////////////////////////////////////////////////////

using FRFramework.Extension;
using System.Diagnostics;
using Script;


// Indicate this assembly in an extension of the framework.
[assembly: FRExtension]

namespace FRPaint
{
    [FRComponentEntry("Script", Company = "FR")]
    public class ScriptComponent : FWComponent
    {
        public override bool OnInitialize()
        {
            CommandGroup standard = CommandManger.Get().GetCommandGroup(
                "FRxStandard");
            Debug.Assert(standard != null, "No required group! Maybe you need to create it yourself");

            if (standard != null)
            {
                standard.AddCommand(new ScriptCmd());
                //standard.AddCommand(new DrCircleCmd());
                //standard.AddCommand(new DrTextCmd());
                //standard.AddCommand(new DrLeaderTextCmd());
            }

            return true;
        }
    }
}
