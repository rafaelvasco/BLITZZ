namespace BLITZZ.Input
{
    public class TextInputEventArgs
    {
        public string Text { get; internal set; }

        internal TextInputEventArgs(string text)
        {
            Text = text;
        }
    }
}
