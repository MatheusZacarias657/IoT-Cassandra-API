using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Application.Factory
{
    internal class CassandraSessionFactory
    {
        private static Cluster _cluster;
        private static ISession _session;

        public static ISession GetSession()
        {
            if (_session == null)
            {
                string clusterIP = ConfigurationFactory.GetValue("Cassandra:IP");
                string keyspace = ConfigurationFactory.GetValue("Cassandra:KeySpace");
                _cluster = Cluster.Builder().AddContactPoint(clusterIP).Build();
                _session = _cluster.Connect(keyspace);
            }

            return _session;
        }
    }
}
