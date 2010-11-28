
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;

using FRContainer;
using FRMath;
using FRGraphic;
using FRPaint.Fragment;

// Save the data of the symbol.
// When its data change, it will notify the 
// observer [Node] to update the display list.

namespace FRPaint
{

    public abstract class SymbolConstraint: Constraint
    {
        public SymbolConstraint(DataFragment fragment, SymbolGraphicState symbolState)
            : base(fragment)
        {
            m_SymbolState = symbolState;
        }

        #region Override for the compute pipeline
        public override void GetPredecessor(FRList<Node> nodeList)
        {            
            base.GetPredecessor(nodeList);
        }

        public override void GetSuccessor(FRList<Node> nodeList)
        {
            nodeList.Add(m_SymbolState);
            base.GetSuccessor(nodeList);
        }
        #endregion

        #region Geometry constraint for snap
        
        // The constraint isn't added to the fragment
        // So the caller doesn't need to remove it from the fragment
        // Suppose it will be collected by the GM
        public virtual SymbolGeometryConstraint GetGeometryConstraint(GePoint point, double tolerance)
        {
            return null;
        }

        // Give a point on the instance, we should provide a index.
        // The meaning of the index is explain by the concrete instance.
        // Give an index, the instance should return a unique point.
        public virtual GePoint GetPointAt(int index)
        {
            return null;
        }

        public virtual int GetPointIndex(GePoint point, double tolerance)
        {
            return - 1;
        }
        #endregion

        public SymbolGraphicState GetSymbolState()
        {
            return m_SymbolState;
        }

        #region Data model delete

        public override bool IsDeletable(FRList<Node> deletedNodeList)
        {
            return false; // I can't be deleted by dependency.
        }

        public override void GetDeleteNominates(FRList<Node> deleteNominateList)
        {
            if (GetFragment() == null)
                return;

            GetFragment().GetImmediatePredecessors(this, deleteNominateList);
            GetFragment().GetImmediateSuccessors(this, deleteNominateList);
        }

        #endregion

        protected SymbolGraphicState m_SymbolState; // output
    }
}
