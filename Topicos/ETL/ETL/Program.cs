using ETL.Application.Processing;
using ETL.Application.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ETL
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ConfigurationFactory.SetConfiguration();

            using (Executor executor = new ())
            {
                executor.ExecuteExtraction();
            };

            bool selfMaanged = Convert.ToBoolean(ConfigurationFactory.GetValue("IsOwnScheduler"));

            if(selfMaanged)
            {
                double interval = ConfigurationFactory.GetInterval("ElapsedTimeEtl");
                interval += ConfigurationFactory.GetInterval("ElapsedDelayEtl");
                Scheduller.ScheduleNextExecution(interval, DateTime.Now);

                var hostBuilder = new HostBuilder().ConfigureServices((hostContext, services) =>
                {

                });

                await hostBuilder.RunConsoleAsync();
            }
        }
    }
}
