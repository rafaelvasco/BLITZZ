using System.Collections.Generic;

namespace BLITZZ.Content
{
    public class TextureAtlas : Asset
    {
        public RectF this[int index] => _mRegions[index];

        public RectF this[string name]
        {
            get
            {
                if (_mNameMap.TryGetValue(name, out int index))
                {
                    return _mRegions[index];
                }

                return default;
            }
        }

        public Texture Texture => _mTexture;

        public int Count => _mRegions.Length;


        private readonly RectF[] _mRegions;
        private readonly Texture _mTexture;
        private Dictionary<string, int> _mNameMap;

        public static TextureAtlas FromGrid(Texture texture, int rows, int columns)
        {
            int tex_w = texture.Width;
            int tex_h = texture.Height;

            int tile_width = tex_w / columns;
            int tile_height = tex_h / rows;

            var regions = new RectF[rows * columns];

            int index = 0;

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    int x = j * tile_width;
                    int y = i * tile_height;

                    regions[index++] = RectF.FromBox(x, y, tile_width, tile_height);
                }
            }

            return new TextureAtlas(texture, regions);
        }

        public static TextureAtlas FromAtlas(Texture texture, Dictionary<string, (int X, int Y, int Width, int Height)> atlas)
        {
            var regions = new RectF[atlas.Count];
            var map = new Dictionary<string, int>();

            var idx = 0;

            foreach (var atlas_pair in atlas)
            {
                regions[idx] = RectF.FromBox(atlas_pair.Value.X, atlas_pair.Value.Y, atlas_pair.Value.Width, atlas_pair.Value.Height);

                map.Add(atlas_pair.Key, idx);

                idx++;
            }

            var tex_atlas = new TextureAtlas(texture, regions) { _mNameMap = map };

            return tex_atlas;

        }

        public static TextureAtlas FromAtlas(Texture texture, Dictionary<string, Rect> atlas)
        {
            var regions = new RectF[atlas.Count];
            var map = new Dictionary<string, int>();

            var idx = 0;

            foreach (var atlas_pair in atlas)
            {
                regions[idx] = RectF.FromBox(atlas_pair.Value.X, atlas_pair.Value.Y, atlas_pair.Value.Width, atlas_pair.Value.Height);

                map.Add(atlas_pair.Key, idx);

                idx++;
            }

            var tex_atlas = new TextureAtlas(texture, regions) { _mNameMap = map };

            return tex_atlas;

        }

        private TextureAtlas(Texture texture, RectF[] regions)
        {
            _mTexture = texture;

            _mRegions = new RectF[regions.Length];

            for (int i = 0; i < regions.Length; ++i)
            {
                var region = regions[i];

                _mRegions[i] = RectF.FromBox(region.X1, region.Y1, region.Width, region.Height);

            }
        }

        protected override void FreeNativeResources()
        {
            _mTexture.Dispose();
        }

    }
}
