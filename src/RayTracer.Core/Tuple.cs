using System;

namespace RayTracer.Core
{
    public class Tuple
    {
        public double X {get; private set;}
        public double Y {get; private set;}
        public double Z {get; private set;}
        public double W {get; private set;}

        public Tuple(double x, double y, double z, double w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public bool IsPoint() => this.W == 1.0;

        public bool IsVector() => this.W == 0.0;

        public static Tuple CreatePoint(double x, double y, double z) => new Tuple(x, y, z, 1.0);

        public static Tuple CreateVector(double x, double y, double z) => new Tuple(x, y, z, 0.0);

        public bool Equals(Tuple other)
        {
            return DoubleComparer.Equal(this.X, other.X)
                && DoubleComparer.Equal(this.Y, other.Y)
                && DoubleComparer.Equal(this.Z, other.Z)
                && DoubleComparer.Equal(this.W, other.W);
        }

        public static Tuple operator +(Tuple t1, Tuple t2) => new Tuple(
            t1.X + t2.X,
            t1.Y + t2.Y,
            t1.Z + t2.Z,
            t1.W + t2.W);

        public static Tuple operator -(Tuple t1, Tuple t2) => new Tuple(
            t1.X - t2.X,
            t1.Y - t2.Y,
            t1.Z - t2.Z,
            t1.W - t2.W);

        public static Tuple operator -(Tuple t) => new Tuple(-t.X, -t.Y, -t.Z, -t.W);

        public static Tuple operator *(Tuple t, double d) => new Tuple(
            t.X * d,
            t.Y * d,
            t.Z * d,
            t.W * d);

        public static Tuple operator /(Tuple t, double d) => new Tuple(
            t.X / d,
            t.Y / d,
            t.Z / d,
            t.W / d);

        public double Magnitude() =>
            Math.Sqrt(this.X * this.X
                + this.Y * this.Y
                + this.Z * this.Z
                + this.W * this.W);

        public Tuple Normalize()
        {
            var mag = this.Magnitude();
            return new Tuple(
                this.X / mag,
                this.Y / mag,
                this.Z / mag,
                this.W / mag);
        }

        public double Dot(Tuple other) => this.X * other.X
            + this.Y * other.Y
            + this.Z * other.Z
            + this.W * other.W;

        public Tuple Cross(Tuple other) => Tuple.CreateVector(
            this.Y * other.Z - this.Z * other.Y,
            this.Z * other.X - this.X * other.Z,
            this.X * other.Y - this.Y * other.X);
    }
}
