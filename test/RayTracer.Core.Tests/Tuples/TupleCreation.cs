using Xunit;
using RayTracer.Core.Tuples;

namespace RayTracer.Core.Tests.Tuples
{
   public class TupleCreation
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
   }
}
