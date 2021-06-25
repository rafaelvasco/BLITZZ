using System.IO;

namespace STB
{
    internal struct ImageInfo
    {
        public int Width;
        public int Height;
        public ColorComponents ColorComponents;
        public int BitsPerChannel;

        public static ImageInfo? FromStream(Stream stream)
        {
            var info = PngDecoder.Info(stream);
            if (info != null)
            {
                return info;
            }

            info = JpgDecoder.Info(stream);

            if (info != null)
            {
                return info;
            }

            info = GifDecoder.Info(stream);
            if (info != null)
            {
                return info;
            }

            info = BmpDecoder.Info(stream);
            if (info != null)
            {
                return info;
            }

            info = TgaDecoder.Info(stream);
            if (info != null)
            {
                return info;
            }

            return null;
        }
    }
}
