using IoTCassandraAPI.Migrations.Resources.DTO;
using IoTCassandraAPI.Migrations.Resources.Interface;
using IoTCassandraAPI.Migrations.Resources.Tools;

namespace IoTCassandraAPI.Migrations.Scripts
{
    public class CreateTemperatureTable : BaseMigration, IMigrationPart
    {
        public MigrationRegister GiveMigrationScript()
        {
            try
            {
                string query = @"CREATE TABLE IF NOT EXISTS temperature (
                                    greenhouse TEXT,
                                    id TEXT,
                                    value DOUBLE,
                                    register_date TIMESTAMP,
                                    PRIMARY KEY(greenhouse)
                                );
                                
                                CREATE INDEX IF NOT EXISTS idx_temperature_id ON temperature (id);
                                CREATE INDEX IF NOT EXISTS idx_temperature_date ON temperature (register_date);";

                return CreateMigrationRegister<CreateTemperatureTable>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
