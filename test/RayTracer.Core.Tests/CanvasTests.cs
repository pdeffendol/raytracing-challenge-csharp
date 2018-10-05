using Xunit;
using RayTracer.Core;
using System;
using System.Linq;

namespace RayTracer.Core.Tests
{
    public class CanvasTests
    {
        [Fact]
        public void CreateCanvas_SetsDimensions()
        {
            var c = new Canvas(10, 20);

            Assert.Equal(10, c.Width);
            Assert.Equal(20, c.Height);
        }

        [Fact]
        public void CreateCanvas_InitializesAllPixelsToBlack()
        {
            var c = new Canvas(5, 5);

            for (var x = 0; x < c.Width; x++)
            {
                for (var y = 0; y < c.Height; y++)
                {
                    Assert.NotNull(c.PixelAt(x, y));
                    Assert.True(c.PixelAt(x, y).Equals(new Color(0, 0, 0)));
                }
            }
        }

        [Fact]
        public void WritePixel_SetsPixelColor()
        {
            var c = new Canvas(5, 5);
            var red = new Color(1, 0, 0);

            c.WritePixel(1, 1, red);

            Assert.True(c.PixelAt(1, 1).Equals(red));
        }

        [Fact]
        public void ToPPM_CreatesHeader()
        {
            var c = new Canvas(5, 3);

            var ppm = c.ToPPM();

            var header = LinesOfString(ppm, 1, 3);

            string expectedHeader = string.Join("\n",
                "P3",
                "5 3",
                "255");

            Assert.Equal(expectedHeader, header);
        }

        [Fact]
        public void ToPPM_OutputsPixelValues()
        {
            var c = new Canvas(5, 3);

            c.WritePixel(0, 0, new Color(1.5, 0, 0));
            c.WritePixel(2, 1, new Color(0, 0.5, 0));
            c.WritePixel(4, 2, new Color(-0.5, 0, 1));

            var ppm = c.ToPPM();

            string expectedLines = string.Join("\n",
                "255 0 0 0 0 0 0 0 0 0 0 0 0 0 0",
                "0 0 0 0 0 0 0 128 0 0 0 0 0 0 0",
                "0 0 0 0 0 0 0 0 0 0 0 0 0 0 255");

            string actualLines = LinesOfString(ppm, 4, 3);

            Assert.Equal(expectedLines, actualLines);
        }

        [Fact]
        public void ToPPM_SplitsLongLinesAt70()
        {
            var c = new Canvas(10, 2);
            var color = new Color(1, 0.8, 0.6);
            for (var x = 0; x < c.Width; x++)
            {
                for (var y = 0; y < c.Height; y++)
                {
                    c.WritePixel(x, y, color);
                }
            }

            var ppm = c.ToPPM();
            var pixelLines = LinesOfString(ppm, 4, 4);

            var expectedLines = string.Join("\n", 
                "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204",
                "153 255 204 153 255 204 153 255 204 153 255 204 153",
                "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204",
                "153 255 204 153 255 204 153 255 204 153 255 204 153");

            Assert.Equal(expectedLines, pixelLines);
        }

        [Fact]
        public void ToPPM_EndsWithNewline()
        {
            var c = new Canvas(5, 3);

            var ppm = c.ToPPM();

            Assert.Equal('\n', ppm[ppm.Length - 1]);
        }

        private string LinesOfString(string s, int firstLine, int count)
        {
            return string.Join(Environment.NewLine, 
                (from l in s.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None) select l)
                .Skip(firstLine - 1)
                .Take(count));
        }
    }
}
