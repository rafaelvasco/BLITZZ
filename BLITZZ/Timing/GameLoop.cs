using BLITZZ.Gfx;
using BLITZZ.Native.SDL;
using System;

namespace BLITZZ
{
    public static unsafe class GameLoop
    {
        public static double UpdateRate { get; set; } = 60.0;
        public static bool UnlockFrameRate { get; set; } = true;

        private const int TimeHistoryCount = 4;
        private const int UpdateMult = 1;

        private static bool _resync = true;
        private static double _fixedDeltatime;
        private static double _desiredFrametime;
        private static double _vsyncMaxError;
        private static double[] _snapFreqs;
        private static double[] _timeAverager;

        private static double _prevFrameTime;
        private static double _frameAccum;


        internal static void Initialize()
        {

            _fixedDeltatime = 1.0 / UpdateRate;
            _desiredFrametime = SDL2.SDL_GetPerformanceFrequency() / UpdateRate;
            _vsyncMaxError = SDL2.SDL_GetPerformanceFrequency() * 0.0002;

            double time_60Hz = SDL2.SDL_GetPerformanceFrequency() / 60;
            _snapFreqs = new[]
            {
                time_60Hz, //60fps
                time_60Hz * 2, //30fps
                time_60Hz * 3, //20fps
                time_60Hz * 4, //15fps
                (time_60Hz + 1) / 2 //120fps
            };

            _timeAverager = new double[TimeHistoryCount];

            for (int i = 0; i < TimeHistoryCount; i++)
            {
                _timeAverager[i] = _desiredFrametime;
            }
        }

        internal static void OnStart()
        {
            _prevFrameTime = SDL2.SDL_GetPerformanceCounter();
            _frameAccum = 0;
        } 

        internal static void Tick(Blitzz engine)
        {
            delegate*<ulong> perfCounter = &SDL2.SDL_GetPerformanceCounter;
            delegate*<ulong> perfFreq = &SDL2.SDL_GetPerformanceFrequency;

            double current_frame_time = perfCounter();

            double delta_time = current_frame_time - _prevFrameTime;

            _prevFrameTime = current_frame_time;

            // Handle unexpected timer anomalies (overflow, extra slow frames, etc)
            if (delta_time > _desiredFrametime * 8)
            {
                delta_time = _desiredFrametime;
            }

            if (delta_time < 0)
            {
                delta_time = 0;
            }

            // VSync Time Snapping
            for (int i = 0; i < _snapFreqs.Length; ++i)
            {
                var snap_freq = _snapFreqs[i];

                if (Math.Abs(delta_time - snap_freq) < _vsyncMaxError)
                {
                    delta_time = snap_freq;
                    break;
                }
            }

            // Delta Time Averaging
            for (int i = 0; i < TimeHistoryCount - 1; ++i)
            {
                _timeAverager[i] = _timeAverager[i + 1];
            }

            _timeAverager[TimeHistoryCount - 1] = delta_time;

            delta_time = 0;

            for (int i = 0; i < TimeHistoryCount; ++i)
            {
                delta_time += _timeAverager[i];
            }

            delta_time /= TimeHistoryCount;

            // Add To Accumulator
            _frameAccum += delta_time;

            // Spiral of Death Protection
            if (_frameAccum > _desiredFrametime * 8)
            {
                _resync = true;
            }

            // Timer Resync Requested
            if (_resync)
            {
                _frameAccum = 0;
                delta_time = _desiredFrametime;
                _resync = false;
            }

            // Process Events and Input

            GamePlatform.ProcessEvents();

            // Unlocked Frame Rate, Interpolation Enabled
            if (UnlockFrameRate)
            {
                double consumed_delta_time = delta_time;

                while (_frameAccum >= _desiredFrametime)
                {
                    engine.FixedUpdate((float)_fixedDeltatime);

                    // Cap Variable Update's dt to not be larger than fixed update, 
                    // and interleave it (so game state can always get animation frame it needs)
                    if (consumed_delta_time > _desiredFrametime)
                    {
                        engine.Update((float)_fixedDeltatime);

                        consumed_delta_time -= _desiredFrametime;
                    }

                    _frameAccum -= _desiredFrametime;
                }

                engine.Update((float)(consumed_delta_time / perfFreq()));

                //if (Engine.Canvas.NeedsResetDisplay)
                //{
                //    Engine.Canvas.HandleDisplayChange();
                //    OnDisplayResize();
                //}

                //Draw(Engine.Canvas, (float)(_frameAccum / _desiredFrametime));

                Graphics.Frame();

            }
            // Locked Frame Rate, No Interpolation
            else
            {
                while (_frameAccum >= _desiredFrametime * UpdateMult)
                {
                    for (int i = 0; i < UpdateMult; ++i)
                    {
                        engine.FixedUpdate((float)_fixedDeltatime);
                        engine.Update((float)_fixedDeltatime);

                        _frameAccum -= _desiredFrametime;
                    }
                }

                //if (Engine.Canvas.NeedsResetDisplay)
                //{
                //    Engine.Canvas.HandleDisplayChange();
                //    OnDisplayResize();
                //}

                //Draw(Engine.Canvas, 1.0f);

                Graphics.Frame();
            }
        }
    }
}
