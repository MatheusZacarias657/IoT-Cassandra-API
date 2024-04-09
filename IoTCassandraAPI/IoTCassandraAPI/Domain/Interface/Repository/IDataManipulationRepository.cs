using Cassandra;
using IoTCassandraAPI.Domain.Entity;

namespace IoTCassandraAPI.Domain.Interface.Repository
{
    public interface IDataManipulationRepository
    {
        Row FindById(string table, string id, string greenhouse);
        void Register<T>(IoTData<T> data, string table);
    }
}