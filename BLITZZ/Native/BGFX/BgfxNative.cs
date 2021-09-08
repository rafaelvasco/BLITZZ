/*
 * Copyright 2011-2021 Branimir Karadzic. All rights reserved.
 * License: https://github.com/bkaradzic/bgfx/blob/master/LICENSE
 */

/*
 *
 * AUTO GENERATED! DO NOT EDIT!
 *
 */

using System;
using System.Runtime.InteropServices;

namespace BLITZZ.Native.BGFX
{
	internal static partial class BgfxNative
	{

		const string DllName = "BGFX.dll";

		/// <summary>
		/// Init attachment.
		/// </summary>
		///
		/// <param name="_handle">Render target texture handle.</param>
		/// <param name="_access">Access. See `Access::Enum`.</param>
		/// <param name="_layer">Cubemap side or depth layer/slice to use.</param>
		/// <param name="_numLayers">Number of texture layer/slice(s) in array to use.</param>
		/// <param name="_mip">Mip level.</param>
		/// <param name="_resolve">Resolve flags. See: `BGFX_RESOLVE_*`</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_attachment_init", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void attachment_init(Attachment* _this, TextureHandle _handle, Access _access, ushort _layer, ushort _numLayers, ushort _mip, byte _resolve);

		/// <summary>
		/// Start VertexLayout.
		/// </summary>
		///
		/// <param name="_rendererType">Renderer backend type. See: `bgfx::RendererType`</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_vertex_layout_begin", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe VertexLayoutData* vertex_layout_begin(VertexLayoutData* _this, RendererType _rendererType);

		/// <summary>
		/// Add attribute to VertexLayout.
		/// @remarks Must be called between begin/end.
		/// </summary>
		///
		/// <param name="_attrib">Attribute semantics. See: `bgfx::Attrib`</param>
		/// <param name="_num">Number of elements 1, 2, 3 or 4.</param>
		/// <param name="_type">Element type.</param>
		/// <param name="_normalized">When using fixed point AttribType (f.e. Uint8) value will be normalized for vertex shader usage. When normalized is set to true, AttribType::Uint8 value in range 0-255 will be in range 0.0-1.0 in vertex shader.</param>
		/// <param name="_asInt">Packaging rule for vertexPack, vertexUnpack, and vertexConvert for AttribType::Uint8 and AttribType::Int16. Unpacking code must be implemented inside vertex shader.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_vertex_layout_add", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe VertexLayoutData* vertex_layout_add(VertexLayoutData* _this, Attrib _attrib, byte _num, AttribType _type, bool _normalized, bool _asInt);

		/// <summary>
		/// Decode attribute.
		/// </summary>
		///
		/// <param name="_attrib">Attribute semantics. See: `bgfx::Attrib`</param>
		/// <param name="_num">Number of elements.</param>
		/// <param name="_type">Element type.</param>
		/// <param name="_normalized">Attribute is normalized.</param>
		/// <param name="_asInt">Attribute is packed as int.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_vertex_layout_decode", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void vertex_layout_decode(VertexLayoutData* _this, Attrib _attrib, byte* _num, AttribType* _type, bool* _normalized, bool* _asInt);

		/// <summary>
		/// Returns `true` if VertexLayout contains attribute.
		/// </summary>
		///
		/// <param name="_attrib">Attribute semantics. See: `bgfx::Attrib`</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_vertex_layout_has", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern unsafe bool vertex_layout_has(VertexLayoutData* _this, Attrib _attrib);

		/// <summary>
		/// Skip `_num` bytes in vertex stream.
		/// </summary>
		///
		/// <param name="_num">Number of bytes to skip.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_vertex_layout_skip", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe VertexLayoutData* vertex_layout_skip(VertexLayoutData* _this, byte _num);

