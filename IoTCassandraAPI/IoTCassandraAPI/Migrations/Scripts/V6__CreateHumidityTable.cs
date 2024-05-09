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
                string query = @"CREATE TABLE IF NOT EXISTS soil_air_humidity (
                                    greenhouse TEXT,
                                    id TEXT,
                                    air_value DOUBLE,
                                    soil_value DOUBLE,
                                    register_date TIMESTAMP,
                                    PRIMARY KEY(id)
                                );

                                CREATE INDEX IF NOT EXISTS idx_soil_air_humidity_date ON soil_air_humidity (register_date);";

                return CreateMigrationRegister<CreateHumidityTable>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
