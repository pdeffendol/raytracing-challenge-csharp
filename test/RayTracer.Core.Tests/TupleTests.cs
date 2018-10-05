using Xunit;
using RayTracer.Core;
using System.Collections.Generic;

namespace RayTracer.Core.Tests
{
    public class TupleTests
    {
        [Fact]
        public void TupleWithW1IsAPoint()
        {
            Tuple t = new Tuple(4.3, -4.2, 3.1, 1.0);

            Assert.Equal(4.3, t.X);
            Assert.Equal(-4.2, t.Y);
            Assert.Equal(3.1, t.Z);
            Assert.Equal(1.0, t.W);
            Assert.True(t.IsPoint());
            Assert.False(t.IsVector());
        }

        [Fact]
        public void TupleWithW0IsAVector()
        {
            Tuple t = new Tuple(4.3, -4.2, 3.1, 0.0);

            Assert.Equal(4.3, t.X);
            Assert.Equal(-4.2, t.Y);
            Assert.Equal(3.1, t.Z);
            Assert.Equal(0.0, t.W);
            Assert.False(t.IsPoint());
            Assert.True(t.IsVector());
        }

        [Fact]
        public void CreatePoint_CreatesTuplesWithW1()
        {
            var point = Tuple.CreatePoint(4, -4, 3);
            var tuple = new Tuple(4, -4, 3, 1);

            Assert.Equal(tuple.X, point.X);
            Assert.Equal(tuple.Y, point.Y);
            Assert.Equal(tuple.Z, point.Z);
            Assert.Equal(tuple.W, point.W);
        }

        [Fact]
        public void CreateVector_CreatesTuplesWithW0()
        {
            var point = Tuple.CreateVector(4, -4, 3);
            var tuple = new Tuple(4, -4, 3, 0);

            Assert.Equal(tuple.X, point.X);
            Assert.Equal(tuple.Y, point.Y);
            Assert.Equal(tuple.Z, point.Z);
            Assert.Equal(tuple.W, point.W);
        }

        [Fact]
        public void Equality_TrueIfValuesEqual()
        {
            var t1 = new Tuple(3, -2, 5, 1);
            var t2 = new Tuple(3, -2, 5, 1);

            Assert.True(t1.Equals(t2));
        }

        [Fact]
        public void Equals_TrueIfValuesClose()
        {
            var t1 = new Tuple(0.33333, -2, 5, 1);
            var t2 = new Tuple(1d / 3d, -2, 5, 1);

            Assert.True(t1.Equals(t2));
        }

        [Fact]
        public void Equality_FalseIfValuesDifferent()
        {
            var t1 = new Tuple(3, -2, 5, 1);
            var t2 = new Tuple(4, -2, 5, 1);

            Assert.False(t1.Equals(t2));
        }

        [Fact]
        public void Addition()
        {
            var t1 = new Tuple(3, -2, 5, 1);
            var t2 = new Tuple(-2, 3, 1, 0);

            var sum = t1 + t2;

            Assert.Equal(1, sum.X);
            Assert.Equal(1, sum.Y);
            Assert.Equal(6, sum.Z);
            Assert.Equal(1, sum.W);
        }

        [Fact]
        public void Subtraction_TwoPoints_CreatesVector()
        {
            var p1 = Tuple.CreatePoint(3, 2, 1);
            var p2 = Tuple.CreatePoint(5, 6, 7);

            Tuple diff = p1 - p2;

            var expected = Tuple.CreateVector(-2, -4, -6);

            Assert.True(diff.Equals(expected));
        }

        [Fact]
        public void Subtraction_VectorFromPoint_CreatesPoint()
        {
            var p = Tuple.CreatePoint(3, 2, 1);
            var v = Tuple.CreateVector(5, 6, 7);

            var diff = p - v;

            var expected = Tuple.CreatePoint(-2, -4, -6);

            Assert.True(diff.Equals(expected));
        }

