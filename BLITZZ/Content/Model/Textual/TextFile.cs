namespace BLITZZ.Content
{
    public class TextFile : Asset
    {
        public string[] Text {get; private set;}

        public string JoinedText => string.Join("\n", Text);

        internal TextFile(string[] text)
        {
            Text = text;
        }

        protected override void FreeManagedResources()
        {
            Text = null;
        }
    }
}
