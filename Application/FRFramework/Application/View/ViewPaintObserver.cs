
//////////////////////////////////////////////////////////////////////////
//
// If we want to display something on the view, 
// we should draw them in the corresponding observer class.
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-31 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using FRContainer;
using FRGraphic;

namespace FRPaint
{
    public class ViewPaintObserver
    {
        public virtual void OnPaintDrawing(DeviceContext DeviceCtx) { }
    }

    // To draw the notes saved in document. Iterate all nodes.
    class NotesObserver : ViewPaintObserver
    {
        public override void OnPaintDrawing(DeviceContext DeviceCtx)
        {
            Debug.Assert(DeviceCtx != null);
            if (null == DeviceCtx) return;

            GraphicContext gpCtx = new DrawGraphicContext(DeviceCtx
                , PtApp.Get().ColorScheme.DrawingColor, PtApp.ActiveView.GetWorldToDeviceMatrix());

            // Draw all the nodes of the document
            foreach (GraphicNode ItemNode in PtApp.GetActiveDocument().GetGraphicNodes())
            {
                if (!IsInPreviewOrSelectedSet(ItemNode))
                    ItemNode.Draw(gpCtx);
            }
        }

        private bool IsInPreviewOrSelectedSet(GraphicNode ItemNode)
        {
            SelectionSet PreviewSet = PtApp.ActiveView.SelectionMgr.GetPreviewSelectionSet();
            foreach (Selection item in PreviewSet.Selections)
            {
                if (ItemNode.Equals(item.GraphicNode))
                    return true;
            }

            SelectionSet SelectedSet = PtApp.ActiveView.SelectionMgr.GetSelectedSelectionSet();
            foreach (Selection item in SelectedSet.Selections)
            {
                if (ItemNode.Equals(item.GraphicNode))
                    return true;
            }

            return false;
        }
    };

    // To draw preview selection. Call Set->Draw(Context)
    class PreviewObserver : ViewPaintObserver
    {
        public override void OnPaintDrawing(DeviceContext DeviceCtx)
        {
            Debug.Assert(DeviceCtx != null);
            if (null == DeviceCtx) return;

            GraphicContext PreviewGpCtx = new SelectionGraphicContext(
                DeviceCtx
                , PtApp.Get().ColorScheme.PreviewPointColor
                , PtApp.Get().ColorScheme.PreviewPointCircleColor
                , PtApp.Get().ColorScheme.PreviewCurveColor
                , PtApp.ActiveView.GetWorldToDeviceMatrix());

            SelectionSet PreviewSet = PtApp.ActiveView.SelectionMgr.GetPreviewSelectionSet();
            PreviewSet.Draw(PreviewGpCtx);

        }
    };

    // To draw selected selection. Call Set->Draw(Context)
    class SelectionObserver : ViewPaintObserver
    {
        public override void OnPaintDrawing(DeviceContext DeviceCtx)
        {
            Debug.Assert(DeviceCtx != null);
            if (null == DeviceCtx) return;

            GraphicContext SelectionGpCtx = new SelectionGraphicContext(
                DeviceCtx
                , PtApp.Get().ColorScheme.SelectedPointColor
                , PtApp.Get().ColorScheme.SelectedPointCircleColor
                , PtApp.Get().ColorScheme.SelectedCurveColor
                , PtApp.ActiveView.GetWorldToDeviceMatrix());

            SelectionSet SelectedSet = PtApp.ActiveView.SelectionMgr.GetSelectedSelectionSet();
            SelectedSet.Draw(SelectionGpCtx);
        }
    };

    public class TransientObjectObserver: ViewPaintObserver
    {
        public TransientObjectObserver()
        {
            m_NodeList = new FRList<GraphicNode>();
        }

        public void AddNode(GraphicNode node)
        {
            if(node != null)
                m_NodeList.Add(node);
        }

        // TBD - we should find a method to compare the nodes.
        public void RemoveNode(GraphicNode node)
        {
            if(node != null)
                m_NodeList.Remove(node);
        }

        public override void OnPaintDrawing(DeviceContext DeviceCtx)
        {   
            Debug.Assert(DeviceCtx != null);
            if (null == DeviceCtx) return;

            GraphicContext gpCtx = new DrawGraphicContext(DeviceCtx
                , PtApp.Get().ColorScheme.DrawingColor
                , PtApp.ActiveView.GetWorldToDeviceMatrix());

            // Draw all the nodes of the document
            foreach (GraphicNode ItemNode in m_NodeList)
                ItemNode.Draw(gpCtx);
        }

        public FRList<GraphicNode> m_NodeList;
    };

}
