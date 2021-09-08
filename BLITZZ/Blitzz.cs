using System;
using System.Threading;
using BLITZZ.Content;
using BLITZZ.Gfx;
using BLITZZ.Input;
using BLITZZ.Logging;
using BLITZZ.Threading;

namespace BLITZZ
{
    public sealed class Blitzz : IDisposable
    {
        private readonly Log _log = LogManager.GetForCurrentAssembly();

        public static Blitzz Instance { get; private set; }

        public string GameTitle { get; }

        public GameInfo GameInfo { get; }

        public bool Running { get; private set; }

        public Blitzz()
        {
            Instance = this;

            _log.Info("BLITZZ Engine Starting");

            var gameInfo = AssetLoader.LoadGameInfo();

            GameInfo.AssumeDefaults(ref gameInfo);

            GameTitle = gameInfo.Title;

            GameInfo = gameInfo;

            try
            {
                InitializeThreading();
                InitializePlatform();
                InitializeGraphics(gameInfo.ResolutionWidth, gameInfo.ResolutionHeight, gameInfo.StartFullscreen);
                InitializeAudio();
                InitializeContent();
                InitializeInput();
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

        public Action Draw;

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

        private void InitializeInput()
        {
            var gameControllerDb = Assets.Get<TextFile>("gamecontrollerdb");

            Controller.SetMappingsDB(gameControllerDb.Text);

#if DEBUG
            Keyboard.KeyPressed += (args =>
            {
                if (args.KeyCode == KeyCode.Escape)
                {
                    Quit();
                }
                else if (args.KeyCode == KeyCode.F11)
                {
                    if (GameWindow.IsFullScreen)
                    {
                        GameWindow.SetWindowSize(1);
                    }
                    else
                    {
                        GameWindow.GoFullscreen();
                    }
                }
            });
#endif
            
        }

        private static void InitializeAudio()
        {
        }

        public void Dispose()
        {
            Assets.FreeEverything();
            Graphics.Terminate();
            GameWindow.Destroy();
            GamePlatform.Terminate();
        }

    }
}
