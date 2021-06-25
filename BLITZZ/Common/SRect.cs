using MessagePack;

namespace BLITZZ
{
    [MessagePackObject]
    public struct SRect
    {
        [Key(0)]
        public int X { get; set; }

        [Key(1)]
        public int Y { get; set; }

        [Key(2)]
        public int W { get; set; }

        [Key(3)]
        public int H { get; set; }

        public SRect(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }
    }
}
