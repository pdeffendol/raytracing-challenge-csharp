using System;
using System.Collections.Generic;
using System.Text;
using Tuple = System.Tuple;

namespace RayTracer.Core
{
    public class Canvas
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Color[,] _pixels;

        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;
            InitializePixels();
        }
        public Color PixelAt(int x, int y)
        {
            return _pixels[x, y];
        }

        public void WritePixel(int x, int y, Color c)
        {
            _pixels[x, y] = c;
        }

        public string ToPPM()
        {
            string ppm = BuildPPMHeader();
            ppm += "\n";

            for (int y = 0; y < Height; y++)
            {
                string row = "";
                int lastWrap = 0;
                for (int x = 0; x < Width; x++)
                {
                    (row, lastWrap) = AppendToRow(row, lastWrap, IntColorValue(_pixels[x,y].Red).ToString());
                    row += " ";
                    (row, lastWrap) = AppendToRow(row, lastWrap, IntColorValue(_pixels[x,y].Green).ToString());
                    row += " ";
                    (row, lastWrap) = AppendToRow(row, lastWrap, IntColorValue(_pixels[x,y].Blue).ToString());
                    row += " ";
                }
                ppm += row.Trim() + "\n";
            }

            return ppm;
        }

        private string BuildPPMHeader()
        {
            return string.Join("\n",
                "P3",
                Width + " " + Height,
                "255");
        }

        private int IntColorValue(double value)
        {
            int converted = (int)Math.Round(value * 255, MidpointRounding.AwayFromZero);
            return Math.Min(255, Math.Max(0, converted));
        }

        private (string, int) AppendToRow(string row, int lastWrap, string append)
        {
            int newLastWrap = lastWrap;
            int lineLength = row.Length - lastWrap;
            if (lineLength + append.Length > 70)
            {
                row = row.Trim() + "\n";
                newLastWrap = row.Length;
            }
            row = row + append;
            return (row, newLastWrap);
        }

        private void InitializePixels()
        {
            _pixels = new Color[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _pixels[x, y] = new Color(0, 0, 0);
                }
            }
        }
    }
}
