using System;
using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ.Input
{
    [Flags]
    public enum KeyModifiers
    {
        None = SDL_Keymod.KMOD_NONE,
        
        Shift = SDL_Keymod.KMOD_SHIFT,
        LeftShift = SDL_Keymod.KMOD_LSHIFT,
        RightShift = SDL_Keymod.KMOD_RSHIFT,
        
        Control = SDL_Keymod.KMOD_CTRL,
        LeftControl = SDL_Keymod.KMOD_LCTRL,
        RightControl = SDL_Keymod.KMOD_RCTRL,
        
        Alt = SDL_Keymod.KMOD_ALT,
        LeftAlt = SDL_Keymod.KMOD_LALT,
        RightAlt = SDL_Keymod.KMOD_RALT,
        
        Super = SDL_Keymod.KMOD_GUI,
        LeftSuper = SDL_Keymod.KMOD_LGUI,
        RightSuper = SDL_Keymod.KMOD_RGUI,
        
        NumLock = SDL_Keymod.KMOD_NUM,
        CapsLock = SDL_Keymod.KMOD_CAPS,
        
        Mode = SDL_Keymod.KMOD_MODE
    }
}
