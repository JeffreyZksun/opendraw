
//////////////////////////////////////////////////////////////////////////
//
// SymbolGeometryConstraint can evaluate the corresponding geometry from the input symbol constraint.
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-28 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using FRMath;
using FRContainer;
using FRPaint.Fragment;

namespace FRPaint
{
    public class SymbolGeometryConstraint : Constraint
    {
        public SymbolGeometryConstraint(DataFragment fragment, 
            SymbolConstraint instance // input
            )
            : base(fragment)
        {
            Debug.Assert(instance != null);

            m_Instance = instance;
        } 

        public SymbolConstraint GetSymbolConstraint()
        {
            return m_Instance;
        }

        #region Override for the compute pipeline
        public override void Compute()
        {
            // Do nothing
        }

        public override void GetPredecessor(FRList<Node> nodeList)
        {
            if (m_Instance != null)
                nodeList.Add(m_Instance);
            base.GetPredecessor(nodeList);
        }

        public override void GetSuccessor(FRList<Node> nodeList)
        {
            base.GetSuccessor(nodeList);
        }
        #endregion

        protected SymbolConstraint m_Instance; // input
    }

    public class SymbolPointConstraint : SymbolGeometryConstraint
    {
        public SymbolPointConstraint(DataFragment fragment, 
            SymbolConstraint instance, // input
            int index)
            : base(fragment, instance)
        {
            m_PointIndex = index;
            m_PointState = new PointState(fragment);
        }

        public SymbolPointConstraint(DataFragment fragment,
            SymbolConstraint instance, // input
            int index,
            PointState pointState // output
            )
            : base(fragment, instance)
        {
            m_PointIndex = index;
            m_PointState = pointState;
        }

        public PointState GetPointState()
        {
            return m_PointState;
        }

        #region Override for the compute pipeline
        public override void Compute()
        {
            if (m_PointState != null && m_Instance != null)
            {
                GePoint point = m_Instance.GetPointAt(m_PointIndex);
                m_PointState.Point = point;
            }
        }

        public override void GetPredecessor(FRList<Node> nodeList)
        {
            base.GetPredecessor(nodeList);
        }

        public override void GetSuccessor(FRList<Node> nodeList)
        {
            if(m_PointState != null)
                nodeList.Add(m_PointState);
            base.GetSuccessor(nodeList);
        }
        #endregion

        private int m_PointIndex; // input
        private PointState m_PointState; // output
    };


    public class SymbolLineConstraint : SymbolGeometryConstraint
    {

        public SymbolLineConstraint(DataFragment fragment, SymbolConstraint instance, int StartIndex, int EndIndex)
            : base(fragment, instance)
        {
            m_StartPointIndex = StartIndex;
            m_EndPointIndex = EndIndex;
            m_LineState = new LineState(fragment);
        }

        #region Override for the compute pipeline
        public override void Compute()
        {
            if (m_LineState != null)
            {
                Debug.Assert(false, "NO IMP");
            }
        }

        public override void GetPredecessor(FRList<Node> nodeList)
        {
            base.GetPredecessor(nodeList);
        }

        public override void GetSuccessor(FRList<Node> nodeList)
        {
            if (m_LineState != null)
                nodeList.Add(m_LineState);
            base.GetSuccessor(nodeList);
        }
        #endregion

        //private GeLine m_line;
        private int m_StartPointIndex; // input
        private int m_EndPointIndex; // input
        private LineState m_LineState; // output
    };
}
