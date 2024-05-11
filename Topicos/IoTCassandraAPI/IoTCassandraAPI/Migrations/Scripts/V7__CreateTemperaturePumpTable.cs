using IoTCassandraAPI.Migrations.Resources.DTO;
using IoTCassandraAPI.Migrations.Resources.Interface;
using IoTCassandraAPI.Migrations.Resources.Tools;

namespace IoTCassandraAPI.Migrations.Scripts
{
    public class CreateTemperaturePumpTable : BaseMigration, IMigrationPart
    {
        public MigrationRegister GiveMigrationScript()
        {
            try
            {
                string query = @"CREATE TABLE IF NOT EXISTS temperature_pump (
                                greenhouse TEXT,
                                id TEXT,
                                temperature_value DOUBLE,
                                pump_value BOOLEAN,
                                register_date TIMESTAMP,
                                PRIMARY KEY((register_date), id)
                            );

                        CREATE INDEX IF NOT EXISTS idx_temperature_pump_greenhouse ON temperature_pump (greenhouse);";

                return CreateMigrationRegister<CreateTemperaturePumpTable>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
