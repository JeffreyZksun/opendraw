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
using FRPaint;
using FRPaint.Fragment;
using FRMath;

namespace FRUML.TimeLine
{
    class UmlTimeLineCommandState : SpacePointSelectState
    {
        public override EventTarget OnAvtive()
        {
            DataFragment fragment = PtApp.ActiveDocument.Database.GetFragment();
            m_Constraint = new UmlTimeLineConstraint(fragment);

            m_SceneNode = new UmlTimeLineGraphicNode(m_Constraint);

            PtApp.ActiveView.TransientObjectObserver.AddNode(m_SceneNode);

            return base.OnAvtive();
        }
        public override void OnTerminate()
        {

            PtApp.ActiveView.TransientObjectObserver.RemoveNode(m_SceneNode);

            PtDocument doc = PtApp.GetActiveDocument();
            doc.AddNode(m_SceneNode);

            base.OnTerminate();
        }

        public override void OnMouseMove(GePoint point)
        {
            m_Constraint.SetCenterPoint(point);
            m_Constraint.Compute();
            PtApp.ActiveView.UpdateView();
        }

        public override void OnLeftButtionClick(GePoint point)
        {
            m_Constraint.SetCenterPoint(point);
            m_Constraint.Compute();
            PtApp.ActiveView.UpdateView();
        }

        public override void OnLeftDoubleClick(GePoint point)
        {
            m_Constraint.SetCenterPoint(point);
            PtApp.ActiveView.UpdateView();
        }

        private UmlTimeLineGraphicNode m_SceneNode;
        private UmlTimeLineConstraint m_Constraint;
    }
}
