using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ.Input
{
    public enum BatteryStatus
    {
        Unknown = SDL_JoystickPowerLevel.SDL_JOYSTICK_POWER_UNKNOWN,
        Dead = SDL_JoystickPowerLevel.SDL_JOYSTICK_POWER_EMPTY,
        Low = SDL_JoystickPowerLevel.SDL_JOYSTICK_POWER_LOW,
        Medium = SDL_JoystickPowerLevel.SDL_JOYSTICK_POWER_MEDIUM,
        High = SDL_JoystickPowerLevel.SDL_JOYSTICK_POWER_FULL,
        Charging = SDL_JoystickPowerLevel.SDL_JOYSTICK_POWER_WIRED,
        FullyCharged = SDL_JoystickPowerLevel.SDL_JOYSTICK_POWER_MAX
    }
}
