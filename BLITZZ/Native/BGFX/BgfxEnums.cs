using System;

namespace BLITZZ.Native.BGFX
{
	[Flags]
	public enum StateFlags : ulong
	{
		/// <summary>
		/// Enable R write.
		/// </summary>
		WriteR = 0x0000000000000001,

		/// <summary>
		/// Enable G write.
		/// </summary>
		WriteG = 0x0000000000000002,

		/// <summary>
		/// Enable B write.
		/// </summary>
		WriteB = 0x0000000000000004,

		/// <summary>
		/// Enable alpha write.
		/// </summary>
		WriteA = 0x0000000000000008,

		/// <summary>
		/// Enable depth write.
		/// </summary>
		WriteZ = 0x0000004000000000,

		/// <summary>
		/// Enable RGB write.
		/// </summary>
		WriteRgb = 0x0000000000000007,

		/// <summary>
		/// Write all channels mask.
		/// </summary>
		WriteMask = 0x000000400000000f,

		/// <summary>
		/// Enable depth test, less.
		/// </summary>
		DepthTestLess = 0x0000000000000010,

		/// <summary>
		/// Enable depth test, less or equal.
		/// </summary>
		DepthTestLequal = 0x0000000000000020,

		/// <summary>
		/// Enable depth test, equal.
		/// </summary>
		DepthTestEqual = 0x0000000000000030,

		/// <summary>
		/// Enable depth test, greater or equal.
		/// </summary>
		DepthTestGequal = 0x0000000000000040,

		/// <summary>
		/// Enable depth test, greater.
		/// </summary>
		DepthTestGreater = 0x0000000000000050,

		/// <summary>
		/// Enable depth test, not equal.
		/// </summary>
		DepthTestNotequal = 0x0000000000000060,

		/// <summary>
		/// Enable depth test, never.
		/// </summary>
		DepthTestNever = 0x0000000000000070,

		/// <summary>
		/// Enable depth test, always.
		/// </summary>
		DepthTestAlways = 0x0000000000000080,
		DepthTestShift = 4,
		DepthTestMask = 0x00000000000000f0,

		/// <summary>
		/// 0, 0, 0, 0
		/// </summary>
		BlendZero = 0x0000000000001000,

		/// <summary>
		/// 1, 1, 1, 1
		/// </summary>
		BlendOne = 0x0000000000002000,

		/// <summary>
		/// Rs, Gs, Bs, As
		/// </summary>
		BlendSrcColor = 0x0000000000003000,

		/// <summary>
		/// 1-Rs, 1-Gs, 1-Bs, 1-As
		/// </summary>
		BlendInvSrcColor = 0x0000000000004000,

		/// <summary>
		/// As, As, As, As
		/// </summary>
		BlendSrcAlpha = 0x0000000000005000,

		/// <summary>
		/// 1-As, 1-As, 1-As, 1-As
		/// </summary>
		BlendInvSrcAlpha = 0x0000000000006000,

		/// <summary>
		/// Ad, Ad, Ad, Ad
		/// </summary>
		BlendDstAlpha = 0x0000000000007000,

		/// <summary>
		/// 1-Ad, 1-Ad, 1-Ad ,1-Ad
		/// </summary>
		BlendInvDstAlpha = 0x0000000000008000,

		/// <summary>
		/// Rd, Gd, Bd, Ad
		/// </summary>
		BlendDstColor = 0x0000000000009000,

		/// <summary>
		/// 1-Rd, 1-Gd, 1-Bd, 1-Ad
		/// </summary>
		BlendInvDstColor = 0x000000000000a000,

		/// <summary>
		/// f, f, f, 1; f = min(As, 1-Ad)
		/// </summary>
		BlendSrcAlphaSat = 0x000000000000b000,

		/// <summary>
		/// Blend factor
		/// </summary>
		BlendFactor = 0x000000000000c000,

		/// <summary>
		/// 1-Blend factor
		/// </summary>
		BlendInvFactor = 0x000000000000d000,
		BlendShift = 12,
		BlendMask = 0x000000000ffff000,

		/// <summary>
		/// Blend add: src + dst.
		/// </summary>
		BlendEquationAdd = 0x0000000000000000,

		/// <summary>
		/// Blend subtract: src - dst.
		/// </summary>
		BlendEquationSub = 0x0000000010000000,

		/// <summary>
		/// Blend reverse subtract: dst - src.
		/// </summary>
		BlendEquationRevsub = 0x0000000020000000,

		/// <summary>
		/// Blend min: min(src, dst).
		/// </summary>
		BlendEquationMin = 0x0000000030000000,

		/// <summary>
		/// Blend max: max(src, dst).
		/// </summary>
		BlendEquationMax = 0x0000000040000000,
		BlendEquationShift = 28,
		BlendEquationMask = 0x00000003f0000000,

		/// <summary>
		/// Cull clockwise triangles.
		/// </summary>
		CullCw = 0x0000001000000000,

		/// <summary>
		/// Cull counter-clockwise triangles.
		/// </summary>
		CullCcw = 0x0000002000000000,
		CullShift = 36,
		CullMask = 0x0000003000000000,
		AlphaRefShift = 40,
		AlphaRefMask = 0x0000ff0000000000,

		/// <summary>
		/// Tristrip.
		/// </summary>
		PtTristrip = 0x0001000000000000,

		/// <summary>
		/// Lines.
		/// </summary>
		PtLines = 0x0002000000000000,

		/// <summary>
		/// Line strip.
		/// </summary>
		PtLinestrip = 0x0003000000000000,

