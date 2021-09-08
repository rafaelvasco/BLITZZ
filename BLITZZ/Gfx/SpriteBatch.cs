using System;
using System.Numerics;
using BLITZZ.Content;
using BLITZZ.Content.Font;
using BLITZZ.Native.BGFX;

namespace BLITZZ.Gfx
{
    public class SpriteBatch
    {
        public int CurrentMaxDrawCalls { get; private set; }

        private readonly SpriteBatcher _batcher;

        private SpriteSortMode _sortMode;

        private readonly ShaderProgram _defaultShader;

        private ShaderProgram _currentShader;

        private bool _beginCalled;

        private Vector2 _texCoordTL = Vector2.Zero;

        private Vector2 _texCoordBR = Vector2.Zero;
        

        public SpriteBatch()
        {
            _defaultShader = Assets.Get<ShaderProgram>("base_shader");

            _currentShader = _defaultShader;

            _batcher = new SpriteBatcher();

            _beginCalled = false;
        }

        public void Begin(
            SpriteSortMode sortMode = SpriteSortMode.Deferred,
            BlendState blendState = null,
            ShaderProgram shader = null
        )
        {
            if (_beginCalled)
            {
                throw new InvalidOperationException(
                    "Begin cannot be called again until End has been successfully called.");
            }

            _currentShader = shader ?? _defaultShader;

            Graphics.SetBlendMode(blendState ?? BlendState.AlphaPre);

            _sortMode = sortMode;

            if (sortMode == SpriteSortMode.Immediate)
            {
                Setup();
            }

            _beginCalled = true;

        }

