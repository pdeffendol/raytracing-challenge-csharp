using RayTracer.Core;

namespace RayTracer.ProjectilesConsole
{
    class World
    {
        public Tuple Gravity {get; private set;}

        public Tuple Wind {get; private set;}

        public World(Tuple gravity, Tuple wind)
        {
            Gravity = gravity;
            Wind = wind;
        }

        public Projectile Tick(Projectile p)
        {
            return new Projectile(
                position: p.Position + p.Velocity,
                velocity: p.Velocity + Gravity + Wind);
        }
    }
}
