using BLITZZ.Native.BGFX;
using System.Runtime.InteropServices;

namespace BLITZZ.Gfx
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct Vertex
    {
        public float X;

        public float Y;

        public float Z;

        public uint Col;

        public float Tx;

        public float Ty;


        public static readonly VertexLayout VertexLayout;

        public Vertex(float x, float y, float z = 0f, uint abgr = 0xffffffff, float tx = 0f, float ty = 0f)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Col = abgr;
            this.Tx = tx;
            this.Ty = ty;
        }

        static Vertex()
        {
            VertexLayout = new VertexLayout();

            Bgfx.VertexLayoutBegin(VertexLayout, Graphics.Renderer);
            Bgfx.VertexLayoutAdd(VertexLayout, Attrib.Position, AttribType.Float, 3, false, false);
            Bgfx.VertexLayoutAdd(VertexLayout, Attrib.Color0, AttribType.Uint8, 4, true, false);
            Bgfx.VertexLayoutAdd(VertexLayout, Attrib.TexCoord0, AttribType.Float, 2, false, false);
            Bgfx.VertexLayoutEnd(VertexLayout);
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z},{Col},{Tx},{Ty}";
        }

        public static int Stride => 24;

    }
}
