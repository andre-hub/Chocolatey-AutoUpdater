using Chocolatey.AutoUpdater.NTService;
using System.ServiceProcess;

namespace Chocolatey.AutoUpdater
{
    public partial class NtService : ServiceBase
    {
        private TimeScheduler timeScheduler;

        public NtService()
        {
            InitializeComponent();
        }

        public NtService(TimeScheduler timeScheduler)
        {
            this.timeScheduler = timeScheduler;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timeScheduler.Start();
        }

        protected override void OnStop()
        {
            timeScheduler.Stop();
        }
    }
}
