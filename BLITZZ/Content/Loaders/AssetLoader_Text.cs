namespace BLITZZ.Content
{
    public static partial class AssetLoader
    {
        public static TextFile LoadTextFile(TextFileData txtData)
        {
            var txt_file = new TextFile(System.Text.Encoding.UTF8.GetString(txtData.TextData))
            {
                Id = txtData.Id
            };

            return txt_file;
        }
    }
}
