using System;

namespace RayTracer.ProjectilesConsole
{
    class ProjectileLauncher
    {
        private readonly World _world;
        private readonly ILaunchLogger _logger;

        public ProjectileLauncher(World world, ILaunchLogger logger)
        {
            _world = world ?? throw new ArgumentNullException(nameof(world));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Launch(Projectile projectile)
        {
            int tick = 0;
            _logger.LogPosition(projectile);
            while (projectile.Position.Y > 0)
            {
                projectile = _world.Tick(projectile);
                tick++;
                _logger.LogPosition(projectile);
            }
            _logger.Complete();
        }
    }
}
