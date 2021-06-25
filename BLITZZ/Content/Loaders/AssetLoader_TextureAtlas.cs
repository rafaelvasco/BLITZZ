namespace BLITZZ.Content
{
    public static partial class AssetLoader
    {
        public static TextureAtlas LoadAtlas(TextureAtlasData data)
        {
            var pixmap = new Pixmap(data.Data, data.Width, data.Height);

            

            var texture = new Texture(pixmap);

            var atlas = TextureAtlas.FromAtlas(texture, data.Atlas);

            atlas.Id = data.Id;

            return atlas;
        }
    }
}
