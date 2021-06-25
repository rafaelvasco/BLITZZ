using BLITZZ.Audio;
using System;
using System.Collections.Generic;
using static BLITZZ.Native.SDL.SDL2_nmix;

namespace BLITZZ.Content
{
    public abstract class AudioSource : Asset
    {
        public delegate void AudioStreamDelegate(Span<byte> audioBufferData, AudioFormat format);

        protected IntPtr Handle { get; set; }

        internal unsafe NMIX_Source* Source
            => (NMIX_Source*)Handle.ToPointer();

        public virtual PlaybackStatus Status { get; set; }

        public bool IsPlaying => NMIX_IsPlaying(Handle);

        public float Panning
        {
            get => NMIX_GetPan(Handle);
            set
            {
                var pan = value;

                if (pan < -1.0f)
                    pan = 1.0f;

                if (pan > 1.0f)
                    pan = 1.0f;

                NMIX_SetPan(Handle, pan);
            }
        }

        public float Volume
        {
            get => NMIX_GetGain(Handle);

            set
            {
                var vol = value;

                if (vol < 0f)
                    vol = 0f;

                if (vol > 2f)
                    vol = 2f;

                NMIX_SetGain(Handle, vol);
            }
        }

        public AudioFormat Format
        {
            get
            {
                unsafe
                {
                    return AudioFormat.FromSdlFormat(
                        Source->format
                    );
                }
            }
        }

        public byte ChannelCount
        {
            get
            {
                unsafe
                {
                    return Source->channels;
                }
            }
        }
        
        public Span<byte> InBuffer
        {
            get
            {
                unsafe
                {
                    return new Span<byte>(Source->in_buffer.ToPointer(), Source->in_buffer_size);
                }
            }
        }

        public Span<byte> OutBuffer
        {
            get
            {
                unsafe
                {
                    return new Span<byte>(Source->out_buffer.ToPointer(), Source->out_buffer_size);
                }
            }
        }

        public List<AudioStreamDelegate> Filters { get; } = new();

        public virtual void Play()
        {
            EnsureHandleValid();
            NMIX_Play(Handle);
        }

        public virtual void Pause()
        {
            EnsureHandleValid();
            NMIX_Pause(Handle);
        }

        public virtual void Stop()
            => throw new NotSupportedException("This audio source does not support stopping.");

        protected void EnsureHandleValid()
        {
            if (Handle == IntPtr.Zero)
                throw new InvalidOperationException("Audio source handle is not valid.");
        }

        protected override void FreeNativeResources()
        {
            if (Handle != IntPtr.Zero)
            {
                NMIX_FreeSource(Handle);
                Handle = IntPtr.Zero;
            }
        }
    }
}