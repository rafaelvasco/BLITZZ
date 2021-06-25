using System;

namespace BLITZZ.Threading
{
    internal class ScheduledAction
    {
        public bool Completed { get; set; }
        public Action Action { get; set; }
    }
}
