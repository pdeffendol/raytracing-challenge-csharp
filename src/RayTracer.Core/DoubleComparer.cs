using System;

namespace RayTracer.Core
{
    public static class DoubleComparer
    {
        public static bool Equal(double d1, double d2)
        {
            const double EPSILON = 0.00001;
            return Math.Abs(d1 - d2) < EPSILON;
        }
    }
}