		/// <summary>
		/// Points.
		/// </summary>
		PtPoints = 0x0004000000000000,
		PtShift = 48,
		PtMask = 0x0007000000000000,
		PointSizeShift = 52,
		PointSizeMask = 0x00f0000000000000,

		/// <summary>
		/// Enable MSAA rasterization.
		/// </summary>
		Msaa = 0x0100000000000000,

		/// <summary>
		/// Enable line AA rasterization.
		/// </summary>
		Lineaa = 0x0200000000000000,

		/// <summary>
		/// Enable conservative rasterization.
		/// </summary>
		ConservativeRaster = 0x0400000000000000,

		/// <summary>
		/// No state.
		/// </summary>
		None = 0x0000000000000000,

		/// <summary>
		/// Front counter-clockwise (default is clockwise).
		/// </summary>
		FrontCcw = 0x0000008000000000,

		/// <summary>
		/// Enable blend independent.
		/// </summary>
		BlendIndependent = 0x0000000400000000,

		/// <summary>
		/// Enable alpha to coverage.
		/// </summary>
		BlendAlphaToCoverage = 0x0000000800000000,

		/// <summary>
		/// Default state is write to RGB, alpha, and depth with depth test less enabled, with clockwise
		/// culling and MSAA (when writing into MSAA frame buffer, otherwise this flag is ignored).
		/// </summary>
		Default = 0x010000500000001f,
		Mask = 0xffffffffffffffff,
		ReservedShift = 61,
		ReservedMask = 0xe000000000000000,
	}

	[Flags]
	public enum StencilFlags : uint
	{
		FuncRefShift = 0,
		FuncRefMask = 0x000000ff,
		FuncRmaskShift = 8,
		FuncRmaskMask = 0x0000ff00,
		None = 0x00000000,
		Mask = 0xffffffff,
		Default = 0x00000000,

		/// <summary>
		/// Enable stencil test, less.
		/// </summary>
		TestLess = 0x00010000,

		/// <summary>
		/// Enable stencil test, less or equal.
		/// </summary>
		TestLequal = 0x00020000,

		/// <summary>
		/// Enable stencil test, equal.
		/// </summary>
		TestEqual = 0x00030000,

		/// <summary>
		/// Enable stencil test, greater or equal.
		/// </summary>
		TestGequal = 0x00040000,

		/// <summary>
		/// Enable stencil test, greater.
		/// </summary>
		TestGreater = 0x00050000,

		/// <summary>
		/// Enable stencil test, not equal.
		/// </summary>
		TestNotequal = 0x00060000,

		/// <summary>
		/// Enable stencil test, never.
		/// </summary>
		TestNever = 0x00070000,

		/// <summary>
		/// Enable stencil test, always.
		/// </summary>
		TestAlways = 0x00080000,
		TestShift = 16,
		TestMask = 0x000f0000,

		/// <summary>
		/// Zero.
		/// </summary>
		OpFailSZero = 0x00000000,

		/// <summary>
		/// Keep.
		/// </summary>
		OpFailSKeep = 0x00100000,

		/// <summary>
		/// Replace.
		/// </summary>
		OpFailSReplace = 0x00200000,

		/// <summary>
		/// Increment and wrap.
		/// </summary>
		OpFailSIncr = 0x00300000,

		/// <summary>
		/// Increment and clamp.
		/// </summary>
		OpFailSIncrsat = 0x00400000,

		/// <summary>
		/// Decrement and wrap.
		/// </summary>
		OpFailSDecr = 0x00500000,

		/// <summary>
		/// Decrement and clamp.
		/// </summary>
		OpFailSDecrsat = 0x00600000,

		/// <summary>
		/// Invert.
		/// </summary>
		OpFailSInvert = 0x00700000,
		OpFailSShift = 20,
		OpFailSMask = 0x00f00000,

		/// <summary>
		/// Zero.
		/// </summary>
		OpFailZZero = 0x00000000,

		/// <summary>
		/// Keep.
		/// </summary>
		OpFailZKeep = 0x01000000,

		/// <summary>
		/// Replace.
		/// </summary>
		OpFailZReplace = 0x02000000,

		/// <summary>
		/// Increment and wrap.
		/// </summary>
		OpFailZIncr = 0x03000000,

		/// <summary>
		/// Increment and clamp.
		/// </summary>
		OpFailZIncrsat = 0x04000000,

		/// <summary>
		/// Decrement and wrap.
		/// </summary>
		OpFailZDecr = 0x05000000,

		/// <summary>
		/// Decrement and clamp.
		/// </summary>
		OpFailZDecrsat = 0x06000000,

		/// <summary>
		/// Invert.
		/// </summary>
		OpFailZInvert = 0x07000000,
		OpFailZShift = 24,
		OpFailZMask = 0x0f000000,

		/// <summary>
		/// Zero.
		/// </summary>
		OpPassZZero = 0x00000000,

		/// <summary>
		/// Keep.
		/// </summary>
		OpPassZKeep = 0x10000000,

		/// <summary>
		/// Replace.
		/// </summary>
		OpPassZReplace = 0x20000000,

		/// <summary>
		/// Increment and wrap.
		/// </summary>
		OpPassZIncr = 0x30000000,

		/// <summary>
		/// Increment and clamp.
		/// </summary>
		OpPassZIncrsat = 0x40000000,

		/// <summary>
		/// Decrement and wrap.
		/// </summary>
		OpPassZDecr = 0x50000000,

		/// <summary>
		/// Decrement and clamp.
		/// </summary>
		OpPassZDecrsat = 0x60000000,

		/// <summary>
		/// Invert.
		/// </summary>
		OpPassZInvert = 0x70000000,
		OpPassZShift = 28,
		OpPassZMask = 0xf0000000,
	}

