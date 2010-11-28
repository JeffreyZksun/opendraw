//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2009-4-24 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;

namespace FRMath.Adaptor
{
    class NativeMatrix44Adaptor : Matrix44Adaptor
    {
         #region constructors

        // The default constructor must create a identity matrix.
        public NativeMatrix44Adaptor()
        {
            m_Matrix2d = new NativeMatrix2dAdaptor(4, 4);
            for (int row = 0; row < 4; row++)
                for (int column = 0; column < 4; column++)
                    if (row == column)
                        m_Matrix2d[row, column] = 1;
                    else
                        m_Matrix2d[row, column] = 0;
        }

        private NativeMatrix44Adaptor(NativeMatrix2dAdaptor src)
        {
            Debug.Assert(src.RowCount == 4 && src.ColumnCount == 4);
            if (src.RowCount == 4 && src.ColumnCount == 4)
                m_Matrix2d = src;
        }
        #endregion

        #region Clone 
        public override Matrix44Adaptor Clone()
        {
            return new NativeMatrix44Adaptor(m_Matrix2d.Clone());
        }
        #endregion

        #region Override
        // Translation
        public override Matrix44Adaptor SetTranslation(VectorAdaptor distance)
        {
            Debug.Assert(distance != null);

            if (distance != null)
            {
                m_Matrix2d[0, 3] = distance.X;
                m_Matrix2d[1, 3] = distance.Y;
                m_Matrix2d[2, 3] = distance.Z;
            }

            return this;
        }

        // Rotate the angle is radian
        public override Matrix44Adaptor SetRotate(double AngleThita, UnitVectorAdaptor axis, PointAdaptor basePoint)
        {
            Debug.Assert(axis != null && basePoint != null);
            if (!(axis != null && basePoint != null)) return this;

            UnitizeSubmatrixA();

            // Translation the origin to base point
            NativeMatrix44Adaptor TA = NativeMatrix44Adaptor.Identity;

            NativePointAdaptor origin = new NativePointAdaptor(0, 0, 0);
            TA.SetTranslation(origin - basePoint);

            // Rotate the axis by X'-axis to X'-Z' plane
            double CosAlpha = 0;
            double SinAlpha = 0;
            double v = System.Math.Sqrt(axis.Y * axis.Y
                 + axis.Z * axis.Z);
            if (MathUtil.IsTwoDoubleEqual(v, 0))
            {
                // The axis is parallel to X-axis
                CosAlpha = 1;
                SinAlpha = 0;
            }
            else
            {
                CosAlpha = axis.Z / v;
                SinAlpha = axis.Y / v;
            }

            NativeMatrix44Adaptor Rx = NativeMatrix44Adaptor.Identity;
            Rx[1, 1] = CosAlpha;
            Rx[1, 2] = -SinAlpha;
            Rx[2, 1] = SinAlpha;
            Rx[2, 2] = CosAlpha;

            // Rotate the axis by Y'-axis to Z'-axis
            double u = axis.Length();

            double CosBeta = v / u;
            double SinBeta = -axis.X / u;

            NativeMatrix44Adaptor Ry = NativeMatrix44Adaptor.Identity;
            Ry[0, 0] = CosBeta;
            Ry[0, 2] = SinBeta;
            Ry[2, 0] = -SinBeta;
            Ry[2, 2] = CosBeta;

            // Rotate by Z' 
            double CosThita = System.Math.Cos(AngleThita);
            double SinThita = System.Math.Sin(AngleThita);
            NativeMatrix44Adaptor Rz = NativeMatrix44Adaptor.Identity;
            Rz[0, 0] = CosThita;
            Rz[0, 1] = -SinThita;
            Rz[1, 0] = SinThita;
            Rz[1, 1] = CosThita;

            // Get the inverse matrix of the above step
            NativeMatrix44Adaptor Ry_1 = Ry.Inverse as NativeMatrix44Adaptor;
            NativeMatrix44Adaptor Rx_1 = Rx.Inverse as NativeMatrix44Adaptor;
            NativeMatrix44Adaptor TA_1 = TA.Inverse as NativeMatrix44Adaptor;

            // The transform result
            Matrix44Adaptor R = TA_1 * Rx_1 * Ry_1 * Rz * Ry * Rx * TA;

            // Save the result
            for (int row = 0; row < 3; row++)
                for (int column = 0; column < 3; column++)
                    m_Matrix2d[row, column] = R[row, column];

            return this;
        }

