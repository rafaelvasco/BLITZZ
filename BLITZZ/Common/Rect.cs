using System;
using System.Runtime.InteropServices;

namespace BLITZZ
{
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct Rect : IEquatable<Rect>
    {
        private static readonly Rect _empty = new(0, 0, 0, 0);
        public static ref readonly Rect Empty => ref _empty;

        public int X1;
        public int Y1;
        public int X2;
        public int Y2;

        public static Rect FromBox(int x, int y, int w, int h)
        {
            return new Rect(x, y, x + w, y + h);
        }

        public Rect(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public int X => X1;

        public int Y => Y1;

        public int Width => X2 - X1;

        public int Height => Y2 - Y1;

        public bool IsEmpty => Width == 0 && Height == 0;

        public bool IsRegular => X2 > X1 && Y2 > Y1;

        public Rect Normalized =>
            new(Math.Min(X1, X2), Math.Min(Y1, Y2), Math.Max(X1, X2), Math.Max(Y1, Y2));

        public int Area => Math.Abs(Width * Height);

        public Point TopLeft => new(X1, Y1);

        public Point TopRight => new(X2, Y1);

        public Point BottomLeft => new(X1, Y2);

        public Point BottomRight => new(X2, Y2);

        public int Left => X1;

        public int Right => X2;

        public int Top => Y1;

        public int Bottom => Y2;

        public Point Center => new(Math.Abs(X2 - X1) / 2, Math.Abs(Y2 - Y1) / 2);

        public override bool Equals(object obj)
        {
            return obj is Rect i && Equals(i);
        }

        public bool Equals(Rect other)
        {
            return Equals(ref other);
        }

        public bool Equals(ref Rect other)
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

        public bool Contains(ref Rect rect)
        {
            return X1 <= rect.X1 && X2 >= rect.X2 && Y1 <= rect.Y1 && Y2 >= rect.Y2;
        }

        public bool Contains(Rect rect)
        {
            return Contains(ref rect);
        }

        public bool Intersects(ref Rect other)
        {
            return X1 <= other.X2 &&
                   Y1 <= other.Y2 &&
                   X2 >= other.X1 &&
                   Y2 >= other.Y1;
        }

        public bool Intersects(Rect other)
        {
            return Intersects(ref other);
        }

        public void CopyTo(out Rect other)
        {
            other.X1 = X1;
            other.Y1 = Y1;
            other.X2 = X2;
            other.Y2 = Y2;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X1, Y1, X2, Y2);
        }

        public Rect Inflated(int x, int y)
        {
            var copy = this;

            copy.X1 -= x;
            copy.X2 += x;
            copy.Y1 -= y;
            copy.Y2 += y;

            return copy;
        }

        public Rect Translated(int dx, int dy)
        {
            var copy = this;

            copy.X1 += dx;
            copy.X2 += dx;
            copy.Y1 += dy;
            copy.Y2 += dy;

            return copy;
        }

        public Rect Inflated(int amount)
        {
            return Inflated(amount, amount);
        }

        public override string ToString()
        {
            return $"{X1},{Y1},{Width},{Height}";
        }

        public static bool operator ==(Rect a, Rect b)
        {
            return a.Equals(ref b);
        }

        public static bool operator !=(Rect a, Rect b)
        {
            return !a.Equals(ref b);
        }

        public static implicit operator RectF(Rect rect)
        {
            return new RectF(rect.X1, rect.Y1, rect.X2, rect.Y2);
        }

        public static implicit operator Rect((int X, int Y, int Width, int Height) rectTuple)
        {
            return FromBox(rectTuple.X, rectTuple.Y, rectTuple.Width, rectTuple.Height);
        }

        public static implicit operator Rect(SRect v)
        {
            throw new NotImplementedException();
        }
    }
}
