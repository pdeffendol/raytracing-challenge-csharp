using System;

namespace RayTracer.Core
{
    public class Color
    {
        public double Red {get; private set;}
        public double Green {get; private set;}
        public double Blue {get; private set;}

        public Color(double red, double green, double blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public bool Equals(Color other)
        {
            return DoubleComparer.Equal(this.Red, other.Red)
                && DoubleComparer.Equal(this.Green, other.Green)
                && DoubleComparer.Equal(this.Blue, other.Blue);
        }

        public static Color operator +(Color c1, Color c2) => new Color(
            c1.Red + c2.Red,
            c1.Green + c2.Green,
            c1.Blue + c2.Blue);

        public static Color operator -(Color c1, Color c2) => new Color(
            c1.Red - c2.Red,
            c1.Green - c2.Green,
            c1.Blue - c2.Blue);

        public static Color operator *(Color c, double d) => new Color(
            c.Red * d,
            c.Green * d,
            c.Blue * d);

        public static Color operator *(Color c1, Color c2) => new Color(
            c1.Red * c2.Red,
            c1.Green * c2.Green,
            c1.Blue * c2.Blue);


    }
}
