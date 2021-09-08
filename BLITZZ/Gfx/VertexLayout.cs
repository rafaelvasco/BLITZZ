using BLITZZ.Native.BGFX;

namespace BLITZZ.Gfx
{
    public sealed class VertexLayout
    {
        internal VertexLayoutData Data;

        public int Stride => Data.stride;

        public VertexLayout Begin () {
            Bgfx.VertexLayoutBegin(ref Data, Graphics.Renderer);
            return this;
        }

        public VertexLayout Add(Attrib attribute, int count, AttribType type, bool normalized = false, bool asInt = false)
        {
            Bgfx.VertexLayoutAdd(ref Data, attribute, type, (byte) count, normalized, asInt);
            return this;
        }

        public VertexLayout Skip(int count)
        {
            Bgfx.VertexLayoutSkip(ref Data, count);
            return this;
        }

        public VertexLayout End()
        {
            Bgfx.VertexLayoutEnd(ref Data);
            return this;
        }
    }
}
