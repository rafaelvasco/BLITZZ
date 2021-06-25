using BLITZZ.Content;
using BLITZZ.Native.BGFX;
using System;
using System.Runtime.InteropServices;

namespace BLITZZ.Gfx
{
    internal struct Quad
    {
        public Vertex TopLeft;
        public Vertex TopRight;
        public Vertex BottomLeft;
        public Vertex BottomRight;

        public TextureHandle Texture;

        public static readonly int SizeInBytes = Marshal.SizeOf<Quad>();

        public float Width => MathF.Abs(TopRight.X - TopLeft.X);
        public float Height => MathF.Abs(BottomRight.Y - TopRight.Y);

        public Quad(Texture texture, RectF srcRect = default, RectF destRect = default)
        {
            Texture = texture.Handle;

            TopLeft = default;
            TopRight = default;
            BottomRight = default;
            BottomLeft = default;

            float ax, ay, bx, by;

            float dest_x1, dest_y1, dest_x2, dest_y2;

            if (srcRect.IsEmpty)
            {
                srcRect = RectF.FromBox(0, 0, texture.Width, texture.Height);

                ax = 0;
                ay = 0;
                bx = 1;
                by = 1;
            }
            else
            {
                float inv_tex_w = 1.0f / texture.Width;
                float inv_tex_h = 1.0f / texture.Height;

                ax = srcRect.X1 * inv_tex_w;
                ay = srcRect.Y1 * inv_tex_h;
                bx = srcRect.X2 * inv_tex_w;
                by = srcRect.Y2 * inv_tex_h;
            }

            if (destRect.IsEmpty)
            {
                dest_x1 = 0;
                dest_y1 = 0;
                dest_x2 = srcRect.Width;
                dest_y2 = srcRect.Height;
            }
            else
            {
                dest_x1 = destRect.X1;
                dest_y1 = destRect.Y1;
                dest_x2 = destRect.X2;
                dest_y2 = destRect.Y2;
            }

            TopLeft.X = dest_x1;
            TopLeft.Y = dest_y1;
            TopLeft.Tx = ax;
            TopLeft.Ty = ay;
            TopLeft.Col = 0xFFFFFFFF;

            TopRight.X = dest_x2;
            TopRight.Y = dest_y1;
            TopRight.Tx = bx;
            TopRight.Ty = ay;
            TopRight.Col = 0xFFFFFFFF;

            BottomRight.X = dest_x2;
            BottomRight.Y = dest_y2;
            BottomRight.Tx = bx;
            BottomRight.Ty = by;
            BottomRight.Col = 0xFFFFFFFF;

            BottomLeft.X = dest_x1;
            BottomLeft.Y = dest_y2;
            BottomLeft.Tx = ax;
            BottomLeft.Ty = by;
            BottomLeft.Col = 0xFFFFFFFF;
        }

        public override string ToString()
        {
            return $"{TopLeft};{TopRight};{BottomRight};{BottomLeft}";
        }
    }
}
