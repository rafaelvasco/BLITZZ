using MessagePack;

namespace BLITZZ.Content.Font
{

    [MessagePackObject]
    public class BitmapFontData
    {
        [Key(0)]
        public string Id { get; set; }
    }
}
