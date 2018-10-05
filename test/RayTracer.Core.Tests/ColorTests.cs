using Xunit;
using RayTracer.Core;

namespace RayTracer.Core.Tests
{
    public class ColorTests
    {
        [Fact]
        public void Constructor_SetsValues()
        {
            var c = new Color(0.1, 0.2, 0.3);

            Assert.Equal(0.1, c.Red);
            Assert.Equal(0.2, c.Green);
            Assert.Equal(0.3, c.Blue);
        }

        [Fact]
        public void Addition()
        {
            var c1 = new Color(0.9, 0.6, 0.75);
            var c2 = new Color(0.7, 0.1, 0.25);

            var sum = c1 + c2;

            Assert.True(sum.Equals(new Color(1.6, 0.7, 1.0)));
        }
        [Fact]
        public void Subtraction()
        {
            var c1 = new Color(0.9, 0.6, 0.75);
            var c2 = new Color(0.7, 0.1, 0.25);

            var sum = c1 - c2;

            Assert.True(sum.Equals(new Color(0.2, 0.5, 0.5)));
        }

        [Fact]
        public void Multiply_ByScalar()
        {
            var c = new Color(0.2, 0.3, 0.4);

            var product = c * 2;

            Assert.True(product.Equals(new Color(0.4, 0.6, 0.8)));
        }

        [Fact]
        public void Multiply_ByColor()
        {
            var c1 = new Color(1, 0.2, 0.4);
            var c2 = new Color(0.9, 1, 0.1);

            var product = c1 * c2;

            Assert.True(product.Equals(new Color(0.9, 0.2, 0.04)));
        }
    }
}
