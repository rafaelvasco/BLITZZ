using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ.Input
{
    public enum ControllerAxis
    {
        LeftStickX = SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTX,
        LeftStickY = SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTY,
        LeftTrigger = SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERLEFT,
        RightStickX = SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTX,
        RightStickY = SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTY,
        RightTrigger = SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERRIGHT
    }
}
