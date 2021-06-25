using BLITZZ.Gfx;
using STB;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BLITZZ.Content
{
    public class Pixmap : DisposableResource
    {
        public int Width { get; private set; }

        public int Height { get; private set; }

        public byte[] Data => _pixelData;

        public int SizeBytes { get; }

        public int Stride => Width * 4;

        private byte[] _pixelData;


        internal Pixmap(byte[] srcData, int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.SizeBytes = srcData.Length;
            this._pixelData = new byte[srcData.Length];
            Unsafe.CopyBlockUnaligned(ref _pixelData[0], ref srcData[0], (uint)SizeBytes);

            ConvertToEngineRepresentation();
        }

        internal Pixmap(int width, int height, Color fillColor = default)
        {
            this.Width = width;
            this.Height = height;
            this.SizeBytes = width * height;

            int length = width * height * 4;

            _pixelData = new byte[length];

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
            _pixelData = null;
        }

        public void SaveToFile(string path)
        {
            using var stream = File.OpenWrite(path);

            var image_writer = new ImageWriter();
            image_writer.WritePng(_pixelData, Width, Height, ColorComponents.RedGreenBlueAlpha, stream);
        }

        private unsafe void ConvertToExportRepresentation()
        {
            var pd = _pixelData;

            fixed (byte* p = &MemoryMarshal.GetArrayDataReference(pd))
            {
                var len = pd.Length - 4;
                for (int i = 0; i <= len; i += 4)
                {
                    byte b = *(p + i);
                    byte g = *(p + i + 1);
                    byte r = *(p + i + 2);
                    byte a = *(p + i + 3);

                    *(p + i) = r;
                    *(p + i + 1) = g;
                    *(p + i + 2) = b;
                    *(p + i + 3) = a;
                }
            }
        }

        private unsafe void ConvertToEngineRepresentation(bool premultiplyAlpha = false)
        {
            var pd = _pixelData;

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
