//////////////////////////////////////////////////////////////////////////
//
// The constraints has inputs and outputs.
// It's responsibility is evaluate the outputs by using of the inputs.
// The inputs and outputs can be any kinds of node, namely constraint and state.
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
using System.Diagnostics;

namespace FRPaint.Fragment
{
    public class Constraint : Node
    {
        public Constraint(DataFragment fragment)
            : base(fragment)
        {

        }

        public virtual void Compute()
        {

        }

        #region Compute pipeline
        public virtual void GetPredecessor(FRList<Node> nodeList)
        {

        }

        public virtual void GetSuccessor(FRList<Node> nodeList)
        {

        }
        #endregion
    }
}
