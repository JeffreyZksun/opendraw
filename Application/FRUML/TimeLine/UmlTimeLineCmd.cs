//////////////////////////////////////////////////////////////////////////
//
//
// Author: Sun Zhongkui
//
// History:
//  2009-6-7 Create
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using FRPaint;

namespace FRUML.TimeLine
{
    class UmlTimeLineCmd : StateCommand
    {
        public UmlTimeLineCmd()
            : base("UmlTimeLineCmd", "&TimeLine")
        {

        }

        // Override StateCommand
        protected override CommandState GetInitialState()
        {
            return new UmlTimeLineCommandState();
        }
    }
}
