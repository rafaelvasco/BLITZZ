using BLITZZ.Content;
using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

namespace BLITZZ.Native.BGFX
{
    internal static unsafe class Bgfx
    {
        public static void SetPlatformData(IntPtr windowHandle)
        {
            PlatformData platformData = new()
            {
                nwh = windowHandle.ToPointer()
            };
            BgfxNative.set_platform_data(&platformData);
        }

        public static Init Initialize(int resolutionWidth, int resolutionHeight, RendererType renderer, ICallbackHandler callbackHandler)
        {
            Init init = new();
            BgfxNative.init_ctor(&init);
            init.type = renderer;
            init.vendorId = (ushort)PciIdFlags.None;
            init.resolution.width = (uint)resolutionWidth;
            init.resolution.height = (uint)resolutionHeight;
            init.resolution.reset = (uint)ResetFlags.None;
            init.callback = CallbackShim.CreateShim(callbackHandler);
            BgfxNative.init(&init);

            return init;
        }

        public static IntPtr MakeRef(IntPtr data, uint size)
        {
            return new(BgfxNative.make_ref(data.ToPointer(), size));
        }

        public static void Frame(bool capture = false)
        {
            BgfxNative.frame(capture);
        }

        public static void Reset(int width, int height, ResetFlags resetFlags, TextureFormat textureFormat)
        {
            BgfxNative.reset((uint)width, (uint)height, (uint)resetFlags, textureFormat);
        }

        public static void SetDebug(DebugFlags flag)
        {
            BgfxNative.set_debug((uint)flag);
        }

        public static void SetViewMode(ushort viewId, ViewMode sequential)
        {
            BgfxNative.set_view_mode(viewId, sequential);
        }

        public static void SetViewClear(ushort viewId, ClearFlags flags, uint color, float depth = 0.0f, byte stencil = 1)
        {
            BgfxNative.set_view_clear(viewId, (ushort)flags, color, depth, stencil);
        }

        public static void Touch(ushort viewId)
        {
            BgfxNative.touch(viewId);
        }

        public static void SetViewRect(ushort viewId, int x, int y, int resolutionWidth, int resolutionHeight)
        {
            BgfxNative.set_view_rect(viewId, (ushort)x, (ushort)y, (ushort)resolutionWidth, (ushort)resolutionHeight);
        }

        public static void SetViewTransformMatrices(ushort viewId, ref float viewTransform, ref float projectionTransform)
        {
            BgfxNative.set_view_transform(viewId, Unsafe.AsPointer(ref viewTransform) , Unsafe.AsPointer(ref projectionTransform) );
        }

        public static TransientVertexBuffer CreateTransientVertexBuffer<T>(Span<T> vertices, VertexLayoutData layout) where T : struct
        {
            var transient_vtx_buffer = new TransientVertexBuffer();

            BgfxNative.alloc_transient_vertex_buffer(&transient_vtx_buffer, (uint)vertices.Length, &layout);

            var data_size = (uint)(vertices.Length * Unsafe.SizeOf<T>());

            Unsafe.CopyBlock(transient_vtx_buffer.data, Unsafe.AsPointer(ref vertices[0]), data_size);

            return transient_vtx_buffer;
        }

        public static TransientIndexBuffer CreateTransientIndexBuffer(Span<ushort> indices)
        {
            var transient_idx_buffer = new TransientIndexBuffer();

            BgfxNative.alloc_transient_index_buffer(&transient_idx_buffer, (uint)indices.Length, false);

            var data_size = (uint)(indices.Length * sizeof(ushort));

            Unsafe.CopyBlock(transient_idx_buffer.data, Unsafe.AsPointer(ref indices[0]), data_size);

            return transient_idx_buffer;
        }

        public static ShaderProgram CreateShaderProgram(byte[] vertexSrc, byte[] fragSrc, string[] samplers, string[] @params)
        {
            if (vertexSrc.Length == 0 || fragSrc.Length == 0)
            {
                throw new Exception("Cannot load ShaderProgram with empty shader sources");
            }

            var vertex_shader = CreateShader(vertexSrc);
            var frag_shader = CreateShader(fragSrc);

            if (!vertex_shader.Valid || !frag_shader.Valid)
            {
                throw new Exception("Could not load shader correctly.");
            }

            var program = new ShaderProgram(CreateProgram(vertex_shader, frag_shader, true), samplers, @params);

            if (!program.Program.Valid)
            {
                throw new Exception("Could not load shader correctly.");
            }

            return program;
        }