	[Flags]
	public enum ClearFlags : ushort
	{
		/// <summary>
		/// No clear flags.
		/// </summary>
		None = 0x0000,

		/// <summary>
		/// Clear color.
		/// </summary>
		Color = 0x0001,

		/// <summary>
		/// Clear depth.
		/// </summary>
		Depth = 0x0002,

		/// <summary>
		/// Clear stencil.
		/// </summary>
		Stencil = 0x0004,

		/// <summary>
		/// Discard frame buffer attachment 0.
		/// </summary>
		DiscardColor0 = 0x0008,

		/// <summary>
		/// Discard frame buffer attachment 1.
		/// </summary>
		DiscardColor1 = 0x0010,

		/// <summary>
		/// Discard frame buffer attachment 2.
		/// </summary>
		DiscardColor2 = 0x0020,

		/// <summary>
		/// Discard frame buffer attachment 3.
		/// </summary>
		DiscardColor3 = 0x0040,

		/// <summary>
		/// Discard frame buffer attachment 4.
		/// </summary>
		DiscardColor4 = 0x0080,

		/// <summary>
		/// Discard frame buffer attachment 5.
		/// </summary>
		DiscardColor5 = 0x0100,

		/// <summary>
		/// Discard frame buffer attachment 6.
		/// </summary>
		DiscardColor6 = 0x0200,

		/// <summary>
		/// Discard frame buffer attachment 7.
		/// </summary>
		DiscardColor7 = 0x0400,

		/// <summary>
		/// Discard frame buffer depth attachment.
		/// </summary>
		DiscardDepth = 0x0800,

		/// <summary>
		/// Discard frame buffer stencil attachment.
		/// </summary>
		DiscardStencil = 0x1000,
		DiscardColorMask = 0x07f8,
		DiscardMask = 0x1ff8,
	}

	[Flags]
	public enum DiscardFlags : uint
	{
		/// <summary>
		/// Preserve everything.
		/// </summary>
		None = 0x00000000,

		/// <summary>
		/// Discard texture sampler and buffer bindings.
		/// </summary>
		Bindings = 0x00000001,

		/// <summary>
		/// Discard index buffer.
		/// </summary>
		IndexBuffer = 0x00000002,

		/// <summary>
		/// Discard instance data.
		/// </summary>
		InstanceData = 0x00000004,

		/// <summary>
		/// Discard state and uniform bindings.
		/// </summary>
		State = 0x00000008,

		/// <summary>
		/// Discard transform.
		/// </summary>
		Transform = 0x00000010,

		/// <summary>
		/// Discard vertex streams.
		/// </summary>
		VertexStreams = 0x00000020,

		/// <summary>
		/// Discard all states.
		/// </summary>
		All = 0x000000ff,
	}

    /// <summary>
    /// Specifies debug text colors.
    /// </summary>
    public enum DebugColor {
        /// <summary>
        /// Black.
        /// </summary>
        Black,

        /// <summary>
        /// Blue.
        /// </summary>
        Blue,

        /// <summary>
        /// Green.
        /// </summary>
        Green,

        /// <summary>
        /// Cyan.
        /// </summary>
        Cyan,

        /// <summary>
        /// Red.
        /// </summary>
        Red,

        /// <summary>
        /// Magenta.
        /// </summary>
        Magenta,

        /// <summary>
        /// Brown.
        /// </summary>
        Brown,

        /// <summary>
        /// Light gray.
        /// </summary>
        LightGray,

        /// <summary>
        /// Dark gray.
        /// </summary>
        DarkGray,

        /// <summary>
        /// Light blue.
        /// </summary>
        LightBlue,

        /// <summary>
        /// Light green.
        /// </summary>
        LightGreen,

        /// <summary>
        /// Light cyan.
        /// </summary>
        LightCyan,

        /// <summary>
        /// Light red.
        /// </summary>
        LightRed,

        /// <summary>
        /// Light magenta.
        /// </summary>
        LightMagenta,

        /// <summary>
        /// Yellow.
        /// </summary>
        Yellow,

        /// <summary>
        /// White.
        /// </summary>
        White
    }

	[Flags]
	public enum DebugFlags : uint
	{
		/// <summary>
		/// No debug.
		/// </summary>
		None = 0x00000000,

		/// <summary>
		/// Enable wireframe for all primitives.
		/// </summary>
		Wireframe = 0x00000001,

		/// <summary>
		/// Enable infinitely fast hardware test. No draw calls will be submitted to driver.
		/// It's useful when profiling to quickly assess bottleneck between CPU and GPU.
		/// </summary>
		Ifh = 0x00000002,

		/// <summary>
		/// Enable statistics display.
		/// </summary>
		Stats = 0x00000004,

		/// <summary>
		/// Enable debug text display.
		/// </summary>
		Text = 0x00000008,

		/// <summary>
		/// Enable profiler.
		/// </summary>
		Profiler = 0x00000010,
	}

	[Flags]
	public enum BufferFlags : ushort
	{
		/// <summary>
		/// 1 8-bit value
		/// </summary>
		ComputeFormat8x1 = 0x0001,

		/// <summary>
		/// 2 8-bit values
		/// </summary>
		ComputeFormat8x2 = 0x0002,

		/// <summary>
		/// 4 8-bit values
		/// </summary>
		ComputeFormat8x4 = 0x0003,

		/// <summary>
		/// 1 16-bit value
		/// </summary>
		ComputeFormat16x1 = 0x0004,

		/// <summary>
		/// 2 16-bit values
		/// </summary>
		ComputeFormat16x2 = 0x0005,

		/// <summary>
		/// 4 16-bit values
		/// </summary>
		ComputeFormat16x4 = 0x0006,

