//////////////////////////////////////////////////////////////////////////
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

using FRPaint.Fragment;
using FRGraphic;
using FRContainer;

namespace FRPaint
{
    public class SymbolGraphicState : State
    {
        public SymbolGraphicState(DataFragment fragment)
            : base(fragment)
        {
        }

        public virtual void GenerateGraphics(DisplayItemList DLList)
        {

        }

        #region Data model delete

        // Normally if all its predecessors are deleted, it is deletable.
        public override bool IsDeletable(FRList<Node> deletedNodeList)
        {
            if (deletedNodeList == null)
                return false;

            if (GetFragment() == null)
                return true;

            FRList<Node> immediatePredecessors = new FRList<Node>();
            GetFragment().GetImmediatePredecessors(this, immediatePredecessors);
            foreach (Node item in immediatePredecessors)
            {
                if (!deletedNodeList.Contains(item))
                    return false; // Some one isn't deleted, I can't be deleted.
            }

            return true;
        }

        #endregion
    }
}
