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
using MathNet.Numerics.LinearAlgebra;

// [Is this class necessary?]
namespace FRMath.Adaptor
{
    class NativeMatrix2dAdaptor
    {
         #region Constructors
        public NativeMatrix2dAdaptor(double[,] data)
        {
            Debug.Assert(data != null);
            m_IriduimMatrix = Matrix.Create(data);
        }

        public NativeMatrix2dAdaptor(int row, int column)
        {
            Debug.Assert(row > 0 && column > 0);
            if (row < 1) row = 1;
            if (column < 1) column = 1;
            m_IriduimMatrix = new Matrix(row, column);
        }

        public NativeMatrix2dAdaptor(NativeMatrix2dAdaptor src)
        {
            Debug.Assert(src != null);

            m_IriduimMatrix = Matrix.Create(src.m_IriduimMatrix);
        }

        private NativeMatrix2dAdaptor(Matrix IriduimMatrix)
        {
            Debug.Assert(IriduimMatrix != null);
            m_IriduimMatrix = IriduimMatrix;
        }
        #endregion

        #region Attributes
        public int ColumnCount
        {
            get
            {
                return m_IriduimMatrix.ColumnCount;
            }
        }

        public int RowCount
        {
            get
            {
                return m_IriduimMatrix.RowCount;
            }
        }

        public double this[int row, int column]
        {
            get
            {
                Debug.Assert(IsValidRow(row) && IsValidColumn(column));

                if (IsValidRow(row) && IsValidColumn(column))
                    return m_IriduimMatrix[row, column];                    
                else
                    return -1;
            }
            set
            {
                Debug.Assert(IsValidRow(row) && IsValidColumn(column));

                if (IsValidRow(row) && IsValidColumn(column))
                    m_IriduimMatrix[row, column] = value;
            }            
        }


        public int Rank
        {
            get { return m_IriduimMatrix.Rank(); }
        }

        public virtual NativeMatrix2dAdaptor Inverse
        {
            get
            {
                if (!IsSingular()) return null;

                return new NativeMatrix2dAdaptor(m_IriduimMatrix.Inverse());
            }
        }
        #endregion

        public bool IsValidRow(int row)
        {
            return (row > -1 && row < RowCount);
        }

        public bool IsValidColumn(int column)
        {
            return (column > -1 && column < ColumnCount);
        }

        public NativeMatrix2dAdaptor Clone()
        {
            NativeMatrix2dAdaptor NewMatrix = new NativeMatrix2dAdaptor(m_IriduimMatrix.Clone());
            return NewMatrix;
        }

        public bool IsSquare()
        {
            return RowCount == ColumnCount;
        }

        public bool IsSingular()
        {
            return (IsSquare() && Rank == RowCount);
        }

        #region Matrix Unite
        public NativeMatrix2dAdaptor RightUnite(NativeMatrix2dAdaptor B)
        {
            if (null == B) return null;

            if (this.RowCount != B.RowCount) return null;

            double[,] data = new double[this.RowCount
                , this.ColumnCount + B.ColumnCount];

            for (int row = 0; row < this.RowCount; row++)
                for (int col = 0; col < this.ColumnCount; col++)
                    data[row, col] = this[row, col];

            for (int row = 0; row < this.RowCount; row++)
                for (int col = 0; col < B.ColumnCount; col++)
                    data[row, this.ColumnCount + col] = B[row, col];

            NativeMatrix2dAdaptor C = new NativeMatrix2dAdaptor(data);

            return C;
        }
        #endregion

        public void RightMutiply(NativeMatrix2dAdaptor rhs)
        {
            if (null == rhs) return;

            NativeMatrix2dAdaptor newMatrix = this * rhs;

            m_IriduimMatrix = new Matrix(newMatrix.m_IriduimMatrix);
        }

        #region operator

        public static NativeMatrix2dAdaptor operator *(NativeMatrix2dAdaptor lhs, NativeMatrix2dAdaptor rhs)
        {
            if (null == lhs || null == rhs) return null;

            return new NativeMatrix2dAdaptor(lhs.m_IriduimMatrix * rhs.m_IriduimMatrix);
        }

        public static NativeMatrix2dAdaptor operator /(NativeMatrix2dAdaptor lhs, double rhs)
        {
            if (null == lhs) return null;

            double[,] data = new double[lhs.RowCount
                , lhs.ColumnCount];

            for (int row = 0; row < lhs.RowCount; row++)
                for (int col = 0; col < lhs.ColumnCount; col++)
                    data[row, col] = lhs[row, col] / rhs;

            return new NativeMatrix2dAdaptor(data);
        }


        #endregion

        #region override Object
        public override string ToString()
        {
            return m_IriduimMatrix.ToString();
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            if (!(obj is NativeMatrix2dAdaptor)) return false;

            NativeMatrix2dAdaptor rhs = (NativeMatrix2dAdaptor)obj;

            if (RowCount != rhs.RowCount
                || ColumnCount != rhs.ColumnCount) return false;

            for (int i = 0; i < rhs.RowCount; i++)
                for (int j = 0; j < rhs.ColumnCount; j++)
                {
                    if (!MathUtil.IsTwoDoubleEqual(this[i, j], rhs[i, j]))
                        return false;
                }

            return true;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        #endregion


        private Matrix m_IriduimMatrix;
    }
}
