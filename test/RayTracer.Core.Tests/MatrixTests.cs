using Xunit;
using RayTracer.Core;
using System;
using System.Collections.Generic;

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
            var m = new Matrix(new double[2, 2] {
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
            var m = new Matrix(new double[3, 3] {
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
            var m1 = new Matrix(new double[4, 4] {
                {1, 2, 3, 4},
                {2, 3, 4, 5},
                {3, 4, 5, 6},
                {4, 5, 6, 7},
            });

            var m2 = new Matrix(new double[4, 4] {
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
            var m1 = new Matrix(new double[4, 4] {
                {1, 2, 3, 4},
                {2, 3, 4, 5},
                {3, 4, 5, 6},
                {4, 5, 6, 7},
            });

            var m2 = new Matrix(new double[4, 4] {
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

            Assert.Throws<InvalidOperationException>(() =>
            {
                var product = m1 * m2;
            });
        }

        [Fact]
        public void Multiple_SquareMatrices_ProducesProduct()
        {
            var m1 = new Matrix(new double[4, 4] {
                {1, 2, 3, 4},
                {2, 3, 4, 5},
                {3, 4, 5, 6},
                {4, 5, 6, 7},
            });

            var m2 = new Matrix(new double[4, 4] {
                {0, 1, 2, 4},
                {1, 2, 4, 8},
                {2, 4, 8, 16},
                {4, 8, 16, 32},
            });

            var expectedProduct = new Matrix(new double[4, 4] {
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
            var m1 = new Matrix(new double[2, 3] {
                {1, 2, 3},
                {2, 3, 4}
            });

            var m2 = new Matrix(new double[3, 2] {
                {0, 1},
                {1, 2},
                {2, 4}
            });

            var expectedProduct = new Matrix(new double[2, 2] {
                {8, 17},
                {11, 24}
            });

            var product = m1 * m2;

            Assert.True(expectedProduct.Equals(product));
        }

        [Fact]
        public void Multiply_ByTuple()
        {
            var m1 = new Matrix(new double[4, 4] {
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
            var m1 = new Matrix(new double[3, 4] {
                {1, 2, 3, 4},
                {2, 4, 4, 2},
                {8, 6, 4, 1},
            });

            var tuple = new Tuple(1, 2, 3, 1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                var product = m1 * tuple;
            });
        }

        [Fact]
        public void Multiply_ByIdentity()
        {
            var m1 = new Matrix(new double[4, 4] {
                {0, 1, 2, 4},
                {1, 2, 4, 8},
                {2, 4, 8, 16},
                {4, 8, 16, 32},
            });

            var identity = Matrix.Identity(4);

            var product = m1 * identity;

            Assert.True(product.Equals(m1));
        }

        [Fact]
        public void Multiple_IdentityByTuple()
        {
            var tuple = new Tuple(1, 2, 3, 1);
            var identity = Matrix.Identity(4);

            var product = identity * tuple;

            Assert.True(tuple.Equals(product));
        }

        [Fact]
        public void Transpose()
        {
            var m = new Matrix(new double[,] {
                {0, 9, 3, 0},
                {9, 8, 0, 8},
                {1, 8, 5, 3},
                {0, 0, 5, 8}
            });

            var result = m.Transpose();

            var expectedResult = new Matrix(new double[,] {
                {0, 9, 1, 0},
                {9, 8, 8, 0},
                {3, 0, 5, 5},
                {0, 8, 3, 8}
            });

            Assert.True(expectedResult.Equals(result));
        }

        [Fact]
        public void Transpose_Identity()
        {
            var m = Matrix.Identity(4);

            Assert.True(m.Equals(m.Transpose()));
        }

        [Fact]
        public void Determinant_2x2()
        {
            var m = new Matrix(new double[,] {
                {1, 5},
                {-3, 2}
            });

            var det = m.Determinant();

            Assert.Equal(17, det);
        }

        [Fact]
        public void Submatrix_Given3x3_RemovesRowAndColumn()
        {
            var m = new Matrix(new double[,] {
                {1, 5, 0},
                {-3, 2, 7},
                {0, 6, -3}
            });

            var expectedResult = new Matrix(new double[,] {
                {-3, 2},
                {0, 6}
            });

            var result = m.Submatrix(0, 2);

            Assert.True(result.Equals(expectedResult));
        }

        [Fact]
        public void Submatrix_Given4x4_RemovesRowAndColumn()
        {
            var m = new Matrix(new double[,] {
                {-6, 1, 1, 6},
                {-8, 5, 8, 6},
                {-1, 0, 8, 2},
                {-7, 1, -1, 1}
            });

            var expectedResult = new Matrix(new double[,] {
                {-6, 1, 6},
                {-8, 8, 6},
                {-7, -1, 1}
            });

            var result = m.Submatrix(2, 1);

            Assert.True(result.Equals(expectedResult));
        }

        [Fact]
        public void Minor_3x3()
        {
            var m = new Matrix(new double[,] {
                {3 ,5 ,0},
                {2, -1, -7},
                {6, -1 ,5}
            });

            Assert.Equal(25, m.Minor(1, 0));
        }

        [Fact]
        public void Cofactor_3x3()
        {
            var m = new Matrix(new double[,] {
                {3 ,5 ,0},
                {2, -1, -7},
                {6, -1 ,5}
            });

            Assert.Equal(-12, m.Cofactor(0, 0));
            Assert.Equal(-25, m.Cofactor(1, 0));
        }

        [Fact]
        public void Determinant_3x3()
        {
            var m = new Matrix(new double[,] {
                {1 ,2 ,6},
                {-5, 8, -4},
                {2, 6 ,4}
            });

            Assert.Equal(56, m.Cofactor(0, 0));
            Assert.Equal(12, m.Cofactor(0, 1));
            Assert.Equal(-46, m.Cofactor(0, 2));
            Assert.Equal(-196, m.Determinant());
        }

        [Fact]
        public void Determinant_4x4()
        {
            var m = new Matrix(new double[,] {
                {-2 ,-8 ,3, 5},
                {-3, 1, 7, 3},
                {1, 2 ,-9, 6},
                {-6, 7 ,7, -9}
            });

            Assert.Equal(690, m.Cofactor(0, 0));
            Assert.Equal(447, m.Cofactor(0, 1));
            Assert.Equal(210, m.Cofactor(0, 2));
            Assert.Equal(51, m.Cofactor(0, 3));
            Assert.Equal(-4071, m.Determinant());
        }

        [Fact]
        public void IsInvertible_GivenNonZeroDeterminant_IsTrue()
        {
            var m = new Matrix(new double[,] {
                {6 ,4 ,4, 4},
                {5, 5, 7, 6},
                {4, -9 ,3, -7},
                {9, 1 ,7, -6}
            });

            Assert.True(m.IsInvertible());
        }

        [Fact]
        public void IsInvertible_GivenZeroDeterminant_IsFalse()
        {
            var m = new Matrix(new double[,] {
                {-4 ,2 ,-2, 3},
                {9, 6, 2, 6},
                {0, -5 ,1, -5},
                {0, 0 ,0, 0}
            });

            Assert.False(m.IsInvertible());
        }

        public static IEnumerable<object[]> InverseFixture =>
            new List<object[]>
            {
                new object[] { 
                    new double[,] {
                        {-5 ,2 ,6, -8},
                        {1, -5, 1, 8},
                        {7, 7 ,-6, -7},
                        {1, -3 ,7, 4}},
                    new[,] {
                        {0.21805, 0.45113, 0.24060, -0.04511},
                        {-0.80827, -1.45677, -0.44361, 0.52068},
                        {-0.07895, -0.22368, -0.05263, 0.19737},
                        {-0.52256, -0.81391, -0.30075, 0.30639}}
                },
                new object[] { new double[,] {
                        {8, -5,  9,  2},
                        {7,  5,  6,  1},
                        {-6,  0,  9,  6},
                        {-3,  0, -9, -4}},
                   new[,] {
                        {-0.15385, -0.15385, -0.28205, -0.53846},
                        {-0.07692,  0.12308,  0.02564,  0.03077},
                        {0.35897,  0.35897,  0.43590,  0.92308},
                        {-0.69231, -0.69231, -0.76923, -1.92308}}
                },
                    new object[] { new double[,] {
                        {9, 3, 0, 9},
                        {-5, -2, -6, -3},
                        {-4, 9, 6, 4},
                        {-7, 6, 6, 2}},
                    new[,] {
                        {-0.04074, -0.07778, 0.14444, -0.22222},
                        {-0.07778, 0.03333, 0.36667, -0.33333},
                        {-0.02901, -0.14630, -0.10926, 0.12963},
                        { 0.17778, 0.06667, -0.26667, 0.33333}}                
                }
            };

        [Theory]
        [MemberData(nameof(InverseFixture))]
        public void Inverse_4x4(double[,] matrixData, double[,] inverseData)
        {
            var m = new Matrix(matrixData);

            var expectedResult = new Matrix(inverseData);

            var inverse = m.Inverse();

            Assert.True(inverse.Equals(expectedResult));
        }

        [Fact]
        public void Inverse_MultiplyProductByInverse()
        {
            var m1 = new Matrix(new double[,] {
                {3, -9,  7,  3},
                {3, -8,  2, -9},
                {-4,  4,  4,  1},
                {-6,  5, -1,  1}});

            var m2 = new Matrix(new double[,] {
                {8, 2, 2, 2},
                {3, -1, 7, 0},
                {7, 0, 5, 4},
                {6, -2, 0, 5}});

            var product = m1 * m2;

            Assert.True(m1.Equals(product * m2.Inverse()));
        }
    }
}
