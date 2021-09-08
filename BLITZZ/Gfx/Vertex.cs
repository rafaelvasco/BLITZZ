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

            VertexLayout.Begin();
            VertexLayout.Add(Attrib.Position, 3, AttribType.Float);
            VertexLayout.Add(Attrib.Color0, 4, AttribType.Uint8, normalized: true);
            VertexLayout.Add(Attrib.TexCoord0, 2, AttribType.Float);
            VertexLayout.End();
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z},{Col},{Tx},{Ty}";
        }

        public static int Stride => VertexLayout.Stride;

       
    }
}
