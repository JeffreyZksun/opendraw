using System;
using System.Collections.Generic;
using System.Text;

namespace FRPaint
{
    // OPT - Some methods do not need.
    // We should change the command structure.
    class PtxContextCommand : PtCommand
    {
        public PtxContextCommand(String InternalName
            , string DisplayName, bool IsDatabaseCommand)
            : base(InternalName, DisplayName, IsDatabaseCommand)
        {

        }
    }

    class  PtxContextDoneCmd:PtxContextCommand 
    {
        public PtxContextDoneCmd()
            : base("Done", "&Done", false)
        {

        }
        protected override bool OnExcute() 
        {
            PtApp.Instance.CmdResolver.OnDone();
            return false;
        }
    }

    class PtxContextCancelCmd : PtxContextCommand
    {
        public PtxContextCancelCmd()
            : base("Cancel", "&Cancel", false)
        {

        }
        protected override bool OnExcute()
        {
            PtApp.Instance.CmdResolver.OnCancel();
            return false;
        }
    }
}
