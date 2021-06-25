using System.Reflection;

namespace BLITZZ.Logging
{
    internal class LogInfo
    {
        internal Assembly OwningAssembly { get; set; }
        internal Log Log { get; set; }
    }
}