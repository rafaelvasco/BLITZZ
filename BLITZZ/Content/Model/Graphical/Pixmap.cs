using System;
using BLITZZ.Gfx;
using STB;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BLITZZ.Native.BGFX;

namespace BLITZZ.Content
{
    public class Pixmap : DisposableResource
    {
        public int Width { get; }

        public int Height { get; }

        public byte[] Data { get; private set; }

        public int SizeBytes { get; }

        public int Stride => Width * _bytesPerPixel;

        private readonly int _bytesPerPixel;

        internal Pixmap(IntPtr srcData, int width, int height, int bytesPerPixel)
        {
            _bytesPerPixel = bytesPerPixel;

            Width = width;
            Height = height;
            SizeBytes = width * height * bytesPerPixel;
            Data = new byte[SizeBytes];

            unsafe
            {
                Unsafe.CopyBlockUnaligned(ref Data[0], ref Unsafe.AsRef<byte>(srcData.ToPointer()), (uint)SizeBytes);    
            }

            if (Graphics.TextureFormat == TextureFormat.BGRA8)
            {
                ConvertToBgra();
            }
        }

        internal Pixmap(byte[] srcData, int width, int height, int bytesPerPixel = 4)
        {
            _bytesPerPixel = bytesPerPixel;
            Width = width;
            Height = height;
            SizeBytes = srcData.Length;
            Data = new byte[srcData.Length];
            Unsafe.CopyBlockUnaligned(ref Data[0], ref srcData[0], (uint)SizeBytes);

            if (Graphics.TextureFormat == TextureFormat.BGRA8)
            {
                ConvertToBgra();
            }
        }

        internal Pixmap(int width, int height, Color fillColor = default)
        {
            Width = width;
            Height = height;
            SizeBytes = width * height;

            int length = width * height * 4;

            Data = new byte[length];

            Blitter.Begin(this);

            Blitter.SetColor(fillColor);

            Blitter.Fill();

            Blitter.End();
        }

        public static Pixmap Create(int width, int height, Color fillColor)
        {
            var pixmap = new Pixmap(width, height, fillColor);

            Assets.RegisterRuntimeLoaded(pixmap);

            return pixmap;
        }

        protected override void FreeManagedResources()
        {
            Data = null;
        }

        public void SaveToFile(string path)
        {
            using var stream = File.OpenWrite(path);

            var image_writer = new ImageWriter();
            image_writer.WritePng(Data, Width, Height, ColorComponents.RedGreenBlueAlpha, stream);
        }

        private unsafe void ConvertToBgra(bool premultiplyAlpha = true)
        {
            var pd = Data;

            fixed (byte* p = &MemoryMarshal.GetArrayDataReference(pd))
            {
                var len = pd.Length - 4;
                for (int i = 0; i <= len; i += 4)
                {
                    byte r = *(p + i);
                    byte g = *(p + i + 1);
                    byte b = *(p + i + 2);
                    byte a = *(p + i + 3);

                    if (!premultiplyAlpha)
                    {
                        *(p + i) = b;
                        *(p + i + 1) = g;
                        *(p + i + 2) = r;
                    }
                    else
                    {
                        *(p + i) = (byte)((b * a) / 255);
                        *(p + i + 1) = (byte)((g * a) / 255);
                        *(p + i + 2) = (byte)(r * a / 255);
                    }

                    *(p + i + 3) = a;
                }
            }
        }
    }
}