        private static ShaderHandle CreateShader(byte[] bytes)
        {
            var data = AllocGraphicsMemoryBuffer(bytes);
            var shader = BgfxNative.create_shader(data);
            return shader;
        }

        private static ProgramHandle CreateProgram(ShaderHandle vertex, ShaderHandle fragment, bool destroyShader)
        {
            var program = BgfxNative.create_program(vertex, fragment, destroyShader);
            return program;
        }

        public static UniformHandle CreateUniform(string name, UniformType type, int numElements)
        {
            var uniform = BgfxNative.create_uniform(name, type, (ushort)numElements);
            return uniform;
        }

        public static TextureHandle CreateTexture2D(ushort width, ushort height, bool hasmips, ushort layers, TextureFormat bgra8, SamplerFlags flags, IntPtr makeRef)
        {
            return BgfxNative.create_texture_2d(width, height, hasmips, layers, bgra8, (ulong)flags, (Memory*)makeRef.ToPointer());
        }

        public static void DestroyTexture2D(TextureHandle texture)
        {
            BgfxNative.destroy_texture(texture);
        }

        public static void UpdateTexture2D(TextureHandle texture, int layer, byte mip, int x, int y, int width, int height, byte[] pixelData, int pitch)
        {
            var data = GetMemoryBufferReference(pixelData);
            BgfxNative.update_texture_2d(texture, (ushort)layer, mip, (ushort)x, (ushort)y, (ushort)width, (ushort)height, data, (ushort)pitch);
        }

        public static IndexBufferHandle CreateIndexBuffer(ushort[] indices, BufferFlags flags = BufferFlags.None)
        {
            var memory = GetMemoryBufferReference(indices);
            var index_buffer = BgfxNative.create_index_buffer(memory, (ushort)flags);
            return index_buffer;
        }

        public static void DestroyIndexBuffer(IndexBufferHandle indexBuffer)
        {
            BgfxNative.destroy_index_buffer(indexBuffer);
        }

        public static VertexBufferHandle CreateVertexBuffer<T>(T[] vertices, VertexLayoutData layout, BufferFlags flags = BufferFlags.None) where T : struct
        {
            var memory = GetMemoryBufferReference(vertices);
            var vertex_buffer = BgfxNative.create_vertex_buffer(memory, &layout, (ushort)flags);
            return vertex_buffer;
        }

        public static void DestroyVertexBuffer(VertexBufferHandle vertexBuffer)
        {
            BgfxNative.destroy_vertex_buffer(vertexBuffer);
        }

        public static DynamicIndexBufferHandle CreateDynamicIndexBuffer(int indexCount, BufferFlags flags = BufferFlags.None)
        {
            var dyn_index_buffer = BgfxNative.create_dynamic_index_buffer((uint)indexCount, (ushort)flags);
            return dyn_index_buffer;
        }

        public static DynamicIndexBufferHandle CreateDynamicIndexBuffer(ushort[] indices, BufferFlags flags = BufferFlags.None)
        {
            var memory = GetMemoryBufferReference(indices);
            var dyn_index_buffer = BgfxNative.create_dynamic_index_buffer_mem(memory, (ushort)flags);
            return dyn_index_buffer;
        }

        public static void DestroyDynamicIndexBuffer(DynamicIndexBufferHandle indexBuffer)
        {
            BgfxNative.destroy_dynamic_index_buffer(indexBuffer);
        }

        public static void UpdateDynamicIndexBuffer(DynamicIndexBufferHandle handle, int startIndex, ushort[] indices)
        {
            var memory = GetMemoryBufferReference(indices);
            BgfxNative.update_dynamic_index_buffer(handle, (uint)startIndex, memory);
        }

        public static DynamicVertexBufferHandle CreateDynamicVertexBuffer(int vertexCount, VertexLayoutData layout, BufferFlags flags = BufferFlags.None)
        {
            var dyn_vertex_buffer = BgfxNative.create_dynamic_vertex_buffer((uint)vertexCount, &layout, (ushort)flags);
            return dyn_vertex_buffer;
        }

        public static DynamicVertexBufferHandle CreateDynamicVertexBuffer<T>(T[] vertices, VertexLayoutData layout, BufferFlags flags = BufferFlags.None) where T : struct
        {
            var memory = GetMemoryBufferReference(vertices);
            var dyn_vertex_buffer = BgfxNative.create_dynamic_vertex_buffer_mem(memory, &layout, (ushort)flags);
            return dyn_vertex_buffer;
        }

