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
using FRContainer;

namespace FRPaint.Fragment
{
    public class Node
    {
        public Node(DataFragment fragment)
        {
            m_Fragment = fragment;
            if (m_Fragment != null)
                m_Fragment.AddNode(this);
        }

        #region Fragment
        public DataFragment GetFragment()
        {
            return m_Fragment;
        }

        public void SetFragment(DataFragment fragment)
        {
            m_Fragment = fragment;
        }
        #endregion

        #region Data model delete

        // Normally if all its successors are deleted, it is deletable.
        // Regarding SymbolConstraint, it returns false.
        // Regarding SymbolGraphicState, it is deletable if all its predecessors are deleted.
        public virtual bool IsDeletable(FRList<Node> deletedNodeList)
        {
            if (deletedNodeList == null)
                return false;

            if (GetFragment() == null)
                return true;

            FRList<Node> immediateSuccessors = new FRList<Node>();
            GetFragment().GetImmediateSuccessors(this, immediateSuccessors);
            foreach (Node item in immediateSuccessors)
            {
                if (!deletedNodeList.Contains(item))
                    return false; // Some one isn't deleted, I can't be deleted.
            }

            return true;
        }

        // If I'm deleted, who will be nominated to be deleted.
        // Normally return its predecessors.
        // Regarding SymbolConstraint, it return predecessors and successors
        public virtual void GetDeleteNominates(FRList<Node> deleteNominateList)
        {
            if (GetFragment() != null)
                GetFragment().GetImmediatePredecessors(this, deleteNominateList);
        }

        #endregion

        #region Data
        private DataFragment m_Fragment;
        #endregion
    }
}
