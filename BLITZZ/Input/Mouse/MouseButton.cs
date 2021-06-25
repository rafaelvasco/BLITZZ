using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ.Input
{
    public enum MouseButton : uint
    {
        Left = SDL_BUTTON_LEFT,
        Right = SDL_BUTTON_RIGHT,
        Middle = SDL_BUTTON_MIDDLE,
        X1 = SDL_BUTTON_X1,
        X2 = SDL_BUTTON_X2
    }
}
