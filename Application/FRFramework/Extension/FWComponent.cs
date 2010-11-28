
//////////////////////////////////////////////////////////////////////////
//
// All the entry class of the component should derive from this class.
// There should be only one entry class in an assembly.
//
// Author: Sun Zhongkui
//
// History:
//  2008-8-15 Create
//
//////////////////////////////////////////////////////////////////////////

namespace FRFramework.Extension
{
    public abstract class FWComponent
    {
        public abstract bool OnInitialize();
    }
}
