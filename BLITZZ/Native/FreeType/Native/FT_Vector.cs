using System;
using System.Runtime.InteropServices;

namespace FreeTypeSharp.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FT_Vector
    {
        public IntPtr x;
        public IntPtr y;
    }
}
