using MessagePack;
using System;
using System.Numerics;

namespace BLITZZ
{
    [MessagePackObject]
    public struct SVector2
    {
        [Key(0)]
        public float X { get; set; }

        [Key(1)]
        public float Y { get; set; }

        public SVector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Vector2(SVector2 v)
        {
            return new (v.X, v.Y);
        }
    }
}
