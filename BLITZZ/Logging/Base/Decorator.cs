﻿namespace BLITZZ.Logging
{
    public abstract class Decorator
    {
        public abstract string Decorate(LogLevel logLevel, string input, string originalMessage, Sink sink);
    }
}