		/// <summary>
		/// 1 32-bit value
		/// </summary>
		ComputeFormat32x1 = 0x0007,

		/// <summary>
		/// 2 32-bit values
		/// </summary>
		ComputeFormat32x2 = 0x0008,

		/// <summary>
		/// 4 32-bit values
		/// </summary>
		ComputeFormat32x4 = 0x0009,
		ComputeFormatShift = 0,
		ComputeFormatMask = 0x000f,

		/// <summary>
		/// Type `int`.
		/// </summary>
		ComputeTypeInt = 0x0010,

		/// <summary>
		/// Type `uint`.
		/// </summary>
		ComputeTypeUint = 0x0020,

		/// <summary>
		/// Type `float`.
		/// </summary>
		ComputeTypeFloat = 0x0030,
		ComputeTypeShift = 4,
		ComputeTypeMask = 0x0030,
		None = 0x0000,

		/// <summary>
		/// Buffer will be read by shader.
		/// </summary>
		ComputeRead = 0x0100,

		/// <summary>
		/// Buffer will be used for writing.
		/// </summary>
		ComputeWrite = 0x0200,

		/// <summary>
		/// Buffer will be used for storing draw indirect commands.
		/// </summary>
		DrawIndirect = 0x0400,

		/// <summary>
		/// Allow dynamic index/vertex buffer resize during update.
		/// </summary>
		AllowResize = 0x0800,

		/// <summary>
		/// Index buffer contains 32-bit indices.
		/// </summary>
		Index32 = 0x1000,
		ComputeReadWrite = 0x0300,
	}

	[Flags]
	public enum TextureFlags : ulong
	{
		None = 0x0000000000000000,

		/// <summary>
		/// Texture will be used for MSAA sampling.
		/// </summary>
		MsaaSample = 0x0000000800000000,

		/// <summary>
		/// Render target no MSAA.
		/// </summary>
		Rt = 0x0000001000000000,

		/// <summary>
		/// Texture will be used for compute write.
		/// </summary>
		ComputeWrite = 0x0000100000000000,

		/// <summary>
		/// Sample texture as sRGB.
		/// </summary>
		Srgb = 0x0000200000000000,

		/// <summary>
		/// Texture will be used as blit destination.
		/// </summary>
		BlitDst = 0x0000400000000000,

		/// <summary>
		/// Texture will be used for read back from GPU.
		/// </summary>
		ReadBack = 0x0000800000000000,

		/// <summary>
		/// Render target MSAAx2 mode.
		/// </summary>
		RtMsaaX2 = 0x0000002000000000,

		/// <summary>
		/// Render target MSAAx4 mode.
		/// </summary>
		RtMsaaX4 = 0x0000003000000000,

		/// <summary>
		/// Render target MSAAx8 mode.
		/// </summary>
		RtMsaaX8 = 0x0000004000000000,

		/// <summary>
		/// Render target MSAAx16 mode.
		/// </summary>
		RtMsaaX16 = 0x0000005000000000,
		RtMsaaShift = 36,
		RtMsaaMask = 0x0000007000000000,

		/// <summary>
		/// Render target will be used for writing
		/// </summary>
		RtWriteOnly = 0x0000008000000000,
		RtShift = 36,
		RtMask = 0x000000f000000000,
	}

	[Flags]
	public enum SamplerFlags : uint
	{
		/// <summary>
		/// Wrap U mode: Mirror
		/// </summary>
		UMirror = 0x00000001,

		/// <summary>
		/// Wrap U mode: Clamp
		/// </summary>
		UClamp = 0x00000002,

		/// <summary>
		/// Wrap U mode: Border
		/// </summary>
		UBorder = 0x00000003,
		UShift = 0,
		UMask = 0x00000003,

		/// <summary>
		/// Wrap V mode: Mirror
		/// </summary>
		VMirror = 0x00000004,

		/// <summary>
		/// Wrap V mode: Clamp
		/// </summary>
		VClamp = 0x00000008,

		/// <summary>
		/// Wrap V mode: Border
		/// </summary>
		VBorder = 0x0000000c,
		VShift = 2,
		VMask = 0x0000000c,

		/// <summary>
		/// Wrap W mode: Mirror
		/// </summary>
		WMirror = 0x00000010,

		/// <summary>
		/// Wrap W mode: Clamp
		/// </summary>
		WClamp = 0x00000020,

		/// <summary>
		/// Wrap W mode: Border
		/// </summary>
		WBorder = 0x00000030,
		WShift = 4,
		WMask = 0x00000030,

		/// <summary>
		/// Min sampling mode: Point
		/// </summary>
		MinPoint = 0x00000040,

		/// <summary>
		/// Min sampling mode: Anisotropic
		/// </summary>
		MinAnisotropic = 0x00000080,
		MinShift = 6,
		MinMask = 0x000000c0,

		/// <summary>
		/// Mag sampling mode: Point
		/// </summary>
		MagPoint = 0x00000100,

		/// <summary>
		/// Mag sampling mode: Anisotropic
		/// </summary>
		MagAnisotropic = 0x00000200,
		MagShift = 8,
		MagMask = 0x00000300,

		/// <summary>
		/// Mip sampling mode: Point
		/// </summary>
		MipPoint = 0x00000400,
		MipShift = 10,
		MipMask = 0x00000400,

		/// <summary>
		/// Compare when sampling depth texture: less.
		/// </summary>
		CompareLess = 0x00010000,

		/// <summary>
		/// Compare when sampling depth texture: less or equal.
		/// </summary>
		CompareLequal = 0x00020000,

		/// <summary>
		/// Compare when sampling depth texture: equal.
		/// </summary>
		CompareEqual = 0x00030000,

