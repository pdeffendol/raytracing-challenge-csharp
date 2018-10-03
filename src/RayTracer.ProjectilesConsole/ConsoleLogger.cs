using System;

namespace RayTracer.ProjectilesConsole
{
    class ConsoleLogger : ILaunchLogger
    {
        private int tick;

        public void Start()
        {
            tick = 0;
        }

        public void LogPosition(Projectile projectile)
        {

            Console.WriteLine($"Tick {tick}: ({projectile.Position.X}, {projectile.Position.Y}, {projectile.Position.Z})");
            tick++;
        }

        public void Complete()
        {
            
        }
    }
}
