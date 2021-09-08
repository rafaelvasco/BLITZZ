namespace BLITZZ.Logging
{
    public class LogLevelDecorator : Decorator
    {
        public override string Decorate(LogLevel logLevel, string input, string originalMessage, Sink sink)
        {
            return logLevel switch
            {
                LogLevel.Info => EncodeAnsiIfConsole("INF", sink, 50, 255, 50),
                LogLevel.Warning => EncodeAnsiIfConsole("WRN", sink, 255, 255, 0),
                LogLevel.Error => EncodeAnsiIfConsole("ERR", sink, 255, 0, 0),
                LogLevel.Debug => EncodeAnsiIfConsole("DBG", sink, 255, 0, 255),
                LogLevel.Exception => EncodeAnsiIfConsole("EXC", sink, 0, 255, 255),

                _ => "???"
            };
        }

        private static string EncodeAnsiIfConsole(string s, Sink sink, byte r, byte g, byte b)
        {
            return sink is ConsoleSink ? s.AnsiColorEncodeRGB(r, g, b) : s;
        }
    }
}