		/// <summary>
		/// Compare when sampling depth texture: greater or equal.
		/// </summary>
		CompareGequal = 0x00040000,

		/// <summary>
		/// Compare when sampling depth texture: greater.
		/// </summary>
		CompareGreater = 0x00050000,

		/// <summary>
		/// Compare when sampling depth texture: not equal.
		/// </summary>
		CompareNotequal = 0x00060000,

		/// <summary>
		/// Compare when sampling depth texture: never.
		/// </summary>
		CompareNever = 0x00070000,

		/// <summary>
		/// Compare when sampling depth texture: always.
		/// </summary>
		CompareAlways = 0x00080000,
		CompareShift = 16,
		CompareMask = 0x000f0000,
		BorderColorShift = 24,
		BorderColorMask = 0x0f000000,
		ReservedShift = 28,
		ReservedMask = 0xf0000000,
		None = 0x00000000,

		/// <summary>
		/// Sample stencil instead of depth.
		/// </summary>
		SampleStencil = 0x00100000,
		Point = 0x00000540,
		UvwMirror = 0x00000015,
		UvwClamp = 0x0000002a,
		UvwBorder = 0x0000003f,
		BitsMask = 0x000f07ff,
	}

	[Flags]
	public enum ResetFlags : uint
	{
		/// <summary>
		/// Enable 2x MSAA.
		/// </summary>
		MsaaX2 = 0x00000010,

		/// <summary>
		/// Enable 4x MSAA.
		/// </summary>
		MsaaX4 = 0x00000020,

		/// <summary>
		/// Enable 8x MSAA.
		/// </summary>
		MsaaX8 = 0x00000030,

		/// <summary>
		/// Enable 16x MSAA.
		/// </summary>
		MsaaX16 = 0x00000040,
		MsaaShift = 4,
		MsaaMask = 0x00000070,

		/// <summary>
		/// No reset flags.
		/// </summary>
		None = 0x00000000,

		/// <summary>
		/// Not supported yet.
		/// </summary>
		Fullscreen = 0x00000001,

		/// <summary>
		/// Enable V-Sync.
		/// </summary>
		Vsync = 0x00000080,

		/// <summary>
		/// Turn on/off max anisotropy.
		/// </summary>
		Maxanisotropy = 0x00000100,

		/// <summary>
		/// Begin screen capture.
		/// </summary>
		Capture = 0x00000200,

		/// <summary>
		/// Flush rendering after submitting to GPU.
		/// </summary>
		FlushAfterRender = 0x00002000,

		/// <summary>
		/// This flag specifies where flip occurs. Default behaviour is that flip occurs
		/// before rendering new frame. This flag only has effect when `BGFX_CONFIG_MULTITHREADED=0`.
		/// </summary>
		FlipAfterRender = 0x00004000,

		/// <summary>
		/// Enable sRGB backbuffer.
		/// </summary>
		SrgbBackbuffer = 0x00008000,

		/// <summary>
		/// Enable HDR10 rendering.
		/// </summary>
		Hdr10 = 0x00010000,

		/// <summary>
		/// Enable HiDPI rendering.
		/// </summary>
		Hidpi = 0x00020000,

		/// <summary>
		/// Enable depth clamp.
		/// </summary>
		DepthClamp = 0x00040000,

		/// <summary>
		/// Suspend rendering.
		/// </summary>
		Suspend = 0x00080000,
		FullscreenShift = 0,
		FullscreenMask = 0x00000001,
		ReservedShift = 31,
		ReservedMask = 0x80000000,
	}

	[Flags]
	public enum CapsFlags : ulong
	{
		/// <summary>
		/// Alpha to coverage is supported.
		/// </summary>
		AlphaToCoverage = 0x0000000000000001,

		/// <summary>
		/// Blend independent is supported.
		/// </summary>
		BlendIndependent = 0x0000000000000002,

		/// <summary>
		/// Compute shaders are supported.
		/// </summary>
		Compute = 0x0000000000000004,

		/// <summary>
		/// Conservative rasterization is supported.
		/// </summary>
		ConservativeRaster = 0x0000000000000008,

		/// <summary>
		/// Draw indirect is supported.
		/// </summary>
		DrawIndirect = 0x0000000000000010,

		/// <summary>
		/// Fragment depth is available in fragment shader.
		/// </summary>
		FragmentDepth = 0x0000000000000020,

		/// <summary>
		/// Fragment ordering is available in fragment shader.
		/// </summary>
		FragmentOrdering = 0x0000000000000040,

		/// <summary>
		/// Graphics debugger is present.
		/// </summary>
		GraphicsDebugger = 0x0000000000000080,

		/// <summary>
		/// HDR10 rendering is supported.
		/// </summary>
		Hdr10 = 0x0000000000000100,

		/// <summary>
		/// HiDPI rendering is supported.
		/// </summary>
		Hidpi = 0x0000000000000200,

		/// <summary>
		/// Image Read/Write is supported.
		/// </summary>
		ImageRw = 0x0000000000000400,

		/// <summary>
		/// 32-bit indices are supported.
		/// </summary>
		Index32 = 0x0000000000000800,

		/// <summary>
		/// Instancing is supported.
		/// </summary>
		Instancing = 0x0000000000001000,

		/// <summary>
		/// Occlusion query is supported.
		/// </summary>
		OcclusionQuery = 0x0000000000002000,

		/// <summary>
		/// Renderer is on separate thread.
		/// </summary>
		RendererMultithreaded = 0x0000000000004000,

		/// <summary>
		/// Multiple windows are supported.
		/// </summary>
		SwapChain = 0x0000000000008000,