        // Scale
        public override Matrix44Adaptor SetScaling(double allScale, PointAdaptor basePoint)
        {
            Debug.Assert(allScale > 0 && basePoint != null);
            if (!(allScale > 0 && basePoint != null)) return this;

            PointAdaptor origin = new NativePointAdaptor(0,0,0);
            if (basePoint.IsEqualTo(origin))
            {
                SetScaling(allScale);
            }
            else
            {
                // Transform the origin to the base point.
                NativeMatrix44Adaptor T = NativeMatrix44Adaptor.Identity;
                T.SetTranslation(origin - basePoint);

                // zoom
                NativeMatrix44Adaptor S = NativeMatrix44Adaptor.Identity;
                S.SetScaling(allScale);


                // Recover the coordinate of the base point
                NativeMatrix44Adaptor T_1 = T.Inverse as NativeMatrix44Adaptor;

                NativeMatrix44Adaptor R = (T_1 * S * T) as NativeMatrix44Adaptor;
                m_Matrix2d = new NativeMatrix2dAdaptor(R.m_Matrix2d);
            }

            return this;
        }

        public override Matrix44Adaptor SetScaling(double allScale)
        {
            Debug.Assert(allScale > 0);
            if ((allScale > 0))
                this[3, 3] = 1 / allScale;

            return this;
        }

        public override double GetAllScaling()
        {
            return (1 / this[3, 3]);
        }

        // Project  SetProject(...)

        // Mirror SetMirror(...) GePoint, GeLine, GePlane
        
        #endregion        

        #region operator *
        
        protected override PointAdaptor OperatorMultiply(Matrix44Adaptor trans, PointAdaptor point)
        {
            Debug.Assert(point != null && trans != null);
            if (null == point || null == trans) return null;

            double[,] data = new double[4, 1];
            data[0, 0] = point.X;
            data[1, 0] = point.Y;
            data[2, 0] = point.Z;
            data[3, 0] = 1;
            NativeMatrix44Adaptor nativeTrans = trans as NativeMatrix44Adaptor;
            NativeMatrix2dAdaptor pointMatrix = new NativeMatrix2dAdaptor(data);

            NativeMatrix2dAdaptor resulr = nativeTrans.GetNativeMatrix2dProxy() * pointMatrix;
            resulr = resulr / resulr[3, 0];

            return new NativePointAdaptor(resulr[0, 0], resulr[1, 0], resulr[2, 0]);
        }

        protected override Matrix44Adaptor OperatorMultiply(Matrix44Adaptor lhs, Matrix44Adaptor rhs)
        {
            if (null == lhs || null == rhs) return null;
            NativeMatrix44Adaptor nativeLhs = lhs as NativeMatrix44Adaptor;
            NativeMatrix44Adaptor nativeRhs = rhs as NativeMatrix44Adaptor;

            NativeMatrix2dAdaptor ret = nativeLhs.m_Matrix2d * nativeRhs.m_Matrix2d;

            if (ret != null)
            {
                return new NativeMatrix44Adaptor(ret);
            }

            return null;
        }
        #endregion

        #region Attributes

        public override double this[int row, int column]
        {
            get
            {
                return m_Matrix2d[row, column];

            }
            set
            {
                m_Matrix2d[row, column] = value;
            }
        }

        public override Matrix44Adaptor Inverse
        {
            get
            {
                return new NativeMatrix44Adaptor(m_Matrix2d.Inverse);
            }
        }
        #endregion

        #region Private Helper
        #region Helper functions
        private void UnitizeSubmatrixA()
        {
            for (int row = 0; row < 3; row++)
                for (int column = 0; column < 3; column++)
                    if (row == column)
                        m_Matrix2d[row, column] = 1;
                    else
                        m_Matrix2d[row, column] = 0;
        }

        public NativeMatrix2dAdaptor GetNativeMatrix2dProxy()
        {
            return m_Matrix2d.Clone();
        }
        #endregion

        #region Helper Attributes
        private static NativeMatrix44Adaptor Identity
        {
            get
            {
                NativeMatrix44Adaptor tran = new NativeMatrix44Adaptor();
                tran[0, 0] = 1;
                tran[1, 1] = 1;
                tran[2, 2] = 1;
                tran[3, 3] = 1;
                return tran;
            }
        }
        #endregion
        #endregion

        #region Override Object
        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            if (!(obj is NativeMatrix44Adaptor)) return false;

            NativeMatrix44Adaptor rhs = (NativeMatrix44Adaptor)obj;

            return m_Matrix2d.Equals(rhs.m_Matrix2d);
        }

        public override string ToString()
        {
            return m_Matrix2d.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        #endregion

        #region Data
        private NativeMatrix2dAdaptor m_Matrix2d;
        #endregion
    }
}
