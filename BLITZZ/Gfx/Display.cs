﻿using BLITZZ.Logging;
using BLITZZ.Native.SDL;
using System;
using System.Collections.Generic;

namespace BLITZZ.Gfx
{
    public class Display
    {
        private Log Log { get; } = LogManager.GetForCurrentAssembly();

        public static readonly Display Invalid = new(-1);

        public int Index { get; }
        public bool IsValid => Index >= 0;

        public string Name
        {
            get
            {
                EnsureValid();
                var name = SDL2.SDL_GetDisplayName(Index);

                if (name == null)
                {
                    Log.Error($"Failed to retrieve display {Index} name: {SDL2.SDL_GetError()}");
                    return string.Empty;
                }

                return name;
            }
        }

        public Rect Bounds
        {
            get
            {
                EnsureValid();

                if (SDL2.SDL_GetDisplayBounds(Index, out var rect) < 0)
                {
                    Log.Error($"Failed to retrieve display {Index} bounds: {SDL2.SDL_GetError()}");
                    return Rect.Empty;
                }

                return Rect.FromBox(
                    rect.x,
                    rect.y,
                    rect.w,
                    rect.h
                );
            }
        }

        public Rect DesktopBounds
        {
            get
            {
                EnsureValid();

                if (SDL2.SDL_GetDisplayUsableBounds(Index, out var rect) < 0)
                {
                    Log.Error($"Failed to retrieve display {Index} desktop bounds: {SDL2.SDL_GetError()}");
                    return Rect.Empty;
                }

                return Rect.FromBox(
                    rect.x,
                    rect.y,
                    rect.w,
                    rect.h
                );
            }
        }

        public DisplayDpi DPI
        {
            get
            {
                EnsureValid();

                if (SDL2.SDL_GetDisplayDPI(Index, out var d, out var h, out var v) < 0)
                {
                    Log.Error($"Failed to retrieve display {Index} DPI info: {SDL2.SDL_GetError()}");
                    return DisplayDpi.None;
                }

                return new DisplayDpi(d, h, v);
            }
        }

        public DisplayMode DesktopMode
        {
            get
            {
                EnsureValid();

                if (SDL2.SDL_GetDesktopDisplayMode(Index, out var mode) < 0)
                {
                    Log.Error($"Failed to retrieve desktop display mode: {SDL2.SDL_GetError()}");
                    return DisplayMode.Invalid;
                }

                return new DisplayMode(mode.w, mode.h, mode.refresh_rate);
            }
        }

        internal Display(int index)
        {
            Index = index;
        }

        public List<DisplayMode> QuerySupportedDisplayModes()
        {
            var ret = new List<DisplayMode>();
            var displayModeCount = SDL2.SDL_GetNumDisplayModes(Index);

            for (var i = 0; i < displayModeCount; i++)
            {
                SDL2.SDL_GetDisplayMode(Index, i, out var mode);
                ret.Add(new DisplayMode(mode.w, mode.h, mode.refresh_rate));
            }

            return ret;
        }

        public DisplayMode GetClosestSupportedMode(int width, int height, int refreshRate = 0)
        {
            var mode = new SDL2.SDL_DisplayMode
            {
                w = width,
                h = height,
                refresh_rate = refreshRate
            };

            var ret = SDL2.SDL_GetClosestDisplayMode(
                Index,
                ref mode,
                out var closest
            );

            if (ret == IntPtr.Zero)
            {
                Log.Error($"Failed to retrieve a closest display mode for {width}x{height}");
                return DisplayMode.Invalid;
            }

            return new DisplayMode(closest.w, closest.h, closest.refresh_rate);
        }

        private void EnsureValid()
        {
            if (!IsValid)
                throw new InvalidOperationException("This operation requires a valid display.");
        }
    }
}
