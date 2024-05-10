using Cassandra;
using IoTCassandraAPI.Domain.Entity;
using IoTCassandraAPI.Domain.Interface.Repository;
using IoTCassandraAPI.Domain.Interface.Service;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace IoTCassandraAPI.Application.Service
{
    public class IoTDataManipulationService : IDataManipulationService
    {
        private readonly IDataManipulationRepository _dataManipulation;

        public IoTDataManipulationService(IDataManipulationRepository dataManipulation)
        {
            _dataManipulation = dataManipulation;
        }

        public IoTData<T> Register<T>(T value, string greenhouse, string table)
        {
            try
            {
                IoTData<T> data = new ()
                {
                    Greenhouse = greenhouse,
                    Id = Guid.NewGuid().ToString(),
                    Value = value,
                    RegisterDate = DateTime.Now
                };

                _dataManipulation.Register(data, table);

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IoTData<T> FindById<T>(string table, string id, string greenhouse)
        {
            try
            {
                Row row = _dataManipulation.FindById(table, id, greenhouse);


                return new IoTData<T>()
                {
                    Greenhouse = row["greenhouse"].ToString(),
                    Id = row["id"].ToString(),
                    Value = (T) row["value"],
                    RegisterDate = DateTime.Parse(row["register_date"].ToString())
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
