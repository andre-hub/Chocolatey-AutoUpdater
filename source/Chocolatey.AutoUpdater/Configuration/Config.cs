using System;

namespace Chocolatey.AutoUpdater.Configuration
{
    public class Config : AppSettingBase<Config>
    {
        public double IntervalInMilliseconds => ParseDouble(x => x.IntervalInMilliseconds);

        public string ChocolateyPath => AppSetting(x => x.ChocolateyPath);

        public string ProxyUrl
        {
            get
            {

                try
                {
                    return AppSetting(x => x.ProxyUrl);
                }
                catch (Exception)
                {
                    return string.Empty;
                }

            }
        }

        public int WaitForExit => ParseInt(x => x.WaitForExit);
    }
}
