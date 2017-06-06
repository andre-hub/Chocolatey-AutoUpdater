using Chocolatey.AutoUpdater.Components;
using System;
using System.Timers;

namespace Chocolatey.AutoUpdater.NTService
{
    public class TimeScheduler
    {
        private readonly Timer timer;
        private readonly AppModel appModel;
        private static bool isRunning;
        private static readonly object lockObject = new object();

        public TimeScheduler(AppModel appModel, double intervalInMilliseconds)
        {
            this.appModel = appModel;
            timer = new Timer(intervalInMilliseconds);
            timer.Elapsed += OnTimerElapsed;
        }

        public void Start()
        {
#if DEBUG
            appModel.Run();
#else
            timer.Enabled = true;
            timer.Start();
#endif
        }

        public void Stop()
        {
            timer.Enabled = false;
            timer.Stop();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (isRunning)
            {
                Console.Write(TimeSchedulerNames.WaitChar);
                return;
            }
            try
            {
                lock (lockObject)
                {
                    Console.Write(TimeSchedulerNames.RunningChar);
                    appModel.Run();
                }
            }
            finally
            {
                lock (lockObject)
                {
                    isRunning = false;
                }
            }
        }
    }

    public static class TimeSchedulerNames
    {
        public const char RunningChar = '#';
        public const char WaitChar = '.';
    }
}