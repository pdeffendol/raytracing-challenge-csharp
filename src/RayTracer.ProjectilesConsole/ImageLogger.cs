using System.IO;
using RayTracer.Core;

namespace RayTracer.ProjectilesConsole
{
    class ImageLogger : ILaunchLogger
    {
        string _filename;
        private readonly Canvas _canvas;
        private Color _color;

        public ImageLogger(string filename, int width, int height)
        {
            _filename = filename;
            _canvas = new Canvas(width, height);
            _color = new Color(1, 0, 0);
        }

        public void Complete()
        {
            File.WriteAllText(_filename, _canvas.ToPPM());
        }

        public void LogPosition(Projectile projectile)
        {
            int xPos = (int)projectile.Position.X;
            int yPos = _canvas.Height - (int)projectile.Position.Y;

            if (xPos >= 0 && xPos < _canvas.Width
                && yPos >= 0 && yPos < _canvas.Height)
            {
                _canvas.WritePixel(xPos, yPos, _color);
            }
        }

        public void Start()
        {
        }
    }
}
