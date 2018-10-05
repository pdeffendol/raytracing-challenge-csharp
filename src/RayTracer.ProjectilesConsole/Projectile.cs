using RayTracer.Core;

namespace RayTracer.ProjectilesConsole
{
    class Projectile
    {
        public Tuple Position {get; private set;}
        public Tuple Velocity {get; private set;}

        public Projectile(Tuple position, Tuple velocity)
        {
            Position = position;
            Velocity = velocity;
        }
    }
}
