
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-12-17 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using FRMath;

namespace FRPaint
{

    // When use the mouse change the view,
    // we change the transform matrix in this manipulator class.
    public class ViewTransformManipulator : Manipulator
    {
        public ViewTransformManipulator(Matrix44 DeviceToWorld)
        {
            Debug.Assert(DeviceToWorld != null);

            m_DeviceToWorld = DeviceToWorld;

            m_LastPoint = GePoint.kOrigin;
            m_CursorShape = "Translation.cur";
        }

        public override void StartDrag(EventContext Ctx)
        {
            //if (Ctx.MouseWorldPoint != null)
            //    m_LastPoint = Ctx.MouseWorldPoint;

            if (Ctx.MouseDevicePoint != null)
                m_LastPoint = Ctx.MouseWorldPoint;
        }

        public override void Drag(EventContext Ctx)
        {
            Debug.Assert(Ctx != null);
            if (null == Ctx) return;

            GePoint currentPoint = Ctx.MouseWorldPoint; //Ctx.MouseWorldPoint;
            if (currentPoint != null &&
                !currentPoint.IsEqualTo(m_LastPoint))
            {
                Matrix44 trans = Matrix44.Identity;
                trans.SetTranslation(m_LastPoint - currentPoint);
                m_DeviceToWorld.LeftMultiply(trans);

                // The last world point desn't change after panning.
                //m_LastPoint = currentPoint;

                PtApp.ActiveView.UpdateView();
            }
        }

        public override void MouseWheel(EventContext Ctx)
        {
            Debug.Assert(Ctx != null);
            if (null == Ctx) return;

            int Delta = Ctx.Delta;
            if (0 == Delta) return;

            double dScale = 0;
            double mouseSpace = 120;
            if(Delta > 0) // Zoom in
            {
                dScale = 1 / (1 + (Delta / mouseSpace) * 0.2);
            }
            else // Delta < 0, Zoom out
            {
                dScale = 1 + (-Delta / mouseSpace) * 0.2;
            }

            double newDeviceToWorldScale = m_DeviceToWorld.GetAllScaling() / dScale;
            double minScale = PtApp.ActiveDocument.DataScheme.MinZoomScale;
            double maxScale = PtApp.ActiveDocument.DataScheme.MaxZoomScale;
            
            if (newDeviceToWorldScale < minScale)
                newDeviceToWorldScale = minScale;
            if (newDeviceToWorldScale > maxScale)
                newDeviceToWorldScale = maxScale;

            m_DeviceToWorld.SetScaling(newDeviceToWorldScale, Ctx.MouseWorldPoint);

            PtApp.ActiveView.UpdateView();
        }

        protected Matrix44 m_DeviceToWorld;

        // Save the old world point. We use world point to handle the 
        // view transform.
        private GePoint m_LastPoint;
    };



}
