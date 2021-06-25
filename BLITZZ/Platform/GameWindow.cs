using System;
using System.Numerics;
using BLITZZ.Gfx;
using BLITZZ.Logging;
using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ
{
    public static class GameWindow
    {
        private static readonly Log _log = LogManager.GetForCurrentAssembly();

        internal static Action<Size> GameWindowResized;

        private const int MinWidth = 128;
        private const int MinHeight = 128;

        private static WindowState _state = WindowState.Normal;

        private static string _title;
        private static IntPtr _handle;
        //private static IntPtr _graphicsContext;

        internal static IntPtr Handle => _handle;

        internal static void Create(string title, int width, int height, bool fullscreen)
        {

            var windowFlags =
                SDL_WindowFlags.SDL_WINDOW_HIDDEN |
                SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS |
                SDL_WindowFlags.SDL_WINDOW_MOUSE_FOCUS;

            if (fullscreen)
            {
                windowFlags |= SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP;
            }

            _handle = SDL_CreateWindow(
                title,
                SDL_WINDOWPOS_CENTERED,
                SDL_WINDOWPOS_CENTERED,
                width,
                height,
                windowFlags
            );

            if (_handle == IntPtr.Zero)
            {
                throw new Exception($"Could not create Window: {SDL_GetError()}");
            }

        }

        

        public static Size Size
        {
            get
            {
                SDL_GetWindowSize(_handle, out var w, out var h);
                return new Size(w, h);
            }

            set
            {
                value.Clamp(MinWidth, CurrentDisplay.Bounds.Width, MinHeight, CurrentDisplay.Bounds.Height);

                SDL_SetWindowSize(_handle, value.Width, value.Height);
                GameWindowResized(value);
            }
        }

        public static Vector2 Position
        {
            get
            {
                SDL_GetWindowPosition(_handle, out var x, out var y);

                return new Vector2(x, y);
            }

            set
            {
                SDL_SetWindowPosition(_handle, (int)value.X, (int)value.Y);
            }
        }

        public static Vector2 Center => new Vector2(Size.Width, Size.Height) / 2;

        public static string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;

                if (_handle != IntPtr.Zero)
                {
                    SDL_SetWindowTitle(_handle, _title);
                }
            }
        }

        public static bool TopMost
        {
            get
            {
                if (_handle == IntPtr.Zero) return false;

                return ((SDL_WindowFlags)SDL_GetWindowFlags(_handle))
                    .HasFlag(SDL_WindowFlags.SDL_WINDOW_ALWAYS_ON_TOP);
            }

            set
            {
                if (_handle == IntPtr.Zero) return;

                unsafe
                {
                    var win = (SDL_Window*)_handle.ToPointer();
                    if (value)
                    {
                        win->flags |= (uint)SDL_WindowFlags.SDL_WINDOW_ALWAYS_ON_TOP;
                    }
                    else
                    {
                        win->flags &= (uint)(~SDL_WindowFlags.SDL_WINDOW_ALWAYS_ON_TOP);
                    }
                }

                SDL_SetWindowSize(_handle, Size.Width, Size.Height);
            }
        }

        public static WindowState State
        {
            get => _state;

            set
            {
                _state = value;

                if (_handle != IntPtr.Zero)
                {
                    switch (value)
                    {
                        case WindowState.Maximized:
                            var flags = (SDL_WindowFlags)SDL_GetWindowFlags(_handle);

                            if (!flags.HasFlag(SDL_WindowFlags.SDL_WINDOW_RESIZABLE))
                            {
                                _log.Warning("Refusing to maximize a non-resizable window.");
                                return;
                            }

                            SDL_MaximizeWindow(_handle);
                            break;

                        case WindowState.Minimized:
                            SDL_MinimizeWindow(_handle);
                            break;

                        case WindowState.Normal:
                            SDL_RestoreWindow(_handle);
                            break;
                    }
                }
            }
        }

        public static bool CanResize
        {
            get
            {
                var flags = (SDL_WindowFlags)SDL_GetWindowFlags(_handle);
                return flags.HasFlag(SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
            }

            set => SDL_SetWindowResizable(
                _handle,
                value
                    ? SDL_bool.SDL_TRUE
                    : SDL_bool.SDL_FALSE
            );
        }

        public static Size MaximumSize
        {
            get
            {
                SDL_GetWindowMaximumSize(_handle, out var w, out var h);
                return new Size(w, h);
            }

            set
            {
                if (value == Size.Empty)
                {
                    SDL_SetWindowMaximumSize(_handle, CurrentDisplay.Bounds.Width, CurrentDisplay.Bounds.Height);
                }
                else
                {
                    SDL_SetWindowMaximumSize(_handle, value.Width, value.Height);
                }
            }
        }

        public static Size MinimumSize
        {
            get
            {
                SDL_GetWindowMinimumSize(_handle, out var w, out var h);
                return new Size(w, h);
            }

            set
            {
                if (value == Size.Empty)
                {
                    SDL_SetWindowMinimumSize(_handle, 1, 1);
                }
                else
                {
                    SDL_SetWindowMinimumSize(_handle, value.Width, value.Height);
                }
            }
        }

        public static bool EnableBorder
        {
            get
            {
                var flags = (SDL_WindowFlags)SDL_GetWindowFlags(_handle);
                return !flags.HasFlag(SDL_WindowFlags.SDL_WINDOW_BORDERLESS);
            }
            set => SDL_SetWindowBordered(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
        }

        public static bool IsFullScreen
        {
            get
            {
                var flags = (SDL_WindowFlags)SDL_GetWindowFlags(_handle);

                return flags.HasFlag(SDL_WindowFlags.SDL_WINDOW_FULLSCREEN)
                       || flags.HasFlag(SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP);
            }
        }

        public static bool IsCursorGrabbed
        {
            get => SDL_GetWindowGrab(_handle) == SDL_bool.SDL_TRUE;
            set => SDL_SetWindowGrab(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
        }

        public static Display CurrentDisplay
        {
            get
            {
                var index = SDL_GetWindowDisplayIndex(_handle);

                if (index < 0)
                {
                    _log.Error($"Failed to retrieve window index: {SDL_GetError()}");
                    return Display.Invalid;
                }

                return Graphics.Displays[index];
            }
        }

        

        public static void Show()
           => SDL_ShowWindow(_handle);

        public static void Hide()
            => SDL_HideWindow(_handle);

        public static void CenterOnScreen()
        {
            var bounds = CurrentDisplay.Bounds;

            var targetX = bounds.Width / 2 - Size.Width / 2;
            var targetY = bounds.Height / 2 - Size.Height / 2;

            Position = new Vector2(bounds.X1 + targetX, bounds.Y1 + targetY);
        }

        public static void GoFullscreen(bool exclusive = false)
        {
            var flag = (uint)SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP;

            if (exclusive)
            {
                flag = (uint)SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
            }

            SDL_SetWindowFullscreen(_handle, flag);

            Size = new Size(
                CurrentDisplay.DesktopMode.Width,
                CurrentDisplay.DesktopMode.Height
            );
        }

        public static void GoWindowed(Size size, bool centerOnScreen = false)
        {
            SDL_SetWindowFullscreen(_handle, 0);

            Size = size;

            if (centerOnScreen)
            {
                CenterOnScreen();
            }
        }

        //public void SetIcon(Texture texture)
        //{
        //    if (texture.Disposed)
        //        throw new InvalidOperationException("The texture provided was already disposed.");

        //    if (_currentIconPtr != IntPtr.Zero)
        //        SDL_FreeSurface(_currentIconPtr);

        //    _currentIconPtr = texture.AsSdlSurface();

        //    SDL_SetWindowIcon(
        //        Handle,
        //        _currentIconPtr
        //    );
        //}

        public static void Terminate()
        {
            //SDL_GL_DeleteContext(_graphicsContext);
            SDL_DestroyWindow(_handle);
        }

        internal static IntPtr GetNativeWindowHandle()
        {
            var info = new SDL_SysWMinfo();

            SDL_GetWindowWMInfo(_handle, ref info);

            switch (GamePlatform.RunningPlatform)
            {
                case PlatformName.Windows:
                    return info.info.win.window;

                case PlatformName.Linux:
                    return info.info.x11.window;

                case PlatformName.Mac:
                    return info.info.cocoa.window;
            }

            throw new Exception("Unsupported OS, could not retrive native window handle.");
        }
    }

    public enum WindowState
    {
        Normal,
        Minimized,
        Maximized
    }
}
