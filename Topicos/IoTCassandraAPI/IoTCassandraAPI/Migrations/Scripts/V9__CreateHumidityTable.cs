using IoTCassandraAPI.Migrations.Resources.DTO;
using IoTCassandraAPI.Migrations.Resources.Interface;
using IoTCassandraAPI.Migrations.Resources.Tools;

namespace IoTCassandraAPI.Migrations.Scripts
{
    public class CreateHumidityTable : BaseMigration, IMigrationPart
    {
        public MigrationRegister GiveMigrationScript()
        {
            try
            {
                string query = @"CREATE TABLE IF NOT EXISTS soil_humidity_air_humidity (
                                    greenhouse TEXT,
                                    id TEXT,
                                    soil_humidity_value DOUBLE,
                                    air_humidity_value DOUBLE,
                                    register_date TIMESTAMP,
                                    PRIMARY KEY((register_date), id)
                                );

                                CREATE INDEX IF NOT EXISTS idx_soil_humidity_air_humidity_greenhouse ON soil_humidity_air_humidity (greenhouse);";

                return CreateMigrationRegister<CreateHumidityTable>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
