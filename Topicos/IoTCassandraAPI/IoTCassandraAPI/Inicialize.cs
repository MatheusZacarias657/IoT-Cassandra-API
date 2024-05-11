using Cassandra;
using Domain.Interface.API.Repository;
using Domain.Interface.API.Service;
using IoTCassandraAPI.Application.Service;
using IoTCassandraAPI.Migrations.Resources.Tools;
using IoTCassandraAPI.Repository;

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
