using System;
using System.Numerics;
using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ.Input
{
    public struct MouseButtonEventArgs
    {
        public Vector2 Position { get; }
        public MouseButton Button { get; }

        public bool Pressed { get; }
        public byte ClickCount { get; }

        internal MouseButtonEventArgs(Vector2 position, byte state, uint button, byte clickCount)
        {
            Position = position;

            Button = button switch
            {
                SDL_BUTTON_LEFT => MouseButton.Left,
                SDL_BUTTON_RIGHT => MouseButton.Right,
                SDL_BUTTON_MIDDLE => MouseButton.Middle,
                SDL_BUTTON_X1 => MouseButton.X1,
                SDL_BUTTON_X2 => MouseButton.X2,
                _ => throw new Exception("Unexpected mouse button constant.")
            };

            Pressed = state == SDL_PRESSED;
            ClickCount = clickCount;
        }
    }
}
