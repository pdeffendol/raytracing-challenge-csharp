using System;
using System.Linq;
using System.Text;

namespace RayTracer.Core
{
    public class Matrix
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        double[,] _matrix;

        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
            _matrix = new double[rows, cols];
        }

        public Matrix(double[,] matrixArray)
        {
            Rows = matrixArray.GetLength(0);
            Columns = matrixArray.GetLength(1);
            _matrix = matrixArray;
        }

        public static Matrix Identity(int size)
        {
            var m = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                m[i,i] = 1;
            }

            return m;
        }

        public double Get(int row, int col)
        {
            return _matrix[row, col];
        }

        public void Set(int row, int col, double value)
        {
            _matrix[row, col] = value;
        }

        public double this[int row, int col]
        {
            get
            {
                return Get(row, col);
            }

            set
            {
                Set(row, col, value);
            }
        }

        public bool Equals(Matrix other)
        {
            if (this.Rows != other.Rows
                || this.Columns != other.Columns)
            {
                return false;
            }

            for (var r = 0; r < this.Rows; r++)
            {
                for (var c = 0; c < this.Columns; c++)
                {
                    if (!DoubleComparer.Equal(this[r,c], other[r,c]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Rows)
            {
                throw new InvalidOperationException("Columns in first matrix must match rows in second matrix");
            }

            var product = new Matrix(m1.Rows, m2.Columns);

            for (int r = 0; r < product.Rows; r++)
            {
                for (int c = 0; c < product.Columns; c++)
                {
                    double dp = 0;
                    for (int s = 0; s < m1.Columns; s++)
                    {
                        dp += m1[r,s] * m2[s,c];
                    }
                    product[r,c] = dp;
                }
            }
            return product;
        }

        public static Tuple operator*(Matrix matrix, Tuple tuple)
        {
            if (matrix.Columns != 4 || matrix.Rows != 4)
            {
                throw new InvalidOperationException("Matrix must be 4x4");
            }

            return new Tuple(
                tuple.X * matrix[0,0]
                + tuple.Y * matrix[0,1]
                + tuple.Z * matrix[0,2]
                + tuple.W * matrix[0,3],
                tuple.X * matrix[1,0]
                + tuple.Y * matrix[1,1]
                + tuple.Z * matrix[1,2]
                + tuple.W * matrix[1,3],
                tuple.X * matrix[2,0]
                + tuple.Y * matrix[2,1]
                + tuple.Z * matrix[2,2]
                + tuple.W * matrix[2,3],
                tuple.X * matrix[3,0]
                + tuple.Y * matrix[3,1]
                + tuple.Z * matrix[3,2]
                + tuple.W * matrix[3,3]
            );
        }

        public Matrix Transpose()
        {
            var m = new Matrix(this.Columns, this.Rows);

            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Columns; c++)
                {
                    m[c,r] = this[r,c];
                }
            }

            return m;
        }
    }
}
