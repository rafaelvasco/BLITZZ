using BLITZZ.Audio;
using BLITZZ.Logging;
using System;
using System.IO;
using System.Runtime.InteropServices;
using static BLITZZ.Native.SDL.SDL2;
using static BLITZZ.Native.SDL.SDL2_nmix;
using static BLITZZ.Native.SDL.SDL2_sound;

namespace BLITZZ.Content
{
    public class AudioFile : AudioSource
    {
        private readonly Log _log = LogManager.GetForCurrentAssembly();
        private NMIX_SourceCallback _originalSourceCallback;
        private NMIX_SourceCallback _internalSourceCallback;

        internal IntPtr FileSourceHandle { get; private set; }
        internal IntPtr RwOpsHandle { get; private set; }

        internal unsafe NMIX_FileSource* FileSource
            => (NMIX_FileSource*)FileSourceHandle.ToPointer();

        internal unsafe Sound_Sample* SoundSample
            => (Sound_Sample*)FileSource->sample.ToPointer();

        public override PlaybackStatus Status { get; set; }

        public event EventHandler<bool> OnFinished;

        public float Duration
        {
            get
            {
                EnsureFileSourceHandleValid();
                return NMIX_GetDuration(FileSourceHandle);
            }
        }

        public bool IsLooping
        {
            get
            {
                EnsureFileSourceHandleValid();
                return NMIX_GetLoop(FileSourceHandle);
            }

            set
            {
                EnsureFileSourceHandleValid();
                NMIX_SetLoop(FileSourceHandle, value);
            }
        }

        public bool Streamed { get; private set; }

        internal AudioFile(byte[] data, bool streamed)
        {
            Streamed = streamed;

            unsafe
            {
                fixed (byte* b = &data[0])
                {
                    RwOpsHandle = SDL_RWFromConstMem(
                        new IntPtr(b),
                        data.Length
                    );

                    if (RwOpsHandle == IntPtr.Zero)
                    {
                        _log.Error($"Failed to initialize RWops from stream: {SDL_GetError()}");
                        return;
                    }

                    foreach (var decoder in AudioManager.Decoders)
                    {
                        foreach (var format in decoder.SupportedFormats)
                        {
                            SDL_RWseek(RwOpsHandle, 0, RW_SEEK_SET);
                            FileSourceHandle = NMIX_NewFileSource(RwOpsHandle, format, !streamed);

                            if (FileSourceHandle != IntPtr.Zero)
                                break;
                        }
                    }

                    if (FileSourceHandle == IntPtr.Zero)
                    {
                        _log.Error($"Failed to initialize audio source from stream: {SDL_GetError()}");
                        return;
                    }

                    Handle = FileSource->source;
                }
            }
            HookSourceCallback();
        }

        public override void Play()
        {
            EnsureHandleValid();

            if (Status == PlaybackStatus.Playing)
            {
                if (NMIX_Pause(Handle) < 0)
                {
                    _log.Error($"Failed to play the audio source [pause]: {SDL_GetError()}");
                    return;
                }

                if (NMIX_Rewind(FileSourceHandle) < 0)
                {
                    _log.Error($"Failed to play the audio source [rewind]: {SDL_GetError()}");
                    return;
                }

                Status = PlaybackStatus.Stopped;
            }

            if (NMIX_Play(Handle) < 0)
            {
                _log.Error($"Failed to play the audio source [play]: {SDL_GetError()}");
                return;
            }

            Status = PlaybackStatus.Playing;
        }

        public override void Pause()
        {
            if (Status == PlaybackStatus.Paused || Status == PlaybackStatus.Stopped)
                return;

            base.Pause();
            Status = PlaybackStatus.Paused;
        }

        public override void Stop()
        {
            EnsureHandleValid();
            EnsureFileSourceHandleValid();

            if (NMIX_Pause(Handle) < 0)
            {
                _log.Error($"Failed to stop the audio source [pause]: {SDL_GetError()}");
                return;
            }

            if (NMIX_Rewind(FileSourceHandle) < 0)
            {
                _log.Error($"Failed to stop the audio source [rewind]: {SDL_GetError()}");
                return;
            }

            Status = PlaybackStatus.Stopped;
        }

        public void Rewind()
        {
            EnsureFileSourceHandleValid();

            if (NMIX_Rewind(FileSourceHandle) < 0)
            {
                _log.Error($"Failed to rewind the requested audio source: {SDL_GetError()}");
            }
        }

        public void Seek(int milliseconds)
        {
            EnsureFileSourceHandleValid();

            if (NMIX_Seek(FileSourceHandle, milliseconds) < 0)
            {
                _log.Error($"Failed to seek to {milliseconds}ms: {SDL_GetError()}");
            }
        }

        protected override void FreeNativeResources()
        {
            if (FileSourceHandle != IntPtr.Zero)
            {
                NMIX_FreeFileSource(Handle);
                FileSourceHandle = IntPtr.Zero;
            }

            if (RwOpsHandle != IntPtr.Zero)
            {
                SDL_FreeRW(RwOpsHandle);
                RwOpsHandle = IntPtr.Zero;
            }
        }

        private void EnsureFileSourceHandleValid()
        {
            if (RwOpsHandle == IntPtr.Zero)
                throw new InvalidOperationException("RWops handle is invalid.");

            if (FileSourceHandle == IntPtr.Zero)
                throw new InvalidOperationException("File source handle is invalid.");
        }

        private void HookSourceCallback()
        {
            EnsureHandleValid();

            unsafe
            {
                _originalSourceCallback =
                    Marshal.GetDelegateForFunctionPointer<NMIX_SourceCallback>(Source->callback);
                _internalSourceCallback = InternalSourceCallback;

                var ptr = Marshal.GetFunctionPointerForDelegate(_internalSourceCallback);

                Source->callback = ptr;
            }
        }

        private void InternalSourceCallback(IntPtr userdata, IntPtr buffer, int bufferSize)
        {
            unsafe
            {
                var span = new Span<byte>(
                    SoundSample->buffer.ToPointer(),
                    (int)SoundSample->buffer_size
                );

                for (var i = 0; i < Filters.Count; i++)
                    Filters[i]?.Invoke(span, AudioFormat.FromSdlFormat(SoundSample->actual.format));

                _originalSourceCallback(userdata, buffer, bufferSize);

                if (Source->eof > 0)
                {
                    if (!IsLooping)
                    {
                        Pause();
                        Status = PlaybackStatus.Stopped;
                    }

                    Rewind();
                    OnFinished?.Invoke(this, IsLooping);
                }
            }
        }
    }
}