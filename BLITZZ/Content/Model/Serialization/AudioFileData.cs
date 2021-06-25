using MessagePack;

namespace BLITZZ.Content
{
    [MessagePackObject]
    public class AudioFileData
    {
        [Key(0)]
        public string Id { get; set; }

        [Key(1)]
        public byte[] Data { get; set; }

        [Key(2)]
        public bool Streamed { get; set; }
    }
}
