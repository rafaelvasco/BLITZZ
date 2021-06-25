using System;
namespace BLITZZ.Native.BGFX
{
	internal unsafe struct Caps
	{
		public unsafe struct GPU
		{
			public ushort vendorId;
			public ushort deviceId;
		}

		public unsafe struct Limits
		{
			public uint maxDrawCalls;
			public uint maxBlits;
			public uint maxTextureSize;
			public uint maxTextureLayers;
			public uint maxViews;
			public uint maxFrameBuffers;
			public uint maxFBAttachments;
			public uint maxPrograms;
			public uint maxShaders;
			public uint maxTextures;
			public uint maxTextureSamplers;
			public uint maxComputeBindings;
			public uint maxVertexLayouts;
			public uint maxVertexStreams;
			public uint maxIndexBuffers;
			public uint maxVertexBuffers;
			public uint maxDynamicIndexBuffers;
			public uint maxDynamicVertexBuffers;
			public uint maxUniforms;
			public uint maxOcclusionQueries;
			public uint maxEncoders;
			public uint minResourceCbSize;
			public uint transientVbSize;
			public uint transientIbSize;
		}

		public RendererType rendererType;
		public ulong supported;
		public ushort vendorId;
		public ushort deviceId;
		public byte homogeneousDepth;
		public byte originBottomLeft;
		public byte numGPUs;
		public fixed uint gpu[4];
		public Limits limits;
		public fixed ushort formats[85];
	}

	internal unsafe struct InternalData
	{
		public Caps* caps;
		public void* context;
	}

	internal unsafe struct PlatformData
	{
		public void* ndt;
		public void* nwh;
		public void* context;
		public void* backBuffer;
		public void* backBufferDS;
	}

	internal unsafe struct Resolution
	{
		public TextureFormat format;
		public uint width;
		public uint height;
		public uint reset;
		public byte numBackBuffers;
		public byte maxFrameLatency;
	}

	internal unsafe struct Init
	{
		public unsafe struct Limits
		{
			public ushort maxEncoders;
			public uint minResourceCbSize;
			public uint transientVbSize;
			public uint transientIbSize;
		}

		public RendererType type;
		public ushort vendorId;
		public ushort deviceId;
		public ulong capabilities;
		public byte debug;
		public byte profile;
		public PlatformData platformData;
		public Resolution resolution;
		public Limits limits;
		public IntPtr callback;
		public IntPtr allocator;
	}

	internal unsafe struct Memory
	{
		public byte* data;
		public uint size;
	}

	internal unsafe struct TransientIndexBuffer
	{
		public byte* data;
		public uint size;
		public uint startIndex;
		public IndexBufferHandle handle;
		public byte isIndex16;
	}

	internal unsafe struct TransientVertexBuffer
	{
		public byte* data;
		public uint size;
		public uint startVertex;
		public ushort stride;
		public VertexBufferHandle handle;
		public VertexLayoutHandle layoutHandle;
	}

	internal unsafe struct InstanceDataBuffer
	{
		public byte* data;
		public uint size;
		public uint offset;
		public uint num;
		public ushort stride;
		public VertexBufferHandle handle;
	}

	internal unsafe struct TextureInfo
	{
		public TextureFormat format;
		public uint storageSize;
		public ushort width;
		public ushort height;
		public ushort depth;
		public ushort numLayers;
		public byte numMips;
		public byte bitsPerPixel;
		public byte cubeMap;
	}

	internal unsafe struct UniformInfo
	{
		public fixed byte name[256];
		public UniformType type;
		public ushort num;
	}

	internal unsafe struct Attachment
	{
		public Access access;
		public TextureHandle handle;
		public ushort mip;
		public ushort layer;
		public ushort numLayers;
		public byte resolve;
	}

	internal unsafe struct Transform
	{
		public float* data;
		public ushort num;
	}

	internal unsafe struct ViewStats
	{
		public fixed byte name[256];
		public ushort view;
		public long cpuTimeBegin;
		public long cpuTimeEnd;
		public long gpuTimeBegin;
		public long gpuTimeEnd;
	}

