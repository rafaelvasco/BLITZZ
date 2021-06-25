using BLITZZ.Native.BGFX;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BLITZZ.Gfx
{
    public enum VertexStreamMode
    {
        Static,
        Dynamic,
        Stream
    }

    internal unsafe class QuadVertexStream : DisposableResource
    {
        private const int MAX_BATCH_COUNT = short.MaxValue / 6;

        public int VertexCount => _vertex_count;

        private readonly IndexBufferHandle _index_buffer;
        private readonly VertexBufferHandle _static_vertex_buffer;
        private readonly DynamicVertexBufferHandle _dynamic_vertex_buffer;

        private readonly VertexStreamMode _stream_mode;
        private Vertex[] _vertices;
        private ushort[] _indices;
        private VertexLayout _vertex_layout = Vertex.VertexLayout;
        private bool _stream_changed = true;
        private int _vertex_count;
        private int _index_count;
        private bool _locked = false;

        public void SetVertexLayout(VertexLayout layout)
        {
            _vertex_layout = layout;
        }

        public QuadVertexStream(VertexStreamMode mode, int quadCount)
        {
            _stream_mode = mode;

            _vertex_count = 0;
            _index_count = 0;

            BuildArrays(quadCount);

            _index_buffer = Bgfx.CreateIndexBuffer(_indices);

            switch (mode)
            {
                case VertexStreamMode.Static:
                    _static_vertex_buffer = Bgfx.CreateVertexBuffer(_vertices, _vertex_layout);
                    break;
                case VertexStreamMode.Dynamic:
                    _dynamic_vertex_buffer = Bgfx.CreateDynamicVertexBuffer(_vertices, _vertex_layout);
                    break;
            }
        }

        public void Reset()
        {
            _vertex_count = 0;
            _index_count = 0;
        }

        public bool PushQuad(in Quad quad)
        {
            if (_locked)
            {
                return false;
            }

            _stream_changed = true;

            var v0 = quad.TopLeft;
            var v1 = quad.TopRight;
            var v2 = quad.BottomLeft;
            var v3 = quad.BottomRight;

            fixed (Vertex* vertex_ref = &MemoryMarshal.GetArrayDataReference(_vertices))
            {
                Vertex* vertex_ptr = vertex_ref + _vertex_count;

                *(vertex_ptr) = new Vertex(v0.X, v0.Y, v0.Z, v0.Col, v0.Tx, v0.Ty);
                *(vertex_ptr + 1) = new Vertex(v1.X, v1.Y, v1.Z, v1.Col, v1.Tx, v1.Ty);
                *(vertex_ptr + 2) = new Vertex(v2.X, v2.Y, v2.Z, v2.Col, v2.Tx, v2.Ty); ;
                *(vertex_ptr + 3) = new Vertex(v3.X, v3.Y, v3.Z, v3.Col, v3.Tx, v3.Ty);
            }

            unchecked
            {
                _vertex_count += 4;

                _index_count += 6;
            }

           

            return true;
        }

        internal void SubmitSpan(int start_vertex_index, int vertex_count, int start_indice_index, int index_count)
        {
            switch (_stream_mode)
            {
                case VertexStreamMode.Static:

                    if (!_locked)
                    {
                        _locked = true;
                    }

                    Bgfx.SetIndexBuffer(_index_buffer, start_indice_index, index_count);
                    Bgfx.SetVertexBuffer(0, _static_vertex_buffer, start_vertex_index, vertex_count);
                    break;

                case VertexStreamMode.Dynamic:

                    if (_stream_changed)
                    {
                        Bgfx.UpdateDynamicVertexBuffer(_dynamic_vertex_buffer, 0, _vertices);
                        _stream_changed = false;
                    }

                    Bgfx.SetIndexBuffer(_index_buffer, start_indice_index, index_count);
                    Bgfx.SetDynamicVertexBuffer(0, _dynamic_vertex_buffer, start_vertex_index, vertex_count);
                    break;

                case VertexStreamMode.Stream:
                    var vertices_span = new Span<Vertex>(_vertices, start_vertex_index, vertex_count);
                    var transient_buffer = Bgfx.CreateTransientVertexBuffer(vertices_span, _vertex_layout);
                    Bgfx.SetIndexBuffer(_index_buffer, 0, index_count);
                    Bgfx.SetTransientVertexBuffer(0, transient_buffer, 0, vertex_count);

                    break;
            }
        }

        internal void Submit()
        {
            SubmitSpan(0, _vertex_count, 0, _index_count);
        }

        protected override void FreeNativeResources()
        {
            Bgfx.DestroyIndexBuffer(_index_buffer);

            if (_static_vertex_buffer.idx > 0)
            {
                Bgfx.DestroyVertexBuffer(_static_vertex_buffer);
            }

            if (_dynamic_vertex_buffer.idx > 0)
            {
                Bgfx.DestroyDynamicVertexBuffer(_dynamic_vertex_buffer);
            }
        }

        private void BuildArrays(int quadCount)
        {
            int neededCapacity = 6 * quadCount;

            if (_indices != null && neededCapacity < _indices.Length)
            {
                return;

            }

            ushort[] newIndices = new ushort[6 * quadCount];

            int start = 0;

            if (_indices != null)
            {
                Unsafe.CopyBlockUnaligned(Unsafe.AsPointer(ref newIndices[0]), Unsafe.AsPointer(ref _indices), (uint)_indices.Length * sizeof(ushort));
                start = _indices.Length / 6;
            }

            fixed (ushort* indexFixedPtr = newIndices)
            {
                var indexPtr = indexFixedPtr + (start * 6);

                for (var i = start; i < quadCount; i++, indexPtr += 6)
                {
                    // Triangle 1
                    *(indexPtr + 0) = (ushort)(i * 4 + 0); // Quad TopLeft
                    *(indexPtr + 1) = (ushort)(i * 4 + 1); // Quad TopRight
                    *(indexPtr + 2) = (ushort)(i * 4 + 2); // Quad BottomRight
                    // Triangle 2
                    *(indexPtr + 3) = (ushort)(i * 4 + 0); // Quad TopLeft
                    *(indexPtr + 4) = (ushort)(i * 4 + 2); // Quad BottomRight
                    *(indexPtr + 5) = (ushort)(i * 4 + 3); // Quad BottomLeft
                }
            }

            _indices = newIndices;

            _vertices = new Vertex[quadCount * 4];
           
        }
    }
}
