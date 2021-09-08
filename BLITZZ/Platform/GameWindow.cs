using System;
using System.Collections.Generic;
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

        public static List<Display> Displays { get; private set; }

        internal static IntPtr Handle => _handle;

        internal static void Create(string title, int width, int height, bool fullscreen)
        {

            var windowFlags =
                SDL_WindowFlags.SDL_WINDOW_HIDDEN |
                SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS |
                SDL_WindowFlags.SDL_WINDOW_MOUSE_FOCUS;

            _handle = SDL_CreateWindow(
                title,
                SDL_WINDOWPOS_CENTERED,
                SDL_WINDOWPOS_CENTERED,
                width,
                height,
                windowFlags
            );

            QueryDisplayList();

            if (fullscreen)
            {
                GoFullscreen();
            }

            if (_handle == IntPtr.Zero)
            {
                throw new Exception($"Could not create Window: {SDL_GetError()}");
            }
        }


        public static Size Size
        {
            get
            {
                if (IsFullScreen)
                {
                    return new Size(CurrentDisplay.Bounds.Width, CurrentDisplay.Bounds.Height);
                }

                SDL_GetWindowSize(_handle, out var w, out var h);
                return new Size(w, h);
            }
        }

        public static Vector2 Position
        {
            get
            {
                SDL_GetWindowPosition(_handle, out var x, out var y);

                return new Vector2(x, y);
            }

            set => SDL_SetWindowPosition(_handle, (int)value.X, (int)value.Y);
        }

        public static Vector2 Center => new Vector2(Size.Width, Size.Height) / 2;

        public static string Title
        {
            get => _title;
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

                return flags.HasFlag(SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP);
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

                return Displays[index];
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

        public static void SetWindowSize(int multiplier)
        {
            if (IsFullScreen)
            {
                SDL_SetWindowFullscreen(_handle, 0);
            }

            if (multiplier > 1)
            {
                multiplier = (int) Calc.Snap(multiplier, 2.0f);
            }

            var size = new Size(
                Blitzz.Instance.GameInfo.ResolutionWidth * multiplier, 
                Blitzz.Instance.GameInfo.ResolutionHeight * multiplier
            );

            if (Size.Width >= CurrentDisplay.Bounds.Width || Size.Height >= CurrentDisplay.Bounds.Height)
            {
                GoFullscreen();
                return;
            }

            SDL_SetWindowSize(_handle, size.Width, size.Height);

            CenterOnScreen();

            GameWindowResized(size);

        }


        public static void GoFullscreen()
        {
            SDL_SetWindowFullscreen(_handle, (uint)SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP);

            GameWindowResized(new Size(CurrentDisplay.Bounds.Width, CurrentDisplay.Bounds.Height));
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

        public static void Destroy()
        {
            SDL_DestroyWindow(_handle);
        }

        internal static IntPtr GetNativeWindowHandle()
        {
            var info = new SDL_SysWMinfo();

            SDL_GetWindowWMInfo(_handle, ref info);

            return GamePlatform.RunningPlatform switch
            {
                PlatformName.Windows => info.info.win.window,
                PlatformName.Linux => info.info.x11.window,
                PlatformName.Mac => info.info.cocoa.window,
                _ => throw new Exception("Unsupported OS, could not retrive native window handle.")
            };
        }

        private static void QueryDisplayList()
        {
            var count = SDL_GetNumVideoDisplays();

            Displays = new List<Display>(count);

            for (int i = 0; i < count; ++i)
            {
                if (SDL_GetCurrentDisplayMode(i, out _) == 0)
                {
                    Displays.Add(new Display(i));
                }
                else
                {
                    _log.Error($"Failed to retrieve display {i} info: {SDL_GetError()}");
                }
            }

            foreach (var d in Displays)
                _log.Info($"  Display {d.Index} ({d.Name}) [{d.Bounds.Width}x{d.Bounds.Height}], mode {d.DesktopMode}");
        }
    }

    public enum WindowState
    {
        Normal,
        Minimized,
        Maximized
    }
}
