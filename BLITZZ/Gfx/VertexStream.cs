using BLITZZ.Native.BGFX;
using System;
using BLITZZ.Content;

namespace BLITZZ.Gfx
{

    public class DynamicVertexStream<T> : DisposableResource where T : struct
    {
        internal T[] Vertices;


        private readonly DynamicIndexBufferHandle _indexBuffer;

        public void UpdateIndices(ushort[] indices)
        {
            Bgfx.UpdateDynamicIndexBuffer(_indexBuffer, 0, indices);

            Vertices = new T[(indices.Length / 6) * 4];
        }

        public DynamicVertexStream(ushort[] indices)
        {
            Vertices = new T[(indices.Length / 6) * 4];

            _indexBuffer = Bgfx.CreateDynamicIndexBuffer(indices);

            Assets.RegisterRuntimeLoaded(this);
        }

        internal void SubmitSpan(int startVerticesIndex, int vertexCount, int startIndiceIndex, int indexCount, VertexLayout vertexLayout)
        {
            Bgfx.SetDynamicIndexBuffer(_indexBuffer, startIndiceIndex, indexCount);
            var verticesSpan = new Span<T>(Vertices, startVerticesIndex, vertexCount);
            var transientVertexBuffer = Bgfx.CreateTransientVertexBuffer(verticesSpan, vertexLayout.Data);
            Bgfx.SetTransientVertexBuffer(0, transientVertexBuffer, 0, vertexCount);
        }

        protected override void FreeNativeResources()
        {
            Bgfx.DestroyDynamicIndexBuffer(_indexBuffer);
        }
    }
}
