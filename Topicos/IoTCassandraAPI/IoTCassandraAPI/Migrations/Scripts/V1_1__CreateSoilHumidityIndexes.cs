using IoTCassandraAPI.Migrations.Resources.DTO;
using IoTCassandraAPI.Migrations.Resources.Interface;
using IoTCassandraAPI.Migrations.Resources.Tools;
using System.CodeDom;

namespace IoTCassandraAPI.Migrations.Scripts
{
    public class CreateSoilHumidityIndexes : BaseMigration, IMigrationPart
    {
        public MigrationRegister GiveMigrationScript()
        {
            try
            {
                string query = @"CREATE INDEX IF NOT EXISTS idx_soil_greenhouse ON soil_humidity (greenhouse);
                                 CREATE INDEX IF NOT EXISTS idx_soil_id ON soil_humidity (id);";

                return CreateMigrationRegister<CreateSoilHumidityIndexes>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