        public static void UpdateDynamicVertexBuffer<T>(DynamicVertexBufferHandle handle, int startVertex, T[] vertices) where T : struct
        {
            var memory = GetMemoryBufferReference(vertices);
            BgfxNative.update_dynamic_vertex_buffer(handle, (uint)startVertex, memory);
        }

        public static void DestroyDynamicVertexBuffer(DynamicVertexBufferHandle vertexBuffer)
        {
            BgfxNative.destroy_dynamic_vertex_buffer(vertexBuffer);
        }

        public static TextureHandle CreateTexture2D(int width, int height, bool hasMips, int numLayers, TextureFormat texFormat, SamplerFlags flags, byte[] pixelData)
        {
            Memory* data = AllocGraphicsMemoryBuffer(pixelData);

            TextureHandle tex = BgfxNative.create_texture_2d((ushort)width, (ushort)height, hasMips, (ushort)numLayers, texFormat, (ulong)flags, data);
            return tex;
        }

        public static TextureHandle CreateTexture2D(int width, int height, int bytesPerPixel, bool hasMips, int numLayers, TextureFormat texFormat, SamplerFlags flags, IntPtr pixelData)
        {
            var data = AllocGraphicsMemoryBuffer(pixelData, width * height * bytesPerPixel);
            TextureHandle tex = BgfxNative.create_texture_2d((ushort)width, (ushort)height, hasMips, (ushort)numLayers, texFormat, (ulong)flags, data);
            return tex;
        }

        public static TextureHandle CreateTexture2D<T>(int width, int height, bool hasMips, int numLayers, TextureFormat texFormat, SamplerFlags flags, T[] pixelBytes) where T : struct
        {
            var data = AllocGraphicsMemoryBuffer(pixelBytes);
            TextureHandle tex = BgfxNative.create_texture_2d((ushort)width, (ushort)height, hasMips, (ushort)numLayers, texFormat, (ulong)flags, data);
            return tex;
        }

        public static TextureHandle CreateDynamicTexture2D(int width, int height, bool hasMips, int numLayers, TextureFormat texFormat, SamplerFlags flags, byte[] pixelData)
        {
            Memory* data = AllocGraphicsMemoryBuffer(pixelData);
            TextureHandle tex = BgfxNative.create_texture_2d((ushort)width, (ushort)height, hasMips, (ushort)numLayers, texFormat, (ulong)flags, null);
            BgfxNative.update_texture_2d(tex, 0, 0, 0, 0, (ushort)width, (ushort)height, data, ushort.MaxValue);
            return tex;
        }

        public static FrameBufferHandle CreateFrameBuffer(int width, int height, TextureFormat texFormat, TextureFlags texFlags)
        {
            return BgfxNative.create_frame_buffer((ushort)width, (ushort)height, texFormat, (ulong)texFlags);
        }

        public static FrameBufferHandle CreateFrameBuffer(TextureHandle texture, TextureFormat texFormat, TextureFlags texFlags)
        {
            var attachment = stackalloc Attachment[1];

            attachment->handle = texture;
            attachment->access = Access.ReadWrite;
            attachment->layer = 0;
            attachment->mip = 0;
            attachment->resolve = 0;

            return BgfxNative.create_frame_buffer_from_attachment(1, attachment, false);
        }

        public static void DestroyFrameBuffer(FrameBufferHandle frameBuffer)
        {
            BgfxNative.destroy_frame_buffer(frameBuffer);
        }

        public static void SetDynamicIndexBuffer(DynamicIndexBufferHandle indexBuffer, int firstIndex, int numIndices)
        {
            BgfxNative.set_dynamic_index_buffer(indexBuffer, (uint)firstIndex, (uint)numIndices);
        }

        public static void SetDynamicVertexBuffer(byte stream, DynamicVertexBufferHandle vertexBuffer, int startVertex, int numVertices)
        {
            BgfxNative.set_dynamic_vertex_buffer(stream, vertexBuffer, (uint)startVertex, (uint)numVertices);
        }

        public static void SetTransientVertexBuffer(byte stream, TransientVertexBuffer vertexBuffer, int startVertex, int numVertices)
        {
            BgfxNative.set_transient_vertex_buffer(stream, &vertexBuffer, (uint)startVertex, (uint)numVertices);
        }

        public static void SetTransientIndexBuffer(TransientIndexBuffer indexBuffer, int firstIndex, int numIndices)
        {
            BgfxNative.set_transient_index_buffer(&indexBuffer, (uint)firstIndex, (uint)numIndices);
        }

