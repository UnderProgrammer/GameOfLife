using System;

namespace Balls
{
    public class Vector2d : IEquatable<Vector2d>
    {
        private const double Tolerance = 0.00001;

        public Vector2d(double x, double y)
        {
            X = x;
            Y = y;
        }
       
        public double X { get; set; }

        public double Y { get; set; }

        public static Vector2d Sum(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2d operator +(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2d operator -(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2d operator *(Vector2d a, double k)
        {
            return new Vector2d(a.X *k , a.Y * k);
        }

        public static Vector2d operator *(double k, Vector2d a)
        {
            return a * k;
        }

        public static double operator *(Vector2d a, Vector2d b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static decimal Mod(Vector2d a)
        {
            return (decimal) Math.Sqrt(a.X * a.X + a.Y * a.Y);
        }


        public static (Vector2d kb, Vector2d kc) Decompose(Vector2d a, Vector2d b, Vector2d c)
        {
            var k1 = (a.Y * b.X - a.X * b.Y) / (b.X * c.Y - b.Y * c.X);
            var k2 = (a.X * c.Y - a.Y * c.X) / (b.X * c.Y - b.Y * c.X);

            var kb = k2 * b;
            var kc = k1 * c;
            return (kb, kc);
        }

        public bool Equals(Vector2d other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Math.Abs(X - other.X) < Tolerance && Math.Abs(Y - other.Y) < Tolerance;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vector2d) obj);
        }

        public static bool operator ==(Vector2d a, Vector2d b)
        {
            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Vector2d a, Vector2d b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}