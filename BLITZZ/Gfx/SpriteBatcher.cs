using System;
using BLITZZ.Content;

namespace BLITZZ.Gfx
{
    internal class SpriteBatcher
    {
        internal int DrawCalls { get; set; }

        private const int InitialBatchSize = 256;

        private const int MaxBatchSize = short.MaxValue / 6;

        private SpriteBatchItem[] _batchItemList;

        private readonly DynamicVertexStream<Vertex> _vertexStream;

        private int _batchItemCount;

        private ushort[] _indices;

        public SpriteBatcher(int capacity = 0)
        {
            if (capacity <= 0)
                capacity = InitialBatchSize;
            else
                capacity = (capacity + 63) & (~63); // ensure chunks of 64.

            _batchItemList = new SpriteBatchItem[capacity];
            _batchItemCount = 0;

            for (int i = 0; i < capacity; i++)
                _batchItemList[i] = new SpriteBatchItem();

            EnsureArrayCapacity(capacity);
            
            _vertexStream = new DynamicVertexStream<Vertex>(_indices);
        }

        /// <summary>
        /// Reuse a previously allocated SpriteBatchItem from the item pool. 
        /// if there is none available grow the pool and initialize new items.
        /// </summary>
        /// <returns></returns>
        public SpriteBatchItem CreateBatchItem()
        {
            if (_batchItemCount >= _batchItemList.Length)
            {
                var oldSize = _batchItemList.Length;
                var newSize = oldSize + oldSize/2; // grow by x1.5
                newSize = (newSize + 63) & (~63); // grow in chunks of 64.
                Array.Resize(ref _batchItemList, newSize);
                for(int i=oldSize; i<newSize; i++)
                    _batchItemList[i]=new SpriteBatchItem();

                EnsureArrayCapacity(Math.Min(newSize, MaxBatchSize));
            }
            var item = _batchItemList[_batchItemCount++];
            return item;
        }

        /// <summary>
        /// Sorts the batch items and then groups batch drawing into maximal allowed batch sets that do not
        /// overflow the 16 bit array indices for vertices.
        /// </summary>
        /// <param name="sortMode">The type of depth sorting desired for the rendering.</param>
        /// <param name="shader">Current ShaderProgram.</param>
        public unsafe void DrawBatch(SpriteSortMode sortMode, ShaderProgram shader)
		{
			// nothing to do
            if (_batchItemCount == 0)
				return;
			
			// sort the batch items
			switch ( sortMode )
			{
			    case SpriteSortMode.Texture :                
			    case SpriteSortMode.FrontToBack :
			    case SpriteSortMode.BackToFront :
                    Array.Sort(_batchItemList, 0, _batchItemCount);
				    break;
			}

            // Determine how many iterations through the drawing code we need to make
            int batchIndex = 0;
            int batchCount = _batchItemCount;
            

            // Iterate through the batches, doing short.MaxValue sets of vertices only.
            while(batchCount > 0)
            {
                // setup the vertexArray array
                var startIndex = 0;
                var index = 0;
                Texture tex = null;

                int numBatchesToProcess = batchCount;
                if (numBatchesToProcess > MaxBatchSize)
                {
                    numBatchesToProcess = MaxBatchSize;
                }
                // Avoid the array checking overhead by using pointer indexing!
                fixed (Vertex* vertexArrayFixedPtr = _vertexStream.Vertices)
                {
                    var vertexArrayPtr = vertexArrayFixedPtr;

                    // Draw the batches
                    for (int i = 0; i < numBatchesToProcess; i++, batchIndex++, index += 4, vertexArrayPtr += 4)
                    {
                        SpriteBatchItem item = _batchItemList[batchIndex];
                        // if the texture changed, we need to flush and bind the new texture
                        var shouldFlush = !ReferenceEquals(item.Texture, tex);
                        if (shouldFlush)
                        {
                            FlushVertexArray(startIndex, index, tex, shader);

                            tex = item.Texture;
                            startIndex = index = 0;
                            vertexArrayPtr = vertexArrayFixedPtr;
                            //shader.SetTexture(0, tex);
                        }

                        // store the SpriteBatchItem data in our vertexArray
                        *(vertexArrayPtr+0) = item.TopLeft;
                        *(vertexArrayPtr+1) = item.TopRight;
                        *(vertexArrayPtr+2) = item.BottomLeft;
                        *(vertexArrayPtr+3) = item.BottomRight;

                        // Release the texture.
                        item.Texture = null;
                    }
                }
                // flush the remaining vertexArray data
                FlushVertexArray(startIndex, index, tex, shader);
                // Update our batch count to continue the process of culling down
                // large batches
                batchCount -= numBatchesToProcess;
            }
            // return items to the pool.  
            _batchItemCount = 0;
		}

