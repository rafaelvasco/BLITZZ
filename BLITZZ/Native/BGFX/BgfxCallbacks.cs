using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BLITZZ.Native.BGFX
{

    public interface ICallbackHandler
    {
        void ReportError(string fileName, int line, Fatal errorType, string message);

        void ReportDebug(string fileName, int line, string format, IntPtr args);

        void ProfilerBegin(string name, int color, string filePath, int line);
        
        void ProfilerEnd();

        int GetCachedSize(long id);

        bool GetCacheEntry(long id, IntPtr data, int size);

        void SetCacheEntry(long id, IntPtr data, int size);

        void SaveScreenShot(string path, int width, int height, int pitch, IntPtr data, int size, bool flipVertical);

        void CaptureStarted(int width, int height, int pitch, TextureFormat format, bool flipVertical);

        void CaptureFinished();

        void CaptureFrame(IntPtr data, int size);
    }

    unsafe struct CallbackShim
    {
        IntPtr vtbl;
        IntPtr reportError;
        IntPtr reportDebug;
        IntPtr profilerBegin;
        IntPtr profilerBeginLiteral;
        IntPtr profilerEnd;
        IntPtr getCachedSize;
        IntPtr getCacheEntry;
        IntPtr setCacheEntry;
        IntPtr saveScreenShot;
        IntPtr captureStarted;
        IntPtr captureFinished;
        IntPtr captureFrame;

        public static unsafe IntPtr CreateShim(ICallbackHandler handler)
        {
            if (handler == null)
                return IntPtr.Zero;

            if (savedDelegates != null)
                throw new InvalidOperationException("Callbacks should only be initialized once; bgfx can only deal with one set at a time.");

            var memory = Marshal.AllocHGlobal(Marshal.SizeOf<CallbackShim>());
            var shim = (CallbackShim*)memory;
            var saver = new DelegateSaver(handler, shim);

            // the shim uses the unnecessary ctor slot to act as a vtbl pointer to itself,
            // so that the same block of memory can act as both bgfx_callback_interface_t and bgfx_callback_vtbl_t
            shim->vtbl = memory + IntPtr.Size;

            // cache the data so we can free it later
            shimMemory = memory;
            savedDelegates = saver;

            return memory;
        }

        public static void FreeShim()
        {
            if (savedDelegates == null)
                return;

            savedDelegates = null;
            Marshal.FreeHGlobal(shimMemory);
        }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        delegate void ReportErrorHandler(IntPtr thisPtr, string fileName, ushort line, Fatal fatal, string message);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        delegate void ReportDebugHandler(IntPtr thisPtr, string fileName, ushort line, string format, IntPtr args);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        delegate void ProfilerBeginHandler(IntPtr thisPtr, sbyte* name, int abgr, sbyte* filePath, ushort line);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void ProfilerEndHandler(IntPtr thisPtr);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate int GetCachedSizeHandler(IntPtr thisPtr, long id);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate bool GetCacheEntryHandler(IntPtr thisPtr, long id, IntPtr data, int size);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void SetCacheEntryHandler(IntPtr thisPtr, long id, IntPtr data, int size);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        delegate void SaveScreenShotHandler(IntPtr thisPtr, string path, int width, int height, int pitch, IntPtr data, int size, [MarshalAs(UnmanagedType.U1)] bool flipVertical);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void CaptureStartedHandler(IntPtr thisPtr, int width, int height, int pitch, TextureFormat format, [MarshalAs(UnmanagedType.U1)] bool flipVertical);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void CaptureFinishedHandler(IntPtr thisPtr);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void CaptureFrameHandler(IntPtr thisPtr, IntPtr data, int size);

        // We're creating delegates to a user's interface methods; we're then converting those delegates
        // to native pointers and passing them into native code. If we don't save the references to the
        // delegates in managed land somewhere, the GC will think they're unreferenced and clean them
        // up, leaving native holding a bag of pointers into nowhere land.
        class DelegateSaver
        {
            ICallbackHandler handler;
            ReportErrorHandler reportError;
            ReportDebugHandler reportDebug;
            ProfilerBeginHandler profilerBegin;
            ProfilerBeginHandler profilerBeginLiteral;
            ProfilerEndHandler profilerEnd;
            GetCachedSizeHandler getCachedSize;
            GetCacheEntryHandler getCacheEntry;
            SetCacheEntryHandler setCacheEntry;
            SaveScreenShotHandler saveScreenShot;
            CaptureStartedHandler captureStarted;
            CaptureFinishedHandler captureFinished;
            CaptureFrameHandler captureFrame;

            public unsafe DelegateSaver(ICallbackHandler handler, CallbackShim* shim)
            {
                this.handler = handler;
                reportError = ReportError;
                reportDebug = ReportDebug;
                profilerBegin = ProfilerBegin;
                profilerBeginLiteral = ProfilerBegin;
                profilerEnd = ProfilerEnd;
                getCachedSize = GetCachedSize;
                getCacheEntry = GetCacheEntry;
                setCacheEntry = SetCacheEntry;
                saveScreenShot = SaveScreenShot;
                captureStarted = CaptureStarted;
                captureFinished = CaptureFinished;
                captureFrame = CaptureFrame;

                shim->reportError = Marshal.GetFunctionPointerForDelegate(reportError);
                shim->reportDebug = Marshal.GetFunctionPointerForDelegate(reportDebug);
                shim->profilerBegin = Marshal.GetFunctionPointerForDelegate(profilerBegin);
                shim->profilerBeginLiteral = Marshal.GetFunctionPointerForDelegate(profilerBeginLiteral);
                shim->profilerEnd = Marshal.GetFunctionPointerForDelegate(profilerEnd);
                shim->getCachedSize = Marshal.GetFunctionPointerForDelegate(getCachedSize);
                shim->getCacheEntry = Marshal.GetFunctionPointerForDelegate(getCacheEntry);
                shim->setCacheEntry = Marshal.GetFunctionPointerForDelegate(setCacheEntry);
                shim->saveScreenShot = Marshal.GetFunctionPointerForDelegate(saveScreenShot);
                shim->captureStarted = Marshal.GetFunctionPointerForDelegate(captureStarted);
                shim->captureFinished = Marshal.GetFunctionPointerForDelegate(captureFinished);
                shim->captureFrame = Marshal.GetFunctionPointerForDelegate(captureFrame);
            }

            void ReportError(IntPtr thisPtr, string fileName, ushort line, Fatal errorType, string message)
            {
                handler.ReportError(fileName, line, errorType, message);
            }

            void ReportDebug(IntPtr thisPtr, string fileName, ushort line, string format, IntPtr args)
            {
                handler.ReportDebug(fileName, line, format, args);
            }

            void ProfilerBegin(IntPtr thisPtr, sbyte* name, int color, sbyte* filePath, ushort line)
            {
                handler.ProfilerBegin(new string(name), color, new string(filePath), line);
            }

            void ProfilerEnd(IntPtr thisPtr)
            {
                handler.ProfilerEnd();
            }

            int GetCachedSize(IntPtr thisPtr, long id)
            {
                return handler.GetCachedSize(id);
            }

            bool GetCacheEntry(IntPtr thisPtr, long id, IntPtr data, int size)
            {
                return handler.GetCacheEntry(id, data, size);
            }

            void SetCacheEntry(IntPtr thisPtr, long id, IntPtr data, int size)
            {
                handler.SetCacheEntry(id, data, size);
            }

            void SaveScreenShot(IntPtr thisPtr, string path, int width, int height, int pitch, IntPtr data, int size, bool flipVertical)
            {
                handler.SaveScreenShot(path, width, height, pitch, data, size, flipVertical);
            }

            void CaptureStarted(IntPtr thisPtr, int width, int height, int pitch, TextureFormat format, bool flipVertical)
            {
                handler.CaptureStarted(width, height, pitch, format, flipVertical);
            }

            void CaptureFinished(IntPtr thisPtr)
            {
                handler.CaptureFinished();
            }

            void CaptureFrame(IntPtr thisPtr, IntPtr data, int size)
            {
                handler.CaptureFrame(data, size);
            }
        }

        static IntPtr shimMemory;
        static DelegateSaver savedDelegates;
    }
}
