namespace BLITZZ.Content.Font
{
    internal class BitmapFontPage
    {
        public int ID { get; }
        public Texture Texture { get; }

        internal BitmapFontPage(int id, Texture texture)
        {
            ID = id;
            Texture = texture;
        }
    }
}