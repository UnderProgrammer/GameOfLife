using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Balls
{
    public class Ball
    {
        private readonly WriteableBitmap _buffer;
        public int X { get; set; }
        public int Y { get; set; }
        public int R { get; set; }
        public Vector2d Speed { get; set; }
        public Color Color { get; set; }


        // TODO: introduce a new constructor that requires a Vector2d instance instead of vx vy;
        // TODO: replace decimal with double in existing constructor
        public Ball(int x, int y, int r, decimal vx, decimal vy, Color color, WriteableBitmap buffer)
        {
            X = x;
            Y = y;
            R = r;
            Speed = new Vector2d((double)vx, (double)vy);
            Color = color;
            _buffer = buffer;
        }

        public void Draw()
        {
            var x1 = X - R;
            var y1 = Y - R;
            var x2 = X + R;
            var y2 = Y + R;
            _buffer.FillEllipse(x1, y1, x2, y2, Color);
        }

        public void Move()
        {
            Draw();
            X = (int) (X + Speed.X);
            Y = (int) (Y + Speed.Y);
            Collision();
        }

        private void Collision()
        {
            if (X + R >= _buffer.PixelWidth || X - R <= 0)
            {
                Speed.X = - Speed.X;
            }
            if (Y + R >= _buffer.PixelHeight || Y - R <= 0)
            {
                Speed.Y = -Speed.Y;
            }
        }
    }
}