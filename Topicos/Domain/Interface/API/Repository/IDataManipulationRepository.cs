using Cassandra;
using IoTCassandraAPI.Domain.Entity;

namespace Domain.Interface.API.Repository
{
    public interface IDataManipulationRepository
    {
        Row FindById(string table, string id, string greenhouse);
        void Register<T>(IoTData<T> data, string table);
    }
}