        public static void SetIndexBuffer(IndexBufferHandle indexBuffer, int firstIndex, int numIndices)
        {
            BgfxNative.set_index_buffer(indexBuffer, (uint)firstIndex, (uint)numIndices);
        }

        public static void SetVertexBuffer(byte stream, VertexBufferHandle vertexBuffer, int startVertex, int numVertices)
        {
            BgfxNative.set_vertex_buffer(stream, vertexBuffer, (uint)startVertex, (uint)numVertices);
        }

        public static void SetFrameBuffer(ushort viewId, FrameBufferHandle handle)
        {
            BgfxNative.set_view_frame_buffer(viewId, handle);
        }

        public static int GetAvailableTransientVertexBuffers(int requiredVertexCount, VertexLayoutData layout)
        {
            return (int)BgfxNative.get_avail_transient_vertex_buffer((uint)requiredVertexCount, (VertexLayoutData*)Unsafe.AsPointer(ref layout));
        }

        public static int GetAvailableTransientIndexBuffers(int numIndices)
        {
            return (int)BgfxNative.get_avail_transient_index_buffer((uint)numIndices, false);
        }

        private static Memory* AllocGraphicsMemoryBuffer<T>(T[] array)
        {
            var size = (uint)(array.Length * Unsafe.SizeOf<T>());
            var data = BgfxNative.alloc(size);
            Unsafe.CopyBlockUnaligned(data->data, Unsafe.AsPointer(ref array[0]), size);
            return data;
        }

        private static Memory* AllocGraphicsMemoryBuffer(IntPtr dataPtr, int dataSize)
        {
            var data = BgfxNative.alloc((uint)dataSize);
            Unsafe.CopyBlockUnaligned(data->data, dataPtr.ToPointer(), (uint)dataSize);
            return data;
        }

        private static Memory* GetMemoryBufferReference<T>(T[] array)
        {
            var size = (uint)(array.Length * Unsafe.SizeOf<T>());
            var data = BgfxNative.make_ref(Unsafe.AsPointer(ref array[0]), size);
            return data;
        }

        public static TextureHandle GetFrameBufferTexture(FrameBufferHandle frameBuffer, byte attachment)
        {
            return BgfxNative.get_texture(frameBuffer, attachment);
        }

        public static void SetState(StateFlags state)
        {
            BgfxNative.set_state((ulong)state, 0);
        }

        public static void SetTexture(byte textureUnit, UniformHandle uniform, TextureHandle texture, SamplerFlags flags = (SamplerFlags)uint.MaxValue)
        {
            BgfxNative.set_texture(textureUnit, uniform, texture, (uint)flags);
        }

        public static void SetTransientVertexBuffer(byte stream, ref TransientVertexBuffer tvb, uint startVertex,
            uint numVertices)
        {
            BgfxNative.set_transient_vertex_buffer(stream, (TransientVertexBuffer*)Unsafe.AsPointer(ref tvb), startVertex, numVertices);
        }

        public static void SetTransientIndexBuffer(ref TransientIndexBuffer tib, uint firstIndex, uint numIndices)
        {
            BgfxNative.set_transient_index_buffer((TransientIndexBuffer*)Unsafe.AsPointer(ref tib), firstIndex, numIndices);
        }

        public static void SetScissor(int x, int y, int width, int height)
        {
            BgfxNative.set_scissor((ushort)x, (ushort)y, (ushort)width, (ushort)height);
        }

        public static void Submit(ushort viewId, ProgramHandle program, uint depth = 0, bool preserveState = false)
        {
            BgfxNative.submit(viewId, program, depth, preserveState ? (byte)1 : (byte)0);
        }

        public static void SetUniform(UniformHandle uniform, float value)
        {
            BgfxNative.set_uniform(uniform, &value, 1);
        }

        public static void SetUniform(UniformHandle uniform, ref Vector4 value)
        {
            BgfxNative.set_uniform(uniform, Unsafe.AsPointer(ref value), 1);
        }

        public static void DestroyProgram(ProgramHandle shaderProgram)
        {
            BgfxNative.destroy_program(shaderProgram);
        }

        public static void DestroyShader(ShaderHandle shader)
        {
            BgfxNative.destroy_shader(shader);
        }

        public static void DestroyUniform(UniformHandle uniform)
        {
            BgfxNative.destroy_uniform(uniform);
        }

