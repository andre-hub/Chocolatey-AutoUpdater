using Chocolatey.AutoUpdater.Components;
using Chocolatey.AutoUpdater.NTService;

namespace Chocolatey.AutoUpdater.Configuration
{

    public sealed class ComponentsContainer
    {
        public static ComponentsContainer Current { get; } = new ComponentsContainer();

        public TimeScheduler GetTimeScheduler()
        {
            var config = new Config();
            AppModel appModel = new AppModel(config);
            var timeScheduler = new TimeScheduler(appModel, config.IntervalInMilliseconds);
            return timeScheduler;
        }
    }
}
