using Chocolatey.AutoUpdater.Configuration;
using System;
using System.ServiceProcess;

namespace Chocolatey.AutoUpdater
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            var container = ComponentsContainer.Current;
            if (Environment.UserInteractive)
            {
                var scheduler = container.GetTimeScheduler();
                Console.WriteLine("Press <enter> to terminate!");
                Console.Write("Service is running ");
                scheduler.Start();
            }
            else
            {
                var ServicesToRun = new ServiceBase[] { new NtService(container.GetTimeScheduler()) };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}