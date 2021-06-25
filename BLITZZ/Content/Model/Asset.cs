namespace BLITZZ.Content
{
    public abstract class Asset : DisposableResource
    {
        public string Id { get; internal set; }

        public AssetType Type { get; internal set; }

    }
}
