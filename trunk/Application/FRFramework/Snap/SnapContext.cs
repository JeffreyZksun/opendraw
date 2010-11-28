
//////////////////////////////////////////////////////////////////////////
//
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-28 Create
//
//////////////////////////////////////////////////////////////////////////

using FRContainer;
using FRMath;
using System.Diagnostics;
using FRGraphic;

namespace FRPaint
{
    public class SnapContext
    {
        protected SnapContext(FRDatabase database)
        {
            m_CandidateProxies = new FRList<SymbolGeometryConstraint>();
            m_SnappedGeoConst = null;
            SnappedPoint = null;

            m_Database = database;
        }

        public static SnapContext InitalizeSnapContextFromPreSelection(GePoint mousePoint)
        {
            Debug.Assert(mousePoint != null);

            SnapContext snapCtx = new SnapContext(PtApp.ActiveDocument.Database);

            PtSelectContext sltCtx = new PtSelectContext();
            sltCtx.WorldPoint = mousePoint;

            SelectionSet tempSet =
                PtApp.ActiveView.SelectionMgr.CreateSelectionSet("SnapTemporarySet");
            tempSet.Clear();
            sltCtx.CurrentSelectionSet = tempSet;

            PtApp.ActiveView.Select(sltCtx);

            // Set selection set.
            SelectionSet PreSet = PtApp.ActiveView.SelectionMgr.GetPreviewSelectionSet();
            PreSet.Clear();

            if (!tempSet.Empty())
            {
                foreach (Selection selection in tempSet.Selections)
                {
                    SymbolConstraint instance = selection.GetInstance();
                    Debug.Assert(instance != null);

                    SymbolGeometryConstraint constr = instance.GetGeometryConstraint(
                        mousePoint, snapCtx.SnapTolerance);
                    if (constr != null)
                    {
                        // We just highlight the snapped nodes.
                        snapCtx.CandidateProxies.Add(constr);
                        PreSet.AddSelection(selection);
                    }
                }
            }

            PtApp.ActiveView.UpdateView();

            return snapCtx;
        }

        public void Accept(GeometryVisitor vistor)
        {
            foreach (SymbolGeometryConstraint geometry in CandidateProxies)
            {
                vistor.Visit(geometry);
            }            
        }

        public bool HasResult()
        {
            return m_SnappedGeoConst != null;
        }

        private void Clear()
        {
            CandidateProxies.Clear();
            m_SnappedGeoConst = null;
            SnappedPoint = null;
        }

        public double SnapTolerance
        {
            get { return m_Database.DataScheme.SnapTolerance; }
        }

        public double ReleaseDependencyTolerance
        {
            get { return m_Database.DataScheme.ReleaseDependencyTolerance; }
        }
        public SymbolGeometryConstraint SnappedGeometryConstraint
        {
            get { return m_SnappedGeoConst; }
            set { m_SnappedGeoConst = value; }
        }

        public GePoint SnappedPoint
        {
            get { return m_SnappedPoint; } 
            set {m_SnappedPoint = value;}
        }

        public FRList<SymbolGeometryConstraint> CandidateProxies
        {
            get { return m_CandidateProxies; }
        }

        private FRList<SymbolGeometryConstraint> m_CandidateProxies; // Be set before snapping
        private SymbolGeometryConstraint m_SnappedGeoConst; // Be set in visitor
        private FRDatabase m_Database; // Be set when initialize
        private GePoint m_SnappedPoint; // Be set in visitor
    }
}
