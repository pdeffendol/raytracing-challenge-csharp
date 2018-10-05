using System;
using Tuple = RayTracer.Core.Tuple;

namespace RayTracer.ProjectilesConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var gravity = Tuple.CreateVector(0, -0.1, 0);
            var wind = Tuple.CreateVector(-0.01, 0, 0);
            var world = new World(gravity, wind);

            var pos = Tuple.CreatePoint(0, 1, 0);
            var velocity = Tuple.CreatePoint(1, 1.8, 0).Normalize() * 11.25;
            var projectile = new Projectile(pos, velocity);

            // var logger = new ConsoleLogger();
            var logger = new ImageLogger("projectile.ppm", 1000, 1000);
            var launcher = new ProjectileLauncher(world, logger);

            launcher.Launch(projectile);
        }
    }
}
