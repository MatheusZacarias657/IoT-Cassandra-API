using ETL.Application.Processing;
using ETL.Application.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra.Geometry;

namespace ETL
{
    internal static class Scheduller
    {

        internal static void ScheduleNextExecution(double timeElapsed, DateTime reference)
        {
            try
            {
                DateTime scheduledTime = reference.AddSeconds(timeElapsed);
                double tickTime = (double)(scheduledTime - reference).TotalMilliseconds;
                System.Timers.Timer timer = new(tickTime);
                timer.Elapsed += (sender, e) => ExecuteETL();
                timer.Start();

                Console.WriteLine($"Schedule Next execution to {scheduledTime}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void ExecuteETL()
        {
            using (Executor executor = new())
            {
                executor.ExecuteExtraction();
            };

            ScheduleNextExecution(ConfigurationFactory.GetInterval("ElapsedTimeEtl"), DateTime.Now);
        }
    }
}