	internal unsafe struct EncoderStats
	{
		public long cpuTimeBegin;
		public long cpuTimeEnd;
	}

	internal unsafe struct Stats
	{
		public long cpuTimeFrame;
		public long cpuTimeBegin;
		public long cpuTimeEnd;
		public long cpuTimerFreq;
		public long gpuTimeBegin;
		public long gpuTimeEnd;
		public long gpuTimerFreq;
		public long waitRender;
		public long waitSubmit;
		public uint numDraw;
		public uint numCompute;
		public uint numBlit;
		public uint maxGpuLatency;
		public ushort numDynamicIndexBuffers;
		public ushort numDynamicVertexBuffers;
		public ushort numFrameBuffers;
		public ushort numIndexBuffers;
		public ushort numOcclusionQueries;
		public ushort numPrograms;
		public ushort numShaders;
		public ushort numTextures;
		public ushort numUniforms;
		public ushort numVertexBuffers;
		public ushort numVertexLayouts;
		public long textureMemoryUsed;
		public long rtMemoryUsed;
		public int transientVbUsed;
		public int transientIbUsed;
		public fixed uint numPrims[5];
		public long gpuMemoryMax;
		public long gpuMemoryUsed;
		public ushort width;
		public ushort height;
		public ushort textWidth;
		public ushort textHeight;
		public ushort numViews;
		public ViewStats* viewStats;
		public byte numEncoders;
		public EncoderStats* encoderStats;
	}

	internal unsafe struct VertexLayout
	{
		public uint hash;
		public ushort stride;
		public fixed ushort offset[18];
		public fixed ushort attributes[18];
	}

	internal unsafe struct Encoder
	{
	}

	internal struct DynamicIndexBufferHandle
	{
		public ushort idx;
		public bool Valid => idx != ushort.MaxValue;

		public static DynamicIndexBufferHandle Null => new() { idx = ushort.MaxValue };
	}

	internal struct DynamicVertexBufferHandle
	{
		public ushort idx;
		public bool Valid => idx != ushort.MaxValue;

		public static DynamicVertexBufferHandle Null => new() { idx = ushort.MaxValue };
	}

	internal struct FrameBufferHandle
	{
		public ushort idx;
		public bool Valid => idx != ushort.MaxValue;

		public static FrameBufferHandle Null => new() { idx = ushort.MaxValue };
	}

	internal struct IndexBufferHandle
	{
		public ushort idx;
		public bool Valid => idx != ushort.MaxValue;

		public static IndexBufferHandle Null => new() { idx = ushort.MaxValue };

	}

	internal struct IndirectBufferHandle
	{
		public ushort idx;
		public bool Valid => idx != ushort.MaxValue;
	}

	internal struct OcclusionQueryHandle
	{
		public ushort idx;
		public bool Valid => idx != ushort.MaxValue;
	}

	internal struct ProgramHandle
	{
		public ushort idx;
		public bool Valid => idx != ushort.MaxValue;

		public static ProgramHandle Null => new() { idx = ushort.MaxValue };
	}

	internal struct ShaderHandle
	{
		public ushort idx;
		public bool Valid => idx != ushort.MaxValue;

		public static ShaderHandle Null => new() { idx = ushort.MaxValue };
	}

	internal struct TextureHandle
	{
		public ushort idx;
		public bool Valid => idx != ushort.MaxValue;

		public static TextureHandle Null => new() { idx = ushort.MaxValue };
	}

	internal struct UniformHandle
	{
		public ushort idx;
		public bool Valid => idx != ushort.MaxValue;
		public static UniformHandle Null => new() { idx = ushort.MaxValue };
	}


	internal struct VertexBufferHandle
	{
		public ushort idx;
		public bool Valid => idx != ushort.MaxValue;

		public static VertexBufferHandle Null => new() { idx = ushort.MaxValue };
	}

	internal struct VertexLayoutHandle
	{
		public ushort idx;
		public bool Valid => idx != ushort.MaxValue;

		public static VertexBufferHandle Null => new() { idx = ushort.MaxValue };
	}
}
