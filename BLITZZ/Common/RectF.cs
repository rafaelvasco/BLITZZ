using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace BLITZZ
{
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct RectF : IEquatable<RectF>
    {
        private static readonly RectF _empty = new (0, 0, 0, 0);
        public static ref readonly RectF Empty => ref _empty;

        public float X1;
        public float Y1;
        public float X2;
        public float Y2;


        public static RectF FromBox(float x, float y, float w, float h)
        {
            return new RectF(x, y, x + w, y + h);
        }

        public RectF(float x1, float y1, float x2, float y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public float Width => X2 - X1;

        public float Height => Y2 - Y1;

        public bool IsEmpty => Width == 0 && Height == 0;

        public bool IsRegular => X2 > X1 && Y2 > Y1;

        public RectF Normalized => new RectF(Math.Min(X1, X2), Math.Min(Y1, Y2), Math.Max(X1, X2), Math.Max(Y1, Y2));

        public float Area => Math.Abs(Width * Height);

        public Vector2 TopLeft => new Vector2(X1, Y1);

        public Vector2 TopRight => new Vector2(X2, Y1);

        public Vector2 BottomLeft => new Vector2(X1, Y2);

        public Vector2 BottomRight => new Vector2(X2, Y2);


        public Vector2 Center => new Vector2(Math.Abs(X2 - X1) / 2, Math.Abs(Y2 - Y1) / 2);

        public override bool Equals(object obj)
        {
            return obj is RectF i && Equals(i);
        }

        public bool Equals(RectF other)
        {
            return Equals(ref other);
        }

        public bool Equals(ref RectF other)
        {
            return X1 == other.X1 && Y1 == other.Y1 && X2 == other.X2 && Y2 == other.Y2;
        }

        public bool Contains(Point p)
        {
            return p.X >= X1 && p.X <= X2 && p.Y >= Y1 && p.Y <= Y2;
        }


        public bool Contains(int x, int y)
        {
            return x >= X1 && x <= X2 && y >= Y1 && y <= Y2;
        }

        public bool Contains(ref RectF rect)
        {
            return X1 <= rect.X1 && X2 >= rect.X2 && Y1 <= rect.Y1 && Y2 >= rect.Y2;
        }

        public bool Contains(RectF rect)
        {
            return Contains(ref rect);
        }

        public bool Intersects(ref RectF other)
        {
            return X1 <= other.X2 &&
                   Y1 <= other.Y2 &&
                   X2 >= other.X1 &&
                   Y2 >= other.Y1;
        }

        public bool Intersects(RectF other)
        {
            return Intersects(ref other);
        }

        public void CopyTo(out RectF other)
        {
            other.X1 = X1;
            other.Y1 = Y1;
            other.X2 = X2;
            other.Y2 = Y2;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = (int)(hash * 23 + X1);
                hash = (int)(hash * 23 + Y1);
                hash = (int)(hash * 23 + X2);
                hash = (int)(hash * 23 + Y2);
                return hash;
            }
        }

        public RectF Inflated(float x, float y)
        {
            var copy = this;

            copy.X1 -= x;
            copy.X2 += x;
            copy.Y1 -= y;
            copy.Y2 += y;

            return copy;
        }

        public RectF Inflated(float amount)
        {
            return Inflated(amount, amount);
        }

        public RectF Translated(float dx, float dy)
        {
            return new RectF(X1 + dx, Y1 + dy, X2 + dx, Y2 + dy);
        }

        public override string ToString()
        {
            return $"{X1},{Y1},{X2},{Y2}";
        }

        public static bool operator ==(RectF a, RectF b)
        {
            return a.Equals(ref b);
        }

        public static bool operator !=(RectF a, RectF b)
        {
            return !a.Equals(ref b);
        }
    }
}
