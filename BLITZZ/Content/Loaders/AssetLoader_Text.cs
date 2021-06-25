namespace BLITZZ.Content
{
    public static partial class AssetLoader
    {
        public static TextFile LoadTextFile(TextFileData txtData)
        {
            var text_lines = new string[txtData.TextData.Length];

            for (int i = 0; i < txtData.TextData.Length; ++i)
            {
                text_lines[i] = System.Text.Encoding.UTF8.GetString(txtData.TextData[i]);
            }

            var txt_file = new TextFile(text_lines)
            {
                Id = txtData.Id
            };

            return txt_file;
        }

        
    }
}
