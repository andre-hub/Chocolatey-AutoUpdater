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
            var proc = new Process();
            proc.StartInfo = FillProcessStartInfo(GetArgments(), proc.StartInfo, _config.ChocolateyPath);
            try
            {
                logger.Debug("Starting");
                proc.Start();
                logger.Debug("Finish");
            }
            catch (Exception ex)
            {
                logger.Error("", ex.ToString());
            }
        }

        private ProcessStartInfo FillProcessStartInfo(string args, ProcessStartInfo startInfo, string chocolateyPath)
        {
            startInfo.FileName = chocolateyPath;
            startInfo.Arguments = args;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            return startInfo;
        }

        private string GetArgments()
        {
            if (string.IsNullOrWhiteSpace(_config.ProxyUrl))
            {
                var proxyCommand = $@"--proxy=""{_config.ProxyUrl}""";
                return String.Format(" {0} upgrade all -y ", proxyCommand);
            }
            else
            {
                return String.Format(" upgrade all -y ");
            }
        }
    }
}