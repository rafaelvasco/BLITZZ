using BLITZZ.Audio;
using BLITZZ.Input;
using BLITZZ.Native;
using System;
using System.Runtime.InteropServices;
using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ
{
    public static class GamePlatform
    {
        public static PlatformName RunningPlatform { get; private set; }

        internal static Action OnQuit;

        internal static void Initialize()
        {
            DetectRunningPlatform();

            SDL_SetMainReady();

            if (RunningPlatform == PlatformName.Windows)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    SDL_SetHint(SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");
                }

                SetupConsoleOutput();
            }

            SDL_Init(
                SDL_INIT_VIDEO |
                SDL_INIT_GAMECONTROLLER |
                SDL_INIT_JOYSTICK |
                SDL_INIT_HAPTIC
            );

            HookupPlatformEvents();
        }

        internal static void ProcessEvents()
        {
            while (SDL_PollEvent(out var ev) != 0)
            {
                PlatformEvents.Process(ev);
            }
        }

        internal static void Terminate()
        {
            SDL_Quit();
        }

        private static void HookupPlatformEvents()
        {
            PlatformEvents.Discard(
               SDL_EventType.SDL_JOYAXISMOTION,
               SDL_EventType.SDL_JOYDEVICEADDED,
               SDL_EventType.SDL_JOYDEVICEREMOVED,
               SDL_EventType.SDL_JOYBUTTONUP,
               SDL_EventType.SDL_JOYBUTTONDOWN,
               SDL_EventType.SDL_JOYHATMOTION,
               SDL_EventType.SDL_JOYBALLMOTION,
               SDL_EventType.SDL_KEYMAPCHANGED,
               SDL_EventType.SDL_TEXTEDITING
           );

            PlatformEvents.On(SDL_EventType.SDL_QUIT, (SDL_Event ev) => { OnQuit(); });

            PlatformEvents.On(SDL_EventType.SDL_MOUSEBUTTONDOWN, Mouse.ProcessMouseButtonEvent);
            PlatformEvents.On(SDL_EventType.SDL_MOUSEBUTTONUP, Mouse.ProcessMouseButtonEvent);
            PlatformEvents.On(SDL_EventType.SDL_MOUSEMOTION, Mouse.ProcessMouseMotionEvent);
            PlatformEvents.On(SDL_EventType.SDL_MOUSEWHEEL, Mouse.ProcessMouseWheelEvent);

            PlatformEvents.On(SDL_EventType.SDL_KEYUP, Keyboard.ProcessKeyEvent);
            PlatformEvents.On(SDL_EventType.SDL_KEYDOWN, Keyboard.ProcessKeyEvent);
            PlatformEvents.On(SDL_EventType.SDL_TEXTINPUT, Keyboard.ProcessTextInputEvent);

            PlatformEvents.On(SDL_EventType.SDL_CONTROLLERDEVICEADDED, Controller.ProcessControllerConected);
            PlatformEvents.On(SDL_EventType.SDL_CONTROLLERDEVICEREMOVED, Controller.ProcessControllerDisconnected);
            PlatformEvents.On(SDL_EventType.SDL_CONTROLLERBUTTONDOWN, Controller.ProcessButtonDown);
            PlatformEvents.On(SDL_EventType.SDL_CONTROLLERBUTTONUP, Controller.ProcessButtonUp);
            PlatformEvents.On(SDL_EventType.SDL_CONTROLLERAXISMOTION, Controller.ProcessAxisMotion);

            PlatformEvents.On(SDL_EventType.SDL_AUDIODEVICEADDED, AudioManager.ProcessAudioEvent);
            PlatformEvents.On(SDL_EventType.SDL_AUDIODEVICEREMOVED, AudioManager.ProcessAudioEvent);
        }



        private static void SetupConsoleOutput()
        {
            var stdHandle = WindowsNative.GetStdHandle(WindowsNative.STD_OUTPUT_HANDLE);

            WindowsNative.GetConsoleMode(stdHandle, out var consoleMode);
            consoleMode |= WindowsNative.ENABLE_PROCESSED_OUTPUT;
            consoleMode |= WindowsNative.ENABLE_VIRTUAL_TERMINAL_PROCESSING;

            WindowsNative.SetConsoleMode(stdHandle, consoleMode);
        }

        private static void DetectRunningPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                RunningPlatform = PlatformName.Windows;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                RunningPlatform = PlatformName.Mac;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                RunningPlatform = PlatformName.Linux;
            }
            else
            {
                throw new Exception("BLITZZ Engine is currently not supported on this Platform");
            }
        }
    }
}
