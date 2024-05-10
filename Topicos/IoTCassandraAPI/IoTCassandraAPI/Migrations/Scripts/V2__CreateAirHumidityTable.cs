using IoTCassandraAPI.Migrations.Resources.DTO;
using IoTCassandraAPI.Migrations.Resources.Interface;
using IoTCassandraAPI.Migrations.Resources.Tools;

namespace IoTCassandraAPI.Migrations.Scripts
{
    public class CreateAirHumidityTable : BaseMigration, IMigrationPart
    {
        public MigrationRegister GiveMigrationScript()
        {
            try
            {
                string query = @"CREATE TABLE IF NOT EXISTS air_humidity (
                                    greenhouse TEXT,
                                    id TEXT,
                                    value DOUBLE,
                                    register_date TIMESTAMP,
                                    PRIMARY KEY(id)
                                );
                                
                                CREATE INDEX IF NOT EXISTS idx_air_greenhouse ON air_humidity (greenhouse);
                                CREATE INDEX IF NOT EXISTS idx_air_date ON air_humidity (register_date);";

                return CreateMigrationRegister<CreateAirHumidityTable>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
