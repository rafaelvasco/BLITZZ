using System.Diagnostics;

namespace BLITZZ.Logging
{
    public class ClassNameDecorator : Decorator
    {
        public override string Decorate(LogLevel logLevel, string input, string originalMessage, Sink sink)
        {
            return new StackTrace()
                .GetFrame(5)
                ?.GetMethod()
                ?.DeclaringType
                ?.Name;
        }
    }
}