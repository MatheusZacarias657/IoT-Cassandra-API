using Cassandra;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Application.Factory
{
    internal class ConfigurationFactory
    {
        private static IConfiguration _configuration;

        internal static void SetConfiguration()
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, true).AddEnvironmentVariables();
            _configuration = configBuilder.Build();
        }

        internal static String GetValue(string key)
        {
            return _configuration[key];
        }

        internal static double GetInterval(string field)
        {
            string timeText = GetValue(field);
            TimeSpan time = TimeSpan.Parse(timeText);

            return Convert.ToDouble(time.Seconds);
        }
    }
}
