using Cassandra;
using Domain.Interface.API.Repository;
using IoTCassandraAPI.Application.Util;
using IoTCassandraAPI.Domain.Entity;
using ISession = Cassandra.ISession;

namespace IoTCassandraAPI.Repository
{
    public class IoTDataManipulationRepository : IDataManipulationRepository
    {
        private readonly ISession _session;

        public IoTDataManipulationRepository(ISession session)
        {
            _session = session;
        }

        public void Register<T>(IoTData<T> data, string table)
        {
            try
            {
                string query = QueryHelper.GetInsertQuery(data, table);
                PreparedStatement statement = _session.Prepare(query);
                _session.Execute(statement.Bind());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Row FindById(string table, string id, string greenhouse)
        {
            try
            {
                string query = @$"SELECT *
                                  FROM {table}
                                  WHERE greenhouse = '{greenhouse}'
                                  AND id = '{id}'";

                PreparedStatement statement = _session.Prepare(query);
                Row row = _session.Execute(statement.Bind()).FirstOrDefault();

                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
