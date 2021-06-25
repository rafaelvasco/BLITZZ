namespace BLITZZ.Audio
{
    public class AudioDeviceEventArgs
    {
        public AudioDevice Device { get; }

        internal AudioDeviceEventArgs(AudioDevice device)
        {
            Device = device;
        }
    }
}