//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2009-6-11 Create
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using FRMath;
using FRPaint.Fragment;
using FRContainer;
using System.Diagnostics;

namespace FRPaint
{
    class CircleCenterMoveRequest : ChangeRequest
    {
        public CircleCenterMoveRequest(CircleInstance draggedCircle,
            GePoint newPosition,
            SymbolConstraint symbolToAttach,
            int pointIndex)
        {
            m_DraggedCircle = draggedCircle;
            m_NewPosition = newPosition;
            m_SymbolToAttach = symbolToAttach;
            m_PointIndex = pointIndex;
        }

        #region Overrride ChangeRequest
        protected override bool OnExecute()
        {
            if (m_DraggedCircle == null)
                return false;
            if (m_DraggedCircle.GetFragment() == null)
                return false;

            PointState oldPoint = m_DraggedCircle.GetCenterPoint();
            oldPoint.Point = m_NewPosition;

            // Get the predecessors of the old point state.
            FRList<Node> predecessors = new FRList<Node>();
            m_DraggedCircle.GetFragment().GetImmediatePredecessors(
            oldPoint, predecessors);
            
            // It is attached to other symbol if the predecessors aren't empty.
            bool IsAttachedToOtherSymbol = (predecessors.Count != 0);
            // Delete the old constraint from fragment.
            if (IsAttachedToOtherSymbol) // The dragged symbol is already attached to a symbol.
            {
                Debug.Assert(predecessors.Count == 1);
                foreach(Node item in predecessors)
                {
                    item.GetFragment().RemoveNode(item, true);
                }            
            } 

            // Add new constraint
            if (m_SymbolToAttach != null)
            {
                new SymbolPointConstraint(m_DraggedCircle.GetFragment(),
                    m_SymbolToAttach, m_PointIndex, oldPoint);
            }

            m_DraggedCircle.GetFragment().Compute();          

            return true;
        }

        #endregion

        #region Data
        private CircleInstance m_DraggedCircle;
        private GePoint m_NewPosition;
        private SymbolConstraint m_SymbolToAttach; // it is null if there is no
        private int m_PointIndex;
        #endregion
    }
}
