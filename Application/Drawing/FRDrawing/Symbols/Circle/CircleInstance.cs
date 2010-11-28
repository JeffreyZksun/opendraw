//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-27 Create
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using FRPaint.Fragment;
using FRMath;
using System.Diagnostics;
using FRContainer;

namespace FRPaint
{
    public class CircleInstance : SymbolConstraint
    {
        public CircleInstance(DataFragment fragment, PointState pointState)
            : base(fragment, new CircleGraphicState(fragment))
        {
            m_dRadius = 1;
            m_DepCenterPoint = pointState;
        }

        public void SetCenter(GePoint Center)
        {
            Debug.Assert(Center != null);

            if (Center != null)
            {
                m_DepCenterPoint.Point = Center;
            }
        }

        #region Override for the compute pipeline
        public override void Compute()
        {
            CircleGraphicState circleState = GetSymbolState() as CircleGraphicState;

            circleState.Radius = m_dRadius;
            if (m_DepCenterPoint != null)
                circleState.CenterPoint = m_DepCenterPoint.Point;
            ObserverManager.Instance.SendEvent(new SubjectEvent(this, EventType.eUpdate));
        }
        #endregion

        #region Override for the compute pipeline
        public override void GetPredecessor(FRList<Node> nodeList)
        {
            nodeList.Add(m_DepCenterPoint);
            base.GetPredecessor(nodeList);
        }

        #endregion

        public void SetCenter(PointState pointState)
        {
            m_DepCenterPoint = pointState;
        }

        public void SetRaduis(GePoint Center)
        {
            Debug.Assert(Center != null && m_DepCenterPoint != null);

            if (Center != null && m_DepCenterPoint != null)
            {
                double dist = m_DepCenterPoint.Point.DistanceTo(Center);
                if (!MathUtil.IsTwoDoubleEqual(dist, 0, PtApp.GetActiveDocument().m_dFloatTolerance))
                {
                    m_dRadius = dist;
                }
            }
        }

        public override SymbolGeometryConstraint GetGeometryConstraint(GePoint point, double tolerance)
        {
            Debug.Assert(point != null);
            if (null == point) return null;

            double distance = point.DistanceTo(m_DepCenterPoint.Point);
            if (distance < tolerance)
            {
                SymbolPointConstraint pointConst = new SymbolPointConstraint(
                    null, this, 0);
                pointConst.Compute(); // Evaluate the point state
                return pointConst;
            }

            return null;
        }

        public override GePoint GetPointAt(int index)
        {
            return m_DepCenterPoint.Point;
        }

        public PointState GetCenterPoint()
        {
            return m_DepCenterPoint;
        }

        #region Attributes
        public GePoint CenterPoint
        {
            get { return m_DepCenterPoint.Point; }
            set
            {
                m_DepCenterPoint.Point = value;
                //GraphicChangeNotify();
            }
        }
        public double Radius
        {
            get { return m_dRadius; }
            set
            {
                if (value > 0)
                {
                    m_dRadius = value;
                    //GraphicChangeNotify();
                }
            }
        }
        #endregion

        #region Data
        private PointState m_DepCenterPoint; // input
        private double m_dRadius;
        #endregion
    };
}
