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
using System.Diagnostics;

namespace FRPaint.Fragment
{
    public class DataFragment
    {
        public DataFragment()
        {
            m_NodeList = new FRList<Node>();
            m_Graph = new NodeGraph();
            m_bIsGraphDirty = true;
        }

        public void Compute()
        {
            UpdateNodeGraph();

            if (m_Graph.TopologySort())
            {
                FRList<Node> SortedNodeList = m_Graph.GetSortedNodeList();
                foreach (Node item in SortedNodeList)
                {
                    Constraint cst = item as Constraint;
                    if (cst != null)
                        cst.Compute();
                }
            }
        }

        #region Data model change
        public void AddNode(Node node)
        {
            if (!IsExist(node))
            {
                if (node.GetFragment() != this)
                    node.SetFragment(this);

                m_NodeList.Add(node);
                m_bIsGraphDirty = true;
            }
        }

        // bClearRelatedNodes indicates whether remove the related nodes
        // or just remove the input one.
        public void RemoveNode(Node node, bool bClearRelatedNodes)
        {
            if (!IsExist(node))
                return;

            if(!bClearRelatedNodes)
            {
                m_NodeList.Remove(node);               
            }
            else
            {
                FRList<Node> deletedNodeList = new FRList<Node>();
                FRList<Node> dieNominateList = new FRList<Node>();

                deletedNodeList.Add(node);
                dieNominateList.Add(node);

                while (dieNominateList.Count > 0)
                {
                    Node tmpNode = dieNominateList[0];
                    dieNominateList.Remove(tmpNode);

                    if (deletedNodeList.Contains(tmpNode) || tmpNode.IsDeletable(deletedNodeList))
                    {
                        if (!deletedNodeList.Contains(tmpNode))
                            deletedNodeList.Add(tmpNode);

                        FRList<Node> nodeNominates = new FRList<Node>();
                        tmpNode.GetDeleteNominates(nodeNominates);

                        foreach (Node item in nodeNominates)
                        {
                            if (!dieNominateList.Contains(item))
                                dieNominateList.Add(item);
                        }
                    }
                }

                // Delete the nodes in the data model
                foreach (Node delNode in deletedNodeList)
                   m_NodeList.Remove(delNode);
            }
            

            m_bIsGraphDirty = true;
        }

        #endregion

        #region Graph position
        public bool IsFirstPriorToSecond(Node firstNode, Node secondNode)
        {
            if (firstNode != null || secondNode != null)
                return false;

            if (!IsExist(firstNode) || !IsExist(secondNode))
                return false;

            UpdateNodeGraph();

            m_Graph.ValidateAll();
            m_Graph.InvalidatePredecessors(firstNode);
            bool bRet = (m_Graph.IsNodeVertexValid(secondNode) == true);
            return bRet;
        }

        public void GetImmediatePredecessors(Node node, FRList<Node> immediatePres)
        {
            if (immediatePres == null)
                return;

            if (!IsExist(node))
                return;

            UpdateNodeGraph();

            m_Graph.GetImmediatePredecessors(node, immediatePres);

        }

        public void GetImmediateSuccessors(Node node, FRList<Node> immediateSucs)
        {
            if (immediateSucs == null)
                return;

            if (!IsExist(node))
                return;

            UpdateNodeGraph();

            m_Graph.GetImmediateSuccessors(node, immediateSucs);

        }
        #endregion

        private bool IsExist(Node node)
        {
            foreach(Node curNode in m_NodeList)
            {
                if (curNode == node)
                    return true;
            }

            return false;
        }
        
        private void UpdateNodeGraph()
        {
            m_Graph.ValidateAll();

            if (m_bIsGraphDirty)
                m_Graph.BuildGraph(m_NodeList);

            m_bIsGraphDirty = false;
        }

        private FRList<Node> m_NodeList;
        private NodeGraph m_Graph;
        // When add or remove the node, it will cause the the graph out of date, 
        // we need to rebuild it before using.
        private bool m_bIsGraphDirty; 
    }
}
