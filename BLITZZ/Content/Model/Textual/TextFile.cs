
namespace BLITZZ.Content
{
    public class TextFile : Asset
    {
        public string Text { get; private set; }

        internal TextFile(string text)
        {
            Text = text;
        }

        protected override void FreeManagedResources()
        {
            Text = null;
        }
    }
}
