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
                                    PRIMARY KEY((register_date), id)
                                );
                                
                                CREATE INDEX IF NOT EXISTS idx_air_greenhouse ON air_humidity (greenhouse);";

                return CreateMigrationRegister<CreateAirHumidityTable>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
