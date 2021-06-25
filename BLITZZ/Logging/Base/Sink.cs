namespace BLITZZ.Logging
{
    public abstract class Sink
    {
        public bool Active { get; set; } = true;

        public abstract void Write(LogLevel logLevel, string message, params object[] args);
    }
}