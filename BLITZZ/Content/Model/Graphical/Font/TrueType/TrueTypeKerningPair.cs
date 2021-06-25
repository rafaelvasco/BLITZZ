namespace BLITZZ.Content.Font
{
    internal struct TrueTypeKerningPair
    {
        public char Left { get; }
        public char Right { get; }
        public int Amount { get; }

        public TrueTypeKerningPair(char left, char right, int amount)
        {
            Left = left;
            Right = right;
            Amount = amount;
        }
    }
}