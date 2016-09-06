using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace SonosWp8
{
    public sealed class TaskScheduler
    {
        private static readonly Queue<Action> Tasks = new Queue<Action>();
        private static Task[] _tasksRunning;
        private static Task _queueTask;

        public TaskScheduler(int simulTasksCount)
        {
            _tasksRunning = new Task[simulTasksCount];
        }

        public void Enqueue(Action action)
        {
            lock (Tasks)
            {
                Tasks.Enqueue(action);
                if (_queueTask == null)
                {
                    _queueTask = Task.Run(() => ProcessQueue());
                }
            }
        }

        public void ClearQueue()
        {
            lock (Tasks)
            {
                Tasks.Clear();
            }
        }

        private static void ProcessQueue()
        {
            while (Tasks.Count > 0)
            {
                for (var i = 0; i < _tasksRunning.Length; i++)
                {
                    if (_tasksRunning[i] != null) continue;

                    Action action;
                    lock (Tasks) action = Tasks.Dequeue();

                    _tasksRunning[i] = Task.Run(action);

                    if (Tasks.Count == 0) break;
                }

                var index = Task.WaitAny(_tasksRunning);

                _tasksRunning[index] = null;
            }
            _queueTask = null;
        }
    }
}