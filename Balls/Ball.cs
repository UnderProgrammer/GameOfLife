using System;
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

        /// <summary>
        /// The ball unique identifier. Please learn about Guid . 
        /// </summary>
        public Guid Id { get; }


        // TODO: introduce a new constructor that requires a Vector2d instance instead of vx vy;
        // TODO: replace decimal with double in existing constructor
        public Ball(int x, int y, int r, decimal vx, decimal vy, Color color, WriteableBitmap buffer)
        {
            X = x;
            Y = y;
            R = r;
            Speed = new Vector2d(vx, vy);
            Color = color;
            _buffer = buffer;
            Id = Guid.NewGuid();
        }

        public void Draw()
        {
            var x1 = X - R;
            var y1 = Y - R;
            var x2 = X + R;
            var y2 = Y + R;
            _buffer.FillEllipse(x1, y1, x2, y2, Color);
        }

        public void Move(Ball[] balls)
        {
            Draw();
            X = (int) (X + Speed.X);
            Y = (int) (Y + Speed.Y);
            CollisionWalls();
            CollisionBalls(balls);
        }

        private void CollisionWalls()
        {
            //if a ball reached a right border the x-speed should be negative
            if (X + R >= _buffer.PixelWidth)
            {
                var spd = -Math.Abs(Speed.X);
                Speed.X = spd < 0 ? spd : -1;
            }

            //if a ball reached a left border the x-speed should be positive
            if (X - R <= 0)
            {
                var spd = Math.Abs(Speed.X);
                Speed.X = spd > 0 ? spd : 1;
            }

            //if a ball reached a bottom border it's y-speed should be negative
            if (Y + R >= _buffer.PixelHeight)
            {
                var spd = -Math.Abs(Speed.Y);
                Speed.Y = spd < 0 ? spd : -1;
            }

            //if a ball reached a top border it's y-speed should be positive
            if (Y - R <= 0)
            {
                var spd = Math.Abs(Speed.Y);
                Speed.Y = spd > 0 ? spd : 1;
            }
        }


        /// <summary>
        /// This method calculates possible collisions to other balls. It takes an array of all balls as a parameter.
        /// </summary>
        /// <param name="balls"></param>
        private void CollisionBalls(Ball[] balls)
        {
            foreach (var target in balls)
            {
                //First check if ball is not the current ball. We should not calculate a ball collision to itself.
                if (Id == target.Id)
                {
                    continue;
                }

                //Getting distance between current ball and a ball that is being processed.
                var distance = CalculateDistance(target);

                // The balls may be created at the same point in this case we should not calculate collisions
                if (distance == 0)
                {
                    continue;
                }

                //Balls collide only if the distance between their centers is less or equal to the sum of their radii
                if (distance > R + target.R)
                {
                    continue;
                }

                


                //Let's find a collision surface vector. It is perpendicular to the vector connecting ball centers:
                var centersVector = new Vector2d(target.X - X, target.Y - Y);
                var collisionSurfaceVector = centersVector.Perpendicular();

                //Let's decompose a ball speed as the centerVector and the collisionSurfaceVector sum:
                var (directSpeedComponent, tangentSpeedComponent) = Speed.Decompose(centersVector, collisionSurfaceVector);

                //Do the same operation for the target ball:
                var (directTargetSpeedComponent, tangentTargetSpeedComponent) = target.Speed.Decompose(centersVector, collisionSurfaceVector);

                //We suppose that all the balls have the equal weight. In this case on collision they just exchange their direct speed components:

                Speed = directTargetSpeedComponent + tangentSpeedComponent;
                target.Speed = directSpeedComponent + tangentTargetSpeedComponent;
            }
        }

        private decimal CalculateDistance(Ball target)
        {
            //vector between the centers of the balls:
            var centersVector = new Vector2d(target.X - X, target.Y - Y);

            //the length of this vector is a dinstance:
            return centersVector.Mod();
        }
    }
}