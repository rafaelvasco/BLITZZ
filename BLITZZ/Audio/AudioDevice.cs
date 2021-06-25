using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ.Audio
{
    public class AudioDevice
    {
        public int Index { get; }
        public bool IsCapture { get; }

        public string Name => SDL_GetAudioDeviceName(Index, IsCapture);

        internal AudioDevice(int index, bool isCapture)
        {
            Index = index;
            IsCapture = isCapture;
        }
    }
}