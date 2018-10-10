using Xunit;
using RayTracer.Core;
using System;

namespace RayTracer.Core.Tests
{
    public class MatrixTests
    {
        [Fact]
        public void ConstructFromArray()
        {
            var m = new Matrix(new double[,] {
                {1, 2, 3, 4},
                {5.5, 6.5, 7.5, 8.5},
                {9, 10, 11, 12},
                {13.5, 14.5, 15.5, 16.5}
            });

            Assert.Equal(4, m.Columns);
            Assert.Equal(4, m.Rows);
            Assert.Equal(1, m[0, 0]);
            Assert.Equal(4, m[0, 3]);
            Assert.Equal(5.5, m[1, 0]);
            Assert.Equal(7.5, m[1, 2]);
            Assert.Equal(11, m[2, 2]);
            Assert.Equal(13.5, m[3, 0]);
            Assert.Equal(15.5, m[3, 2]);
        }

        [Fact]
        public void Allows2By2()
        {
            var m = new Matrix(new double[2,2] {
                {-3, 5},
                {1, 2}
            });

            Assert.Equal(2, m.Columns);
            Assert.Equal(2, m.Rows);
            Assert.Equal(-3, m[0, 0]);
            Assert.Equal(5, m[0, 1]);
            Assert.Equal(1, m[1, 0]);
            Assert.Equal(2, m[1, 1]);
        }

        [Fact]
        public void Allows3By3()
        {
            var m = new Matrix(new double[3,3] {
                {-3, 5, 0},
                {1, -2, 7},
                {0, 1, 1}
            });

            Assert.Equal(3, m.Columns);
            Assert.Equal(3, m.Rows);
            Assert.Equal(-3, m[0, 0]);
            Assert.Equal(-2, m[1, 1]);
            Assert.Equal(1, m[2, 2]);
        }

        [Fact]
        public void Equals_WithEqualMatrices()
        {
            var m1 = new Matrix(new double[4,4] {
                {1, 2, 3, 4},
                {2, 3, 4, 5},
                {3, 4, 5, 6},
                {4, 5, 6, 7},
            });

            var m2 = new Matrix(new double[4,4] {
                {1, 2, 3, 4},
                {2, 3, 4, 5},
                {3, 4, 5, 6},
                {4, 5, 6, 7},
            });

            Assert.True(m1.Equals(m2));
        }

        [Fact]
        public void Equals_FailsWithInequalMatrices()
        {
            var m1 = new Matrix(new double[4,4] {
                {1, 2, 3, 4},
                {2, 3, 4, 5},
                {3, 4, 5, 6},
                {4, 5, 6, 7},
            });

            var m2 = new Matrix(new double[4,4] {
                {0, 2, 3, 4},
                {2, 3, 4, 5},
                {3, 4, 5, 6},
                {4, 5, 6, 7},
            });

            Assert.False(m1.Equals(m2));
        }

        [Fact]
        public void Multiply_WithBadDimensions_ThrowsException()
        {
            var m1 = new Matrix(new double[,] {
                {1, 2, 3, 4},
                {2, 3, 4, 5},
            });

            var m2 = new Matrix(new double[,] {
                {0, 2, 3, 4},
                {2, 3, 4, 5},
            });

            Assert.Throws(typeof(InvalidOperationException), () =>
            {
                var product = m1 * m2;
            });
        }

        [Fact]
        public void Multiple_SquareMatrices_ProducesProduct()
        {
            var m1 = new Matrix(new double[4,4] {
                {1, 2, 3, 4},
                {2, 3, 4, 5},
                {3, 4, 5, 6},
                {4, 5, 6, 7},
            });

            var m2 = new Matrix(new double[4,4] {
                {0, 1, 2, 4},
                {1, 2, 4, 8},
                {2, 4, 8, 16},
                {4, 8, 16, 32},
            });

            var expectedProduct = new Matrix(new double[4,4] {
                {24, 49, 98, 196},
                {31, 64, 128, 256},
                {38, 79, 158, 316},
                {45, 94, 188, 376},
            });

            var product = m1 * m2;

            Assert.True(expectedProduct.Equals(product));
        }

        [Fact]
        public void Multiply_NonSquareMatrices_ProducesProduct()
        {
            var m1 = new Matrix(new double[2,3] {
                {1, 2, 3},
                {2, 3, 4}
            });

            var m2 = new Matrix(new double[3,2] {
                {0, 1},
                {1, 2},
                {2, 4}
            });

            var expectedProduct = new Matrix(new double[2,2] {
                {8, 17},
                {11, 24}
            });

            var product = m1 * m2;

            Assert.True(expectedProduct.Equals(product));
        }

        [Fact]
        public void Multiply_ByTuple()
        {
            var m1 = new Matrix(new double[4,4] {
                {1, 2, 3, 4},
                {2, 4, 4, 2},
                {8, 6, 4, 1},
                {0, 0, 0, 1},
            });

            var tuple = new Tuple(1, 2, 3, 1);

            var expectedProduct = new Tuple(18, 24, 33, 1);

            Tuple product = m1 * tuple;

            Assert.True(expectedProduct.Equals(product));
        }

        [Fact]
        public void Multiply_ByTuple_WhenMatrixWrongSize()
        {
            var m1 = new Matrix(new double[3,4] {
                {1, 2, 3, 4},
                {2, 4, 4, 2},
                {8, 6, 4, 1},
            });

            var tuple = new Tuple(1, 2, 3, 1);

            Assert.Throws(typeof(InvalidOperationException), () =>
            {
                var product = m1 * tuple;
            });
        }
    }
}
