using Cassandra;
using IoTCassandraAPI.Application.Service;
using IoTCassandraAPI.Domain.Interface.Repository;
using IoTCassandraAPI.Domain.Interface.Service;
using IoTCassandraAPI.Migrations.Resources.Tools;
using IoTCassandraAPI.Repository;
using System.Diagnostics.Metrics;
using ISession = Cassandra.ISession;

namespace IoTCassandraAPI
{
    public class Inicialize
    {
        public static void InicializeServices(IServiceCollection services, IConfiguration configuration)
        {
            string clusterIP = configuration.GetValue<string>("Settings:Cassandra:IP");
            string keyspace = configuration.GetValue<string>("Settings:Cassandra:KeySpace");
            DbSetup.InicializeDB(configuration, keyspace, clusterIP);

            //Add DB
            services.AddScoped(serviceProvider => {

                Cluster cluster = Cluster.Builder().AddContactPoint(clusterIP).Build();
                return cluster.Connect(keyspace);
            });

            services.AddScoped<IDataManipulationRepository, IoTDataManipulationRepository>();
            services.AddScoped<IDataManipulationService, IoTDataManipulationService>();

            return;
        }
    }
}
