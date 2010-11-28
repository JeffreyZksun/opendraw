
//////////////////////////////////////////////////////////////////////////
//
// 
//
// Author: Sun Zhongkui
//
// History:
//  2008-8-17 Create
//
//////////////////////////////////////////////////////////////////////////
using FRPaint;
using FRFramework.Extension;
using System.Diagnostics;

// Indicate this assembly in an extension of the framework.
[assembly: FRExtension]

namespace FRPart
{
    [FRComponentEntry("Part", Company = "FR")]
    public class PTComponent : FWComponent
    {
        public override bool OnInitialize()
        {
             CommandGroup standard = CommandManger.Get().GetCommandGroup(
                "FRxStandard");
            Debug.Assert(standard != null, "No required group! Maybe you need to create it yourself");

            if (standard != null)
            {
                standard.AddCommand(new PTExtrudeCmd());
            }
            return true;
        }
    }
}
