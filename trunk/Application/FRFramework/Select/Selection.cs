using System;
using System.Diagnostics;

using FRContainer;

//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

// FRD - Maintain the selection information. It should save enough information
//  for all the operation

// SelectionManager [1]---[*] SelectionSet [1]---[*]Selection

using FRGraphic;

namespace FRPaint
{
    public class SelectionManager
    {
        #region Constructors
        public SelectionManager()
        {
            m_SelectionSetList = new FRList<SelectionSet>();
            m_PreViewSet = null;
            m_SelectedSet = null;
        }
        #endregion

        #region Attributes

        #endregion

        #region Create, Get, Remove selection set

        public SelectionSet CreateSelectionSet(String InternalName)
        {
            // The name must be unique for the selection
            // If exist, return it.
            SelectionSet ExistSet = GetSelectionSet(InternalName);
            if (ExistSet != null) return ExistSet;

            SelectionSet NewSet = new SelectionSet(InternalName);
            m_SelectionSetList.Add(NewSet);

            return NewSet;
        }
        public SelectionSet GetSelectionSet(String InternalName)
        {
            SelectionSet ExistSet = null;

            foreach(SelectionSet item in m_SelectionSetList)
            {
                if (item.InternalName == InternalName)
                    ExistSet = item;
            }

            return ExistSet;
        } 

        public void RemoveSelectionSet(String InternalName)
        {
            SelectionSet ExistSet = GetSelectionSet(InternalName);
            if (ExistSet != null)
                m_SelectionSetList.Remove(ExistSet);
        }

        public SelectionSet GetPreviewSelectionSet()
        {
            if (null == m_PreViewSet)
                m_PreViewSet = new SelectionSet("PreviewSet");
            return m_PreViewSet;
        }
        public SelectionSet GetSelectedSelectionSet()
        {
            if (null == m_SelectedSet)
                m_SelectedSet = new SelectionSet("SelectedSet");
            return m_SelectedSet;
        }

        #endregion


        #region Data
        private FRList<SelectionSet> m_SelectionSetList;
        private SelectionSet m_PreViewSet;
        private SelectionSet m_SelectedSet;

        #endregion
    }

    // We shouldn't new the selection set directly.
    // We should create a selection set by manager.
    // So this selection set can be managed.
    // OPT - We should find a method to aviod this
    public class SelectionSet
    {
        #region Constructors
        public SelectionSet(String InternalName)
        {
            m_InternalName = String.Copy(InternalName);
            m_SelectionList = new FRList<Selection>();
        }
        #endregion

        #region Attributes 
        public String InternalName
        {
            get { return m_InternalName; }
        }

        public FRList<Selection> Selections 
        {
            get { return m_SelectionList; }
        }
        #endregion

        #region Add, Remove, Contains selection
        public void AddSelection(Selection SelecionItem)
        {
            if (!Contions(SelecionItem))
                m_SelectionList.Add(SelecionItem);
        }
        public void RemoveSelection(Selection SelecionItem)
        {
            foreach (Selection item in m_SelectionList)
            {
                if (item.ID == SelecionItem.ID)
                {
                    m_SelectionList.Remove(item);
                    break;
                }
            }            
        }

        public bool Contions(Selection SelecionItem)
        {
            foreach (Selection item in m_SelectionList)
            {
                if (item.ID == SelecionItem.ID)
                    return true;
            }
            return false;
        }
        public void Clear()
        {
            m_SelectionList.Clear();
        }

        // N/A meed test
        public void Union(SelectionSet other)
        {
            Debug.Assert(other != null);
            if (null == other) return;

            for(int i = 0; i < other.Selections.Count; i++)
            {
                AddSelection(other.Selections[i]);
            }
        }

        // N/A need test
        public void Subtract(SelectionSet other)
        {
            Debug.Assert(other != null);
            if (null == other) return;

            for (int i = 0; i < other.Selections.Count; i++)
            {
                RemoveSelection(other.Selections[i]);
            }
        }

        #endregion

        public bool Empty()
        {
            return m_SelectionList.Empty();
        }

        #region Graphic draw
        public void Draw(GraphicContext gpCtx)
        {
            Debug.Assert(gpCtx != null);
            if (null == gpCtx) return;

            foreach (Selection item in m_SelectionList)
                item.Draw(gpCtx);
        }
        #endregion

        #region Data - name,  selection list

        private readonly String m_InternalName;
        private FRList<Selection> m_SelectionList;
        #endregion
    }

    public class Selection
    {
        #region Constructors
        public Selection(GraphicNode node)
        {
            m_SelectedNode = node;
            m_ID = s_IDDistributor++;

            m_Manipulator = null;
        }
        #endregion

        #region Attributes ID
        public long ID
        {
            get { return m_ID; }
        }

        public GraphicNode GraphicNode
        {
            get { return m_SelectedNode; }
        }

        #endregion

        #region Manipulator
        public Manipulator GetManipulator()
        {
            return m_Manipulator;
        }

        public void SetManipulator(Manipulator src)
        {
            m_Manipulator = src;
        }
        #endregion

        public SymbolConstraint GetInstance()
        {
            if(m_SelectedNode != null)
                return m_SelectedNode.GetInstance();

            return null;
        }

        #region Graphic draw
        public void Draw(GraphicContext gpCtx)
        {
            Debug.Assert(gpCtx != null);
            if (null == gpCtx) return;

           m_SelectedNode.Draw(gpCtx);
        }
        #endregion

        #region Data

        protected GraphicNode m_SelectedNode;

        // This is for the operation of the selection.
        private Manipulator m_Manipulator;

        // OPT - We should find another method to 
        // indicate the selection to improve the performance.
        // These data will change automatically. 
        // Didn't need user to change them.
        private readonly long m_ID;
        private static long s_IDDistributor = 0;
        #endregion
    }
}
