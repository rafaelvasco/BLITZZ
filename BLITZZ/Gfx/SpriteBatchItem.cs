using BLITZZ.Content;
using System;
using System.Numerics;

namespace BLITZZ.Gfx
{
    internal class SpriteBatchItem : IComparable<SpriteBatchItem>
    {
        public Texture Texture;
        public float SortKey;

        public Vertex TopLeft;
        public Vertex TopRight;
        public Vertex BottomLeft;
        public Vertex BottomRight;

        public SpriteBatchItem()
        {
            TopLeft = new Vertex();
            TopRight = new Vertex();
            BottomLeft = new Vertex();
            BottomRight = new Vertex();
        }

        public void Set(
            float x, float y, 
            float dx, float dy, 
            float w, float h, 
            float sin, float cos, 
            Color color,
            Vector2 texCoordTL, 
            Vector2 texCoordBR, 
            float depth)
        {
            TopLeft.X = x+dx*cos-dy*sin;
            TopLeft.Y = y+dx*sin+dy*cos;
            TopLeft.Z = depth;
            TopLeft.Col = color;
            TopLeft.Tx = texCoordTL.X;
            TopLeft.Ty = texCoordTL.Y;

            TopRight.X = x+(dx+w)*cos-dy*sin;
            TopRight.Y = y+(dx+w)*sin+dy*cos;
            TopRight.Z = depth;
            TopRight.Col = color;
            TopRight.Tx = texCoordBR.X;
            TopRight.Ty = texCoordTL.Y;

            BottomLeft.X = x+dx*cos-(dy+h)*sin;
            BottomLeft.Y = y+dx*sin+(dy+h)*cos;
            BottomLeft.Z = depth;
            BottomLeft.Col = color;
            BottomLeft.Tx = texCoordTL.X;
            BottomLeft.Ty = texCoordBR.Y;

            BottomRight.X = x+(dx+w)*cos-(dy+h)*sin;
            BottomRight.Y = y+(dx+w)*sin+(dy+h)*cos;
            BottomRight.Z = depth;
            BottomRight.Col= color;
            BottomRight.Tx = texCoordBR.X;
            BottomRight.Ty = texCoordBR.Y;
        }
   
        public void Set(float x, float y, float w, float h, Color color, Vector2 texCoordTL, Vector2 texCoordBR, float depth)
        {
            TopLeft.X = x;
            TopLeft.Y = y;
            TopLeft.Z = depth;
            TopLeft.Col = color;
            TopLeft.Tx = texCoordTL.X;
            TopLeft.Ty = texCoordTL.Y;

            TopRight.X = x + w;
            TopRight.Y = y;
            TopRight.Z = depth;
            TopRight.Col = color;
            TopRight.Tx = texCoordBR.X;
            TopRight.Ty = texCoordTL.Y;

            BottomLeft.X = x;
            BottomLeft.Y = y + h;
            BottomLeft.Z = depth;
            BottomLeft.Col = color;
            BottomLeft.Tx = texCoordTL.X;
            BottomLeft.Ty = texCoordBR.Y;

            BottomRight.X = x + w;
            BottomRight.Y = y + h;
            BottomRight.Z = depth;
            BottomRight.Col = color;
            BottomRight.Tx = texCoordBR.X;
            BottomRight.Ty = texCoordBR.Y;
        }


        public int CompareTo(SpriteBatchItem other)
        {
            return SortKey.CompareTo(other.SortKey);
        }
    }
}
