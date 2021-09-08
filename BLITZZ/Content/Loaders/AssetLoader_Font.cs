
using BLITZZ.Content.Font;

namespace BLITZZ.Content
{
    public static partial class AssetLoader
    {
        public static TrueTypeFont LoadTrueTypeFont(TrueTypeFontData fontData)
        {
            var texture = LoadTexture(fontData.FontSheet);

            var font = new TrueTypeFont(texture, fontData)
            {
                Id = fontData.Id
            };

            return font;
        }

        public static BitmapFont LoadBitmapFont(BitmapFontData fontData)
        {
            var texture = LoadTexture(fontData.FontSheet);

            var font = new BitmapFont(texture, fontData)
            {
                Id = fontData.Id
            };

            return font;
        }
    }
}
