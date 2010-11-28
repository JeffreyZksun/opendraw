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
using System.IO;

namespace Script
{
    class ScriptCmd : PtCommand
    {
        public ScriptCmd()
            : base("ScriptCmd", "&Script Editor",false)
        {

        }

        protected override bool OnExcute()
        {

            String codeFile = System.Environment.CurrentDirectory;
            if (!codeFile.EndsWith(@"\"))
                codeFile += @"\";
            codeFile += "CSharpExample.txt";

            ScriptManager mgr = new ScriptManager(null);
            mgr.Edit(codeFile);

            return false;
        }
        
    }
}
