namespace BLITZZ.Content.Font
{
    internal struct BitmapFontKerningPair
    {
        public char First { get; internal set; }
        public char Second { get; internal set; }
        public float Amount { get; internal set; }

        public BitmapFontKerningPair(char first, char second, float amount)
        {
            First = first;
            Second = second;
            Amount = amount;
        }
    }
}