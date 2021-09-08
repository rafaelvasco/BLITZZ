using System;
using System.Numerics;
using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ.Input
{
    public static class Mouse
    {
        public static event Action<MouseButtonEventArgs> MousePressed;
        public static event Action<MouseButtonEventArgs> MouseReleased;
        public static event Action<MouseMoveEventArgs> MouseMoved;
        public static event Action<MouseWheelEventArgs> MouseWheel;

        private static readonly MouseMoveEventArgs _mouseMoveArgs = new(Vector2.Zero, Vector2.Zero, default);

        public static bool IsRelativeModeEnabled
        {
            get => SDL_GetRelativeMouseMode() == SDL_bool.SDL_TRUE;
            set => SDL_SetRelativeMouseMode(value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
        }

        public static Vector2 GetPosition()
        {
            _ = SDL_GetMouseState(out var x, out var y);
            return new Vector2(x, y);
        }

        public static bool IsButtonDown(MouseButton button)
        {
            var state = SDL_GetMouseState(out _, out _);
            var mask = SDL_BUTTON((uint)button);

            return (state & mask) != 0;
        }

        public static bool IsButtonUp(MouseButton button)
            => !IsButtonDown(button);

        internal static void ProcessMouseButtonEvent(SDL_Event ev)
        {
            if (MousePressed == null && MouseReleased == null)
            {
                return;
            }

            var mouseEventArgs = new MouseButtonEventArgs(
                new Vector2(ev.button.x, ev.button.y),
                ev.button.state,
                ev.button.button,
                ev.button.clicks
            );

            switch (ev.type)
            {
                case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    MousePressed?.Invoke(mouseEventArgs);
                    break;
                case SDL_EventType.SDL_MOUSEBUTTONUP:
                    MouseReleased?.Invoke(mouseEventArgs);
                    break;
            }
        }

        internal static void ProcessMouseMotionEvent(SDL_Event ev)
        {
            if (MouseMoved == null)
            {
                return;
            }

            _mouseMoveArgs.Position = new Vector2(ev.motion.x, ev.motion.y);
            _mouseMoveArgs.Delta = new Vector2(ev.motion.xrel, ev.motion.yrel);
            _mouseMoveArgs.ButtonState = new MouseButtonState(ev.motion.state);

            MouseMoved(_mouseMoveArgs);
        }

        internal static void ProcessMouseWheelEvent(SDL_Event ev)
        {
            if (MouseWheel == null)
            {
                return;
            }

            var eventArgs = new MouseWheelEventArgs(
                new Vector2(ev.wheel.x, ev.wheel.y),
                ev.wheel.direction
            );

            MouseWheel(eventArgs);
        }
    }
}
