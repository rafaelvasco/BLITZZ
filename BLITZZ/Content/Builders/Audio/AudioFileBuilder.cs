
using System.IO;

namespace BLITZZ.Content
{
    public static class AudioFileBuilder
    {
        public static AudioFileData Build(string id, string relativePath, bool streamed)
        {
            var data = File.ReadAllBytes(AssetLoader.GetFullResourcePath(relativePath));


            var audioFileData = new AudioFileData()
            {
                Id = id,
                Data = data,
                Streamed = streamed
            };

            return audioFileData;
        }
    }
}
