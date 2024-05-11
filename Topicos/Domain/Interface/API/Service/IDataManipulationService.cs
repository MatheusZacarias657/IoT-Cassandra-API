using Cassandra;
using IoTCassandraAPI.Domain.Entity;

namespace Domain.Interface.API.Service
{
    public interface IDataManipulationService
    {
        IoTData<T> FindById<T>(string table, string id, string greenhouse);
        IoTData<T> Register<T>(T value, string greenhouse, string table);
    }
}