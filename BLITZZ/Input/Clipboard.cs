using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ.Input
{
    public static class Clipboard
    {
        public static bool HasText => SDL_HasClipboardText();

        public static string Text
        {
            get => SDL_GetClipboardText();
            set => SDL_SetClipboardText(value);
        }
    }
}
