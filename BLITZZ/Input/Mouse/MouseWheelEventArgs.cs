using System.Numerics;
using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ.Input
{
    public readonly struct MouseWheelEventArgs
    {
        public Vector2 Motion { get; }
        public bool DirectionFlipped { get; }

        internal MouseWheelEventArgs(Vector2 motion, uint direction)
        {
            Motion = motion;
            DirectionFlipped = direction == (uint)SDL_MouseWheelDirection.SDL_MOUSEWHEEL_FLIPPED;
        }
    }
}
