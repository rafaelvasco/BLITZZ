using BLITZZ.Gfx;
using BLITZZ.Native.BGFX;
using System;

namespace BLITZZ.Content
{
    public enum TextureWrappingMode
    {
        Clamp,
        Repeat
    }

    public enum TextureFilterMode
    {
        NearestNeighbor,
        Linear,
    }

    public class Texture : Asset, IEquatable<Texture>
    {
        internal readonly TextureHandle Handle;
        internal SamplerFlags SamplerFlags { get; private set; }

        public Pixmap Pixmap { get; }

        private TextureWrappingMode _horizontalWrapMode;
        private TextureWrappingMode _verticalWrapMode;
        private TextureFilterMode _filterMode;
        

        public int Width { get; private set; }

        public int Height { get; private set; }

        public TextureWrappingMode HorizontalWrappingMode
        {
            get => _horizontalWrapMode;
            set
            {
                if (_horizontalWrapMode != value)
                {
                    _horizontalWrapMode = value;
                    UpdateTextureFlags();
                }
            }
        }

        public TextureWrappingMode VerticalWrappingMode
        {
            get => _verticalWrapMode;
            set
            {
                if (_verticalWrapMode != value)
                {
                    _verticalWrapMode = value;
                    UpdateTextureFlags();
                }
            }
        }

        public TextureFilterMode FilterMode
        {
            get => _filterMode;
            set
            {
                if (_filterMode != value)
                {
                    _filterMode = value;
                    UpdateTextureFlags();
                }
            }
        }

        internal Texture(Pixmap pixmap)
        {
            Pixmap = pixmap;
            Width = pixmap.Width;
            Height = pixmap.Height;
            HorizontalWrappingMode = TextureWrappingMode.Clamp;
            VerticalWrappingMode = TextureWrappingMode.Repeat;
            FilterMode = TextureFilterMode.NearestNeighbor;
            UpdateTextureFlags();
            Handle = Bgfx.CreateDynamicTexture2D(pixmap.Width, pixmap.Height, false, 0, TextureFormat.BGRA8, SamplerFlags, pixmap.Data);
        }

        public static Texture Create(int width, int height, Color fillColor)
        {
            var pixmap = new Pixmap(width, height, fillColor);

            return Create(pixmap);
        }

        public static Texture Create(Pixmap pixmap)
        {
            var texture = new Texture(pixmap);

            Assets.RegisterRuntimeLoaded(texture);

            return texture;
        }

        public bool Equals(Texture other)
        {
            return other != null && Handle.idx == other.Handle.idx;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Texture);
        }

        public override int GetHashCode()
        {
            return Handle.idx.GetHashCode();
        }

        protected override void FreeNativeResources()
        {
            Bgfx.DestroyTexture2D(Handle);
        }

        internal void ReloadPixelData()
        {
            Bgfx.UpdateTexture2D(Handle, 0, 0, 0, 0, Width, Height, Pixmap.Data, Pixmap.Stride);
        }

        private void UpdateTextureFlags()
        {
            this.SamplerFlags = BuildSamplerFlags(this);
        }

        private static SamplerFlags BuildSamplerFlags(Texture texture)
        {
            var samplerFlags = SamplerFlags.None;

            if (texture.FilterMode == TextureFilterMode.NearestNeighbor)
            {
                samplerFlags |= SamplerFlags.Point;
            }

            if (texture.HorizontalWrappingMode == TextureWrappingMode.Clamp)
            {
                samplerFlags |= SamplerFlags.UClamp;
            }

            if (texture.VerticalWrappingMode == TextureWrappingMode.Clamp)
            {
                samplerFlags |= SamplerFlags.VClamp;
            }

            return samplerFlags;
        }
    }
}
