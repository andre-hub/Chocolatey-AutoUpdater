namespace Chocolatey.AutoUpdater.Configuration
{
    public class Config : AppSettingBase<Config>
    {
        public double IntervalInMilliseconds => ParseDouble(x => x.IntervalInMilliseconds);

        public string ChocolateyPath => AppSetting(x => x.ChocolateyPath);

        public string ProxyUrl => AppSetting(x => x.ProxyUrl);
    }
}
