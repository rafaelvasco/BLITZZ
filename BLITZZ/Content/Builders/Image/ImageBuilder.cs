using STB;
using System.IO;

namespace BLITZZ.Content
{
    public static class ImageBuilder
    {
        public static ImageData Build(string id, string relativePath)
        {
            using var file = File.OpenRead(AssetLoader.GetFullResourcePath(relativePath));

            var image = ImageResult.FromStream(file, ColorComponents.RedGreenBlueAlpha);

            var image_data = new ImageData()
            {
                Id = id,
                Data = image.Data,
                Width = image.Width,
                Height = image.Height
            };

            return image_data;
        }
    }
}
