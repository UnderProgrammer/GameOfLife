using System;

namespace Balls
{
    public class Vector2d : IEquatable<Vector2d>
    {
        private const double Tolerance = 0.00001;

        public Vector2d(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }
       
        public decimal X { get; set; }

        public decimal Y { get; set; }

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

        public static Vector2d operator *(Vector2d a, decimal k)
        {
            return new Vector2d(a.X *k , a.Y * k);
        }

        public static Vector2d operator *(decimal k, Vector2d a)
        {
            return a * k;
        }

        public static decimal operator *(Vector2d a, Vector2d b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public decimal Mod()
        {
            var quad = (double) (X * X + Y * Y);
            return (decimal) Math.Sqrt(quad);
        }

        public Vector2d Perpendicular()
        {
            var yb = 1;
            if (X == 0)
            {
                return new Vector2d(1, 0);
            }
            var xb = -Y / X;
            return new Vector2d(xb, yb);
        }

        public Vector2d Unar()
        {
            var modCount = Mod();
            var x = X / modCount;
            var y = Y / modCount;
            return new Vector2d(x, y);
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
            return Math.Abs((double)X - (double)other.X) < Tolerance && Math.Abs((double)Y - (double)other.Y) < Tolerance;
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