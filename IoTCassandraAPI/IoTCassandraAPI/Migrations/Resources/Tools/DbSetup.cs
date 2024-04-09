using Cassandra;
using IoTCassandraAPI.Migrations.Resources.DTO;
using ISession = Cassandra.ISession;

namespace IoTCassandraAPI.Migrations.Resources.Tools
{
    public class DbSetup
    {
        public static void InicializeDB(IConfiguration configuration, string keyspace, string clusterIP)
        {
            try
            {
                Cluster cluster = Cluster.Builder().AddContactPoint(clusterIP).Build();
                bool firstStart = false;                              
                ISession session;

                try
                {
                    session = cluster.Connect(keyspace);
                }
                catch (Exception)
                {
                    string keyspaceConfig = GetReplicationConfig(configuration);
                    CreateKeySpace(cluster, keyspace, keyspaceConfig);

                    cluster.RefreshSchema();
                    session = cluster.Connect(keyspace);
                    firstStart = true;
                }

                if (firstStart)
                    MigrationHelper.CreateMigrationTable(session);

                List<MigrationRegister> migrations = MigrationHelper.GetMigrations(session, keyspace);
                
                foreach (MigrationRegister migration in migrations)
                {
                    MigrationHelper.ExecuteMigration(session, migration.Migration);
                    MigrationHelper.RegisterMigration(session, migration, keyspace);
                }

                session.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GetReplicationConfig(IConfiguration configuration)
        {
            try
            {
                var replicationSettings = configuration.GetSection("Settings:Cassandra:Replication");
                //{ 'class' : 'SimpleStrategy', 'replication_factor' : '1' }

                var config = "{";
                foreach (var (key, s) in replicationSettings.AsEnumerable())
                {
                    if (s != null)
                    {
                        string finalKey = key.Split(':')[^1];
                        config += $" '{finalKey}' : '{s}',";

                    }
                }

                config = config[..^1];
                config += " }";

                return config;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void CreateKeySpace(Cluster cluster, string keyspace, string replicationSettings)
        {
            try
            {
                var session = cluster.Connect();
                string query = $"CREATE KEYSPACE IF NOT EXISTS " + keyspace + " WITH REPLICATION = " + replicationSettings + ";";
                session.Execute(query);
                session.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