        public void End()
        {
            if (!_beginCalled)
            {
                throw new InvalidOperationException("Begin must be called before calling End.");
            }

            _beginCalled = false;

            if (_sortMode != SpriteSortMode.Immediate)
            {
                Setup();
            }

            _batcher.DrawBatch(_sortMode, _currentShader);

            if (_batcher.DrawCalls > CurrentMaxDrawCalls)
            {
                CurrentMaxDrawCalls = _batcher.DrawCalls;
            }

            _batcher.DrawCalls = 0;

            Bgfx.DebugTextClear(DebugColor.Black);
            Bgfx.DebugTextWrite(3, 3, DebugColor.White, DebugColor.Blue, $"Draw Calls: {CurrentMaxDrawCalls}");
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="sourceRect">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this sprite.</param>
        /// <param name="flipH">Flip Sprite Horizontally.</param>
        /// <param name="flipV">Flip Sprite Vertically</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
        public void Draw(
            Texture texture,
            Vector2 position,
            Rect? sourceRect,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            bool flipH,
            bool flipV,
            float layerDepth
        )
        {
            CheckValid(texture);

            var item = _batcher.CreateBatchItem();
            item.Texture = texture;

            item.SortKey = _sortMode switch
            {
                SpriteSortMode.Texture => texture.SortingKey,
                SpriteSortMode.FrontToBack => layerDepth,
                SpriteSortMode.BackToFront => -layerDepth,
                _ => item.SortKey
            };

            origin *= scale;

            float w, h;

            if (sourceRect.HasValue)
            {
                var srcRect = sourceRect.GetValueOrDefault();
                w = srcRect.Width * scale.X;
                h = srcRect.Height * scale.Y;
                _texCoordTL.X = srcRect.X * texture.Width;
                _texCoordTL.Y = srcRect.Y * texture.Height;
                _texCoordBR.X = srcRect.X2 * texture.Width;
                _texCoordBR.Y = srcRect.Y2 * texture.Height;
            }
            else
            {
                w = texture.Width * scale.X;
                h = texture.Height * scale.Y;
                _texCoordTL = Vector2.Zero;
                _texCoordBR = Vector2.One;
            }

            if (flipV)
            {
                var temp = _texCoordBR.Y;
                _texCoordBR.Y = _texCoordTL.Y;
                _texCoordTL.Y = temp;
            }

            if (flipH)
            {
                var temp = _texCoordBR.X;
                _texCoordBR.X = _texCoordTL.X;
                _texCoordTL.X = temp;
            }

            if (rotation == 0f)
            {
                item.Set(
                    position.X - origin.X,
                    position.Y - origin.Y,
                    w,
                    h,
                    color,
                    _texCoordTL,
                    _texCoordBR,
                    layerDepth
                );
            }
            else
            {
                item.Set(
                    position.X,
                    position.Y,
                    -origin.X,
                    -origin.Y,
                    w,
                    h,
                    Calc.Sin(rotation),
                    Calc.Cos(rotation),
                    color,
                    _texCoordTL,
                    _texCoordBR,
                    layerDepth
                );
            }

            FlushIfNeeded();
        }


        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="sourceRect">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this sprite.</param>
        /// <param name="flipH">Flip Sprite Horizontally.</param>
        /// <param name="flipV">Flip Sprite Vertically</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
        public void Draw(
            Texture texture,
            Vector2 position,
            Rect? sourceRect,
            Color color,
            float rotation,
            Vector2 origin,
            float scale,
            bool flipH,
            bool flipV,
            float layerDepth
        )
        {
            var scaleVec = new Vector2(scale, scale);
            Draw(texture, position, sourceRect, color, rotation, origin, scaleVec, flipH, flipV, layerDepth);
        }


        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destRect">The drawing bounds on screen.</param>
        /// <param name="sourceRect">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="flipH">Flip Sprite Horizontally.</param>
        /// <param name="flipV">Flip Sprite Vertically</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
        public void Draw(
            Texture texture,
            Rect destRect,
            Rect? sourceRect,
            Color color,
            float rotation,
            Vector2 origin,
            bool flipH,
            bool flipV,
            float layerDepth
        )
        {
            CheckValid(texture);

            var item = _batcher.CreateBatchItem();
            item.Texture = texture;

            item.SortKey = _sortMode switch
            {
                SpriteSortMode.Texture => texture.SortingKey,
                SpriteSortMode.FrontToBack => layerDepth,
                SpriteSortMode.BackToFront => -layerDepth,
                _ => item.SortKey
            };

            if (sourceRect.HasValue)
            {
                var srcRect = sourceRect.GetValueOrDefault();
                _texCoordTL.X = srcRect.X * texture.Width;
                _texCoordTL.Y = srcRect.Y * texture.Height;
                _texCoordBR.X = srcRect.X2 * texture.Width;
                _texCoordBR.Y = srcRect.Y2 * texture.Height;

                if (srcRect.Width != 0)
                {
                    origin.X *= (float)destRect.Width / srcRect.Width;
                }
                else
                {
                    origin.X *= (float)destRect.Width / texture.Width;
                }
                if (srcRect.Height != 0)
                {
                    origin.Y *= (float)destRect.Height / srcRect.Height;
                }
                else
                {
                    origin.Y *= (float)destRect.Height / texture.Height;
                }
            }
            else
            {
                _texCoordTL = Vector2.Zero;
                _texCoordBR = Vector2.One;

                origin.X *= destRect.Width * texture.Width;
                origin.Y *= destRect.Height * texture.Height;
            }

            if (flipV)
            {
                var temp = _texCoordBR.Y;
                _texCoordBR.Y = _texCoordTL.Y;
                _texCoordTL.Y = temp;
            }

            if (flipH)
            {
                var temp = _texCoordBR.X;
                _texCoordBR.X = _texCoordTL.X;
                _texCoordTL.X = temp;
            }

            if (rotation == 0f)
            {
                item.Set(
                    destRect.X - origin.X,
                    destRect.Y - origin.Y,
                    destRect.Width,
                    destRect.Height,
                    color,
                    _texCoordTL,
                    _texCoordBR,
                    layerDepth
                );
            }
            else
            {
                item.Set(
                    destRect.X,
                    destRect.Y,
                    -origin.X,
                    -origin.Y,
                    destRect.Width,
                    destRect.Height,
                    Calc.Sin(rotation),
                    Calc.Cos(rotation),
                    color,
                    _texCoordTL,
                    _texCoordBR,
                    layerDepth
                );
            }

            FlushIfNeeded();
        }


        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="sourceRect">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        public void Draw(Texture texture, Vector2 position, Rect? sourceRect, Color color)
        {
            CheckValid(texture);

            var item = _batcher.CreateBatchItem();
            item.Texture = texture;

            item.SortKey = _sortMode == SpriteSortMode.Texture ? texture.SortingKey : 0;

            Vector2 size;

            if (sourceRect.HasValue)
            {
                var srcRect = sourceRect.GetValueOrDefault();
                size = new Vector2(srcRect.Width, srcRect.Height);
                _texCoordTL.X = srcRect.X * texture.Width;
                _texCoordTL.Y = srcRect.Y * texture.Height;
                _texCoordBR.X = srcRect.X2 * texture.Width;
                _texCoordBR.Y = srcRect.Y2 * texture.Height;
            }
            else
            {
                size = new Vector2(texture.Width, texture.Height);
                _texCoordTL = Vector2.Zero;
                _texCoordBR = Vector2.One;
            }

            item.Set(
                position.X,
                position.Y,
                size.X,
                size.Y,
                color,
                _texCoordTL,
                _texCoordBR,
                0
            );

            FlushIfNeeded();
        }


        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destRect">The drawing bounds on screen.</param>
        /// <param name="sourceRect">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        public void Draw(Texture texture, Rect destRect, Rect? sourceRect, Color color)
        {
            CheckValid(texture);

            var item = _batcher.CreateBatchItem();
            item.Texture = texture;

            item.SortKey = _sortMode == SpriteSortMode.Texture ? texture.SortingKey : 0;

            if (sourceRect.HasValue)
            {
                var srcRect = sourceRect.GetValueOrDefault();
                _texCoordTL.X = srcRect.X * texture.Width;
                _texCoordTL.Y = srcRect.Y * texture.Height;
                _texCoordBR.X = srcRect.X2 * texture.Width;
                _texCoordBR.Y = srcRect.Y2 * texture.Height;
            }
            else
            {
                _texCoordTL = Vector2.Zero;
                _texCoordBR = Vector2.One;
            }

            item.Set(
                destRect.X,
                destRect.Y,
                destRect.Width,
                destRect.Height,
                color,
                _texCoordTL,
                _texCoordBR,
                0
            );

            FlushIfNeeded();
        }


        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        public void Draw(Texture texture, Vector2 position, Color color)
        {
            CheckValid(texture);

            var item = _batcher.CreateBatchItem();
            item.Texture = texture;

            item.SortKey = _sortMode == SpriteSortMode.Texture ? texture.SortingKey : 0;

            item.Set(
                position.X,
                position.Y,
                texture.Width,
                texture.Height,
                color,
                Vector2.Zero,
                Vector2.One, 
                0
            );

            FlushIfNeeded();
        }


        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destRect">The drawing bounds on screen.</param>
        /// <param name="color">A color mask.</param>
        public void Draw(Texture texture, Rect destRect, Color color)
        {
            CheckValid(texture);

            var item = _batcher.CreateBatchItem();
            item.Texture = texture;

            item.SortKey = _sortMode == SpriteSortMode.Texture ? texture.SortingKey : 0;

            item.Set(
                destRect.X,
                destRect.Y,
                destRect.Width,
                destRect.Height,
                color,
                Vector2.Zero,
                Vector2.One,
                0
            );

            FlushIfNeeded();
        }


        /// <summary>
        /// Submit a text string of sprites for drawing in the current batch.
        /// </summary>
        /// <param name="font">A font.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        public void DrawString(Font font, string text, Vector2 position, Color color)
        {
            CheckValid(font, text);

            float sortKey = (_sortMode == SpriteSortMode.Texture) ? font.Texture.SortingKey : 0;

            var x = position.X;
            var y = position.Y;

            for (int i = 0; i < text.Length; ++i)
            {
                var c = text[i];

                if (c == '\r')
                {
                    continue;
                }

                if (c == '\n')
                {
                    x = position.X;
                    y += font.LineSpacing;
                    continue;
                }

                var glyph = font.GetGlyphOrDefault(c);

                var xPos = x + glyph.Offset.X;
                var yPos = y + glyph.Offset.Y;

                if (font.IsKerningEnabled && i != 0)
                {
                    var kerning = font.GetKerning(text[i - 1], text[i]);
                    xPos += kerning;
                }

                var item = _batcher.CreateBatchItem();
                item.Texture = font.Texture;
                item.SortKey = sortKey;

                _texCoordTL.X = glyph.Region.X * font.Texture.Width;
                _texCoordTL.Y = glyph.Region.Y * font.Texture.Height;
                _texCoordBR.X = glyph.Region.X2 * font.Texture.Width;
                _texCoordBR.Y = glyph.Region.Y2 * font.Texture.Height;

                item.Set(
                    xPos,
                    yPos,
                    glyph.Region.Width,
                    glyph.Region.Height,
                    color,
                    _texCoordTL,
                    _texCoordBR,
                    0
                );

                x += glyph.XAdvance;
            }
        }


        internal void FlushIfNeeded()
        {
            if (_sortMode == SpriteSortMode.Immediate)
            {
                _batcher.DrawBatch(_sortMode, _currentShader);
            }
        }

        private void Setup()
        {
            Graphics.SetupView(0, Rect.FromBox(0, 0, GameWindow.Size.Width, GameWindow.Size.Height));
            _currentShader.ApplyParameters();
        }

        private void CheckValid(Texture texture)
        {
            if (texture == null)
            {
                throw new ArgumentNullException(nameof(texture));
            }

            if (!_beginCalled)
            {
                throw new InvalidOperationException("Draw was called, but Begin has not yet been called. Begin must be called successfully before you can call Draw.");
            }
        }

        private void CheckValid(Font font, string text)
        {
            if (font == null)
                throw new ArgumentNullException(nameof(font));
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (!_beginCalled)
                throw new InvalidOperationException("DrawString was called, but Begin has not yet been called. Begin must be called successfully before you can call DrawString.");
        }

    }
}
