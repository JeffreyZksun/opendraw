
//////////////////////////////////////////////////////////////////////////
//
// The Entry of the UML component.
//
// Author: Sun Zhongkui
//
// History:
//  2009-6-7 Create
//
//////////////////////////////////////////////////////////////////////////

using FRFramework.Extension;
using System.Diagnostics;
using FRUML.TimeLine;

// Indicate this assembly in an extension of the framework.
[assembly: FRExtension]

namespace FRPaint
{
    [FRComponentEntry("UML", Company = "FR")]
    public class UMLComponent : FWComponent
    {
        public override bool OnInitialize()
        {
            CommandGroup standard = CommandManger.Get().GetCommandGroup(
                "FRxStandard");
            Debug.Assert(standard != null, "No required group! Maybe you need to create it yourself");

            if (standard != null)
            {
                standard.AddCommand(new UmlTimeLineCmd());
                //standard.AddCommand(new DrCircleCmd());
                //standard.AddCommand(new DrTextCmd());
                //standard.AddCommand(new DrLeaderTextCmd());
            }

            return true;
        }
    }
}
