using System.Text;

namespace STB
{
	static unsafe partial class StbImageWrite
	{
		public static int StbiWriteTgaWithRle = 1;

		public delegate int WriteCallback(void* context, void* data, int size);

		public class StbiWriteContext
		{
			public WriteCallback Func;
			public void* Context;
		}

		public static void stbi__start_write_callbacks(StbiWriteContext s, WriteCallback c, void* context)
		{
			s.Func = c;
			s.Context = context;
		}

		public static void stbiw__writefv(StbiWriteContext s, string fmt, params object[] v)
		{
			var vindex = 0;
			for (var i = 0; i < fmt.Length; ++i)
			{
				var c = fmt[i];
				switch (c)
				{
					case ' ':
						break;
					case '1':
						{
							var x = (byte)((int)v[vindex++] & 0xff);
							s.Func(s.Context, &x, 1);
							break;
						}
					case '2':
						{
							var x = (int)v[vindex++];
							var b = stackalloc byte[2];
							b[0] = (byte)(x & 0xff);
							b[1] = (byte)((x >> 8) & 0xff);
							s.Func(s.Context, b, 2);
							break;
						}
					case '4':
						{
							var x = (int)v[vindex++];
							var b = stackalloc byte[4];
							b[0] = (byte)(x & 0xff);
							b[1] = (byte)((x >> 8) & 0xff);
							b[2] = (byte)((x >> 16) & 0xff);
							b[3] = (byte)((x >> 24) & 0xff);
							s.Func(s.Context, b, 4);
							break;
						}
				}
			}
		}

		public static void stbiw__writef(StbiWriteContext s, string fmt, params object[] v)
		{
			stbiw__writefv(s, fmt, v);
		}

		public static int stbiw__outfile(StbiWriteContext s, int rgbDir, int vdir, int x, int y, int comp,
			int expandMono, void* data, int alpha, int pad, string fmt, params object[] v)
		{
			if ((y < 0) || (x < 0))
			{
				return 0;
			}

			stbiw__writefv(s, fmt, v);
			stbiw__write_pixels(s, rgbDir, vdir, x, y, comp, data, alpha, pad, expandMono);
			return 1;
		}

		public static int stbi_write_bmp_to_func(WriteCallback func,
			void* context,
			int x,
			int y,
			int comp,
			void* data
			)
		{
			var s = new StbiWriteContext();
			stbi__start_write_callbacks(s, func, context);
			return stbi_write_bmp_core(s, x, y, comp, data);
		}

		public static int stbi_write_tga_to_func(WriteCallback func,
			void* context,
			int x,
			int y,
			int comp,
			void* data
			)
		{
			var s = new StbiWriteContext();
			stbi__start_write_callbacks(s, func, context);
			return stbi_write_tga_core(s, x, y, comp, data);
		}

		public static int stbi_write_hdr_to_func(WriteCallback func,
			void* context,
			int x,
			int y,
			int comp,
			float* data
			)
		{
			StbiWriteContext s = new StbiWriteContext();
			stbi__start_write_callbacks(s, func, context);
			return stbi_write_hdr_core(s, x, y, comp, data);
		}

		public static int stbi_write_png_to_func(WriteCallback func,
			void* context,
			int x,
			int y,
			int comp,
			void* data,
			int strideBytes
			)
		{
			int len;
			var png = stbi_write_png_to_mem((byte*)(data), strideBytes, x, y, comp, &len);
			if (png == null) return 0;
			func(context, png, len);
			CRuntime.Free(png);
			return 1;
		}

		public static int stbi_write_jpg_to_func(WriteCallback func,
			void* context,
			int x,
			int y,
			int comp,
			void* data,
			int quality
			)
		{
			StbiWriteContext s = new StbiWriteContext();
			stbi__start_write_callbacks(s, func, context);
			return stbi_write_jpg_core(s, x, y, comp, data, quality);
		}

		public static int stbi_write_hdr_core(StbiWriteContext s, int x, int y, int comp, float* data)
		{
			if ((y <= 0) || (x <= 0) || (data == null))
			{
				return 0;
			}

			var scratch = (byte*)(CRuntime.Malloc((ulong)(x * 4)));

			int i;
			var header = "#?RADIANCE\n# Written by stb_image_write.h\nFORMAT=32-bit_rle_rgbe\n";
			var bytes = Encoding.UTF8.GetBytes(header);
			fixed (byte* ptr = bytes)
			{
				s.Func(s.Context, ((sbyte*)ptr), bytes.Length);
			}

			var str = string.Format("EXPOSURE=          1.0000000000000\n\n-Y {0} +X {1}\n", y, x);
			bytes = Encoding.UTF8.GetBytes(str);
			fixed (byte* ptr = bytes)
			{
				s.Func(s.Context, ((sbyte*)ptr), bytes.Length);
			}
			for (i = 0; i < y; i++)
			{
				stbiw__write_hdr_scanline(s, x, comp, scratch, data + comp * i * x);
			}
			CRuntime.Free(scratch);
			return 1;
		}
	}
}
