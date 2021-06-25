using BLITZZ.Logging;
using BLITZZ.Native.SDL;
using System;

namespace BLITZZ
{
    public static partial class Extensions
    {
        internal static void TraceSDLError(this Log log, string msg, Func<int> func)
        {
            if (func() < 0)
                log.Warning($"{msg}: {SDL2.SDL_GetError()}");
        }
    }
}