		/// <summary>
		/// 2D texture array is supported.
		/// </summary>
		Texture2dArray = 0x0000000000010000,

		/// <summary>
		/// 3D textures are supported.
		/// </summary>
		Texture3d = 0x0000000000020000,

		/// <summary>
		/// Texture blit is supported.
		/// </summary>
		TextureBlit = 0x0000000000040000,
		TextureCompareReserved = 0x0000000000080000,

		/// <summary>
		/// Texture compare less equal mode is supported.
		/// </summary>
		TextureCompareLequal = 0x0000000000100000,

		/// <summary>
		/// Cubemap texture array is supported.
		/// </summary>
		TextureCubeArray = 0x0000000000200000,

		/// <summary>
		/// CPU direct access to GPU texture memory.
		/// </summary>
		TextureDirectAccess = 0x0000000000400000,

		/// <summary>
		/// Read-back texture is supported.
		/// </summary>
		TextureReadBack = 0x0000000000800000,

		/// <summary>
		/// Vertex attribute half-float is supported.
		/// </summary>
		VertexAttribHalf = 0x0000000001000000,

		/// <summary>
		/// Vertex attribute 10_10_10_2 is supported.
		/// </summary>
		VertexAttribUint10 = 0x0000000002000000,

		/// <summary>
		/// Rendering with VertexID only is supported.
		/// </summary>
		VertexId = 0x0000000004000000,

		/// <summary>
		/// Viewport layer is available in vertex shader.
		/// </summary>
		ViewportLayerArray = 0x0000000008000000,

		/// <summary>
		/// All texture compare modes are supported.
		/// </summary>
		TextureCompareAll = 0x0000000000180000,
	}

	[Flags]
	public enum CapsFormatFlags : uint
	{
		/// <summary>
		/// Texture format is not supported.
		/// </summary>
		TextureNone = 0x00000000,

		/// <summary>
		/// Texture format is supported.
		/// </summary>
		Texture2d = 0x00000001,

		/// <summary>
		/// Texture as sRGB format is supported.
		/// </summary>
		Texture2dSrgb = 0x00000002,

		/// <summary>
		/// Texture format is emulated.
		/// </summary>
		Texture2dEmulated = 0x00000004,

		/// <summary>
		/// Texture format is supported.
		/// </summary>
		Texture3d = 0x00000008,

		/// <summary>
		/// Texture as sRGB format is supported.
		/// </summary>
		Texture3dSrgb = 0x00000010,

		/// <summary>
		/// Texture format is emulated.
		/// </summary>
		Texture3dEmulated = 0x00000020,

		/// <summary>
		/// Texture format is supported.
		/// </summary>
		TextureCube = 0x00000040,

		/// <summary>
		/// Texture as sRGB format is supported.
		/// </summary>
		TextureCubeSrgb = 0x00000080,

		/// <summary>
		/// Texture format is emulated.
		/// </summary>
		TextureCubeEmulated = 0x00000100,

		/// <summary>
		/// Texture format can be used from vertex shader.
		/// </summary>
		TextureVertex = 0x00000200,

		/// <summary>
		/// Texture format can be used as image and read from.
		/// </summary>
		TextureImageRead = 0x00000400,

		/// <summary>
		/// Texture format can be used as image and written to.
		/// </summary>
		TextureImageWrite = 0x00000800,

		/// <summary>
		/// Texture format can be used as frame buffer.
		/// </summary>
		TextureFramebuffer = 0x00001000,

		/// <summary>
		/// Texture format can be used as MSAA frame buffer.
		/// </summary>
		TextureFramebufferMsaa = 0x00002000,

		/// <summary>
		/// Texture can be sampled as MSAA.
		/// </summary>
		TextureMsaa = 0x00004000,

		/// <summary>
		/// Texture format supports auto-generated mips.
		/// </summary>
		TextureMipAutogen = 0x00008000,
	}

	[Flags]
	public enum ResolveFlags : uint
	{
		/// <summary>
		/// No resolve flags.
		/// </summary>
		None = 0x00000000,

		/// <summary>
		/// Auto-generate mip maps on resolve.
		/// </summary>
		AutoGenMips = 0x00000001,
	}

	[Flags]
	public enum PciIdFlags : ushort
	{
		/// <summary>
		/// Autoselect adapter.
		/// </summary>
		None = 0x0000,

		/// <summary>
		/// Software rasterizer.
		/// </summary>
		SoftwareRasterizer = 0x0001,

		/// <summary>
		/// AMD adapter.
		/// </summary>
		Amd = 0x1002,

		/// <summary>
		/// Intel adapter.
		/// </summary>
		Intel = 0x8086,

		/// <summary>
		/// nVidia adapter.
		/// </summary>
		Nvidia = 0x10de,
	}

	[Flags]
	public enum CubeMapFlags : uint
	{
		/// <summary>
		/// Cubemap +x.
		/// </summary>
		PositiveX = 0x00000000,

		/// <summary>
		/// Cubemap -x.
		/// </summary>
		NegativeX = 0x00000001,

		/// <summary>
		/// Cubemap +y.
		/// </summary>
		PositiveY = 0x00000002,

		/// <summary>
		/// Cubemap -y.
		/// </summary>
		NegativeY = 0x00000003,

		/// <summary>
		/// Cubemap +z.
		/// </summary>
		PositiveZ = 0x00000004,

		/// <summary>
		/// Cubemap -z.
		/// </summary>
		NegativeZ = 0x00000005,
	}

	public enum Fatal
	{
		DebugCheck,
		InvalidShader,
		UnableToInitialize,
		UnableToCreateTexture,
		DeviceLost,

		Count
	}

