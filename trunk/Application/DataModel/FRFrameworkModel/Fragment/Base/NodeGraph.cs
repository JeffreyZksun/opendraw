//////////////////////////////////////////////////////////////////////////
//
//
// Author: Sun Zhongkui
//
// History:
//  2009-6-8 Create
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using FRContainer;
using System.Diagnostics;

namespace FRPaint.Fragment
{
    class NodeGraph
    {
        public NodeGraph()
        {
            m_NodeGraphVertexMap = new Dictionary<Node, GraphVertex>();
            m_TopologySortedNodeList = new FRList<Node>();
            m_NodeGraphEdgeList = new FRList<GraphEdge>();
        }

        public bool TopologySort()
        {
            m_TopologySortedNodeList.Clear();

            FRList<GraphVertex> ZeroPredecessorVertexList = new FRList<GraphVertex>();
            while (true)
            {
                ZeroPredecessorVertexList.Clear();
                foreach (GraphVertex curVertex in m_NodeGraphVertexMap.Values)
                {
                    if (curVertex.Valid() && curVertex.GetpredecessorCount() == 0)
                        ZeroPredecessorVertexList.Add(curVertex);
                }

                // There is no zero degree vertex. 
                // 1. Finish all the vertex. 
                // 2. The left vertex are circles.
                if (ZeroPredecessorVertexList.Count == 0)
                    break;

                foreach (GraphVertex vertex in ZeroPredecessorVertexList)
                {
                    m_TopologySortedNodeList.Add(vertex.GetNode());

                    // In valid the vertex and edges.
                    vertex.SetValid(false);
                    foreach (GraphEdge edge in vertex.GetSuccessorEdges())
                    {
                        edge.SetValid(false);
                    }
                }
            }

            // If the sorted vertexes count isn't equal to the graph vertex count, that means there exist circle.
            Debug.Assert(m_TopologySortedNodeList.Count == m_NodeGraphVertexMap.Count);
            if (m_TopologySortedNodeList.Count != m_NodeGraphVertexMap.Count)
                return false; 

            return true;
        }

        public void BuildGraph(FRList<Node> nodeList)
        {
            m_NodeGraphVertexMap.Clear();
            m_NodeGraphEdgeList.Clear();

            foreach (Node node in nodeList)
            {
                GraphVertex thisVertex = GetVertex(node);
                Constraint cons = node as Constraint;

                if (cons != null)
                {
                    FRList<Node> nodes = new FRList<Node>();
                    cons.GetPredecessor(nodes);
                    foreach (Node preNode in nodes)
                    {
                        GraphVertex preVertex = GetVertex(preNode);
                        AddEdge(preVertex, thisVertex);
                    }

                    nodes.Clear();
                    cons.GetSuccessor(nodes);
                    foreach (Node sucNode in nodes)
                    {
                        GraphVertex sucVertex = GetVertex(sucNode);
                        AddEdge(thisVertex, sucVertex);
                    }
                }
            }
        }

        public FRList<Node> GetSortedNodeList()
        {
            return m_TopologySortedNodeList;
        }

        public void ValidateAll()
        {
            foreach(GraphVertex vertex in m_NodeGraphVertexMap.Values)
            {
                vertex.SetValid(true); 
            }
            foreach(GraphEdge edge in m_NodeGraphEdgeList)
            {
                edge.SetValid(true);
            }
        }

        public bool IsNodeVertexValid(Node node)
        {
            GraphVertex vertex = null;
            if (m_NodeGraphVertexMap.ContainsKey(node))
                vertex = m_NodeGraphVertexMap[node];

            if (vertex == null)
                return false;

            return vertex.Valid();
        }
        public void InvalidatePredecessors(Node node)
        {
            GraphVertex thisVertex = null;
            if(m_NodeGraphVertexMap.ContainsKey(node))
                thisVertex = m_NodeGraphVertexMap[node];
            if (thisVertex == null)
                return;

            FRList<GraphVertex> ZeroPredecessorVertexList = new FRList<GraphVertex>();

            while (true)
            {
                ZeroPredecessorVertexList.Clear();
                foreach (GraphVertex curVertex in m_NodeGraphVertexMap.Values)
                {
                    // Don't invalidate the input node vertex
                    if (curVertex.Valid() && curVertex != thisVertex && 
                        curVertex.GetpredecessorCount() == 0)
                        ZeroPredecessorVertexList.Add(curVertex);
                }

                // There is no zero degree vertex.
                if (ZeroPredecessorVertexList.Count == 0)
                    break;

                foreach (GraphVertex vertex in ZeroPredecessorVertexList)
                {
                    m_TopologySortedNodeList.Add(vertex.GetNode());

                    // In valid the vertex and edges.
                    vertex.SetValid(false);
                    foreach (GraphEdge edge in vertex.GetSuccessorEdges())
                    {
                        edge.SetValid(false);
                    }
                }
            }
        }

