using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace BLITZZ
{
    public struct Color : IEquatable<Color>, IComparable<Color>
    {
        public static readonly Color Transparent = new(0x00000000);
        public static readonly Color AliceBlue = new(0xfffff8f0);
        public static readonly Color AntiqueWhite = new(0xffd7ebfa);
        public static readonly Color Aqua = new(0xffffff00);
        public static readonly Color Aquamarine = new(0xffd4ff7f);
        public static readonly Color Azure = new(0xfffffff0);
        public static readonly Color Beige = new(0xffdcf5f5);
        public static readonly Color Bisque = new(0xffc4e4ff);
        public static readonly Color Black = new(0xff000000);
        public static readonly Color BlanchedAlmond = new(0xffcdebff);
        public static readonly Color Blue = new(0xffff0000);
        public static readonly Color BlueViolet = new(0xffe22b8a);
        public static readonly Color Brown = new(0xff2a2aa5);
        public static readonly Color BurlyWood = new(0xff87b8de);
        public static readonly Color CadetBlue = new(0xffa09e5f);
        public static readonly Color Chartreuse = new(0xff00ff7f);
        public static readonly Color Chocolate = new(0xff1e69d2);
        public static readonly Color Coral = new(0xff507fff);
        public static readonly Color CornflowerBlue = new(0xffed9564);
        public static readonly Color Cornsilk = new(0xffdcf8ff);
        public static readonly Color Crimson = new(0xff3c14dc);
        public static readonly Color Cyan = new(0xffffff00);
        public static readonly Color DarkBlue = new(0xff8b0000);
        public static readonly Color DarkCyan = new(0xff8b8b00);
        public static readonly Color DarkGoldenrod = new(0xff0b86b8);
        public static readonly Color DarkGray = new(0xffa9a9a9);
        public static readonly Color DarkGreen = new(0xff006400);
        public static readonly Color DarkKhaki = new(0xff6bb7bd);
        public static readonly Color DarkMagenta = new(0xff8b008b);
        public static readonly Color DarkOliveGreen = new(0xff2f6b55);
        public static readonly Color DarkOrange = new(0xff008cff);
        public static readonly Color DarkOrchid = new(0xffcc3299);
        public static readonly Color DarkRed = new(0xff00008b);
        public static readonly Color DarkSalmon = new(0xff7a96e9);
        public static readonly Color DarkSeaGreen = new(0xff8bbc8f);
        public static readonly Color DarkSlateBlue = new(0xff8b3d48);
        public static readonly Color DarkSlateGray = new(0xff4f4f2f);
        public static readonly Color DarkTurquoise = new(0xffd1ce00);
        public static readonly Color DarkViolet = new(0xffd30094);
        public static readonly Color DeepPink = new(0xff9314ff);
        public static readonly Color DeepSkyBlue = new(0xffffbf00);
        public static readonly Color DimGray = new(0xff696969);
        public static readonly Color DodgerBlue = new(0xffff901e);
        public static readonly Color Firebrick = new(0xff2222b2);
        public static readonly Color FloralWhite = new(0xfff0faff);
        public static readonly Color ForestGreen = new(0xff228b22);
        public static readonly Color Fuchsia = new(0xffff00ff);
        public static readonly Color Gainsboro = new(0xffdcdcdc);
        public static readonly Color GhostWhite = new(0xfffff8f8);
        public static readonly Color Gold = new(0xff00d7ff);
        public static readonly Color Goldenrod = new(0xff20a5da);
        public static readonly Color Gray = new(0xff808080);
        public static readonly Color Green = new(0xff00ff00);
        public static readonly Color GreenYellow = new(0xff2fffad);
        public static readonly Color Honeydew = new(0xfff0fff0);
        public static readonly Color HotPink = new(0xffb469ff);
        public static readonly Color IndianRed = new(0xff5c5ccd);
        public static readonly Color Indigo = new(0xff82004b);
        public static readonly Color Ivory = new(0xfff0ffff);
        public static readonly Color Khaki = new(0xff8ce6f0);
        public static readonly Color Lavender = new(0xfffae6e6);
        public static readonly Color LavenderBlush = new(0xfff5f0ff);
        public static readonly Color LawnGreen = new(0xff00fc7c);
        public static readonly Color LemonChiffon = new(0xffcdfaff);
        public static readonly Color LightBlue = new(0xffe6d8ad);
        public static readonly Color LightCoral = new(0xff8080f0);
        public static readonly Color LightCyan = new(0xffffffe0);
        public static readonly Color LightGoldenrodYellow = new(0xffd2fafa);
        public static readonly Color LightGray = new(0xffd3d3d3);
        public static readonly Color LightGreen = new(0xff90ee90);
        public static readonly Color LightPink = new(0xffc1b6ff);
        public static readonly Color LightSalmon = new(0xff7aa0ff);
        public static readonly Color LightSeaGreen = new(0xffaab220);
        public static readonly Color LightSkyBlue = new(0xffface87);
        public static readonly Color LightSlateGray = new(0xff998877);
        public static readonly Color LightSteelBlue = new(0xffdec4b0);
        public static readonly Color LightYellow = new(0xffe0ffff);
        public static readonly Color Lime = new(0xff00ff00);
        public static readonly Color LimeGreen = new(0xff32cd32);
        public static readonly Color Linen = new(0xffe6f0fa);
        public static readonly Color Magenta = new(0xffff00ff);
        public static readonly Color Maroon = new(0xff000080);
        public static readonly Color MediumAquamarine = new(0xffaacd66);
        public static readonly Color MediumBlue = new(0xffcd0000);
        public static readonly Color MediumOrchid = new(0xffd355ba);
        public static readonly Color MediumPurple = new(0xffdb7093);
        public static readonly Color MediumSeaGreen = new(0xff71b33c);
        public static readonly Color MediumSlateBlue = new(0xffee687b);
        public static readonly Color MediumSpringGreen = new(0xff9afa00);
        public static readonly Color MediumTurquoise = new(0xffccd148);
        public static readonly Color MediumVioletRed = new(0xff8515c7);
        public static readonly Color MidnightBlue = new(0xff701919);
        public static readonly Color MintCream = new(0xfffafff5);
        public static readonly Color MistyRose = new(0xffe1e4ff);
        public static readonly Color Moccasin = new(0xffb5e4ff);
        public static readonly Color MonoGameOrange = new(0xff003ce7);
        public static readonly Color NavajoWhite = new(0xffaddeff);
        public static readonly Color Navy = new(0xff800000);
        public static readonly Color OldLace = new(0xffe6f5fd);
        public static readonly Color Olive = new(0xff008080);
        public static readonly Color OliveDrab = new(0xff238e6b);
        public static readonly Color Orange = new(0xff00a5ff);
        public static readonly Color OrangeRed = new(0xff0045ff);
        public static readonly Color Orchid = new(0xffd670da);
        public static readonly Color PaleGoldenrod = new(0xffaae8ee);
        public static readonly Color PaleGreen = new(0xff98fb98);
        public static readonly Color PaleTurquoise = new(0xffeeeeaf);
        public static readonly Color PaleVioletRed = new(0xff9370db);
        public static readonly Color PapayaWhip = new(0xffd5efff);
        public static readonly Color PeachPuff = new(0xffb9daff);
        public static readonly Color Peru = new(0xff3f85cd);
        public static readonly Color Pink = new(0xffcbc0ff);
        public static readonly Color Plum = new(0xffdda0dd);
        public static readonly Color PowderBlue = new(0xffe6e0b0);
        public static readonly Color Purple = new(0xff800080);
        public static readonly Color Red = new(0xff0000ff);
        public static readonly Color RosyBrown = new(0xff8f8fbc);
        public static readonly Color RoyalBlue = new(0xffe16941);
        public static readonly Color SaddleBrown = new(0xff13458b);
        public static readonly Color Salmon = new(0xff7280fa);
        public static readonly Color SandyBrown = new(0xff60a4f4);
        public static readonly Color SeaGreen = new(0xff578b2e);
        public static readonly Color SeaShell = new(0xffeef5ff);
        public static readonly Color Sienna = new(0xff2d52a0);
        public static readonly Color Silver = new(0xffc0c0c0);
        public static readonly Color SkyBlue = new(0xffebce87);
        public static readonly Color SlateBlue = new(0xffcd5a6a);
        public static readonly Color SlateGray = new(0xff908070);
        public static readonly Color Snow = new(0xfffafaff);
        public static readonly Color SpringGreen = new(0xff7fff00);
        public static readonly Color SteelBlue = new(0xffb48246);
        public static readonly Color Tan = new(0xff8cb4d2);
        public static readonly Color Teal = new(0xff808000);
        public static readonly Color Thistle = new(0xffd8bfd8);
        public static readonly Color Tomato = new(0xff4763ff);
        public static readonly Color Turquoise = new(0xffd0e040);
        public static readonly Color Violet = new(0xffee82ee);
        public static readonly Color Wheat = new(0xffb3def5);
        public static readonly Color White = new(0xffffffff);
        public static readonly Color WhiteSmoke = new(0xfff5f5f5);
        public static readonly Color Yellow = new(0xff00ffff);
        public static readonly Color YellowGreen = new(0xff32cd9a);

        // Stored as RGBA with R in the least significant octet:
        // |-------|-------|-------|-------
        // A       B       G       R
        private readonly uint _abgr;

        public Color(uint abgr)
        {
            _abgr = abgr;
        }

        public Color(Color color, int alpha)
        {
            if ((alpha & 0xFFFFFF00) != 0)
            {
                var clampedA = (uint) Calc.Clamp(alpha, Byte.MinValue, Byte.MaxValue);

                _abgr = (color._abgr & 0x00FFFFFF) | (clampedA << 24);
            }
            else
            {
                _abgr = (color._abgr & 0x00FFFFFF) | ((uint)alpha << 24);
            }
        }

        public Color(Color color, float alpha) :
            this(color, (int)(alpha * 255))
        {
        }

        public Color(float r, float g, float b)
            : this((int)(r * 255), (int)(g * 255), (int)(b * 255))
        {
        }

        public Color(float r, float g, float b, float alpha)
            : this((int)(r * 255), (int)(g * 255), (int)(b * 255), (int)(alpha * 255))
        {
        }

        public Color(int r, int g, int b)
        {
            _abgr = 0xFF000000; // A = 255

            if (((r | g | b) & 0xFFFFFF00) != 0)
            {
                var clampedR = (uint)Calc.Clamp(r, Byte.MinValue, Byte.MaxValue);
                var clampedG = (uint)Calc.Clamp(g, Byte.MinValue, Byte.MaxValue);
                var clampedB = (uint)Calc.Clamp(b, Byte.MinValue, Byte.MaxValue);

                _abgr |= (clampedB << 16) | (clampedG << 8) | clampedR;
            }
            else
            {
                _abgr |= ((uint)b << 16) | ((uint)g << 8) | (uint)r;
            }
        }

        public Color(int r, int g, int b, int alpha)
        {
            if (((r | g | b | alpha) & 0xFFFFFF00) != 0)
            {
                var clampedR = (uint)Calc.Clamp(r, Byte.MinValue, Byte.MaxValue);
                var clampedG = (uint)Calc.Clamp(g, Byte.MinValue, Byte.MaxValue);
                var clampedB = (uint)Calc.Clamp(b, Byte.MinValue, Byte.MaxValue);
                var clampedA = (uint)Calc.Clamp(alpha, Byte.MinValue, Byte.MaxValue);

                _abgr = (clampedA << 24) | (clampedB << 16) | (clampedG << 8) | (clampedR);
            }
            else
            {
                _abgr = ((uint)alpha << 24) | ((uint)b << 16) | ((uint)g << 8) | (uint)r;
            }
        }

        public Color(byte r, byte g, byte b, byte alpha)
        {
            _abgr = ((uint)alpha << 24) | ((uint)b << 16) | ((uint)g << 8) | (r);
        }

        public byte A
        {
            get
            {
                unchecked
                {
                    return (byte)(this._abgr >> 24);
                }
            }
        }

        public byte B
        {
            get
            {
                unchecked
                {
                    return (byte)(this._abgr >> 16);
                }
            }
        }

        public byte G
        {
            get
            {
                unchecked
                {
                    return (byte)(this._abgr >> 8);
                }
            }
        }

        public byte R
        {
            get
            {
                unchecked
                {
                    return (byte)this._abgr;
                }
            }
        }

        public float Af => (this._abgr >> 24) / 255f;

        public float Bf => (byte)(this._abgr >> 16) / 255f;

        public float Gf => (byte)(this._abgr >> 8) / 255f;

        public float Rf => (byte)this._abgr / 255f;

        public static bool operator ==(Color a, Color b)
        {
            return a._abgr == b._abgr;
        }

        public static bool operator !=(Color a, Color b)
        {
            return a._abgr != b._abgr;
        }

        public override int GetHashCode()
        {
            return this._abgr.GetHashCode();
        }

        public bool Equals(Color obj)
        {
            return this._abgr == obj._abgr;
        }

        public override bool Equals(object obj)
        {
            return obj is Color color && this.Equals(color);
        }

        public static Color Lerp(Color value1, Color value2, float amount)
        {
            amount = Calc.Normalize(amount);
            return new Color(
                (int)Calc.Lerp(value1.R, value2.R, amount),
                (int)Calc.Lerp(value1.G, value2.G, amount),
                (int)Calc.Lerp(value1.B, value2.B, amount),
                (int)Calc.Lerp(value1.A, value2.A, amount));
        }

        public void CopyTo(out Color c)
        {
            c = new Color(_abgr);
        }

        public static Color Multiply(Color value, float scale)
        {
            return new Color((int)(value.Rf * scale), (int)(value.Gf * scale), (int)(value.Bf * scale), (int)(value.Af * scale));
        }

        public static Color operator *(Color value, float scale)
        {
            return new Color((int)(value.Rf * scale), (int)(value.Gf * scale), (int)(value.Bf * scale), (int)(value.Af * scale));
        }

        public static Color operator +(Color value1, Color value2)
        {
            return new Color(value1.Rf + value2.Rf, value1.Gf + value2.Gf, value1.Bf + value2.Bf, value1.Af + value2.Af);
        }

        public static Color operator -(Color value1, Color value2)
        {
            return new Color(value1.Rf - value2.Rf, value1.Gf - value2.Gf, value1.Bf - value2.Bf, value1.Af - value2.Af);
        }

        public static implicit operator uint(Color color)
        {
            return color._abgr;
        }

        public static implicit operator Color(uint value)
        {
            return new Color(value);
        }

        public static implicit operator Vector4(Color color)
        {
            return new Vector4(color.Rf, color.Gf, color.Bf, color.Af);
        }

        public static implicit operator Color(Vector4 vec)
        {
            return new Color(vec.X, vec.Y, vec.Z, vec.W);
        }

        public void Deconstruct(out float r, out float g, out float b)
        {
            r = R;
            g = G;
            b = B;
        }

        public void Deconstruct(out float r, out float g, out float b, out float a)
        {
            r = R;
            g = G;
            b = B;
            a = A;
        }

        public Color WithOpacity(float opacity)
        {
            opacity = Calc.Normalize(opacity);

            return new Color(this, opacity);
        }

        public override string ToString()
        {
            var sb = new StringBuilder(25);
            sb.Append("{R:");
            sb.Append(R);
            sb.Append(" G:");
            sb.Append(G);
            sb.Append(" B:");
            sb.Append(B);
            sb.Append(" A:");
            sb.Append(A);
            sb.Append('}');
            return sb.ToString();
        }

        public uint Abgr => _abgr;

        public uint Rgba => (uint)((R << 24) | (G << 16) | (B << 8) | A);

        public int CompareTo(Color other)
        {
            return _abgr.CompareTo(other._abgr);
        }

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Color));

        public static Color FromHex(string hex)
        {
            hex = ToRgbaHex(hex);

            if (hex is null || !uint.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint packed_value))
            {
                throw new ArgumentException("Hexadecimal string is not in the correct format.", nameof(hex));
            }

            var a = (byte)(packed_value);
            var b = (byte)(packed_value >> 8);
            var g = (byte)(packed_value >> 16);
            var r = (byte)(packed_value >> 24);

            var result = new Color(r, g, b, a);

            return result;
        }

        private static string ToRgbaHex(string hex)
        {
            if (hex[0] == '#')
            {
                hex = hex[1..];
            }

            if (hex.Length == 8)
            {
                return hex;
            }

            if (hex.Length == 6)
            {
                return hex + "FF";
            }

            if (hex.Length < 3 || hex.Length > 4)
            {
                return null;
            }

            char r = hex[0];
            char g = hex[1];
            char b = hex[2];
            char a = hex.Length == 3 ? 'F' : hex[3];

            return new string(new[] { r, r, g, g, b, b, a, a });
        }
    }
}