        [Fact]
        public void Subtraction_TwoVectors_CreatesVector()
        {
            var v1 = Tuple.CreateVector(3, 2, 1);
            var v2 = Tuple.CreateVector(5, 6, 7);

            var diff = v1 - v2;
            var expected = Tuple.CreateVector(-2, -4, -6);

            Assert.True(diff.Equals(expected));
        }

        [Fact]
        public void Subtraction_VectorFromZeroVector_Negates()
        {
            var zero = Tuple.CreateVector(0, 0, 0);
            var v = Tuple.CreateVector(1, -2, 3);

            var diff = zero - v;
            var expected = Tuple.CreateVector(-1, 2, -3);

            Assert.True(diff.Equals(expected));
        }

        [Fact]
        public void Negate_SubtractsFromZero()
        {
            var t = new Tuple(1, -2, 3, -4);

            var negated = -t;
            var expected = new Tuple(-1, 2, -3, 4);

            Assert.True(negated.Equals(expected));
        }

        [Fact]
        public void Multiply_ByScalar()
        {
            var t = new Tuple(1, -2, 3, -4);

            var result = t * 3.5;

            var expected = new Tuple(3.5, -7, 10.5, -14);

            Assert.True(result.Equals(expected));
        }

        [Fact]
        public void Multiply_ByFraction()
        {
            var t = new Tuple(1, -2, 3, -4);

            var result = t * 0.5;

            var expected = new Tuple(0.5, -1, 1.5, -2);

            Assert.True(result.Equals(expected));
        }

        [Fact]
        public void Divide_ByScalar()
        {
            var t = new Tuple(1, -2, 3, -4);

            var result = t / 2;

            var expected = new Tuple(0.5, -1, 1.5, -2);

            Assert.True(result.Equals(expected));
        }

        public static IEnumerable<object[]> MagnitudeFixture =>
            new List<object[]>
            {
                new object[] { Tuple.CreateVector(1, 0, 0), 1 },
                new object[] { Tuple.CreateVector(0, 1, 0), 1 },
                new object[] { Tuple.CreateVector(0, 0, 1), 1 },
                new object[] { Tuple.CreateVector(1, 2, 3), System.Math.Sqrt(14) },
                new object[] { Tuple.CreateVector(-1, -2, -3), System.Math.Sqrt(14) },
            };

        [Theory]
        [MemberData("MagnitudeFixture")]
        public void Magnitude(Tuple t, double expectedMagnitude)
        {
            var mag = t.Magnitude();

            Assert.Equal(expectedMagnitude, mag);
        }

        public static IEnumerable<object[]> NormalizeFixture =>
            new List<object[]>
            {
                new object[] { Tuple.CreateVector(4, 0, 0), Tuple.CreateVector(1, 0, 0) },
                new object[] { Tuple.CreateVector(1, 2, 3), Tuple.CreateVector(0.26726, 0.53452, 0.80178) },
            };

        [Theory]
        [MemberData("NormalizeFixture")]
        public void Normalize_GivesUnitVectors(Tuple t, Tuple expectedNormalized)
        {
            var norm = t.Normalize();

            Assert.True(norm.Equals(expectedNormalized));
        }

        [Fact]
        public void Magnitude_OfNormalizedVector_IsOne()
        {
            var v = Tuple.CreateVector(1, 2, 3);
            var norm = v.Normalize();

            Assert.Equal(1d, norm.Magnitude());
        }

        [Fact]
        public void Dot_ProducesDotProduct()
        {
            var a = Tuple.CreateVector(1, 2, 3);
            var b = Tuple.CreateVector(2, 3, 4);

            Assert.Equal(20, a.Dot(b));
        }

        [Fact]
        public void Cross_ProducesCrossProduct()
        {
            var a = Tuple.CreateVector(1, 2, 3);
            var b = Tuple.CreateVector(2, 3, 4);

            var ab = Tuple.CreateVector(-1, 2, -1);
            var ba = Tuple.CreateVector(1, -2, 1);

            Assert.True(a.Cross(b).Equals(ab));
            Assert.True(b.Cross(a).Equals(ba));
        }
    }
}
