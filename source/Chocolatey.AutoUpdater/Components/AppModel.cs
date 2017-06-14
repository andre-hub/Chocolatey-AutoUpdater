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
            logger.Debug("Starting");
            var proc = new Process();
            proc.StartInfo = FillProcessStartInfo(GetArgments(), proc.StartInfo, _config.ChocolateyPath);
            try
            {
                proc.Start();
                proc.BeginErrorReadLine();
                proc.ErrorDataReceived += ErrorDataReceived;
                logger.Debug("Wait for Run");
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                logger.Error("", ex.ToString());
            }
            logger.Debug("Finish");
        }

        private void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
                logger.Error("Error from chocolatey: ", e.Data);
        }

        private ProcessStartInfo FillProcessStartInfo(string args, ProcessStartInfo startInfo, string chocolateyPath)
        {
            startInfo.FileName = chocolateyPath;
            startInfo.Arguments = args;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardOutput = false;
            startInfo.RedirectStandardError = true;
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