using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLITZZ.Threading
{
    internal static class Dispatcher
    {
        internal static Queue<ScheduledAction> ActionQueue { get; } = new();

        private static int _count;

        public static bool HasActions => _count > 0;

        public static int MainThreadId { get; internal set; }

        public static bool IsMainThread
            => Thread.CurrentThread.ManagedThreadId == MainThreadId;

        public static ScheduledAction Pop()
        {
            if (_count == 0)
            {
                return null;
            }

            _count--;

            return ActionQueue.Dequeue();
        }

        public static Task RunOnMainThread(Action action)
        {
            var scheduledAction = new ScheduledAction { Action = action };

            ActionQueue.Enqueue(scheduledAction);

            return new Task(async () =>
            {
                while (!scheduledAction.Completed)
                    await Task.Delay(1);
            });
        }
    }
}
