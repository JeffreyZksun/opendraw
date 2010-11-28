//////////////////////////////////////////////////////////////////////////
//
// This attribute indicates whether a assembly is a component.
//  The component assembly should include the following code.
//  [assembly: FRExtension]
//
// Author: Sun Zhongkui
//
// History:
//  2008-8-21 Create
//
//////////////////////////////////////////////////////////////////////////

using System;

namespace FRFramework.Extension
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class FRExtensionAttribute : Attribute
    {
    }
}