        public void GetImmediatePredecessors(Node node, FRList<Node> immediatePres)
        {
            GraphVertex vertex = null;
            if (!m_NodeGraphVertexMap.ContainsKey(node))
                return;

            vertex = m_NodeGraphVertexMap[node];

            foreach(GraphEdge edge in vertex.GetPredecessorEdges())
            {
                Node preNode = edge.GetStartVertex().GetNode();
                Debug.Assert(preNode != node);

                if (!immediatePres.Contains(preNode))
                    immediatePres.Add(preNode);
            }
        }

        public void GetImmediateSuccessors(Node node, FRList<Node> immediateSucs)
        {
            GraphVertex vertex = null;
            if (!m_NodeGraphVertexMap.ContainsKey(node))
                return;

            vertex = m_NodeGraphVertexMap[node];

            foreach (GraphEdge edge in vertex.GetSuccessorEdges())
            {
                Node sucNode = edge.GetEndVertex().GetNode();
                Debug.Assert(sucNode != node);

                if (!immediateSucs.Contains(sucNode))
                    immediateSucs.Add(sucNode);
            }
        }

        #region private
        // Return the existing vertex for the node.
        // If no, add a new vertex to graph. 
        private GraphVertex GetVertex(Node node)
        {
            if(m_NodeGraphVertexMap.ContainsKey(node))
            {
                GraphVertex vertex = m_NodeGraphVertexMap[node];
                return vertex; // Return the existing one
            }

            // Create a new vertex for this node.
            GraphVertex newVertex = new GraphVertex(node);

            // Add it to the cache
            m_NodeGraphVertexMap.Add(node, newVertex);

            return newVertex;
        }

        // Return the existing edge for the input vertexes.
        // If no add a new edge to the graph. 
        private void AddEdge(GraphVertex startVertex, GraphVertex endVertex)
        {
            // Quickly check
            if (startVertex.GetSuccessorEdges().Count == 0 || endVertex.GetPredecessorEdges().Count == 0)
            {
                GraphEdge edge = new GraphEdge(startVertex, endVertex);
                m_NodeGraphEdgeList.Add(edge);
                return;
            }

            // Just need to check one vertex
            FRList<GraphEdge> sucessorGraphEdges = startVertex.GetSuccessorEdges();
            foreach (GraphEdge edge in sucessorGraphEdges)
            {
                if (edge.GetEndVertex() == endVertex)
                    return; // Do nothing if it exists.
            }

            // Add a new edge
            GraphEdge edge2 = new GraphEdge(startVertex, endVertex);
            m_NodeGraphEdgeList.Add(edge2);
        }
        #endregion

        private Dictionary<Node, GraphVertex> m_NodeGraphVertexMap;
        private FRList<Node> m_TopologySortedNodeList; // Cache data
        private FRList<GraphEdge> m_NodeGraphEdgeList;
    }

    class GraphItemBase
    {
        public GraphItemBase()
        {
            m_bIsValid = true;
        }
        public bool Valid()
        {
            return m_bIsValid;
        }
        public void SetValid(bool bValid)
        {
            m_bIsValid = bValid;
        }
        private bool m_bIsValid; // When this item is processed, set it invalid
    }

    class GraphVertex : GraphItemBase
    {
        public GraphVertex(Node node)
        {
            m_Node = node;
            m_PredecessorGraphEdges = new FRList<GraphEdge>();
            m_SucessorGraphEdges = new FRList<GraphEdge>();
        }

        public int GetpredecessorCount()// Return the count of the valid edges.
        {
            int count = 0;
            foreach (GraphEdge edge in m_PredecessorGraphEdges)
            {
                if (edge.Valid())
                    ++count;
            }
            return count;
        }

        public void AddPredecessor(GraphEdge edge)
        {
            // We don't check whether the edge is valid or 
            // Whether it already exits.
            // The caller should make sure the input edge
            // is valid and not exists.
            m_PredecessorGraphEdges.Add(edge);
        }
        public void AddSuccessor(GraphEdge edge)
        {
            // We don't check whether the edge is valid or 
            // Whether it already exits.
            // The caller should make sure the input edge
            // is valid and not exists.
            m_SucessorGraphEdges.Add(edge);
        }

        public FRList<GraphEdge> GetPredecessorEdges()
        {
            return m_PredecessorGraphEdges;
        }

        public FRList<GraphEdge> GetSuccessorEdges()
        {
            return m_SucessorGraphEdges;
        }

        public Node GetNode()
        {
            return m_Node;
        }

        private FRList<GraphEdge> m_PredecessorGraphEdges;
        private FRList<GraphEdge> m_SucessorGraphEdges;
        private Node m_Node;
    }

    class GraphEdge : GraphItemBase
    {
        public GraphEdge(GraphVertex startVertex, GraphVertex endVertex)
        {
            m_startVertex = startVertex;
            m_endVertex = endVertex;
            m_startVertex.AddSuccessor(this);
            m_endVertex.AddPredecessor(this);
        }

        public GraphVertex GetEndVertex()
        {
            return m_endVertex;
        }

        public GraphVertex GetStartVertex()
        {
            return m_startVertex;
        }

        private GraphVertex m_startVertex;
        private GraphVertex m_endVertex;
    }
}
