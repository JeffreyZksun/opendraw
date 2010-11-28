//////////////////////////////////////////////////////////////////////////
//
//
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
using FRContainer;
using System.Diagnostics;

namespace FRPaint
{
    public class PolyLineInstance : SymbolConstraint
    {
        public PolyLineInstance(DataFragment fragment)
            : base(fragment, new PolyLineGraphicState(fragment))
        {
            m_Points = new FRList<GePoint>();
        }


        #region Store data
        public void AddLastPointBefore(GePoint point)
        {
            if (point != null)
            {
                if (!m_Points.Empty())
                    m_Points.Insert(m_Points.Count - 1, point);
                else
                    m_Points.Add(point);

                //GraphicChangeNotify();
            }
        }

        public void SetLastPoint(GePoint point)
        {
            if (point != null)
            {
                if (m_Points.Empty())
                    m_Points.Add(point);
                else
                    m_Points[m_Points.Count - 1] = point;

                //GraphicChangeNotify();
            }
        }

        public void RemoveLastPoint()
        {
            if (!m_Points.Empty())
            {
                m_Points.PopBack();
                //GraphicChangeNotify();
            }
        }
        #endregion

        #region Override for the compute pipeline
        public override void Compute()
        {
            PolyLineGraphicState symState = GetSymbolState() as PolyLineGraphicState;

            symState.LineVertexes = m_Points;           
            ObserverManager.Instance.SendEvent(new SubjectEvent(this, EventType.eUpdate));
        }
        #endregion
        // If index is -1, we'll move all the points.
        public void MovePoints(Vector vec, int index)
        {
            Debug.Assert(vec != null && index < m_Points.Count && index > -2);
            if (!(vec != null && index < m_Points.Count && index > -2)) return;

            if (-1 == index)
            {
                foreach (GePoint point in m_Points)
                {
                    point.Move(vec);
                }
            }
            else
                m_Points[index].Move(vec);

        }

        public override int GetPointIndex(GePoint point, double tolerance)
        {
            Debug.Assert(point != null);
            if (null == point) return -1;

            int index = -1;

            for (int i = 0; i < m_Points.Count; i++)
            {
                GePoint item = m_Points[i];
                if (item.DistanceTo(point) < tolerance)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public override GePoint GetPointAt(int index)
        {
            Debug.Assert(index < m_Points.Count && index > -2);
            if (!(index < m_Points.Count && index > -2)) return null;

            return m_Points[index];
        }

        public FRList<GePoint> Points
        {
            get { return m_Points; }
        }

        private FRList<GePoint> m_Points;
    };
}