	public enum RendererType
	{
		/// <summary>
		/// No rendering.
		/// </summary>
		Noop,

		/// <summary>
		/// Direct3D 9.0
		/// </summary>
		Direct3D9,

		/// <summary>
		/// Direct3D 11.0
		/// </summary>
		Direct3D11,

		/// <summary>
		/// Direct3D 12.0
		/// </summary>
		Direct3D12,

		/// <summary>
		/// GNM
		/// </summary>
		Gnm,

		/// <summary>
		/// Metal
		/// </summary>
		Metal,

		/// <summary>
		/// NVN
		/// </summary>
		Nvn,

		/// <summary>
		/// OpenGL ES 2.0+
		/// </summary>
		OpenGLES,

		/// <summary>
		/// OpenGL 2.1+
		/// </summary>
		OpenGL,

		/// <summary>
		/// Vulkan
		/// </summary>
		Vulkan,

		/// <summary>
		/// WebGPU
		/// </summary>
		WebGPU,

		Count
	}

	public enum Access
	{
		/// <summary>
		/// Read.
		/// </summary>
		Read,

		/// <summary>
		/// Write.
		/// </summary>
		Write,

		/// <summary>
		/// Read and write.
		/// </summary>
		ReadWrite,

		Count
	}

	public enum Attrib
	{
		/// <summary>
		/// a_position
		/// </summary>
		Position,

		/// <summary>
		/// a_normal
		/// </summary>
		Normal,

		/// <summary>
		/// a_tangent
		/// </summary>
		Tangent,

		/// <summary>
		/// a_bitangent
		/// </summary>
		Bitangent,

		/// <summary>
		/// a_color0
		/// </summary>
		Color0,

		/// <summary>
		/// a_color1
		/// </summary>
		Color1,

		/// <summary>
		/// a_color2
		/// </summary>
		Color2,

		/// <summary>
		/// a_color3
		/// </summary>
		Color3,

		/// <summary>
		/// a_indices
		/// </summary>
		Indices,

		/// <summary>
		/// a_weight
		/// </summary>
		Weight,

		/// <summary>
		/// a_texcoord0
		/// </summary>
		TexCoord0,

		/// <summary>
		/// a_texcoord1
		/// </summary>
		TexCoord1,

		/// <summary>
		/// a_texcoord2
		/// </summary>
		TexCoord2,

		/// <summary>
		/// a_texcoord3
		/// </summary>
		TexCoord3,

		/// <summary>
		/// a_texcoord4
		/// </summary>
		TexCoord4,

		/// <summary>
		/// a_texcoord5
		/// </summary>
		TexCoord5,

		/// <summary>
		/// a_texcoord6
		/// </summary>
		TexCoord6,

		/// <summary>
		/// a_texcoord7
		/// </summary>
		TexCoord7,

		Count
	}

	public enum AttribType
	{
		/// <summary>
		/// Uint8
		/// </summary>
		Uint8,

		/// <summary>
		/// Uint10, availability depends on: `BGFX_CAPS_VERTEX_ATTRIB_UINT10`.
		/// </summary>
		Uint10,

		/// <summary>
		/// Int16
		/// </summary>
		Int16,

		/// <summary>
		/// Half, availability depends on: `BGFX_CAPS_VERTEX_ATTRIB_HALF`.
		/// </summary>
		Half,

		/// <summary>
		/// Float
		/// </summary>
		Float,

		Count
	}

	public enum TextureFormat
	{
		/// <summary>
		/// DXT1 R5G6B5A1
		/// </summary>
		BC1,

		/// <summary>
		/// DXT3 R5G6B5A4
		/// </summary>
		BC2,

		/// <summary>
		/// DXT5 R5G6B5A8
		/// </summary>
		BC3,

		/// <summary>
		/// LATC1/ATI1 R8
		/// </summary>
		BC4,

		/// <summary>
		/// LATC2/ATI2 RG8
		/// </summary>
		BC5,

		/// <summary>
		/// BC6H RGB16F
		/// </summary>
		BC6H,

		/// <summary>
		/// BC7 RGB 4-7 bits per color channel, 0-8 bits alpha
		/// </summary>
		BC7,

		/// <summary>
		/// ETC1 RGB8
		/// </summary>
		ETC1,

		/// <summary>
		/// ETC2 RGB8
		/// </summary>
		ETC2,

		/// <summary>
		/// ETC2 RGBA8
		/// </summary>
		ETC2A,

		/// <summary>
		/// ETC2 RGB8A1
		/// </summary>
		ETC2A1,

		/// <summary>
		/// PVRTC1 RGB 2BPP
		/// </summary>
		PTC12,

		/// <summary>
		/// PVRTC1 RGB 4BPP
		/// </summary>
		PTC14,

		/// <summary>
		/// PVRTC1 RGBA 2BPP
		/// </summary>
		PTC12A,

		/// <summary>
		/// PVRTC1 RGBA 4BPP
		/// </summary>
		PTC14A,

		/// <summary>
		/// PVRTC2 RGBA 2BPP
		/// </summary>
		PTC22,

		/// <summary>
		/// PVRTC2 RGBA 4BPP
		/// </summary>
		PTC24,

		/// <summary>
		/// ATC RGB 4BPP
		/// </summary>
		ATC,

		/// <summary>
		/// ATCE RGBA 8 BPP explicit alpha
		/// </summary>
		ATCE,

		/// <summary>
		/// ATCI RGBA 8 BPP interpolated alpha
		/// </summary>
		ATCI,

		/// <summary>
		/// ASTC 4x4 8.0 BPP
		/// </summary>
		ASTC4x4,

