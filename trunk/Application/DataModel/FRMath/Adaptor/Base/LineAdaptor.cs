//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2009-4-24 Create
//
//////////////////////////////////////////////////////////////////////////
using System.Diagnostics;

namespace FRMath.Adaptor
{
    public abstract class LineAdaptor
    {
        #region Implementation

        // The shortest distance of point to line.
        public double DistanceTo(PointAdaptor pointProxy)
        {
            Debug.Assert(pointProxy != null);

            PointAdaptor pointOnLine = ClosestPointTo(pointProxy);
            double distance = pointOnLine.DistanceTo(pointProxy);

            return distance;
        }

        public PointAdaptor ClosestPointTo(PointAdaptor pointProxy)
        {
            if (null == pointProxy) return null;

            PlaneAdaptor Plane = GetPerpendicularPlane(pointProxy);
            if (null == Plane) return null;

            PointAdaptor Intersect = GetIntersection(Plane);

            return Intersect;
        }

        public PlaneAdaptor GetPerpendicularPlane(PointAdaptor pointProxy)
        {
            if (null == pointProxy)
                pointProxy = BasePoint;

            PlaneAdaptor Plane = MathAdaptorFactory.Instance.CreatePlaneAdaptor(pointProxy, Direction);
            return Plane;
        }

        // The shortest distance of two lines.
        public double DistanceTo(LineAdaptor lineB)
        {
            Debug.Assert(lineB != null);

            if (IsParallelTo(lineB))
            {
                // Get the distance of point to line
                return DistanceTo(lineB.BasePoint);
            }
            else
            {
                // Get a plan contains line B and is parallel to this line.
                VectorAdaptor normal = this.Direction.Cross(lineB.Direction.VectorAdaptor);
                PlaneAdaptor newPlane = MathAdaptorFactory.Instance.CreatePlaneAdaptor(
                    lineB.BasePoint
                    , MathAdaptorFactory.Instance.CreateUnitVectorAdaptor(
                    normal.X, normal.Y, normal.Z));

                // Get the distance of point to plane
                return this.BasePoint.DistanceTo(newPlane);
            }
        }

        // The shortest distance of line to plane.
        public double DistanceTo(PlaneAdaptor plane)
        {
            Debug.Assert(plane != null);

            if (!IsParallelTo(plane))
                return 0.0;
            else
            {
                double distance = BasePoint.DistanceTo(plane);
                return distance;
            }
        }

        #region Gemotry relationship
        public bool IsOn(PlaneAdaptor Plane)
        {
            if (null == Plane) return false;

            return Plane.Contains(this);
        }

        public bool IsParallelTo(PlaneAdaptor Plane)
        {
            if (null == Plane) return false;

            return (Direction.IsPerpendicularTo(Plane.Normal));
        }

        public bool IsParallelTo(LineAdaptor lineB)
        {
            if (null == lineB) return false;

            return (this.Direction.IsParallelTo(lineB.Direction));
        }

        public bool Contains(PointAdaptor point)
        {
            if (null == point) return false;

            PointAdaptor PointOnLine = BasePoint + Direction.VectorAdaptor;

            VectorAdaptor P2S = MathAdaptorFactory.Instance.CreateVectorAdaptor(point, PointOnLine);
            VectorAdaptor P2E = MathAdaptorFactory.Instance.CreateVectorAdaptor(point, BasePoint);

            return P2S.IsParallelTo(P2E);
        }

        #endregion

        #endregion


        #region Clone
        public LineAdaptor Clone()
        {
            return MathAdaptorFactory.Instance.CreateLineAdaptor(BasePoint, Direction);
        }
        #endregion


        #region Abstract functions

        public abstract PointAdaptor GetIntersection(PlaneAdaptor planeProxy);

        #endregion


        #region Operator

        #endregion

        #region Attributes
        public abstract PointAdaptor BasePoint
        {
            get;
        }
        public abstract UnitVectorAdaptor Direction
        {
            get;
        }
        #endregion
    }
}
