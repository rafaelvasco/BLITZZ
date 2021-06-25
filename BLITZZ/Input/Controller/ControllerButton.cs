using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ.Input
{
    public enum ControllerButton
    {
        X = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X,
        Y = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y,
        A = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A,
        B = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_B,
        View = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_BACK,
        Menu = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_START,
        Xbox = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_GUIDE,
        LeftStick = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSTICK,
        RightStick = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSTICK,
        DpadLeft = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_LEFT,
        DpadRight = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_RIGHT,
        DpadUp = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_UP,
        DpadDown = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_DOWN,
        RightBumper = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSHOULDER,
        LeftBumper = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSHOULDER
    }
}
