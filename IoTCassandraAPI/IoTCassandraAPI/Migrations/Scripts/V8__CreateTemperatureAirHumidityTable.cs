using IoTCassandraAPI.Migrations.Resources.DTO;
using IoTCassandraAPI.Migrations.Resources.Interface;
using IoTCassandraAPI.Migrations.Resources.Tools;

namespace IoTCassandraAPI.Migrations.Scripts
{
    public class CreateTemperatureAirHumidityTable : BaseMigration, IMigrationPart
    {
        public MigrationRegister GiveMigrationScript()
        {
            try
            {
                string query = @"CREATE TABLE IF NOT EXISTS temperature_air_humidity (
                            greenhouse TEXT,
                            id TEXT,
                            air_value DOUBLE,
                            temperature_value DOUBLE,
                            register_date TIMESTAMP,
                            PRIMARY KEY(id)
                        );

                        CREATE INDEX IF NOT EXISTS idx_temperature_air_humidity_greenhouse ON temperature_air_humidity (greenhouse);
                        CREATE INDEX IF NOT EXISTS idx_temperature_air_humidity_date ON temperature_air_humidity (register_date);";

                return CreateMigrationRegister<CreateTemperatureAirHumidityTable>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
