using BLITZZ.Logging;
using static BLITZZ.Native.SDL.SDL2;
using System.Collections.Generic;
using BLITZZ.Native.BGFX;
using BLITZZ.Content;
using System;
using System.IO;
using STB;

namespace BLITZZ.Gfx
{

    public static unsafe class Graphics
    {
        private static readonly Log _log = LogManager.GetForCurrentAssembly();

        private static VerticalSyncMode _vSyncMode;

        private static List<Display> _displays;

        public static RendererType Renderer { get; private set; }

        public static VerticalSyncMode VerticalSyncMode
        {
            get => _vSyncMode;
            set
            {
                if (_vSyncMode != value)
                {
                    _vSyncMode = value;
                }
            }
        }

        public static bool IsAdaptiveVSyncSupported { get; private set; }

        public static List<Display> Displays => _displays;

        public static float ScreenGamma
        {
            get => SDL_GetWindowBrightness(GameWindow.Handle);
            set => SDL_SetWindowBrightness(GameWindow.Handle, value);
        }

        internal static void Initialize()
        {
            InitGraphicsContext();
            QueryDisplayList();


            _log.Info(" Available displays:");
            foreach (var d in _displays)
                _log.Info($"  Display {d.Index} ({d.Name}) [{d.Bounds.Width}x{d.Bounds.Height}], mode {d.DesktopMode}");
        }


        public static void SaveScreenshot(string path)
        {
            Bgfx.RequestScreenShot(path);
        }

        private static void InitGraphicsContext()
        {
            Bgfx.SetPlatformData(GameWindow.GetNativeWindowHandle());

            switch (GamePlatform.RunningPlatform)
            {
                case PlatformName.Windows:
                    Renderer = RendererType.Direct3D11;
                    break;
                case PlatformName.Mac:
                    Renderer = RendererType.Metal;
                    break;
                case PlatformName.Linux:
                    Renderer = RendererType.OpenGL;
                    break;
            }

            var init = Bgfx.Initialize(GameWindow.Size.Width, GameWindow.Size.Height, Renderer, callbackHandler: new BgfxCallbackHandler());

            Bgfx.SetViewClear(0, ClearFlags.Color | ClearFlags.Depth, color:0x6495edff, depth: 0, stencil: 0);
            Bgfx.SetViewRect(0, 0, 0, GameWindow.Size.Width, GameWindow.Size.Height);
            
            Bgfx.SetViewMode(0, ViewMode.Sequential);
            Bgfx.SetViewMode(255, ViewMode.Sequential);
            Bgfx.SetDebug(DebugFlags.None);
            Bgfx.Reset(GameWindow.Size.Width, GameWindow.Size.Height, ResetFlags.Vsync, init.resolution.format);
        }


        internal static void Frame()
        {
            Bgfx.Touch(0);
            Bgfx.Frame();
        }


        private static void QueryDisplayList()
        {
            var count = SDL_GetNumVideoDisplays();

            _displays = new List<Display>(count);

            for (int i = 0; i < count; ++i)
            {
                if (SDL_GetCurrentDisplayMode(i, out _) == 0)
                {
                    _displays.Add(new Display(i));
                }
                else
                {
                    _log.Error($"Failed to retrieve display {i} info: {SDL_GetError()}");
                }
            }
        }

        public static void Terminate()
        {
            Bgfx.Shutdown();
        }

        class BgfxCallbackHandler : Bgfx.DefaultCallbackHandler
        {
            AviWriter aviWriter;

            public override void SaveScreenShot(string path, int width, int height, int pitch, IntPtr data, int size, bool flipVertical)
            {
                using var stream = File.OpenWrite(path);

                var image_writer = new ImageWriter();
                image_writer.WritePng(data.ToPointer(), width, height, ColorComponents.RedGreenBlueAlpha, stream);
            }

            public override void CaptureStarted(int width, int height, int pitch, TextureFormat format, bool flipVertical)
            {
                aviWriter = new AviWriter(File.Create("Capture/capture.avi", pitch * height), width, height, 60, !flipVertical);
            }

            public override void CaptureFrame(IntPtr data, int size)
            {
                aviWriter.WriteFrame(data, size);
            }

            public override void CaptureFinished()
            {
                aviWriter.Close();
                aviWriter = null;
            }
        }
    }
}
