//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2009-4-24 Create
//
//////////////////////////////////////////////////////////////////////////

namespace FRMath.Adaptor
{
    class NativeLineAdaptor : LineAdaptor
    {
        #region Constructors
        public NativeLineAdaptor(PointAdaptor sp, PointAdaptor ep)
        {
            if (sp != null)
                m_Point = sp.Clone();
            else
                m_Point = new NativePointAdaptor();

            if (ep != null && !m_Point.IsEqualTo(ep))
            {
                VectorAdaptor direction = (ep - m_Point);

                m_Direction = direction.UnitVectorAdaptor;
            }
            else
                m_Direction = new UnitVectorAdaptor(1, 0, 0);
        }

        public NativeLineAdaptor(PointAdaptor point, UnitVectorAdaptor direction)
        {
            if (point != null)
                m_Point = point.Clone();
            else
                m_Point = new NativePointAdaptor();

            if (direction != null)
            {
                m_Direction = direction.Clone();
            }
            else
                m_Direction = new UnitVectorAdaptor(1,0,0);
        }
        #endregion

        #region Override
        public override PointAdaptor GetIntersection(PlaneAdaptor planeProxy)
        {
            if (null == planeProxy) return null;

            if (IsOn(planeProxy)) return m_Point;

            if (IsParallelTo(planeProxy)) return null;

            double[,] dA = new double[,]{
            {planeProxy.Normal.X, planeProxy.Normal.Y, planeProxy.Normal.Z},
            {m_Direction.Y, - m_Direction.X, 0},
            {0, m_Direction.Z, - m_Direction.Y}
            };

            double[,] dB = new double[,] { 
            { planeProxy.ConstNumber}, 
            { m_Direction.Y * m_Point.X
                - m_Direction.X * m_Point.Y}, 
            { m_Direction.Z * m_Point.Y
                - m_Direction.Y * m_Point.Z}
            };

            try
            {
                NativeMatrix2dAdaptor Ret = ResolveLinearEquation(
                    new NativeMatrix2dAdaptor(dA), new NativeMatrix2dAdaptor(dB));

                if(Ret != null)
                {
                    return new NativePointAdaptor(Ret[0, 0], Ret[1, 0], Ret[2, 0]);
                }
            }
            catch{}

            return null;
        }
        #endregion

        #region Private helper
        // OPT - The function name should be changed.
        // OPT - We only resolve one case now. We should implement this function later.
        private NativeMatrix2dAdaptor ResolveLinearEquation(NativeMatrix2dAdaptor A, NativeMatrix2dAdaptor B)
        {
            if (null == A || null == B) return null;

            // If A.RowCount = A.ColCount && Rank(A) = Rank(C) = A.RowCount
            // The equation has unique solution.
            if (!A.IsSingular()) return null;

            NativeMatrix2dAdaptor C = A.RightUnite(B);
            if (null == C) return null;
            if (C.Rank != A.Rank) return null;

            NativeMatrix2dAdaptor InvMatix = A.Inverse;
            if (null == InvMatix) return null;

            NativeMatrix2dAdaptor RetMatrix = InvMatix * B;

            return RetMatrix;
        }
        #endregion

        #region Attributes
        public override PointAdaptor BasePoint
        {
            get { return m_Point; }
        }
        public override UnitVectorAdaptor Direction
        {
            get { return m_Direction; }
        }

        #endregion

        #region Data
        private UnitVectorAdaptor m_Direction;
        private PointAdaptor m_Point;
        #endregion
    }
}
