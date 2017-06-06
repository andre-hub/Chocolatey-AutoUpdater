using Chocolatey.AutoUpdater.Configuration;
using NLog;
using System;
using System.Diagnostics;

namespace Chocolatey.AutoUpdater.Components
{
    public class AppModel
    {
        private readonly Config _config;
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public AppModel(Config config)
        {
            _config = config;
        }

        public void Run()
        {
            var ars = String.Format(" upgrade all -y ");
            var proc = new Process();
            var startInfo = proc.StartInfo;
            startInfo.FileName = _config.ChocolateyPath;
            startInfo.Arguments = ars;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            try
            {
                logger.Debug("Starting");
                proc.Start();
                logger.Debug("Finish");
            }
            catch (Exception ex)
            {
                logger.ErrorException("", ex);
            }
        }
    }
}