using System;
using System.Threading;
using BLITZZ.Content;
using BLITZZ.Gfx;
using BLITZZ.Logging;
using BLITZZ.Threading;

namespace BLITZZ
{
    public sealed class Blitzz : IDisposable
    {
        private readonly Log _log = LogManager.GetForCurrentAssembly();

        public string GameTitle { get; private set; }

        public GameInfo GameInfo { get; private set; }

        public bool Running { get; private set; }

        public Blitzz(string title, int width, int height, bool fullscreen = false)
        {
            _log.Info("BLITZZ Engine Starting");

            GameTitle = title;

            GameInfo = AssetLoader.LoadGameInfo();

            try
            {
                InitializeThreading();
                InitializePlatform();
                InitializeGraphics(width, height, fullscreen);
                InitializeAudio();
                InitializeContent();
            }
            catch (Exception e)
            {
                _log.Exception(e);
                Environment.Exit(1);
            }

            GameWindow.Show();
            GameLoop.Initialize();

            AppDomain.CurrentDomain.UnhandledException += OnDomainUnhandledException;
        }

        public Action LoadContent;

        public Action<float> Update;

        public Action<float> FixedUpdate;

        private void OnDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _log.Error(
                $"[F] \n\n{e.ExceptionObject}"
            );
        }

        public void Run()
        {
            if (Running)
            {
                return;
            }

            Running = true;

            LoadContent?.Invoke();

            GameLoop.OnStart();

            while (Running)
            {
                GameLoop.Tick(this);
            }
        }

        public void Quit()
        {
            Running = false;
        }

        private void InitializePlatform()
        {
            GamePlatform.Initialize();
            GamePlatform.OnQuit = Quit;
        }

        private static void InitializeThreading()
        {
            Dispatcher.MainThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        private void InitializeGraphics(int windowWidth, int windowHeight, bool fullscreen)
        {
            GameWindow.Create(GameTitle, windowWidth, windowHeight, fullscreen);

            Graphics.Initialize();
        }

        private void InitializeContent()
        {
            Assets.Initialize(GameInfo);
            Blitter.GetDefaultAssets();
        }

        private void InitializeAudio()
        {
        }

        public void Dispose()
        {
            Assets.FreeEverything();
            Graphics.Terminate();
            GameWindow.Terminate();
            GamePlatform.Terminate();
        }

    }
}