		/// <summary>
		/// End VertexLayout.
		/// </summary>
		///
		[DllImport(DllName, EntryPoint = "bgfx_vertex_layout_end", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void vertex_layout_end(VertexLayoutData* _this);


        /// <summary>
        /// Pack vertex attribute into vertex stream format.
        /// </summary>
        ///
        /// <param name="_input">Value to be packed into vertex stream.</param>
        /// <param name="_inputNormalized">`true` if input value is already normalized.</param>
        /// <param name="_attr">Attribute to pack.</param>
        /// <param name="_layout">Vertex stream layout.</param>
        /// <param name="_data">Destination vertex stream where data will be packed.</param>
        /// <param name="_index">Vertex index that will be modified.</param>
        ///
        [DllImport(DllName, EntryPoint = "bgfx_vertex_pack", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void vertex_pack(float _input, bool _inputNormalized, Attrib _attr, VertexLayoutData* _layout, void* _data, uint _index);

		/// <summary>
		/// Unpack vertex attribute from vertex stream format.
		/// </summary>
		///
		/// <param name="_output">Result of unpacking.</param>
		/// <param name="_attr">Attribute to unpack.</param>
		/// <param name="_layout">Vertex stream layout.</param>
		/// <param name="_data">Source vertex stream from where data will be unpacked.</param>
		/// <param name="_index">Vertex index that will be unpacked.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_vertex_unpack", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void vertex_unpack(float _output, Attrib _attr, VertexLayoutData* _layout, void* _data, uint _index);

		/// <summary>
		/// Converts vertex stream data from one vertex stream format to another.
		/// </summary>
		///
		/// <param name="_dstLayout">Destination vertex stream layout.</param>
		/// <param name="_dstData">Destination vertex stream.</param>
		/// <param name="_srcLayout">Source vertex stream layout.</param>
		/// <param name="_srcData">Source vertex stream data.</param>
		/// <param name="_num">Number of vertices to convert from source to destination.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_vertex_convert", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void vertex_convert(VertexLayoutData* _dstLayout, void* _dstData, VertexLayoutData* _srcLayout, void* _srcData, uint _num);

		/// <summary>
		/// Weld vertices.
		/// </summary>
		///
		/// <param name="_output">Welded vertices remapping table. The size of buffer must be the same as number of vertices.</param>
		/// <param name="_layout">Vertex stream layout.</param>
		/// <param name="_data">Vertex stream.</param>
		/// <param name="_num">Number of vertices in vertex stream.</param>
		/// <param name="_index32">Set to `true` if input indices are 32-bit.</param>
		/// <param name="_epsilon">Error tolerance for vertex position comparison.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_weld_vertices", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint weld_vertices(void* _output, VertexLayoutData* _layout, void* _data, uint _num, bool _index32, float _epsilon);

		/// <summary>
		/// Convert index buffer for use with different primitive topologies.
		/// </summary>
		///
		/// <param name="_conversion">Conversion type, see `TopologyConvert::Enum`.</param>
		/// <param name="_dst">Destination index buffer. If this argument is NULL function will return number of indices after conversion.</param>
		/// <param name="_dstSize">Destination index buffer in bytes. It must be large enough to contain output indices. If destination size is insufficient index buffer will be truncated.</param>
		/// <param name="_indices">Source indices.</param>
		/// <param name="_numIndices">Number of input indices.</param>
		/// <param name="_index32">Set to `true` if input indices are 32-bit.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_topology_convert", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint topology_convert(TopologyConvert _conversion, void* _dst, uint _dstSize, void* _indices, uint _numIndices, bool _index32);

		/// <summary>
		/// Sort indices.
		/// </summary>
		///
		/// <param name="_sort">Sort order, see `TopologySort::Enum`.</param>
		/// <param name="_dst">Destination index buffer.</param>
		/// <param name="_dstSize">Destination index buffer in bytes. It must be large enough to contain output indices. If destination size is insufficient index buffer will be truncated.</param>
		/// <param name="_dir">Direction (vector must be normalized).</param>
		/// <param name="_pos">Position.</param>
		/// <param name="_vertices">Pointer to first vertex represented as float x, y, z. Must contain at least number of vertices referencende by index buffer.</param>
		/// <param name="_stride">Vertex stride.</param>
		/// <param name="_indices">Source indices.</param>
		/// <param name="_numIndices">Number of input indices.</param>
		/// <param name="_index32">Set to `true` if input indices are 32-bit.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_topology_sort_tri_list", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void topology_sort_tri_list(TopologySort _sort, void* _dst, uint _dstSize, float _dir, float _pos, void* _vertices, uint _stride, void* _indices, uint _numIndices, bool _index32);

		/// <summary>
		/// Returns supported backend API renderers.
		/// </summary>
		///
		/// <param name="_max">Maximum number of elements in _enum array.</param>
		/// <param name="_enum">Array where supported renderers will be written.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_get_supported_renderers", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe byte get_supported_renderers(byte _max, RendererType* _enum);

		/// <summary>
		/// Returns name of renderer.
		/// </summary>
		///
		/// <param name="_type">Renderer backend type. See: `bgfx::RendererType`</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_get_renderer_name", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe IntPtr get_renderer_name(RendererType _type);

		[DllImport(DllName, EntryPoint = "bgfx_init_ctor", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void init_ctor(Init* _init);

		/// <summary>
		/// Initialize bgfx library.
		/// </summary>
		///
		/// <param name="_init">Initialization parameters. See: `bgfx::Init` for more info.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_init", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern unsafe bool init(Init* _init);

		/// <summary>
		/// Shutdown bgfx library.
		/// </summary>
		///
		[DllImport(DllName, EntryPoint = "bgfx_shutdown", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void shutdown();

		/// <summary>
		/// Reset graphic settings and back-buffer size.
		/// @attention This call doesn't actually change window size, it just
		///   resizes back-buffer. Windowing code has to change window size.
		/// </summary>
		///
		/// <param name="_width">Back-buffer width.</param>
		/// <param name="_height">Back-buffer height.</param>
		/// <param name="_flags">See: `BGFX_RESET_*` for more info.   - `BGFX_RESET_NONE` - No reset flags.   - `BGFX_RESET_FULLSCREEN` - Not supported yet.   - `BGFX_RESET_MSAA_X[2/4/8/16]` - Enable 2, 4, 8 or 16 x MSAA.   - `BGFX_RESET_VSYNC` - Enable V-Sync.   - `BGFX_RESET_MAXANISOTROPY` - Turn on/off max anisotropy.   - `BGFX_RESET_CAPTURE` - Begin screen capture.   - `BGFX_RESET_FLUSH_AFTER_RENDER` - Flush rendering after submitting to GPU.   - `BGFX_RESET_FLIP_AFTER_RENDER` - This flag  specifies where flip     occurs. Default behaviour is that flip occurs before rendering new     frame. This flag only has effect when `BGFX_CONFIG_MULTITHREADED=0`.   - `BGFX_RESET_SRGB_BACKBUFFER` - Enable sRGB backbuffer.</param>
		/// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_reset", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void reset(uint _width, uint _height, uint _flags, TextureFormat _format);

		/// <summary>
		/// Advance to next frame. When using multithreaded renderer, this call
		/// just swaps internal buffers, kicks render thread, and returns. In
		/// singlethreaded renderer this call does frame rendering.
		/// </summary>
		///
		/// <param name="_capture">Capture frame with graphics debugger.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_frame", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint frame(bool _capture);

		/// <summary>
		/// Returns current renderer backend API type.
		/// @remarks
		///   Library must be initialized.
		/// </summary>
		///
		[DllImport(DllName, EntryPoint = "bgfx_get_renderer_type", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe RendererType get_renderer_type();

		/// <summary>
		/// Returns renderer capabilities.
		/// @remarks
		///   Library must be initialized.
		/// </summary>
		///
		[DllImport(DllName, EntryPoint = "bgfx_get_caps", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe Caps* get_caps();

		/// <summary>
		/// Returns performance counters.
		/// @attention Pointer returned is valid until `bgfx::frame` is called.
		/// </summary>
		///
		[DllImport(DllName, EntryPoint = "bgfx_get_stats", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe Stats* get_stats();

		/// <summary>
		/// Allocate buffer to pass to bgfx calls. Data will be freed inside bgfx.
		/// </summary>
		///
		/// <param name="_size">Size to allocate.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_alloc", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe Memory* alloc(uint _size);

		/// <summary>
		/// Allocate buffer and copy data into it. Data will be freed inside bgfx.
		/// </summary>
		///
		/// <param name="_data">Pointer to data to be copied.</param>
		/// <param name="_size">Size of data to be copied.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_copy", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe Memory* copy(void* _data, uint _size);

		/// <summary>
		/// Make reference to data to pass to bgfx. Unlike `bgfx::alloc`, this call
		/// doesn't allocate memory for data. It just copies the _data pointer. You
		/// can pass `ReleaseFn` function pointer to release this memory after it's
		/// consumed, otherwise you must make sure _data is available for at least 2
		/// `bgfx::frame` calls. `ReleaseFn` function must be able to be called
		/// from any thread.
		/// @attention Data passed must be available for at least 2 `bgfx::frame` calls.
		/// </summary>
		///
		/// <param name="_data">Pointer to data.</param>
		/// <param name="_size">Size of data.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_make_ref", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe Memory* make_ref(void* _data, uint _size);

		/// <summary>
		/// Make reference to data to pass to bgfx. Unlike `bgfx::alloc`, this call
		/// doesn't allocate memory for data. It just copies the _data pointer. You
		/// can pass `ReleaseFn` function pointer to release this memory after it's
		/// consumed, otherwise you must make sure _data is available for at least 2
		/// `bgfx::frame` calls. `ReleaseFn` function must be able to be called
		/// from any thread.
		/// @attention Data passed must be available for at least 2 `bgfx::frame` calls.
		/// </summary>
		///
		/// <param name="_data">Pointer to data.</param>
		/// <param name="_size">Size of data.</param>
		/// <param name="_releaseFn">Callback function to release memory after use.</param>
		/// <param name="_userData">User data to be passed to callback function.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_make_ref_release", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe Memory* make_ref_release(void* _data, uint _size, IntPtr _releaseFn, void* _userData);

		/// <summary>
		/// Set debug flags.
		/// </summary>
		///
		/// <param name="_debug">Available flags:   - `BGFX_DEBUG_IFH` - Infinitely fast hardware. When this flag is set     all rendering calls will be skipped. This is useful when profiling     to quickly assess potential bottlenecks between CPU and GPU.   - `BGFX_DEBUG_PROFILER` - Enable profiler.   - `BGFX_DEBUG_STATS` - Display internal statistics.   - `BGFX_DEBUG_TEXT` - Display debug text.   - `BGFX_DEBUG_WIREFRAME` - Wireframe rendering. All rendering     primitives will be rendered as lines.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_debug", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_debug(uint _debug);

		/// <summary>
		/// Clear internal debug text buffer.
		/// </summary>
		///
		/// <param name="_attr">Background color.</param>
		/// <param name="_small">Default 8x16 or 8x8 font.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_dbg_text_clear", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void dbg_text_clear(byte _attr, bool _small);

		/// <summary>
		/// Print formatted data to internal debug text character-buffer (VGA-compatible text mode).
		/// </summary>
		///
		/// <param name="_x">Position x from the left corner of the window.</param>
		/// <param name="_y">Position y from the top corner of the window.</param>
		/// <param name="_attr">Color palette. Where top 4-bits represent index of background, and bottom 4-bits represent foreground color from standard VGA text palette (ANSI escape codes).</param>
		/// <param name="_format">`printf` style format.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_dbg_text_printf", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void dbg_text_printf(ushort _x, ushort _y, byte _attr, [MarshalAs(UnmanagedType.LPStr)] string _format, [MarshalAs(UnmanagedType.LPStr)] string args);

		/// <summary>
		/// Print formatted data from variable argument list to internal debug text character-buffer (VGA-compatible text mode).
		/// </summary>
		///
		/// <param name="_x">Position x from the left corner of the window.</param>
		/// <param name="_y">Position y from the top corner of the window.</param>
		/// <param name="_attr">Color palette. Where top 4-bits represent index of background, and bottom 4-bits represent foreground color from standard VGA text palette (ANSI escape codes).</param>
		/// <param name="_format">`printf` style format.</param>
		/// <param name="_argList">Variable arguments list for format string.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_dbg_text_vprintf", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void dbg_text_vprintf(ushort _x, ushort _y, byte _attr, [MarshalAs(UnmanagedType.LPStr)] string _format, IntPtr _argList);

		/// <summary>
		/// Draw image into internal debug text buffer.
		/// </summary>
		///
		/// <param name="_x">Position x from the left corner of the window.</param>
		/// <param name="_y">Position y from the top corner of the window.</param>
		/// <param name="_width">Image width.</param>
		/// <param name="_height">Image height.</param>
		/// <param name="_data">Raw image data (character/attribute raw encoding).</param>
		/// <param name="_pitch">Image pitch in bytes.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_dbg_text_image", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void dbg_text_image(ushort _x, ushort _y, ushort _width, ushort _height, void* _data, ushort _pitch);

		/// <summary>
		/// Create static index buffer.
		/// </summary>
		///
		/// <param name="_mem">Index buffer data.</param>
		/// <param name="_flags">Buffer creation flags.   - `BGFX_BUFFER_NONE` - No flags.   - `BGFX_BUFFER_COMPUTE_READ` - Buffer will be read from by compute shader.   - `BGFX_BUFFER_COMPUTE_WRITE` - Buffer will be written into by compute shader. When buffer       is created with `BGFX_BUFFER_COMPUTE_WRITE` flag it cannot be updated from CPU.   - `BGFX_BUFFER_COMPUTE_READ_WRITE` - Buffer will be used for read/write by compute shader.   - `BGFX_BUFFER_ALLOW_RESIZE` - Buffer will resize on buffer update if a different amount of       data is passed. If this flag is not specified, and more data is passed on update, the buffer       will be trimmed to fit the existing buffer size. This flag has effect only on dynamic       buffers.   - `BGFX_BUFFER_INDEX32` - Buffer is using 32-bit indices. This flag has effect only on       index buffers.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe IndexBufferHandle create_index_buffer(Memory* _mem, ushort _flags);

		/// <summary>
		/// Set static index buffer debug name.
		/// </summary>
		///
		/// <param name="_handle">Static index buffer handle.</param>
		/// <param name="_name">Static index buffer name.</param>
		/// <param name="_len">Static index buffer name length (if length is INT32_MAX, it's expected that _name is zero terminated string.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_index_buffer_name", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_index_buffer_name(IndexBufferHandle _handle, [MarshalAs(UnmanagedType.LPStr)] string _name, int _len);

		/// <summary>
		/// Destroy static index buffer.
		/// </summary>
		///
		/// <param name="_handle">Static index buffer handle.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_destroy_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void destroy_index_buffer(IndexBufferHandle _handle);

		/// <summary>
		/// Create vertex layout.
		/// </summary>
		///
		/// <param name="_layout">Vertex layout.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_vertex_layout", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe VertexLayoutHandle create_vertex_layout(VertexLayoutData* _layout);

		/// <summary>
		/// Destroy vertex layout.
		/// </summary>
		///
		/// <param name="_layoutHandle">Vertex layout handle.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_destroy_vertex_layout", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void destroy_vertex_layout(VertexLayoutHandle _layoutHandle);

		/// <summary>
		/// Create static vertex buffer.
		/// </summary>
		///
		/// <param name="_mem">Vertex buffer data.</param>
		/// <param name="_layout">Vertex layout.</param>
		/// <param name="_flags">Buffer creation flags.  - `BGFX_BUFFER_NONE` - No flags.  - `BGFX_BUFFER_COMPUTE_READ` - Buffer will be read from by compute shader.  - `BGFX_BUFFER_COMPUTE_WRITE` - Buffer will be written into by compute shader. When buffer      is created with `BGFX_BUFFER_COMPUTE_WRITE` flag it cannot be updated from CPU.  - `BGFX_BUFFER_COMPUTE_READ_WRITE` - Buffer will be used for read/write by compute shader.  - `BGFX_BUFFER_ALLOW_RESIZE` - Buffer will resize on buffer update if a different amount of      data is passed. If this flag is not specified, and more data is passed on update, the buffer      will be trimmed to fit the existing buffer size. This flag has effect only on dynamic buffers.  - `BGFX_BUFFER_INDEX32` - Buffer is using 32-bit indices. This flag has effect only on index buffers.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe VertexBufferHandle create_vertex_buffer(Memory* _mem, VertexLayoutData* _layout, ushort _flags);

		/// <summary>
		/// Set static vertex buffer debug name.
		/// </summary>
		///
		/// <param name="_handle">Static vertex buffer handle.</param>
		/// <param name="_name">Static vertex buffer name.</param>
		/// <param name="_len">Static vertex buffer name length (if length is INT32_MAX, it's expected that _name is zero terminated string.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_vertex_buffer_name", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_vertex_buffer_name(VertexBufferHandle _handle, [MarshalAs(UnmanagedType.LPStr)] string _name, int _len);

		/// <summary>
		/// Destroy static vertex buffer.
		/// </summary>
		///
		/// <param name="_handle">Static vertex buffer handle.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_destroy_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void destroy_vertex_buffer(VertexBufferHandle _handle);

		/// <summary>
		/// Create empty dynamic index buffer.
		/// </summary>
		///
		/// <param name="_num">Number of indices.</param>
		/// <param name="_flags">Buffer creation flags.   - `BGFX_BUFFER_NONE` - No flags.   - `BGFX_BUFFER_COMPUTE_READ` - Buffer will be read from by compute shader.   - `BGFX_BUFFER_COMPUTE_WRITE` - Buffer will be written into by compute shader. When buffer       is created with `BGFX_BUFFER_COMPUTE_WRITE` flag it cannot be updated from CPU.   - `BGFX_BUFFER_COMPUTE_READ_WRITE` - Buffer will be used for read/write by compute shader.   - `BGFX_BUFFER_ALLOW_RESIZE` - Buffer will resize on buffer update if a different amount of       data is passed. If this flag is not specified, and more data is passed on update, the buffer       will be trimmed to fit the existing buffer size. This flag has effect only on dynamic       buffers.   - `BGFX_BUFFER_INDEX32` - Buffer is using 32-bit indices. This flag has effect only on       index buffers.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_dynamic_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe DynamicIndexBufferHandle create_dynamic_index_buffer(uint _num, ushort _flags);

		/// <summary>
		/// Create dynamic index buffer and initialized it.
		/// </summary>
		///
		/// <param name="_mem">Index buffer data.</param>
		/// <param name="_flags">Buffer creation flags.   - `BGFX_BUFFER_NONE` - No flags.   - `BGFX_BUFFER_COMPUTE_READ` - Buffer will be read from by compute shader.   - `BGFX_BUFFER_COMPUTE_WRITE` - Buffer will be written into by compute shader. When buffer       is created with `BGFX_BUFFER_COMPUTE_WRITE` flag it cannot be updated from CPU.   - `BGFX_BUFFER_COMPUTE_READ_WRITE` - Buffer will be used for read/write by compute shader.   - `BGFX_BUFFER_ALLOW_RESIZE` - Buffer will resize on buffer update if a different amount of       data is passed. If this flag is not specified, and more data is passed on update, the buffer       will be trimmed to fit the existing buffer size. This flag has effect only on dynamic       buffers.   - `BGFX_BUFFER_INDEX32` - Buffer is using 32-bit indices. This flag has effect only on       index buffers.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_dynamic_index_buffer_mem", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe DynamicIndexBufferHandle create_dynamic_index_buffer_mem(Memory* _mem, ushort _flags);

		/// <summary>
		/// Update dynamic index buffer.
		/// </summary>
		///
		/// <param name="_handle">Dynamic index buffer handle.</param>
		/// <param name="_startIndex">Start index.</param>
		/// <param name="_mem">Index buffer data.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_update_dynamic_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void update_dynamic_index_buffer(DynamicIndexBufferHandle _handle, uint _startIndex, Memory* _mem);

		/// <summary>
		/// Destroy dynamic index buffer.
		/// </summary>
		///
		/// <param name="_handle">Dynamic index buffer handle.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_destroy_dynamic_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void destroy_dynamic_index_buffer(DynamicIndexBufferHandle _handle);

		/// <summary>
		/// Create empty dynamic vertex buffer.
		/// </summary>
		///
		/// <param name="_num">Number of vertices.</param>
		/// <param name="_layout">Vertex layout.</param>
		/// <param name="_flags">Buffer creation flags.   - `BGFX_BUFFER_NONE` - No flags.   - `BGFX_BUFFER_COMPUTE_READ` - Buffer will be read from by compute shader.   - `BGFX_BUFFER_COMPUTE_WRITE` - Buffer will be written into by compute shader. When buffer       is created with `BGFX_BUFFER_COMPUTE_WRITE` flag it cannot be updated from CPU.   - `BGFX_BUFFER_COMPUTE_READ_WRITE` - Buffer will be used for read/write by compute shader.   - `BGFX_BUFFER_ALLOW_RESIZE` - Buffer will resize on buffer update if a different amount of       data is passed. If this flag is not specified, and more data is passed on update, the buffer       will be trimmed to fit the existing buffer size. This flag has effect only on dynamic       buffers.   - `BGFX_BUFFER_INDEX32` - Buffer is using 32-bit indices. This flag has effect only on       index buffers.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_dynamic_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe DynamicVertexBufferHandle create_dynamic_vertex_buffer(uint _num, VertexLayoutData* _layout, ushort _flags);

		/// <summary>
		/// Create dynamic vertex buffer and initialize it.
		/// </summary>
		///
		/// <param name="_mem">Vertex buffer data.</param>
		/// <param name="_layout">Vertex layout.</param>
		/// <param name="_flags">Buffer creation flags.   - `BGFX_BUFFER_NONE` - No flags.   - `BGFX_BUFFER_COMPUTE_READ` - Buffer will be read from by compute shader.   - `BGFX_BUFFER_COMPUTE_WRITE` - Buffer will be written into by compute shader. When buffer       is created with `BGFX_BUFFER_COMPUTE_WRITE` flag it cannot be updated from CPU.   - `BGFX_BUFFER_COMPUTE_READ_WRITE` - Buffer will be used for read/write by compute shader.   - `BGFX_BUFFER_ALLOW_RESIZE` - Buffer will resize on buffer update if a different amount of       data is passed. If this flag is not specified, and more data is passed on update, the buffer       will be trimmed to fit the existing buffer size. This flag has effect only on dynamic       buffers.   - `BGFX_BUFFER_INDEX32` - Buffer is using 32-bit indices. This flag has effect only on       index buffers.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_dynamic_vertex_buffer_mem", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe DynamicVertexBufferHandle create_dynamic_vertex_buffer_mem(Memory* _mem, VertexLayoutData* _layout, ushort _flags);

		/// <summary>
		/// Update dynamic vertex buffer.
		/// </summary>
		///
		/// <param name="_handle">Dynamic vertex buffer handle.</param>
		/// <param name="_startVertex">Start vertex.</param>
		/// <param name="_mem">Vertex buffer data.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_update_dynamic_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void update_dynamic_vertex_buffer(DynamicVertexBufferHandle _handle, uint _startVertex, Memory* _mem);

		/// <summary>
		/// Destroy dynamic vertex buffer.
		/// </summary>
		///
		/// <param name="_handle">Dynamic vertex buffer handle.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_destroy_dynamic_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void destroy_dynamic_vertex_buffer(DynamicVertexBufferHandle _handle);

		/// <summary>
		/// Returns number of requested or maximum available indices.
		/// </summary>
		///
		/// <param name="_num">Number of required indices.</param>
		/// <param name="_index32">Set to `true` if input indices will be 32-bit.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_get_avail_transient_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint get_avail_transient_index_buffer(uint _num, bool _index32);

		/// <summary>
		/// Returns number of requested or maximum available vertices.
		/// </summary>
		///
		/// <param name="_num">Number of required vertices.</param>
		/// <param name="_layout">Vertex layout.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_get_avail_transient_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint get_avail_transient_vertex_buffer(uint _num, VertexLayoutData* _layout);

		/// <summary>
		/// Returns number of requested or maximum available instance buffer slots.
		/// </summary>
		///
		/// <param name="_num">Number of required instances.</param>
		/// <param name="_stride">Stride per instance.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_get_avail_instance_data_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint get_avail_instance_data_buffer(uint _num, ushort _stride);

		/// <summary>
		/// Allocate transient index buffer.
		/// </summary>
		///
		/// <param name="_tib">TransientIndexBuffer structure is filled and is valid for the duration of frame, and it can be reused for multiple draw calls.</param>
		/// <param name="_num">Number of indices to allocate.</param>
		/// <param name="_index32">Set to `true` if input indices will be 32-bit.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_alloc_transient_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void alloc_transient_index_buffer(TransientIndexBuffer* _tib, uint _num, bool _index32);

		/// <summary>
		/// Allocate transient vertex buffer.
		/// </summary>
		///
		/// <param name="_tvb">TransientVertexBuffer structure is filled and is valid for the duration of frame, and it can be reused for multiple draw calls.</param>
		/// <param name="_num">Number of vertices to allocate.</param>
		/// <param name="_layout">Vertex layout.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_alloc_transient_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void alloc_transient_vertex_buffer(TransientVertexBuffer* _tvb, uint _num, VertexLayoutData* _layout);

		/// <summary>
		/// Check for required space and allocate transient vertex and index
		/// buffers. If both space requirements are satisfied function returns
		/// true.
		/// </summary>
		///
		/// <param name="_tvb">TransientVertexBuffer structure is filled and is valid for the duration of frame, and it can be reused for multiple draw calls.</param>
		/// <param name="_layout">Vertex layout.</param>
		/// <param name="_numVertices">Number of vertices to allocate.</param>
		/// <param name="_tib">TransientIndexBuffer structure is filled and is valid for the duration of frame, and it can be reused for multiple draw calls.</param>
		/// <param name="_numIndices">Number of indices to allocate.</param>
		/// <param name="_index32">Set to `true` if input indices will be 32-bit.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_alloc_transient_buffers", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern unsafe bool alloc_transient_buffers(TransientVertexBuffer* _tvb, VertexLayoutData* _layout, uint _numVertices, TransientIndexBuffer* _tib, uint _numIndices, bool _index32);

		/// <summary>
		/// Allocate instance data buffer.
		/// </summary>
		///
		/// <param name="_idb">InstanceDataBuffer structure is filled and is valid for duration of frame, and it can be reused for multiple draw calls.</param>
		/// <param name="_num">Number of instances.</param>
		/// <param name="_stride">Instance stride. Must be multiple of 16.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_alloc_instance_data_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void alloc_instance_data_buffer(InstanceDataBuffer* _idb, uint _num, ushort _stride);

		/// <summary>
		/// Create draw indirect buffer.
		/// </summary>
		///
		/// <param name="_num">Number of indirect calls.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_indirect_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe IndirectBufferHandle create_indirect_buffer(uint _num);

		/// <summary>
		/// Destroy draw indirect buffer.
		/// </summary>
		///
		/// <param name="_handle">Indirect buffer handle.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_destroy_indirect_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void destroy_indirect_buffer(IndirectBufferHandle _handle);

		/// <summary>
		/// Create shader from memory buffer.
		/// </summary>
		///
		/// <param name="_mem">Shader binary.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_shader", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe ShaderHandle create_shader(Memory* _mem);

		/// <summary>
		/// Returns the number of uniforms and uniform handles used inside a shader.
		/// @remarks
		///   Only non-predefined uniforms are returned.
		/// </summary>
		///
		/// <param name="_handle">Shader handle.</param>
		/// <param name="_uniforms">UniformHandle array where data will be stored.</param>
		/// <param name="_max">Maximum capacity of array.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_get_shader_uniforms", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe ushort get_shader_uniforms(ShaderHandle _handle, UniformHandle* _uniforms, ushort _max);

		/// <summary>
		/// Set shader debug name.
		/// </summary>
		///
		/// <param name="_handle">Shader handle.</param>
		/// <param name="_name">Shader name.</param>
		/// <param name="_len">Shader name length (if length is INT32_MAX, it's expected that _name is zero terminated string).</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_shader_name", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_shader_name(ShaderHandle _handle, [MarshalAs(UnmanagedType.LPStr)] string _name, int _len);

		/// <summary>
		/// Destroy shader.
		/// @remark Once a shader program is created with _handle,
		///   it is safe to destroy that shader.
		/// </summary>
		///
		/// <param name="_handle">Shader handle.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_destroy_shader", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void destroy_shader(ShaderHandle _handle);

		/// <summary>
		/// Create program with vertex and fragment shaders.
		/// </summary>
		///
		/// <param name="_vsh">Vertex shader.</param>
		/// <param name="_fsh">Fragment shader.</param>
		/// <param name="_destroyShaders">If true, shaders will be destroyed when program is destroyed.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_program", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe ProgramHandle create_program(ShaderHandle _vsh, ShaderHandle _fsh, bool _destroyShaders);

		/// <summary>
		/// Create program with compute shader.
		/// </summary>
		///
		/// <param name="_csh">Compute shader.</param>
		/// <param name="_destroyShaders">If true, shaders will be destroyed when program is destroyed.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_compute_program", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe ProgramHandle create_compute_program(ShaderHandle _csh, bool _destroyShaders);

		/// <summary>
		/// Destroy program.
		/// </summary>
		///
		/// <param name="_handle">Program handle.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_destroy_program", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void destroy_program(ProgramHandle _handle);

		/// <summary>
		/// Validate texture parameters.
		/// </summary>
		///
		/// <param name="_depth">Depth dimension of volume texture.</param>
		/// <param name="_cubeMap">Indicates that texture contains cubemap.</param>
		/// <param name="_numLayers">Number of layers in texture array.</param>
		/// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
		/// <param name="_flags">Texture flags. See `BGFX_TEXTURE_*`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_is_texture_valid", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern unsafe bool is_texture_valid(ushort _depth, bool _cubeMap, ushort _numLayers, TextureFormat _format, ulong _flags);

		/// <summary>
		/// Validate frame buffer parameters.
		/// </summary>
		///
		/// <param name="_num">Number of attachments.</param>
		/// <param name="_attachment">Attachment texture info. See: `bgfx::Attachment`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_is_frame_buffer_valid", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern unsafe bool is_frame_buffer_valid(byte _num, Attachment* _attachment);

		/// <summary>
		/// Calculate amount of memory required for texture.
		/// </summary>
		///
		/// <param name="_info">Resulting texture info structure. See: `TextureInfo`.</param>
		/// <param name="_width">Width.</param>
		/// <param name="_height">Height.</param>
		/// <param name="_depth">Depth dimension of volume texture.</param>
		/// <param name="_cubeMap">Indicates that texture contains cubemap.</param>
		/// <param name="_hasMips">Indicates that texture contains full mip-map chain.</param>
		/// <param name="_numLayers">Number of layers in texture array.</param>
		/// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_calc_texture_size", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void calc_texture_size(TextureInfo* _info, ushort _width, ushort _height, ushort _depth, bool _cubeMap, bool _hasMips, ushort _numLayers, TextureFormat _format);

		/// <summary>
		/// Create texture from memory buffer.
		/// </summary>
		///
		/// <param name="_mem">DDS, KTX or PVR texture binary data.</param>
		/// <param name="_flags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
		/// <param name="_skip">Skip top level mips when parsing texture.</param>
		/// <param name="_info">When non-`NULL` is specified it returns parsed texture information.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_texture", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe TextureHandle create_texture(Memory* _mem, ulong _flags, byte _skip, TextureInfo* _info);

		/// <summary>
		/// Create 2D texture.
		/// </summary>
		///
		/// <param name="_width">Width.</param>
		/// <param name="_height">Height.</param>
		/// <param name="_hasMips">Indicates that texture contains full mip-map chain.</param>
		/// <param name="_numLayers">Number of layers in texture array. Must be 1 if caps `BGFX_CAPS_TEXTURE_2D_ARRAY` flag is not set.</param>
		/// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
		/// <param name="_flags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
		/// <param name="_mem">Texture data. If `_mem` is non-NULL, created texture will be immutable. If `_mem` is NULL content of the texture is uninitialized. When `_numLayers` is more than 1, expected memory layout is texture and all mips together for each array element.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_texture_2d", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe TextureHandle create_texture_2d(ushort _width, ushort _height, bool _hasMips, ushort _numLayers, TextureFormat _format, ulong _flags, Memory* _mem);

		/// <summary>
		/// Create texture with size based on backbuffer ratio. Texture will maintain ratio
		/// if back buffer resolution changes.
		/// </summary>
		///
		/// <param name="_ratio">Texture size in respect to back-buffer size. See: `BackbufferRatio::Enum`.</param>
		/// <param name="_hasMips">Indicates that texture contains full mip-map chain.</param>
		/// <param name="_numLayers">Number of layers in texture array. Must be 1 if caps `BGFX_CAPS_TEXTURE_2D_ARRAY` flag is not set.</param>
		/// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
		/// <param name="_flags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_texture_2d_scaled", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe TextureHandle create_texture_2d_scaled(BackbufferRatio _ratio, bool _hasMips, ushort _numLayers, TextureFormat _format, ulong _flags);

		/// <summary>
		/// Create 3D texture.
		/// </summary>
		///
		/// <param name="_width">Width.</param>
		/// <param name="_height">Height.</param>
		/// <param name="_depth">Depth.</param>
		/// <param name="_hasMips">Indicates that texture contains full mip-map chain.</param>
		/// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
		/// <param name="_flags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
		/// <param name="_mem">Texture data. If `_mem` is non-NULL, created texture will be immutable. If `_mem` is NULL content of the texture is uninitialized. When `_numLayers` is more than 1, expected memory layout is texture and all mips together for each array element.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_texture_3d", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe TextureHandle create_texture_3d(ushort _width, ushort _height, ushort _depth, bool _hasMips, TextureFormat _format, ulong _flags, Memory* _mem);

		/// <summary>
		/// Create Cube texture.
		/// </summary>
		///
		/// <param name="_size">Cube side size.</param>
		/// <param name="_hasMips">Indicates that texture contains full mip-map chain.</param>
		/// <param name="_numLayers">Number of layers in texture array. Must be 1 if caps `BGFX_CAPS_TEXTURE_2D_ARRAY` flag is not set.</param>
		/// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
		/// <param name="_flags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
		/// <param name="_mem">Texture data. If `_mem` is non-NULL, created texture will be immutable. If `_mem` is NULL content of the texture is uninitialized. When `_numLayers` is more than 1, expected memory layout is texture and all mips together for each array element.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_texture_cube", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe TextureHandle create_texture_cube(ushort _size, bool _hasMips, ushort _numLayers, TextureFormat _format, ulong _flags, Memory* _mem);

		/// <summary>
		/// Update 2D texture.
		/// @attention It's valid to update only mutable texture. See `bgfx::createTexture2D` for more info.
		/// </summary>
		///
		/// <param name="_handle">Texture handle.</param>
		/// <param name="_layer">Layer in texture array.</param>
		/// <param name="_mip">Mip level.</param>
		/// <param name="_x">X offset in texture.</param>
		/// <param name="_y">Y offset in texture.</param>
		/// <param name="_width">Width of texture block.</param>
		/// <param name="_height">Height of texture block.</param>
		/// <param name="_mem">Texture update data.</param>
		/// <param name="_pitch">Pitch of input image (bytes). When _pitch is set to UINT16_MAX, it will be calculated internally based on _width.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_update_texture_2d", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void update_texture_2d(TextureHandle _handle, ushort _layer, byte _mip, ushort _x, ushort _y, ushort _width, ushort _height, Memory* _mem, ushort _pitch);

		/// <summary>
		/// Update 3D texture.
		/// @attention It's valid to update only mutable texture. See `bgfx::createTexture3D` for more info.
		/// </summary>
		///
		/// <param name="_handle">Texture handle.</param>
		/// <param name="_mip">Mip level.</param>
		/// <param name="_x">X offset in texture.</param>
		/// <param name="_y">Y offset in texture.</param>
		/// <param name="_z">Z offset in texture.</param>
		/// <param name="_width">Width of texture block.</param>
		/// <param name="_height">Height of texture block.</param>
		/// <param name="_depth">Depth of texture block.</param>
		/// <param name="_mem">Texture update data.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_update_texture_3d", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void update_texture_3d(TextureHandle _handle, byte _mip, ushort _x, ushort _y, ushort _z, ushort _width, ushort _height, ushort _depth, Memory* _mem);

		/// <summary>
		/// Update Cube texture.
		/// @attention It's valid to update only mutable texture. See `bgfx::createTextureCube` for more info.
		/// </summary>
		///
		/// <param name="_handle">Texture handle.</param>
		/// <param name="_layer">Layer in texture array.</param>
		/// <param name="_side">Cubemap side `BGFX_CUBE_MAP_<POSITIVE or NEGATIVE>_<X, Y or Z>`,   where 0 is +X, 1 is -X, 2 is +Y, 3 is -Y, 4 is +Z, and 5 is -Z.                  +----------+                  |-z       2|                  | ^  +y    |                  | |        |    Unfolded cube:                  | +---->+x |       +----------+----------+----------+----------+       |+y       1|+y       4|+y       0|+y       5|       | ^  -x    | ^  +z    | ^  +x    | ^  -z    |       | |        | |        | |        | |        |       | +---->+z | +---->+x | +---->-z | +---->-x |       +----------+----------+----------+----------+                  |+z       3|                  | ^  -y    |                  | |        |                  | +---->+x |                  +----------+</param>
		/// <param name="_mip">Mip level.</param>
		/// <param name="_x">X offset in texture.</param>
		/// <param name="_y">Y offset in texture.</param>
		/// <param name="_width">Width of texture block.</param>
		/// <param name="_height">Height of texture block.</param>
		/// <param name="_mem">Texture update data.</param>
		/// <param name="_pitch">Pitch of input image (bytes). When _pitch is set to UINT16_MAX, it will be calculated internally based on _width.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_update_texture_cube", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void update_texture_cube(TextureHandle _handle, ushort _layer, byte _side, byte _mip, ushort _x, ushort _y, ushort _width, ushort _height, Memory* _mem, ushort _pitch);

		/// <summary>
		/// Read back texture content.
		/// @attention Texture must be created with `BGFX_TEXTURE_READ_BACK` flag.
		/// @attention Availability depends on: `BGFX_CAPS_TEXTURE_READ_BACK`.
		/// </summary>
		///
		/// <param name="_handle">Texture handle.</param>
		/// <param name="_data">Destination buffer.</param>
		/// <param name="_mip">Mip level.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_read_texture", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint read_texture(TextureHandle _handle, void* _data, byte _mip);

		/// <summary>
		/// Set texture debug name.
		/// </summary>
		///
		/// <param name="_handle">Texture handle.</param>
		/// <param name="_name">Texture name.</param>
		/// <param name="_len">Texture name length (if length is INT32_MAX, it's expected that _name is zero terminated string.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_texture_name", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_texture_name(TextureHandle _handle, [MarshalAs(UnmanagedType.LPStr)] string _name, int _len);

		/// <summary>
		/// Returns texture direct access pointer.
		/// @attention Availability depends on: `BGFX_CAPS_TEXTURE_DIRECT_ACCESS`. This feature
		///   is available on GPUs that have unified memory architecture (UMA) support.
		/// </summary>
		///
		/// <param name="_handle">Texture handle.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_get_direct_access_ptr", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void* get_direct_access_ptr(TextureHandle _handle);

		/// <summary>
		/// Destroy texture.
		/// </summary>
		///
		/// <param name="_handle">Texture handle.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_destroy_texture", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void destroy_texture(TextureHandle _handle);

		/// <summary>
		/// Create frame buffer (simple).
		/// </summary>
		///
		/// <param name="_width">Texture width.</param>
		/// <param name="_height">Texture height.</param>
		/// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
		/// <param name="_textureFlags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_frame_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe FrameBufferHandle create_frame_buffer(ushort _width, ushort _height, TextureFormat _format, ulong _textureFlags);

		/// <summary>
		/// Create frame buffer with size based on backbuffer ratio. Frame buffer will maintain ratio
		/// if back buffer resolution changes.
		/// </summary>
		///
		/// <param name="_ratio">Frame buffer size in respect to back-buffer size. See: `BackbufferRatio::Enum`.</param>
		/// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
		/// <param name="_textureFlags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_frame_buffer_scaled", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe FrameBufferHandle create_frame_buffer_scaled(BackbufferRatio _ratio, TextureFormat _format, ulong _textureFlags);

		/// <summary>
		/// Create MRT frame buffer from texture handles (simple).
		/// </summary>
		///
		/// <param name="_num">Number of texture handles.</param>
		/// <param name="_handles">Texture attachments.</param>
		/// <param name="_destroyTexture">If true, textures will be destroyed when frame buffer is destroyed.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_frame_buffer_from_handles", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe FrameBufferHandle create_frame_buffer_from_handles(byte _num, TextureHandle* _handles, bool _destroyTexture);

		/// <summary>
		/// Create MRT frame buffer from texture handles with specific layer and
		/// mip level.
		/// </summary>
		///
		/// <param name="_num">Number of attachments.</param>
		/// <param name="_attachment">Attachment texture info. See: `bgfx::Attachment`.</param>
		/// <param name="_destroyTexture">If true, textures will be destroyed when frame buffer is destroyed.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_frame_buffer_from_attachment", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe FrameBufferHandle create_frame_buffer_from_attachment(byte _num, Attachment* _attachment, bool _destroyTexture);

		/// <summary>
		/// Create frame buffer for multiple window rendering.
		/// @remarks
		///   Frame buffer cannot be used for sampling.
		/// @attention Availability depends on: `BGFX_CAPS_SWAP_CHAIN`.
		/// </summary>
		///
		/// <param name="_nwh">OS' target native window handle.</param>
		/// <param name="_width">Window back buffer width.</param>
		/// <param name="_height">Window back buffer height.</param>
		/// <param name="_format">Window back buffer color format.</param>
		/// <param name="_depthFormat">Window back buffer depth format.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_frame_buffer_from_nwh", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe FrameBufferHandle create_frame_buffer_from_nwh(void* _nwh, ushort _width, ushort _height, TextureFormat _format, TextureFormat _depthFormat);

		/// <summary>
		/// Set frame buffer debug name.
		/// </summary>
		///
		/// <param name="_handle">Frame buffer handle.</param>
		/// <param name="_name">Frame buffer name.</param>
		/// <param name="_len">Frame buffer name length (if length is INT32_MAX, it's expected that _name is zero terminated string.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_frame_buffer_name", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_frame_buffer_name(FrameBufferHandle _handle, [MarshalAs(UnmanagedType.LPStr)] string _name, int _len);

		/// <summary>
		/// Obtain texture handle of frame buffer attachment.
		/// </summary>
		///
		/// <param name="_handle">Frame buffer handle.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_get_texture", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe TextureHandle get_texture(FrameBufferHandle _handle, byte _attachment);

		/// <summary>
		/// Destroy frame buffer.
		/// </summary>
		///
		/// <param name="_handle">Frame buffer handle.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_destroy_frame_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void destroy_frame_buffer(FrameBufferHandle _handle);

		/// <summary>
		/// Create shader uniform parameter.
		/// @remarks
		///   1. Uniform names are unique. It's valid to call `bgfx::createUniform`
		///      multiple times with the same uniform name. The library will always
		///      return the same handle, but the handle reference count will be
		///      incremented. This means that the same number of `bgfx::destroyUniform`
		///      must be called to properly destroy the uniform.
		///   2. Predefined uniforms (declared in `bgfx_shader.sh`):
		///      - `u_viewRect vec4(x, y, width, height)` - view rectangle for current
		///        view, in pixels.
		///      - `u_viewTexel vec4(1.0/width, 1.0/height, undef, undef)` - inverse
		///        width and height
		///      - `u_view mat4` - view matrix
		///      - `u_invView mat4` - inverted view matrix
		///      - `u_proj mat4` - projection matrix
		///      - `u_invProj mat4` - inverted projection matrix
		///      - `u_viewProj mat4` - concatenated view projection matrix
		///      - `u_invViewProj mat4` - concatenated inverted view projection matrix
		///      - `u_model mat4[BGFX_CONFIG_MAX_BONES]` - array of model matrices.
		///      - `u_modelView mat4` - concatenated model view matrix, only first
		///        model matrix from array is used.
		///      - `u_modelViewProj mat4` - concatenated model view projection matrix.
		///      - `u_alphaRef float` - alpha reference value for alpha test.
		/// </summary>
		///
		/// <param name="_name">Uniform name in shader.</param>
		/// <param name="_type">Type of uniform (See: `bgfx::UniformType`).</param>
		/// <param name="_num">Number of elements in array.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_uniform", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe UniformHandle create_uniform([MarshalAs(UnmanagedType.LPStr)] string _name, UniformType _type, ushort _num);

		/// <summary>
		/// Retrieve uniform info.
		/// </summary>
		///
		/// <param name="_handle">Handle to uniform object.</param>
		/// <param name="_info">Uniform info.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_get_uniform_info", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void get_uniform_info(UniformHandle _handle, UniformInfo* _info);

		/// <summary>
		/// Destroy shader uniform parameter.
		/// </summary>
		///
		/// <param name="_handle">Handle to uniform object.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_destroy_uniform", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void destroy_uniform(UniformHandle _handle);

		/// <summary>
		/// Create occlusion query.
		/// </summary>
		///
		[DllImport(DllName, EntryPoint = "bgfx_create_occlusion_query", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe OcclusionQueryHandle create_occlusion_query();

		/// <summary>
		/// Retrieve occlusion query result from previous frame.
		/// </summary>
		///
		/// <param name="_handle">Handle to occlusion query object.</param>
		/// <param name="_result">Number of pixels that passed test. This argument can be `NULL` if result of occlusion query is not needed.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_get_result", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe OcclusionQueryResult get_result(OcclusionQueryHandle _handle, int* _result);

		/// <summary>
		/// Destroy occlusion query.
		/// </summary>
		///
		/// <param name="_handle">Handle to occlusion query object.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_destroy_occlusion_query", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void destroy_occlusion_query(OcclusionQueryHandle _handle);

		/// <summary>
		/// Set palette color value.
		/// </summary>
		///
		/// <param name="_index">Index into palette.</param>
		/// <param name="_rgba">RGBA floating point values.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_palette_color", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_palette_color(byte _index, float _rgba);

		/// <summary>
		/// Set palette color value.
		/// </summary>
		///
		/// <param name="_index">Index into palette.</param>
		/// <param name="_rgba">Packed 32-bit RGBA value.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_palette_color_rgba8", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_palette_color_rgba8(byte _index, uint _rgba);

		/// <summary>
		/// Set view name.
		/// @remarks
		///   This is debug only feature.
		///   In graphics debugger view name will appear as:
		///       "nnnc <view name>"
		///        ^  ^ ^
		///        |  +--- compute (C)
		///        +------ view id
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_name">View name.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_view_name", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_view_name(ushort _id, [MarshalAs(UnmanagedType.LPStr)] string _name);

		/// <summary>
		/// Set view rectangle. Draw primitive outside view will be clipped.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_x">Position x from the left corner of the window.</param>
		/// <param name="_y">Position y from the top corner of the window.</param>
		/// <param name="_width">Width of view port region.</param>
		/// <param name="_height">Height of view port region.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_view_rect", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_view_rect(ushort _id, ushort _x, ushort _y, ushort _width, ushort _height);

		/// <summary>
		/// Set view rectangle. Draw primitive outside view will be clipped.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_x">Position x from the left corner of the window.</param>
		/// <param name="_y">Position y from the top corner of the window.</param>
		/// <param name="_ratio">Width and height will be set in respect to back-buffer size. See: `BackbufferRatio::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_view_rect_ratio", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_view_rect_ratio(ushort _id, ushort _x, ushort _y, BackbufferRatio _ratio);

		/// <summary>
		/// Set view scissor. Draw primitive outside view will be clipped. When
		/// _x, _y, _width and _height are set to 0, scissor will be disabled.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_x">Position x from the left corner of the window.</param>
		/// <param name="_y">Position y from the top corner of the window.</param>
		/// <param name="_width">Width of view scissor region.</param>
		/// <param name="_height">Height of view scissor region.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_view_scissor", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_view_scissor(ushort _id, ushort _x, ushort _y, ushort _width, ushort _height);

		/// <summary>
		/// Set view clear flags.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_flags">Clear flags. Use `BGFX_CLEAR_NONE` to remove any clear operation. See: `BGFX_CLEAR_*`.</param>
		/// <param name="_rgba">Color clear value.</param>
		/// <param name="_depth">Depth clear value.</param>
		/// <param name="_stencil">Stencil clear value.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_view_clear", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_view_clear(ushort _id, ushort _flags, uint _rgba, float _depth, byte _stencil);

		/// <summary>
		/// Set view clear flags with different clear color for each
		/// frame buffer texture. Must use `bgfx::setPaletteColor` to setup clear color
		/// palette.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_flags">Clear flags. Use `BGFX_CLEAR_NONE` to remove any clear operation. See: `BGFX_CLEAR_*`.</param>
		/// <param name="_depth">Depth clear value.</param>
		/// <param name="_stencil">Stencil clear value.</param>
		/// <param name="_c0">Palette index for frame buffer attachment 0.</param>
		/// <param name="_c1">Palette index for frame buffer attachment 1.</param>
		/// <param name="_c2">Palette index for frame buffer attachment 2.</param>
		/// <param name="_c3">Palette index for frame buffer attachment 3.</param>
		/// <param name="_c4">Palette index for frame buffer attachment 4.</param>
		/// <param name="_c5">Palette index for frame buffer attachment 5.</param>
		/// <param name="_c6">Palette index for frame buffer attachment 6.</param>
		/// <param name="_c7">Palette index for frame buffer attachment 7.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_view_clear_mrt", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_view_clear_mrt(ushort _id, ushort _flags, float _depth, byte _stencil, byte _c0, byte _c1, byte _c2, byte _c3, byte _c4, byte _c5, byte _c6, byte _c7);

		/// <summary>
		/// Set view sorting mode.
		/// @remarks
		///   View mode must be set prior calling `bgfx::submit` for the view.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_mode">View sort mode. See `ViewMode::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_view_mode", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_view_mode(ushort _id, ViewMode _mode);

		/// <summary>
		/// Set view frame buffer.
		/// @remarks
		///   Not persistent after `bgfx::reset` call.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_handle">Frame buffer handle. Passing `BGFX_INVALID_HANDLE` as frame buffer handle will draw primitives from this view into default back buffer.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_view_frame_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_view_frame_buffer(ushort _id, FrameBufferHandle _handle);

		/// <summary>
		/// Set view view and projection matrices, all draw primitives in this
		/// view will use these matrices.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_view">View matrix.</param>
		/// <param name="_proj">Projection matrix.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_view_transform", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_view_transform(ushort _id, void* _view, void* _proj);

		/// <summary>
		/// Post submit view reordering.
		/// </summary>
		///
		/// <param name="_id">First view id.</param>
		/// <param name="_num">Number of views to remap.</param>
		/// <param name="_order">View remap id table. Passing `NULL` will reset view ids to default state.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_view_order", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_view_order(ushort _id, ushort _num, ushort* _order);

		/// <summary>
		/// Reset all view settings to default.
		/// </summary>
		///
		[DllImport(DllName, EntryPoint = "bgfx_reset_view", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void reset_view(ushort _id);

		/// <summary>
		/// Begin submitting draw calls from thread.
		/// </summary>
		///
		/// <param name="_forThread">Explicitly request an encoder for a worker thread.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_begin", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe Encoder* encoder_begin(bool _forThread);

		/// <summary>
		/// End submitting draw calls from thread.
		/// </summary>
		///
		/// <param name="_encoder">Encoder.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_end", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_end(Encoder* _encoder);

		/// <summary>
		/// Sets a debug marker. This allows you to group graphics calls together for easy browsing in
		/// graphics debugging tools.
		/// </summary>
		///
		/// <param name="_marker">Marker string.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_marker", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_marker(Encoder* _this, [MarshalAs(UnmanagedType.LPStr)] string _marker);

		/// <summary>
		/// Set render states for draw primitive.
		/// @remarks
		///   1. To setup more complex states use:
		///      `BGFX_STATE_ALPHA_REF(_ref)`,
		///      `BGFX_STATE_POINT_SIZE(_size)`,
		///      `BGFX_STATE_BLEND_FUNC(_src, _dst)`,
		///      `BGFX_STATE_BLEND_FUNC_SEPARATE(_srcRGB, _dstRGB, _srcA, _dstA)`,
		///      `BGFX_STATE_BLEND_EQUATION(_equation)`,
		///      `BGFX_STATE_BLEND_EQUATION_SEPARATE(_equationRGB, _equationA)`
		///   2. `BGFX_STATE_BLEND_EQUATION_ADD` is set when no other blend
		///      equation is specified.
		/// </summary>
		///
		/// <param name="_state">State flags. Default state for primitive type is   triangles. See: `BGFX_STATE_DEFAULT`.   - `BGFX_STATE_DEPTH_TEST_*` - Depth test function.   - `BGFX_STATE_BLEND_*` - See remark 1 about BGFX_STATE_BLEND_FUNC.   - `BGFX_STATE_BLEND_EQUATION_*` - See remark 2.   - `BGFX_STATE_CULL_*` - Backface culling mode.   - `BGFX_STATE_WRITE_*` - Enable R, G, B, A or Z write.   - `BGFX_STATE_MSAA` - Enable hardware multisample antialiasing.   - `BGFX_STATE_PT_[TRISTRIP/LINES/POINTS]` - Primitive type.</param>
		/// <param name="_rgba">Sets blend factor used by `BGFX_STATE_BLEND_FACTOR` and   `BGFX_STATE_BLEND_INV_FACTOR` blend modes.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_state", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_state(Encoder* _this, ulong _state, uint _rgba);

		/// <summary>
		/// Set condition for rendering.
		/// </summary>
		///
		/// <param name="_handle">Occlusion query handle.</param>
		/// <param name="_visible">Render if occlusion query is visible.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_condition", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_condition(Encoder* _this, OcclusionQueryHandle _handle, bool _visible);

		/// <summary>
		/// Set stencil test state.
		/// </summary>
		///
		/// <param name="_fstencil">Front stencil state.</param>
		/// <param name="_bstencil">Back stencil state. If back is set to `BGFX_STENCIL_NONE` _fstencil is applied to both front and back facing primitives.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_stencil", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_stencil(Encoder* _this, uint _fstencil, uint _bstencil);

		/// <summary>
		/// Set scissor for draw primitive.
		/// @remark
		///   To scissor for all primitives in view see `bgfx::setViewScissor`.
		/// </summary>
		///
		/// <param name="_x">Position x from the left corner of the window.</param>
		/// <param name="_y">Position y from the top corner of the window.</param>
		/// <param name="_width">Width of view scissor region.</param>
		/// <param name="_height">Height of view scissor region.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_scissor", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe ushort encoder_set_scissor(Encoder* _this, ushort _x, ushort _y, ushort _width, ushort _height);

		/// <summary>
		/// Set scissor from cache for draw primitive.
		/// @remark
		///   To scissor for all primitives in view see `bgfx::setViewScissor`.
		/// </summary>
		///
		/// <param name="_cache">Index in scissor cache.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_scissor_cached", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_scissor_cached(Encoder* _this, ushort _cache);

		/// <summary>
		/// Set model matrix for draw primitive. If it is not called,
		/// the model will be rendered with an identity model matrix.
		/// </summary>
		///
		/// <param name="_mtx">Pointer to first matrix in array.</param>
		/// <param name="_num">Number of matrices in array.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_transform", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint encoder_set_transform(Encoder* _this, void* _mtx, ushort _num);

		/// <summary>
		///  Set model matrix from matrix cache for draw primitive.
		/// </summary>
		///
		/// <param name="_cache">Index in matrix cache.</param>
		/// <param name="_num">Number of matrices from cache.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_transform_cached", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_transform_cached(Encoder* _this, uint _cache, ushort _num);

		/// <summary>
		/// Reserve matrices in internal matrix cache.
		/// @attention Pointer returned can be modifed until `bgfx::frame` is called.
		/// </summary>
		///
		/// <param name="_transform">Pointer to `Transform` structure.</param>
		/// <param name="_num">Number of matrices.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_alloc_transform", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint encoder_alloc_transform(Encoder* _this, Transform* _transform, ushort _num);

		/// <summary>
		/// Set shader uniform parameter for draw primitive.
		/// </summary>
		///
		/// <param name="_handle">Uniform.</param>
		/// <param name="_value">Pointer to uniform data.</param>
		/// <param name="_num">Number of elements. Passing `UINT16_MAX` will use the _num passed on uniform creation.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_uniform", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_uniform(Encoder* _this, UniformHandle _handle, void* _value, ushort _num);

		/// <summary>
		/// Set index buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_handle">Index buffer.</param>
		/// <param name="_firstIndex">First index to render.</param>
		/// <param name="_numIndices">Number of indices to render.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_index_buffer(Encoder* _this, IndexBufferHandle _handle, uint _firstIndex, uint _numIndices);

		/// <summary>
		/// Set index buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_handle">Dynamic index buffer.</param>
		/// <param name="_firstIndex">First index to render.</param>
		/// <param name="_numIndices">Number of indices to render.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_dynamic_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_dynamic_index_buffer(Encoder* _this, DynamicIndexBufferHandle _handle, uint _firstIndex, uint _numIndices);

		/// <summary>
		/// Set index buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_tib">Transient index buffer.</param>
		/// <param name="_firstIndex">First index to render.</param>
		/// <param name="_numIndices">Number of indices to render.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_transient_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_transient_index_buffer(Encoder* _this, TransientIndexBuffer* _tib, uint _firstIndex, uint _numIndices);

		/// <summary>
		/// Set vertex buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_stream">Vertex stream.</param>
		/// <param name="_handle">Vertex buffer.</param>
		/// <param name="_startVertex">First vertex to render.</param>
		/// <param name="_numVertices">Number of vertices to render.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_vertex_buffer(Encoder* _this, byte _stream, VertexBufferHandle _handle, uint _startVertex, uint _numVertices);

		/// <summary>
		/// Set vertex buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_stream">Vertex stream.</param>
		/// <param name="_handle">Vertex buffer.</param>
		/// <param name="_startVertex">First vertex to render.</param>
		/// <param name="_numVertices">Number of vertices to render.</param>
		/// <param name="_layoutHandle">Vertex layout for aliasing vertex buffer. If invalid handle is used, vertex layout used for creation of vertex buffer will be used.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_vertex_buffer_with_layout", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_vertex_buffer_with_layout(Encoder* _this, byte _stream, VertexBufferHandle _handle, uint _startVertex, uint _numVertices, VertexLayoutHandle _layoutHandle);

		/// <summary>
		/// Set vertex buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_stream">Vertex stream.</param>
		/// <param name="_handle">Dynamic vertex buffer.</param>
		/// <param name="_startVertex">First vertex to render.</param>
		/// <param name="_numVertices">Number of vertices to render.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_dynamic_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_dynamic_vertex_buffer(Encoder* _this, byte _stream, DynamicVertexBufferHandle _handle, uint _startVertex, uint _numVertices);

		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_dynamic_vertex_buffer_with_layout", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_dynamic_vertex_buffer_with_layout(Encoder* _this, byte _stream, DynamicVertexBufferHandle _handle, uint _startVertex, uint _numVertices, VertexLayoutHandle _layoutHandle);

		/// <summary>
		/// Set vertex buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_stream">Vertex stream.</param>
		/// <param name="_tvb">Transient vertex buffer.</param>
		/// <param name="_startVertex">First vertex to render.</param>
		/// <param name="_numVertices">Number of vertices to render.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_transient_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_transient_vertex_buffer(Encoder* _this, byte _stream, TransientVertexBuffer* _tvb, uint _startVertex, uint _numVertices);

		/// <summary>
		/// Set vertex buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_stream">Vertex stream.</param>
		/// <param name="_tvb">Transient vertex buffer.</param>
		/// <param name="_startVertex">First vertex to render.</param>
		/// <param name="_numVertices">Number of vertices to render.</param>
		/// <param name="_layoutHandle">Vertex layout for aliasing vertex buffer. If invalid handle is used, vertex layout used for creation of vertex buffer will be used.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_transient_vertex_buffer_with_layout", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_transient_vertex_buffer_with_layout(Encoder* _this, byte _stream, TransientVertexBuffer* _tvb, uint _startVertex, uint _numVertices, VertexLayoutHandle _layoutHandle);

		/// <summary>
		/// Set number of vertices for auto generated vertices use in conjuction
		/// with gl_VertexID.
		/// @attention Availability depends on: `BGFX_CAPS_VERTEX_ID`.
		/// </summary>
		///
		/// <param name="_numVertices">Number of vertices.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_vertex_count", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_vertex_count(Encoder* _this, uint _numVertices);

		/// <summary>
		/// Set instance data buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_idb">Transient instance data buffer.</param>
		/// <param name="_start">First instance data.</param>
		/// <param name="_num">Number of data instances.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_instance_data_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_instance_data_buffer(Encoder* _this, InstanceDataBuffer* _idb, uint _start, uint _num);

		/// <summary>
		/// Set instance data buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_handle">Vertex buffer.</param>
		/// <param name="_startVertex">First instance data.</param>
		/// <param name="_num">Number of data instances. Set instance data buffer for draw primitive.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_instance_data_from_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_instance_data_from_vertex_buffer(Encoder* _this, VertexBufferHandle _handle, uint _startVertex, uint _num);

		/// <summary>
		/// Set instance data buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_handle">Dynamic vertex buffer.</param>
		/// <param name="_startVertex">First instance data.</param>
		/// <param name="_num">Number of data instances.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_instance_data_from_dynamic_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_instance_data_from_dynamic_vertex_buffer(Encoder* _this, DynamicVertexBufferHandle _handle, uint _startVertex, uint _num);

		/// <summary>
		/// Set number of instances for auto generated instances use in conjuction
		/// with gl_InstanceID.
		/// @attention Availability depends on: `BGFX_CAPS_VERTEX_ID`.
		/// </summary>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_instance_count", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_instance_count(Encoder* _this, uint _numInstances);

		/// <summary>
		/// Set texture stage for draw primitive.
		/// </summary>
		///
		/// <param name="_stage">Texture unit.</param>
		/// <param name="_sampler">Program sampler.</param>
		/// <param name="_handle">Texture handle.</param>
		/// <param name="_flags">Texture sampling mode. Default value UINT32_MAX uses   texture sampling settings from the texture.   - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap     mode.   - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic     sampling.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_texture", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_texture(Encoder* _this, byte _stage, UniformHandle _sampler, TextureHandle _handle, uint _flags);

		/// <summary>
		/// Submit an empty primitive for rendering. Uniforms and draw state
		/// will be applied but no geometry will be submitted. Useful in cases
		/// when no other draw/compute primitive is submitted to view, but it's
		/// desired to execute clear view.
		/// @remark
		///   These empty draw calls will sort before ordinary draw calls.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_touch", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_touch(Encoder* _this, ushort _id);

		/// <summary>
		/// Submit primitive for rendering.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_program">Program.</param>
		/// <param name="_depth">Depth for sorting.</param>
		/// <param name="_flags">Discard or preserve states. See `BGFX_DISCARD_*`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_submit", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_submit(Encoder* _this, ushort _id, ProgramHandle _program, uint _depth, byte _flags);

		/// <summary>
		/// Submit primitive with occlusion query for rendering.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_program">Program.</param>
		/// <param name="_occlusionQuery">Occlusion query.</param>
		/// <param name="_depth">Depth for sorting.</param>
		/// <param name="_flags">Discard or preserve states. See `BGFX_DISCARD_*`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_submit_occlusion_query", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_submit_occlusion_query(Encoder* _this, ushort _id, ProgramHandle _program, OcclusionQueryHandle _occlusionQuery, uint _depth, byte _flags);

		/// <summary>
		/// Submit primitive for rendering with index and instance data info from
		/// indirect buffer.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_program">Program.</param>
		/// <param name="_indirectHandle">Indirect buffer.</param>
		/// <param name="_start">First element in indirect buffer.</param>
		/// <param name="_num">Number of dispatches.</param>
		/// <param name="_depth">Depth for sorting.</param>
		/// <param name="_flags">Discard or preserve states. See `BGFX_DISCARD_*`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_submit_indirect", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_submit_indirect(Encoder* _this, ushort _id, ProgramHandle _program, IndirectBufferHandle _indirectHandle, ushort _start, ushort _num, uint _depth, byte _flags);

		/// <summary>
		/// Set compute index buffer.
		/// </summary>
		///
		/// <param name="_stage">Compute stage.</param>
		/// <param name="_handle">Index buffer handle.</param>
		/// <param name="_access">Buffer access. See `Access::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_compute_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_compute_index_buffer(Encoder* _this, byte _stage, IndexBufferHandle _handle, Access _access);

		/// <summary>
		/// Set compute vertex buffer.
		/// </summary>
		///
		/// <param name="_stage">Compute stage.</param>
		/// <param name="_handle">Vertex buffer handle.</param>
		/// <param name="_access">Buffer access. See `Access::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_compute_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_compute_vertex_buffer(Encoder* _this, byte _stage, VertexBufferHandle _handle, Access _access);

		/// <summary>
		/// Set compute dynamic index buffer.
		/// </summary>
		///
		/// <param name="_stage">Compute stage.</param>
		/// <param name="_handle">Dynamic index buffer handle.</param>
		/// <param name="_access">Buffer access. See `Access::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_compute_dynamic_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_compute_dynamic_index_buffer(Encoder* _this, byte _stage, DynamicIndexBufferHandle _handle, Access _access);

		/// <summary>
		/// Set compute dynamic vertex buffer.
		/// </summary>
		///
		/// <param name="_stage">Compute stage.</param>
		/// <param name="_handle">Dynamic vertex buffer handle.</param>
		/// <param name="_access">Buffer access. See `Access::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_compute_dynamic_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_compute_dynamic_vertex_buffer(Encoder* _this, byte _stage, DynamicVertexBufferHandle _handle, Access _access);

		/// <summary>
		/// Set compute indirect buffer.
		/// </summary>
		///
		/// <param name="_stage">Compute stage.</param>
		/// <param name="_handle">Indirect buffer handle.</param>
		/// <param name="_access">Buffer access. See `Access::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_compute_indirect_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_compute_indirect_buffer(Encoder* _this, byte _stage, IndirectBufferHandle _handle, Access _access);

		/// <summary>
		/// Set compute image from texture.
		/// </summary>
		///
		/// <param name="_stage">Compute stage.</param>
		/// <param name="_handle">Texture handle.</param>
		/// <param name="_mip">Mip level.</param>
		/// <param name="_access">Image access. See `Access::Enum`.</param>
		/// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_set_image", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_set_image(Encoder* _this, byte _stage, TextureHandle _handle, byte _mip, Access _access, TextureFormat _format);

		/// <summary>
		/// Dispatch compute.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_program">Compute program.</param>
		/// <param name="_numX">Number of groups X.</param>
		/// <param name="_numY">Number of groups Y.</param>
		/// <param name="_numZ">Number of groups Z.</param>
		/// <param name="_flags">Discard or preserve states. See `BGFX_DISCARD_*`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_dispatch", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_dispatch(Encoder* _this, ushort _id, ProgramHandle _program, uint _numX, uint _numY, uint _numZ, byte _flags);

		/// <summary>
		/// Dispatch compute indirect.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_program">Compute program.</param>
		/// <param name="_indirectHandle">Indirect buffer.</param>
		/// <param name="_start">First element in indirect buffer.</param>
		/// <param name="_num">Number of dispatches.</param>
		/// <param name="_flags">Discard or preserve states. See `BGFX_DISCARD_*`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_dispatch_indirect", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_dispatch_indirect(Encoder* _this, ushort _id, ProgramHandle _program, IndirectBufferHandle _indirectHandle, ushort _start, ushort _num, byte _flags);

		/// <summary>
		/// Discard previously set state for draw or compute call.
		/// </summary>
		///
		/// <param name="_flags">Discard or preserve states. See `BGFX_DISCARD_*`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_discard", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_discard(Encoder* _this, byte _flags);

		/// <summary>
		/// Blit 2D texture region between two 2D textures.
		/// @attention Destination texture must be created with `BGFX_TEXTURE_BLIT_DST` flag.
		/// @attention Availability depends on: `BGFX_CAPS_TEXTURE_BLIT`.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_dst">Destination texture handle.</param>
		/// <param name="_dstMip">Destination texture mip level.</param>
		/// <param name="_dstX">Destination texture X position.</param>
		/// <param name="_dstY">Destination texture Y position.</param>
		/// <param name="_dstZ">If texture is 2D this argument should be 0. If destination texture is cube this argument represents destination texture cube face. For 3D texture this argument represents destination texture Z position.</param>
		/// <param name="_src">Source texture handle.</param>
		/// <param name="_srcMip">Source texture mip level.</param>
		/// <param name="_srcX">Source texture X position.</param>
		/// <param name="_srcY">Source texture Y position.</param>
		/// <param name="_srcZ">If texture is 2D this argument should be 0. If source texture is cube this argument represents source texture cube face. For 3D texture this argument represents source texture Z position.</param>
		/// <param name="_width">Width of region.</param>
		/// <param name="_height">Height of region.</param>
		/// <param name="_depth">If texture is 3D this argument represents depth of region, otherwise it's unused.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_encoder_blit", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void encoder_blit(Encoder* _this, ushort _id, TextureHandle _dst, byte _dstMip, ushort _dstX, ushort _dstY, ushort _dstZ, TextureHandle _src, byte _srcMip, ushort _srcX, ushort _srcY, ushort _srcZ, ushort _width, ushort _height, ushort _depth);

		/// <summary>
		/// Request screen shot of window back buffer.
		/// @remarks
		///   `bgfx::CallbackI::screenShot` must be implemented.
		/// @attention Frame buffer handle must be created with OS' target native window handle.
		/// </summary>
		///
		/// <param name="_handle">Frame buffer handle. If handle is `BGFX_INVALID_HANDLE` request will be made for main window back buffer.</param>
		/// <param name="_filePath">Will be passed to `bgfx::CallbackI::screenShot` callback.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_request_screen_shot", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void request_screen_shot(FrameBufferHandle _handle, [MarshalAs(UnmanagedType.LPStr)] string _filePath);

		/// <summary>
		/// Render frame.
		/// @attention `bgfx::renderFrame` is blocking call. It waits for
		///   `bgfx::frame` to be called from API thread to process frame.
		///   If timeout value is passed call will timeout and return even
		///   if `bgfx::frame` is not called.
		/// @warning This call should be only used on platforms that don't
		///   allow creating separate rendering thread. If it is called before
		///   to bgfx::init, render thread won't be created by bgfx::init call.
		/// </summary>
		///
		/// <param name="_msecs">Timeout in milliseconds.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_render_frame", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe RenderFrame render_frame(int _msecs);

		/// <summary>
		/// Set platform data.
		/// @warning Must be called before `bgfx::init`.
		/// </summary>
		///
		/// <param name="_data">Platform data.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_platform_data", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_platform_data(PlatformData* _data);

		/// <summary>
		/// Get internal data for interop.
		/// @attention It's expected you understand some bgfx internals before you
		///   use this call.
		/// @warning Must be called only on render thread.
		/// </summary>
		///
		[DllImport(DllName, EntryPoint = "bgfx_get_internal_data", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe InternalData* get_internal_data();

		/// <summary>
		/// Override internal texture with externally created texture. Previously
		/// created internal texture will released.
		/// @attention It's expected you understand some bgfx internals before you
		///   use this call.
		/// @warning Must be called only on render thread.
		/// </summary>
		///
		/// <param name="_handle">Texture handle.</param>
		/// <param name="_ptr">Native API pointer to texture.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_override_internal_texture_ptr", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe UIntPtr override_internal_texture_ptr(TextureHandle _handle, UIntPtr _ptr);

		/// <summary>
		/// Override internal texture by creating new texture. Previously created
		/// internal texture will released.
		/// @attention It's expected you understand some bgfx internals before you
		///   use this call.
		/// @returns Native API pointer to texture. If result is 0, texture is not created yet from the
		///   main thread.
		/// @warning Must be called only on render thread.
		/// </summary>
		///
		/// <param name="_handle">Texture handle.</param>
		/// <param name="_width">Width.</param>
		/// <param name="_height">Height.</param>
		/// <param name="_numMips">Number of mip-maps.</param>
		/// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
		/// <param name="_flags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_override_internal_texture", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe UIntPtr override_internal_texture(TextureHandle _handle, ushort _width, ushort _height, byte _numMips, TextureFormat _format, ulong _flags);

		/// <summary>
		/// Sets a debug marker. This allows you to group graphics calls together for easy browsing in
		/// graphics debugging tools.
		/// </summary>
		///
		/// <param name="_marker">Marker string.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_marker", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_marker([MarshalAs(UnmanagedType.LPStr)] string _marker);

		/// <summary>
		/// Set render states for draw primitive.
		/// @remarks
		///   1. To setup more complex states use:
		///      `BGFX_STATE_ALPHA_REF(_ref)`,
		///      `BGFX_STATE_POINT_SIZE(_size)`,
		///      `BGFX_STATE_BLEND_FUNC(_src, _dst)`,
		///      `BGFX_STATE_BLEND_FUNC_SEPARATE(_srcRGB, _dstRGB, _srcA, _dstA)`,
		///      `BGFX_STATE_BLEND_EQUATION(_equation)`,
		///      `BGFX_STATE_BLEND_EQUATION_SEPARATE(_equationRGB, _equationA)`
		///   2. `BGFX_STATE_BLEND_EQUATION_ADD` is set when no other blend
		///      equation is specified.
		/// </summary>
		///
		/// <param name="_state">State flags. Default state for primitive type is   triangles. See: `BGFX_STATE_DEFAULT`.   - `BGFX_STATE_DEPTH_TEST_*` - Depth test function.   - `BGFX_STATE_BLEND_*` - See remark 1 about BGFX_STATE_BLEND_FUNC.   - `BGFX_STATE_BLEND_EQUATION_*` - See remark 2.   - `BGFX_STATE_CULL_*` - Backface culling mode.   - `BGFX_STATE_WRITE_*` - Enable R, G, B, A or Z write.   - `BGFX_STATE_MSAA` - Enable hardware multisample antialiasing.   - `BGFX_STATE_PT_[TRISTRIP/LINES/POINTS]` - Primitive type.</param>
		/// <param name="_rgba">Sets blend factor used by `BGFX_STATE_BLEND_FACTOR` and   `BGFX_STATE_BLEND_INV_FACTOR` blend modes.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_state", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_state(ulong _state, uint _rgba);

		/// <summary>
		/// Set condition for rendering.
		/// </summary>
		///
		/// <param name="_handle">Occlusion query handle.</param>
		/// <param name="_visible">Render if occlusion query is visible.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_condition", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_condition(OcclusionQueryHandle _handle, bool _visible);

		/// <summary>
		/// Set stencil test state.
		/// </summary>
		///
		/// <param name="_fstencil">Front stencil state.</param>
		/// <param name="_bstencil">Back stencil state. If back is set to `BGFX_STENCIL_NONE` _fstencil is applied to both front and back facing primitives.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_stencil", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_stencil(uint _fstencil, uint _bstencil);

		/// <summary>
		/// Set scissor for draw primitive.
		/// @remark
		///   To scissor for all primitives in view see `bgfx::setViewScissor`.
		/// </summary>
		///
		/// <param name="_x">Position x from the left corner of the window.</param>
		/// <param name="_y">Position y from the top corner of the window.</param>
		/// <param name="_width">Width of view scissor region.</param>
		/// <param name="_height">Height of view scissor region.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_scissor", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe ushort set_scissor(ushort _x, ushort _y, ushort _width, ushort _height);

		/// <summary>
		/// Set scissor from cache for draw primitive.
		/// @remark
		///   To scissor for all primitives in view see `bgfx::setViewScissor`.
		/// </summary>
		///
		/// <param name="_cache">Index in scissor cache.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_scissor_cached", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_scissor_cached(ushort _cache);

		/// <summary>
		/// Set model matrix for draw primitive. If it is not called,
		/// the model will be rendered with an identity model matrix.
		/// </summary>
		///
		/// <param name="_mtx">Pointer to first matrix in array.</param>
		/// <param name="_num">Number of matrices in array.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_transform", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint set_transform(void* _mtx, ushort _num);

		/// <summary>
		///  Set model matrix from matrix cache for draw primitive.
		/// </summary>
		///
		/// <param name="_cache">Index in matrix cache.</param>
		/// <param name="_num">Number of matrices from cache.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_transform_cached", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_transform_cached(uint _cache, ushort _num);

		/// <summary>
		/// Reserve matrices in internal matrix cache.
		/// @attention Pointer returned can be modifed until `bgfx::frame` is called.
		/// </summary>
		///
		/// <param name="_transform">Pointer to `Transform` structure.</param>
		/// <param name="_num">Number of matrices.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_alloc_transform", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint alloc_transform(Transform* _transform, ushort _num);

		/// <summary>
		/// Set shader uniform parameter for draw primitive.
		/// </summary>
		///
		/// <param name="_handle">Uniform.</param>
		/// <param name="_value">Pointer to uniform data.</param>
		/// <param name="_num">Number of elements. Passing `UINT16_MAX` will use the _num passed on uniform creation.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_uniform", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_uniform(UniformHandle _handle, void* _value, ushort _num);

		/// <summary>
		/// Set index buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_handle">Index buffer.</param>
		/// <param name="_firstIndex">First index to render.</param>
		/// <param name="_numIndices">Number of indices to render.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_index_buffer(IndexBufferHandle _handle, uint _firstIndex, uint _numIndices);

		/// <summary>
		/// Set index buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_handle">Dynamic index buffer.</param>
		/// <param name="_firstIndex">First index to render.</param>
		/// <param name="_numIndices">Number of indices to render.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_dynamic_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_dynamic_index_buffer(DynamicIndexBufferHandle _handle, uint _firstIndex, uint _numIndices);

		/// <summary>
		/// Set index buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_tib">Transient index buffer.</param>
		/// <param name="_firstIndex">First index to render.</param>
		/// <param name="_numIndices">Number of indices to render.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_transient_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_transient_index_buffer(TransientIndexBuffer* _tib, uint _firstIndex, uint _numIndices);

		/// <summary>
		/// Set vertex buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_stream">Vertex stream.</param>
		/// <param name="_handle">Vertex buffer.</param>
		/// <param name="_startVertex">First vertex to render.</param>
		/// <param name="_numVertices">Number of vertices to render.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_vertex_buffer(byte _stream, VertexBufferHandle _handle, uint _startVertex, uint _numVertices);

		/// <summary>
		/// Set vertex buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_stream">Vertex stream.</param>
		/// <param name="_handle">Vertex buffer.</param>
		/// <param name="_startVertex">First vertex to render.</param>
		/// <param name="_numVertices">Number of vertices to render.</param>
		/// <param name="_layoutHandle">Vertex layout for aliasing vertex buffer. If invalid handle is used, vertex layout used for creation of vertex buffer will be used.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_vertex_buffer_with_layout", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_vertex_buffer_with_layout(byte _stream, VertexBufferHandle _handle, uint _startVertex, uint _numVertices, VertexLayoutHandle _layoutHandle);

		/// <summary>
		/// Set vertex buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_stream">Vertex stream.</param>
		/// <param name="_handle">Dynamic vertex buffer.</param>
		/// <param name="_startVertex">First vertex to render.</param>
		/// <param name="_numVertices">Number of vertices to render.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_dynamic_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_dynamic_vertex_buffer(byte _stream, DynamicVertexBufferHandle _handle, uint _startVertex, uint _numVertices);

		/// <summary>
		/// Set vertex buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_stream">Vertex stream.</param>
		/// <param name="_handle">Dynamic vertex buffer.</param>
		/// <param name="_startVertex">First vertex to render.</param>
		/// <param name="_numVertices">Number of vertices to render.</param>
		/// <param name="_layoutHandle">Vertex layout for aliasing vertex buffer. If invalid handle is used, vertex layout used for creation of vertex buffer will be used.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_dynamic_vertex_buffer_with_layout", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_dynamic_vertex_buffer_with_layout(byte _stream, DynamicVertexBufferHandle _handle, uint _startVertex, uint _numVertices, VertexLayoutHandle _layoutHandle);

		/// <summary>
		/// Set vertex buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_stream">Vertex stream.</param>
		/// <param name="_tvb">Transient vertex buffer.</param>
		/// <param name="_startVertex">First vertex to render.</param>
		/// <param name="_numVertices">Number of vertices to render.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_transient_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_transient_vertex_buffer(byte _stream, TransientVertexBuffer* _tvb, uint _startVertex, uint _numVertices);

		/// <summary>
		/// Set vertex buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_stream">Vertex stream.</param>
		/// <param name="_tvb">Transient vertex buffer.</param>
		/// <param name="_startVertex">First vertex to render.</param>
		/// <param name="_numVertices">Number of vertices to render.</param>
		/// <param name="_layoutHandle">Vertex layout for aliasing vertex buffer. If invalid handle is used, vertex layout used for creation of vertex buffer will be used.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_transient_vertex_buffer_with_layout", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_transient_vertex_buffer_with_layout(byte _stream, TransientVertexBuffer* _tvb, uint _startVertex, uint _numVertices, VertexLayoutHandle _layoutHandle);

		/// <summary>
		/// Set number of vertices for auto generated vertices use in conjuction
		/// with gl_VertexID.
		/// @attention Availability depends on: `BGFX_CAPS_VERTEX_ID`.
		/// </summary>
		///
		/// <param name="_numVertices">Number of vertices.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_vertex_count", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_vertex_count(uint _numVertices);

		/// <summary>
		/// Set instance data buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_idb">Transient instance data buffer.</param>
		/// <param name="_start">First instance data.</param>
		/// <param name="_num">Number of data instances.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_instance_data_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_instance_data_buffer(InstanceDataBuffer* _idb, uint _start, uint _num);

		/// <summary>
		/// Set instance data buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_handle">Vertex buffer.</param>
		/// <param name="_startVertex">First instance data.</param>
		/// <param name="_num">Number of data instances. Set instance data buffer for draw primitive.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_instance_data_from_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_instance_data_from_vertex_buffer(VertexBufferHandle _handle, uint _startVertex, uint _num);

		/// <summary>
		/// Set instance data buffer for draw primitive.
		/// </summary>
		///
		/// <param name="_handle">Dynamic vertex buffer.</param>
		/// <param name="_startVertex">First instance data.</param>
		/// <param name="_num">Number of data instances.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_instance_data_from_dynamic_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_instance_data_from_dynamic_vertex_buffer(DynamicVertexBufferHandle _handle, uint _startVertex, uint _num);

		/// <summary>
		/// Set number of instances for auto generated instances use in conjuction
		/// with gl_InstanceID.
		/// @attention Availability depends on: `BGFX_CAPS_VERTEX_ID`.
		/// </summary>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_instance_count", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_instance_count(uint _numInstances);

		/// <summary>
		/// Set texture stage for draw primitive.
		/// </summary>
		///
		/// <param name="_stage">Texture unit.</param>
		/// <param name="_sampler">Program sampler.</param>
		/// <param name="_handle">Texture handle.</param>
		/// <param name="_flags">Texture sampling mode. Default value UINT32_MAX uses   texture sampling settings from the texture.   - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap     mode.   - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic     sampling.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_texture", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_texture(byte _stage, UniformHandle _sampler, TextureHandle _handle, uint _flags);

		/// <summary>
		/// Submit an empty primitive for rendering. Uniforms and draw state
		/// will be applied but no geometry will be submitted.
		/// @remark
		///   These empty draw calls will sort before ordinary draw calls.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_touch", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void touch(ushort _id);

		/// <summary>
		/// Submit primitive for rendering.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_program">Program.</param>
		/// <param name="_depth">Depth for sorting.</param>
		/// <param name="_flags">Which states to discard for next draw. See `BGFX_DISCARD_*`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_submit", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void submit(ushort _id, ProgramHandle _program, uint _depth, byte _flags);

		/// <summary>
		/// Submit primitive with occlusion query for rendering.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_program">Program.</param>
		/// <param name="_occlusionQuery">Occlusion query.</param>
		/// <param name="_depth">Depth for sorting.</param>
		/// <param name="_flags">Which states to discard for next draw. See `BGFX_DISCARD_*`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_submit_occlusion_query", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void submit_occlusion_query(ushort _id, ProgramHandle _program, OcclusionQueryHandle _occlusionQuery, uint _depth, byte _flags);

		/// <summary>
		/// Submit primitive for rendering with index and instance data info from
		/// indirect buffer.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_program">Program.</param>
		/// <param name="_indirectHandle">Indirect buffer.</param>
		/// <param name="_start">First element in indirect buffer.</param>
		/// <param name="_num">Number of dispatches.</param>
		/// <param name="_depth">Depth for sorting.</param>
		/// <param name="_flags">Which states to discard for next draw. See `BGFX_DISCARD_*`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_submit_indirect", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void submit_indirect(ushort _id, ProgramHandle _program, IndirectBufferHandle _indirectHandle, ushort _start, ushort _num, uint _depth, byte _flags);

		/// <summary>
		/// Set compute index buffer.
		/// </summary>
		///
		/// <param name="_stage">Compute stage.</param>
		/// <param name="_handle">Index buffer handle.</param>
		/// <param name="_access">Buffer access. See `Access::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_compute_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_compute_index_buffer(byte _stage, IndexBufferHandle _handle, Access _access);

		/// <summary>
		/// Set compute vertex buffer.
		/// </summary>
		///
		/// <param name="_stage">Compute stage.</param>
		/// <param name="_handle">Vertex buffer handle.</param>
		/// <param name="_access">Buffer access. See `Access::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_compute_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_compute_vertex_buffer(byte _stage, VertexBufferHandle _handle, Access _access);

		/// <summary>
		/// Set compute dynamic index buffer.
		/// </summary>
		///
		/// <param name="_stage">Compute stage.</param>
		/// <param name="_handle">Dynamic index buffer handle.</param>
		/// <param name="_access">Buffer access. See `Access::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_compute_dynamic_index_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_compute_dynamic_index_buffer(byte _stage, DynamicIndexBufferHandle _handle, Access _access);

		/// <summary>
		/// Set compute dynamic vertex buffer.
		/// </summary>
		///
		/// <param name="_stage">Compute stage.</param>
		/// <param name="_handle">Dynamic vertex buffer handle.</param>
		/// <param name="_access">Buffer access. See `Access::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_compute_dynamic_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_compute_dynamic_vertex_buffer(byte _stage, DynamicVertexBufferHandle _handle, Access _access);

		/// <summary>
		/// Set compute indirect buffer.
		/// </summary>
		///
		/// <param name="_stage">Compute stage.</param>
		/// <param name="_handle">Indirect buffer handle.</param>
		/// <param name="_access">Buffer access. See `Access::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_compute_indirect_buffer", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_compute_indirect_buffer(byte _stage, IndirectBufferHandle _handle, Access _access);

		/// <summary>
		/// Set compute image from texture.
		/// </summary>
		///
		/// <param name="_stage">Compute stage.</param>
		/// <param name="_handle">Texture handle.</param>
		/// <param name="_mip">Mip level.</param>
		/// <param name="_access">Image access. See `Access::Enum`.</param>
		/// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_set_image", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void set_image(byte _stage, TextureHandle _handle, byte _mip, Access _access, TextureFormat _format);

		/// <summary>
		/// Dispatch compute.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_program">Compute program.</param>
		/// <param name="_numX">Number of groups X.</param>
		/// <param name="_numY">Number of groups Y.</param>
		/// <param name="_numZ">Number of groups Z.</param>
		/// <param name="_flags">Discard or preserve states. See `BGFX_DISCARD_*`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_dispatch", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void dispatch(ushort _id, ProgramHandle _program, uint _numX, uint _numY, uint _numZ, byte _flags);

		/// <summary>
		/// Dispatch compute indirect.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_program">Compute program.</param>
		/// <param name="_indirectHandle">Indirect buffer.</param>
		/// <param name="_start">First element in indirect buffer.</param>
		/// <param name="_num">Number of dispatches.</param>
		/// <param name="_flags">Discard or preserve states. See `BGFX_DISCARD_*`.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_dispatch_indirect", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void dispatch_indirect(ushort _id, ProgramHandle _program, IndirectBufferHandle _indirectHandle, ushort _start, ushort _num, byte _flags);

		/// <summary>
		/// Discard previously set state for draw or compute call.
		/// </summary>
		///
		/// <param name="_flags">Draw/compute states to discard.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_discard", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void discard(byte _flags);

		/// <summary>
		/// Blit 2D texture region between two 2D textures.
		/// @attention Destination texture must be created with `BGFX_TEXTURE_BLIT_DST` flag.
		/// @attention Availability depends on: `BGFX_CAPS_TEXTURE_BLIT`.
		/// </summary>
		///
		/// <param name="_id">View id.</param>
		/// <param name="_dst">Destination texture handle.</param>
		/// <param name="_dstMip">Destination texture mip level.</param>
		/// <param name="_dstX">Destination texture X position.</param>
		/// <param name="_dstY">Destination texture Y position.</param>
		/// <param name="_dstZ">If texture is 2D this argument should be 0. If destination texture is cube this argument represents destination texture cube face. For 3D texture this argument represents destination texture Z position.</param>
		/// <param name="_src">Source texture handle.</param>
		/// <param name="_srcMip">Source texture mip level.</param>
		/// <param name="_srcX">Source texture X position.</param>
		/// <param name="_srcY">Source texture Y position.</param>
		/// <param name="_srcZ">If texture is 2D this argument should be 0. If source texture is cube this argument represents source texture cube face. For 3D texture this argument represents source texture Z position.</param>
		/// <param name="_width">Width of region.</param>
		/// <param name="_height">Height of region.</param>
		/// <param name="_depth">If texture is 3D this argument represents depth of region, otherwise it's unused.</param>
		///
		[DllImport(DllName, EntryPoint = "bgfx_blit", CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void blit(ushort _id, TextureHandle _dst, byte _dstMip, ushort _dstX, ushort _dstY, ushort _dstZ, TextureHandle _src, byte _srcMip, ushort _srcX, ushort _srcY, ushort _srcZ, ushort _width, ushort _height, ushort _depth);

	}
}