        public static void VertexLayoutBegin(ref VertexLayoutData layout, RendererType rendererType)
        {
            BgfxNative.vertex_layout_begin((VertexLayoutData*)Unsafe.AsPointer(ref layout), rendererType);
        }

        public static void VertexLayoutAdd(ref VertexLayoutData layout, Attrib attrib, AttribType attribType, byte num, bool normalized, bool asInt)
        {
            BgfxNative.vertex_layout_add((VertexLayoutData*)Unsafe.AsPointer(ref layout), attrib, num, attribType, normalized, asInt);
        }

        public static void VertexLayoutSkip(ref VertexLayoutData layout, int count)
        {
            BgfxNative.vertex_layout_skip((VertexLayoutData*) Unsafe.AsPointer(ref layout), (byte) count);
        }

        public static void VertexLayoutEnd(ref VertexLayoutData layout)
        {
            BgfxNative.vertex_layout_end((VertexLayoutData*)Unsafe.AsPointer(ref layout));
        }

        public static void RequestScreenShot(string path)
        {
            BgfxNative.request_screen_shot(FrameBufferHandle.Null, path);
        }

        public static void Shutdown()
        {
            CallbackShim.FreeShim();
            BgfxNative.shutdown();
        }

        /// <summary>
        /// Clears the debug text buffer.
        /// </summary>
        /// <param name="color">The color with which to clear the background.</param>
        /// <param name="smallText"><c>true</c> to use a small font for debug output; <c>false</c> to use normal sized text.</param>
        public static void DebugTextClear(DebugColor color = DebugColor.Black, bool smallText = false)
        {
            var attr = (byte)((byte)color << 4);
            BgfxNative.dbg_text_clear(attr, smallText);
        }

        /// <summary>
        /// Writes debug text to the screen.
        /// </summary>
        /// <param name="x">The X position, in cells.</param>
        /// <param name="y">The Y position, in cells.</param>
        /// <param name="foreColor">The foreground color of the text.</param>
        /// <param name="backColor">The background color of the text.</param>
        /// <param name="format">The format of the message.</param>
        /// <param name="args">The arguments with which to format the message.</param>
        public static void DebugTextWrite (int x, int y, DebugColor foreColor, DebugColor backColor, string format, params object[] args) {
            DebugTextWrite(x, y, foreColor, backColor, string.Format(CultureInfo.CurrentCulture, format, args));
        }

        /// <summary>
        /// Writes debug text to the screen.
        /// </summary>
        /// <param name="x">The X position, in cells.</param>
        /// <param name="y">The Y position, in cells.</param>
        /// <param name="foreColor">The foreground color of the text.</param>
        /// <param name="backColor">The background color of the text.</param>
        /// <param name="message">The message to write.</param>
        public static void DebugTextWrite (int x, int y, DebugColor foreColor, DebugColor backColor, string message) {
            var attr = (byte)(((byte)backColor << 4) | (byte)foreColor);
            BgfxNative.dbg_text_printf((ushort)x, (ushort)y, attr, "%s", message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StateFlags STATE_BLEND_FUNC(StateFlags src, StateFlags dst)
        {
            return STATE_BLEND_FUNC_SEPARATE(src, dst, src, dst);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StateFlags STATE_BLEND_FUNC_SEPARATE(StateFlags srcRgb, StateFlags dstRgb, StateFlags srcA, StateFlags dstA)
        {
            return (StateFlags)((((ulong)(srcRgb) | ((ulong)(dstRgb) << 4))) | (((ulong)(srcA) | ((ulong)(dstA) << 4)) << 8));
        }

        public abstract class DefaultCallbackHandler : ICallbackHandler
        {
            public virtual void ProfilerBegin(string name, int color, string filePath, int line) { }
            public virtual void ProfilerEnd() { }
            public virtual void CaptureStarted(int width, int height, int pitch, TextureFormat format, bool flipVertical) { }
            public virtual void CaptureFrame(IntPtr data, int size) { }
            public virtual void CaptureFinished() { }
            public virtual int GetCachedSize(long id) { return 0; }
            public virtual bool GetCacheEntry(long id, IntPtr data, int size) { return false; }
            public virtual void SetCacheEntry(long id, IntPtr data, int size) { }
            public virtual void SaveScreenShot(string path, int width, int height, int pitch, IntPtr data, int size, bool flipVertical) { }
            public virtual void ReportDebug(string fileName, int line, string format, IntPtr args)
            {
            }
            public virtual void ReportError(string fileName, int line, Fatal errorType, string message)
            {
            }
        }
    }
}
