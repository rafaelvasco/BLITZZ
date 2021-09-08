using System.IO;

namespace BLITZZ.Content
{
    public static class TextBuilder
    {
        public static TextFileData Build(string id, string relativePath)
        {
            var text = File.ReadAllBytes(AssetLoader.GetFullResourcePath(relativePath));

            var text_file_data = new TextFileData()
            {
                Id = id,
                TextData = text
            };

            return text_file_data;
        }
    }
}