        /// <summary>
        /// Sends the triangle list to the graphics device. Here is where the actual drawing starts.
        /// </summary>
        /// <param name="start">Start index of vertices to draw. Not used except to compute the count of vertices to draw.</param>
        /// <param name="end">End index of vertices to draw. Not used except to compute the count of vertices to draw.</param>
        /// <param name="texture">Texture to draw.</param>
        /// <param name="shader">Current ShaderProgram.</param>
        private void FlushVertexArray(int start, int end, Texture texture, ShaderProgram shader)
        {
            if (start == end)
                return;

            DrawCalls += 1;

            var vertexCount = end - start;

            shader.SetTexture(0, texture);

            Graphics.SubmitVertexStream(_vertexStream, shader, vertexCount);

            // If the effect is not null, then apply each pass and render the geometry
            //if (effect != null)
            //{
            //    var passes = effect.CurrentTechnique.Passes;
            //    foreach (var pass in passes)
            //    {
            //        pass.Apply();

            //        // Whatever happens in pass.Apply, make sure the texture being drawn
            //        // ends up in Textures[0].
            //        _device.Textures[0] = texture;

            //        _device.DrawUserIndexedPrimitives(
            //            PrimitiveType.TriangleList,
            //            _vertexArray,
            //            0,
            //            vertexCount,
            //            _index,
            //            0,
            //            (vertexCount / 4) * 2,
            //            VertexPositionColorTexture.VertexDeclaration);
            //    }
            //}
            //else
            //{
            //    // If no custom effect is defined, then simply render.
            //    _device.DrawUserIndexedPrimitives(
            //        PrimitiveType.TriangleList,
            //        _vertexArray,
            //        0,
            //        vertexCount,
            //        _index,
            //        0,
            //        (vertexCount / 4) * 2,
            //        VertexPositionColorTexture.VertexDeclaration);
            //}
        }

        private unsafe void EnsureArrayCapacity(int numBatchItems)
        {
            int neededCapacity = 6 * numBatchItems;
            if (_indices != null && neededCapacity <= _indices.Length)
            {
                // Short circuit out of here because we have enough capacity.
                return;
            }
            ushort[] newIndex = new ushort[6 * numBatchItems];
            int start = 0;
            if (_indices != null)
            {
                _indices.CopyTo(newIndex, 0);
                start = _indices.Length / 6;
            }
            fixed (ushort* indexFixedPtr = newIndex)
            {
                var indexPtr = indexFixedPtr + (start * 6);
                for (var i = start; i < numBatchItems; i++, indexPtr += 6)
                {
                    /*
                     *  TL    TR
                     *   0----1 0,1,2,3 = index offsets for vertex indices
                     *   |   /| TL,TR,BL,BR are vertex references in SpriteBatchItem.
                     *   |  / |
                     *   | /  |
                     *   |/   |
                     *   2----3
                     *  BL    BR
                     */
                    // Triangle 1
                    *(indexPtr + 0) = (ushort)(i * 4);
                    *(indexPtr + 1) = (ushort)(i * 4 + 1);
                    *(indexPtr + 2) = (ushort)(i * 4 + 2);
                    // Triangle 2
                    *(indexPtr + 3) = (ushort)(i * 4 + 1);
                    *(indexPtr + 4) = (ushort)(i * 4 + 3);
                    *(indexPtr + 5) = (ushort)(i * 4 + 2);
                }
            }
            _indices = newIndex;

            _vertexStream?.UpdateIndices(_indices);
        }
    }
}
