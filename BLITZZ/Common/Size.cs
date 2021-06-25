using System;

namespace BLITZZ
{
    public struct Size : IEquatable<Size>
    {
        private static readonly Size _empty = new(0, 0);
        public static ref readonly Size Empty => ref _empty;

        public int Width;
        public int Height;

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public Size(int size)
        {
            Width = size;
            Height = size;
        }

        public bool Equals(Size other)
        {
            return Width == other.Width && Height == other.Height;
        }

        public override bool Equals(object obj)
        {
            return obj is Size size && Equals(size);
        }

        public static bool operator ==(Size left, Size right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Size left, Size right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return Width.GetHashCode() + Height.GetHashCode();
        }
    }
}
