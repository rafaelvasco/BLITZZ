﻿using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ.Input
{
    public struct MouseButtonState
    {
        public bool Left { get; }
        public bool Right { get; }
        public bool Middle { get; }
        public bool X1 { get; }
        public bool X2 { get; }

        internal MouseButtonState(uint sdlMask)
        {
            Left = (sdlMask & SDL_BUTTON_LMASK) != 0;
            Right = (sdlMask & SDL_BUTTON_RMASK) != 0;
            Middle = (sdlMask & SDL_BUTTON_MMASK) != 0;
            X1 = (sdlMask & SDL_BUTTON_X1MASK) != 0;
            X2 = (sdlMask & SDL_BUTTON_X2MASK) != 0;
        }
    }
}
