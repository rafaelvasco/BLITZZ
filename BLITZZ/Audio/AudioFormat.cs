using System;
using System.Collections.Generic;
using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ.Audio
{
    public class AudioFormat
    {
        private Dictionary<SampleFormat, ushort> SdlFormatBE = new()
        {
            {SampleFormat.U8, AUDIO_U8},
            {SampleFormat.S8, AUDIO_S8},
            {SampleFormat.U16, AUDIO_U16MSB},
            {SampleFormat.S16, AUDIO_S16MSB},
            {SampleFormat.S32, AUDIO_S32MSB},
            {SampleFormat.F32, AUDIO_F32MSB}
        };

        private Dictionary<SampleFormat, ushort> SdlFormatLE = new()
        {
            {SampleFormat.U8, AUDIO_U8},
            {SampleFormat.S8, AUDIO_S8},
            {SampleFormat.U16, AUDIO_U16LSB},
            {SampleFormat.S16, AUDIO_S16LSB},
            {SampleFormat.S32, AUDIO_S32LSB},
            {SampleFormat.F32, AUDIO_F32LSB}
        };

        private Dictionary<SampleFormat, ushort> SdlFormatSYS = new()
        {
            {SampleFormat.U8, AUDIO_U8},
            {SampleFormat.S8, AUDIO_S8},
            {SampleFormat.U16, AUDIO_U16SYS},
            {SampleFormat.S16, AUDIO_S16SYS},
            {SampleFormat.S32, AUDIO_S32SYS},
            {SampleFormat.F32, AUDIO_F32SYS}
        };

        public static readonly AudioFormat Default = BitConverter.IsLittleEndian
            ? new AudioFormat(SampleFormat.U16, ByteOrder.LittleEndian)
            : new AudioFormat(SampleFormat.U16, ByteOrder.BigEndian);

        public static readonly AudioFormat ChromaDefault = BitConverter.IsLittleEndian
            ? new AudioFormat(SampleFormat.F32, ByteOrder.LittleEndian)
            : new AudioFormat(SampleFormat.F32, ByteOrder.BigEndian);


        public SampleFormat SampleFormat { get; private set; }
        public ByteOrder ByteOrder { get; private set; }

        public AudioFormat(SampleFormat sampleFormat)
        {
            ByteOrder = ByteOrder.LittleEndian;
            SampleFormat = sampleFormat;
        }

        public AudioFormat(SampleFormat sampleFormat, ByteOrder byteOrder)
        {
            SampleFormat = sampleFormat;
            ByteOrder = byteOrder;
        }

        internal ushort SdlFormat
        {
            get
            {
                return ByteOrder switch
                {
                    ByteOrder.Native => SdlFormatSYS[SampleFormat],
                    ByteOrder.BigEndian => SdlFormatBE[SampleFormat],
                    ByteOrder.LittleEndian => SdlFormatLE[SampleFormat],
                    _ => throw new FormatException("Unknown byte order specified."),
                };
            }
        }

        internal static AudioFormat FromSdlFormat(ushort format)
        {
            var (sampleFormat, byteOrder) = DetectFormat(format);
            return new AudioFormat(sampleFormat, byteOrder);
        }
        
        private static (SampleFormat, ByteOrder) DetectFormat(ushort sdlFormat)
        {
            switch (sdlFormat)
            {
                case AUDIO_U8:
                    return (SampleFormat.U8, ByteOrder.LittleEndian);
                
                case AUDIO_S8:
                    return (SampleFormat.S8, ByteOrder.LittleEndian);
                
                case AUDIO_U16LSB:
                    return (SampleFormat.U16, ByteOrder.LittleEndian);
                
                case AUDIO_U16MSB:
                    return (SampleFormat.U16, ByteOrder.BigEndian);
                
                case AUDIO_S16LSB:
                    return (SampleFormat.S16, ByteOrder.LittleEndian);
                
                case AUDIO_S16MSB:
                    return (SampleFormat.S16, ByteOrder.BigEndian);
                
                case AUDIO_S32LSB:
                    return (SampleFormat.S32, ByteOrder.LittleEndian);
                
                case AUDIO_S32MSB:
                    return (SampleFormat.S32, ByteOrder.BigEndian);

                case AUDIO_F32LSB:
                    return (SampleFormat.F32, ByteOrder.LittleEndian);
                
                case AUDIO_F32MSB:
                    return (SampleFormat.F32, ByteOrder.BigEndian);
             
                default: throw new NotSupportedException("Unsupported SDL audio format.");
            }
        }
    }
}