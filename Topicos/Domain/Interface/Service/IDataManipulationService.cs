using Cassandra;
using IoTCassandraAPI.Domain.Entity;

namespace IoTCassandraAPI.Domain.Interface.Service
{
    public interface IDataManipulationService
    {
        IoTData<T> FindById<T>(string table, string id, string greenhouse);
        IoTData<T> Register<T>(T value, string greenhouse, string table);
    }
}