
namespace BLITZZ.Content
{
    public static partial class AssetLoader
    {
        public static Texture LoadTexture(ImageData imageData)
        {
            var pixmap = new Pixmap(imageData.Data, imageData.Width, imageData.Height);

            var texture = new Texture(pixmap)
            {
                Id = imageData.Id
            };

            return texture;
        }

       
    }
}
