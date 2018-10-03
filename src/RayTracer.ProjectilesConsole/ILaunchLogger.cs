namespace RayTracer.ProjectilesConsole
{
    interface ILaunchLogger
    {
        void Start();

        void LogPosition(Projectile projectile);

        void Complete();
    }
}
