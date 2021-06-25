namespace BLITZZ.Content
{
    public static partial class AssetLoader
    {
        public static AudioFile LoadAudioFile(AudioFileData data)
        {
            var audio = new AudioFile(data.Data, data.Streamed);

            return audio;
        }
    }
}
