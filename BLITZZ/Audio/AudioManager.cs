using BLITZZ.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using static BLITZZ.Native.SDL.SDL2;
using static BLITZZ.Native.SDL.SDL2_nmix;
using static BLITZZ.Native.SDL.SDL2_sound;

namespace BLITZZ.Audio
{
    public static class AudioManager
    {
        private static readonly Log _log = LogManager.GetForCurrentAssembly();

        private static List<AudioDevice> _devices;
        private static List<Decoder> _decoders = new();

        private static bool _mixerInitialized;
        private static bool _backendInitialized;
        private static bool _playbackPaused;

        public static event Action<AudioDeviceEventArgs> DeviceConnected;
        public static event Action<AudioDeviceEventArgs> DeviceDisconnected;

        public static IReadOnlyList<AudioDevice> Devices => _devices;
        public static IReadOnlyList<Decoder> Decoders => _decoders;

        public static int Frequency { get; private set; }
        public static int SampleCount { get; private set; }


        public static float MasterVolume
        {
            get => NMIX_GetMasterGain();
            set
            {
                var vol = value;

                if (vol < 0f)
                    vol = 0f;

                if (vol > 2f)
                    vol = 2f;

                NMIX_SetMasterGain(vol);
            }
        }

        public static AudioDevice CurrentOutputDevice => _devices.FirstOrDefault(
            x => x.Index == NMIX_GetAudioDevice()
        );

        internal static void Initialize(int frequency = 44100, int sampleCount = 1024)
        {
            _devices = new();
            _decoders = new();

            Open(null, frequency, sampleCount);
        }


        public static void PauseAll()
        {
            _playbackPaused = !_playbackPaused;
            NMIX_PausePlayback(_playbackPaused);
        }

        public static void Open(AudioDevice device = null, int frequency = 44100, int sampleCount = 1024)
        {
            Close();
            EnumerateDevices();
            
            Frequency = frequency;
            SampleCount = sampleCount;

            if (Sound_Init() < 0)
            {
                _log.Error($"Failed to initialize audio backend: {SDL_GetError()}");
                return;
            }
            _backendInitialized = true;
            EnumerateDecoders();

            if (NMIX_OpenAudio(device?.Name, Frequency, SampleCount) < 0)
            {
                _log.Error($"Failed to initialize audio mixer: {SDL_GetError()}");
                return;
            }
            _mixerInitialized = true;
        }

        public static void Close()
        {            
            if (_mixerInitialized)
            {
                if (NMIX_CloseAudio() < 0)
                {
                    _log.Error($"Failed to stop the audio mixer: {SDL_GetError()}");
                    return;
                }
                _mixerInitialized = false;
            }

            if (_backendInitialized)
            {
                if (Sound_Quit() < 0)
                {
                    _log.Error($"Failed to stop the audio backend: {SDL_GetError()}");
                    return;
                }
                _backendInitialized = false;
            }
        }

        public static void EnumerateDevices()
        {
            _devices.Clear();

            var numberOfOutputDevices = SDL_GetNumAudioDevices(0);
            var numberOfInputDevices = SDL_GetNumAudioDevices(1);

            for (var i = 0; i < numberOfOutputDevices; i++)
                _devices.Add(new AudioDevice(i, false));

            for (var i = 0; i < numberOfInputDevices; i++)
                _devices.Add(new AudioDevice(i, true));
        }

        private static void EnumerateDecoders()
        {
            _decoders.Clear();

            unsafe
            {
                var p = (Sound_DecoderInfo**)Sound_AvailableDecoders();

                if (p == null)
                    return;

                for (var i = 0;; i++)
                {
                    if (p[i] == null)
                        break;

                    var decoder = Marshal.PtrToStructure<Sound_DecoderInfo>(new IntPtr(p[i]));
                    var p2 = (byte**)decoder.extensions;

                    var fmts = new List<string>();
                    
                    if (p2 != null)
                    {
                        for (var j = 0;; j++)
                        {
                            var ext = Marshal.PtrToStringAnsi(new IntPtr(p2[j]));

                            if (ext == null)
                                break;

                            fmts.Add(ext);
                        }
                    }

                    _decoders.Add(
                        new Decoder(
                            Marshal.PtrToStringAnsi(p[i]->description),
                            Marshal.PtrToStringUTF8(p[i]->author),
                            Marshal.PtrToStringAnsi(p[i]->url)
                        ) {SupportedFormats = fmts}
                    );
                }
            }
        }

        internal static void ProcessAudioEvent(SDL_Event ev)
        {
            switch (ev.type)
            {
                case SDL_EventType.SDL_AUDIODEVICEADDED:

                    DeviceDisconnected?.Invoke(
                        new AudioDeviceEventArgs(
                            new AudioDevice((int)ev.adevice.which, ev.adevice.iscapture != 0)
                        )
                    );

                    break;
                case SDL_EventType.SDL_AUDIODEVICEREMOVED:

                    DeviceDisconnected?.Invoke(
                       new AudioDeviceEventArgs(
                           new AudioDevice((int)ev.adevice.which, ev.adevice.iscapture != 0)
                       )
                   );

                    break;
            }
        }
    }
}