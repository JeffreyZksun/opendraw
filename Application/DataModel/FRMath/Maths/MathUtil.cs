
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

// FRD - They are corresponding to the math concept.
// They can finish the math calculation.
using System.Diagnostics;
using FRContainer;

namespace FRMath
{
    public static class MathUtil
    {
        public static bool IsTwoDoubleEqual(double value1, double value2, double tol)
        {
            double dDiff = value1 - value2;
            return !(dDiff > tol || dDiff < -tol);
        }

        public static bool IsTwoDoubleEqual(double value1, double value2)
        {
            return IsTwoDoubleEqual(value1, value2, kTolerance);
        }

        public static double Max(double value1, double valu2)
        {
            return value1 > valu2 ? value1 : valu2;
        }

        public static double Min(double value1, double valu2)
        {
            return value1 < valu2 ? value1 : valu2;
        }

        // If we use 1e-8, some compare will be failed.
        public static double kTolerance {get{ return 1e-6;}} 

        public static double PI { get{return 3.1415926;} }

        #region Transform geometries
        // The old point can't be null, the trans can be null.
        public static GePoint GetTransformedPoint(GePoint oldPoint, Matrix44 trans)
        {
            Debug.Assert(oldPoint != null);
            GePoint newPoint = null;
            if (oldPoint != null && trans != null)
                newPoint = trans * oldPoint;
            else // either of them is null.
                newPoint = oldPoint;

            return newPoint;
        }

        // The old oldPoints can't be null, the trans can be null.
        public static FRList<GePoint> GetTransformedPoints(FRList<GePoint> oldPoints, Matrix44 trans)
        {
            Debug.Assert(oldPoints != null);
            FRList<GePoint> newPoints = null;
            if (oldPoints != null && trans != null)
            {
                newPoints = new FRList<GePoint>();
                for (int i = 0; i < oldPoints.Count; i++)
                {
                    newPoints.Add(trans * oldPoints[i]);
                }
            }
            else // either of them is null.
                newPoints = oldPoints;

            return newPoints;
        }

        // The old oldLineSeg can't be null, the trans can be null.
        public static GeLineSeg GetTransformedLineSeg(GeLineSeg oldLineSeg, Matrix44 trans)
        {
            Debug.Assert(oldLineSeg != null);
            GeLineSeg newLineSeg = null;
            if (oldLineSeg != null && trans != null)
            {
                newLineSeg = new GeLineSeg(
                    trans * oldLineSeg.StartPoint,
                    trans * oldLineSeg.EndPoint);
            }                
            else // either of them is null.
                newLineSeg = oldLineSeg;

            return newLineSeg;
        }
        #endregion
    };

}
