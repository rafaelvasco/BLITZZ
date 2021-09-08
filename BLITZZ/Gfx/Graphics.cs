using BLITZZ.Logging;
using static BLITZZ.Native.SDL.SDL2;
using BLITZZ.Native.BGFX;
using BLITZZ.Content;
using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using STB;

namespace BLITZZ.Gfx
{

    public static unsafe class Graphics
    {
       

        private static readonly Log _log = LogManager.GetForCurrentAssembly();

        private static StateFlags _renderBaseState;

        private static StateFlags _blendState;

        private static bool _vSyncEnabled = true;
        
        public static TextureFormat TextureFormat { get; private set; }

        public static RendererType Renderer { get; private set; }

        public static bool VSyncEnabled
        {
            get => _vSyncEnabled;
            set
            {
                if (_vSyncEnabled != value)
                {
                    _vSyncEnabled = value;

                    var flags = ResetFlags.None;

                    if (_vSyncEnabled)
                    {
                        flags |= ResetFlags.Vsync;
                    }

                    Bgfx.Reset(GameWindow.Size.Width, GameWindow.Size.Height, flags, TextureFormat);
                }
            }
        }


        public static float ScreenGamma
        {
            get => SDL_GetWindowBrightness(GameWindow.Handle);
            set => SDL_SetWindowBrightness(GameWindow.Handle, value);
        }

        internal static void Initialize()
        {
            InitGraphicsContext();

            GameWindow.GameWindowResized = OnGameWindowResized;

            _log.Info(" Available displays:");
            
        }

        private static void OnGameWindowResized(Size obj)
        {
            _log.Info($"Window Resized To: {obj.Width},{obj.Height}");

            SetDisplayMode(obj.Width, obj.Height, VSyncEnabled, TextureFormat);
            SetupView(0, Rect.FromBox(0, 0, obj.Width, obj.Height));

        }

        public static void SaveScreenshot(string path)
        {
            Bgfx.RequestScreenShot(path);
        }

        public static void SetBlendMode(BlendState blend)
        {
            _blendState = blend.StateFlags;
        }

        private static void InitGraphicsContext()
        {
            Bgfx.SetPlatformData(GameWindow.GetNativeWindowHandle());

            Renderer = GamePlatform.RunningPlatform switch
            {
                PlatformName.Windows => RendererType.Direct3D11,
                PlatformName.Mac => RendererType.Metal,
                PlatformName.Linux => RendererType.OpenGL,
                _ => Renderer
            };

            _log.Info($"Init Graphics Context with Size: {GameWindow.Size.Width}, {GameWindow.Size.Height}");

            Bgfx.Initialize(GameWindow.Size.Width, GameWindow.Size.Height, Renderer, callbackHandler: new BgfxCallbackHandler());

            TextureFormat = GamePlatform.RunningPlatform switch
            {
                PlatformName.Windows => TextureFormat.BGRA8,
                PlatformName.Mac => TextureFormat.RGBA8,
                PlatformName.Linux => TextureFormat.RGBA8,
                _ => TextureFormat.BGRA8
            };

            InitGraphicsState();
        }

        private static void InitGraphicsState()
        {
            _renderBaseState = StateFlags.WriteRgb | StateFlags.WriteA | StateFlags.WriteZ;

            Bgfx.SetDebug(DebugFlags.Text);

            SetViewClear(0, 0x6495edff);
            SetViewRect(0, 0, 0, GameWindow.Size.Width, GameWindow.Size.Height);
            SetDisplayMode(GameWindow.Size.Width, GameWindow.Size.Height, vsync: _vSyncEnabled, TextureFormat);
        }

        public static void SetDisplayMode(int width, int height, bool vsync, TextureFormat textFormat)
        {
            Bgfx.Reset(width, height, vsync ? ResetFlags.Vsync : ResetFlags.None, textFormat);
        }

        public static void SetViewRect(byte index, int x, int y, int w, int h)
        {
            Bgfx.SetViewRect(index, x, y, w, h);
        }

        public static void SetViewProjection(byte index, ref Matrix4x4 projMatrix)
        {
            Bgfx.SetViewTransformMatrices(index, ref projMatrix.M11, ref Unsafe.NullRef<float>());
        }

        public static void SetViewClear(byte index, Color color)
        {
            Bgfx.SetViewClear(index, ClearFlags.Color | ClearFlags.Depth, color);
        }

        public static void SetupView(byte index, Rect viewport)
        {
            var projection = Matrix4x4.CreateOrthographicOffCenter(viewport.Left, viewport.Right, viewport.Bottom,
                viewport.Top, 0.0f, 10000.0f);

            SetViewRect(index, viewport.Left, viewport.Top, viewport.Width, viewport.Height);
            SetViewProjection(index, ref projection);
        }


        public static void SubmitVertexStream<T>(DynamicVertexStream<T> stream,  ShaderProgram shader, int vertexCount) where T : struct
        {
            Bgfx.SetState(_renderBaseState | _blendState);

            stream.SubmitSpan(0, vertexCount, 0, (vertexCount/4)*6, Vertex.VertexLayout);
            
            shader.ApplyTextures();

            Bgfx.Submit(0, shader.Program);
        }


        internal static void Frame()
        {
            Bgfx.Touch(0);
            Bgfx.Frame();
        }


        public static void Terminate()
        {
            Bgfx.Shutdown();
        }

        private class BgfxCallbackHandler : Bgfx.DefaultCallbackHandler
        {
            private AviWriter aviWriter;

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