		/// <summary>
		/// ASTC 5x5 5.12 BPP
		/// </summary>
		ASTC5x5,

		/// <summary>
		/// ASTC 6x6 3.56 BPP
		/// </summary>
		ASTC6x6,

		/// <summary>
		/// ASTC 8x5 3.20 BPP
		/// </summary>
		ASTC8x5,

		/// <summary>
		/// ASTC 8x6 2.67 BPP
		/// </summary>
		ASTC8x6,

		/// <summary>
		/// ASTC 10x5 2.56 BPP
		/// </summary>
		ASTC10x5,

		/// <summary>
		/// Compressed formats above.
		/// </summary>
		Unknown,
		R1,
		A8,
		R8,
		R8I,
		R8U,
		R8S,
		R16,
		R16I,
		R16U,
		R16F,
		R16S,
		R32I,
		R32U,
		R32F,
		RG8,
		RG8I,
		RG8U,
		RG8S,
		RG16,
		RG16I,
		RG16U,
		RG16F,
		RG16S,
		RG32I,
		RG32U,
		RG32F,
		RGB8,
		RGB8I,
		RGB8U,
		RGB8S,
		RGB9E5F,
		BGRA8,
		RGBA8,
		RGBA8I,
		RGBA8U,
		RGBA8S,
		RGBA16,
		RGBA16I,
		RGBA16U,
		RGBA16F,
		RGBA16S,
		RGBA32I,
		RGBA32U,
		RGBA32F,
		R5G6B5,
		RGBA4,
		RGB5A1,
		RGB10A2,
		RG11B10F,

		/// <summary>
		/// Depth formats below.
		/// </summary>
		UnknownDepth,
		D16,
		D24,
		D24S8,
		D32,
		D16F,
		D24F,
		D32F,
		D0S8,

		Count
	}

	public enum UniformType
	{
		/// <summary>
		/// Sampler.
		/// </summary>
		Sampler,

		/// <summary>
		/// Reserved, do not use.
		/// </summary>
		End,

		/// <summary>
		/// 4 floats vector.
		/// </summary>
		Vec4,

		/// <summary>
		/// 3x3 matrix.
		/// </summary>
		Mat3,

		/// <summary>
		/// 4x4 matrix.
		/// </summary>
		Mat4,

		Count
	}

	public enum BackbufferRatio
	{
		/// <summary>
		/// Equal to backbuffer.
		/// </summary>
		Equal,

		/// <summary>
		/// One half size of backbuffer.
		/// </summary>
		Half,

		/// <summary>
		/// One quarter size of backbuffer.
		/// </summary>
		Quarter,

		/// <summary>
		/// One eighth size of backbuffer.
		/// </summary>
		Eighth,

		/// <summary>
		/// One sixteenth size of backbuffer.
		/// </summary>
		Sixteenth,

		/// <summary>
		/// Double size of backbuffer.
		/// </summary>
		Double,

		Count
	}

	public enum OcclusionQueryResult
	{
		/// <summary>
		/// Query failed test.
		/// </summary>
		Invisible,

		/// <summary>
		/// Query passed test.
		/// </summary>
		Visible,

		/// <summary>
		/// Query result is not available yet.
		/// </summary>
		NoResult,

		Count
	}

	public enum Topology
	{
		/// <summary>
		/// Triangle list.
		/// </summary>
		TriList,

		/// <summary>
		/// Triangle strip.
		/// </summary>
		TriStrip,

		/// <summary>
		/// Line list.
		/// </summary>
		LineList,

		/// <summary>
		/// Line strip.
		/// </summary>
		LineStrip,

		/// <summary>
		/// Point list.
		/// </summary>
		PointList,

		Count
	}

	public enum TopologyConvert
	{
		/// <summary>
		/// Flip winding order of triangle list.
		/// </summary>
		TriListFlipWinding,

		/// <summary>
		/// Flip winding order of triangle strip.
		/// </summary>
		TriStripFlipWinding,

		/// <summary>
		/// Convert triangle list to line list.
		/// </summary>
		TriListToLineList,

		/// <summary>
		/// Convert triangle strip to triangle list.
		/// </summary>
		TriStripToTriList,

		/// <summary>
		/// Convert line strip to line list.
		/// </summary>
		LineStripToLineList,

		Count
	}

	public enum TopologySort
	{
		DirectionFrontToBackMin,
		DirectionFrontToBackAvg,
		DirectionFrontToBackMax,
		DirectionBackToFrontMin,
		DirectionBackToFrontAvg,
		DirectionBackToFrontMax,
		DistanceFrontToBackMin,
		DistanceFrontToBackAvg,
		DistanceFrontToBackMax,
		DistanceBackToFrontMin,
		DistanceBackToFrontAvg,
		DistanceBackToFrontMax,

		Count
	}

	public enum ViewMode
	{
		/// <summary>
		/// Default sort order.
		/// </summary>
		Default,

		/// <summary>
		/// Sort in the same order in which submit calls were called.
		/// </summary>
		Sequential,

		/// <summary>
		/// Sort draw call depth in ascending order.
		/// </summary>
		DepthAscending,

		/// <summary>
		/// Sort draw call depth in descending order.
		/// </summary>
		DepthDescending,

		Count
	}

	public enum RenderFrame
	{
		/// <summary>
		/// Renderer context is not created yet.
		/// </summary>
		NoContext,

		/// <summary>
		/// Renderer context is created and rendering.
		/// </summary>
		Render,

		/// <summary>
		/// Renderer context wait for main thread signal timed out without rendering.
		/// </summary>
		Timeout,

		/// <summary>
		/// Renderer context is getting destroyed.
		/// </summary>
		Exiting,

		Count
	}
}
