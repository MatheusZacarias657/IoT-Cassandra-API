using IoTCassandraAPI.Migrations.Resources.DTO;
using IoTCassandraAPI.Migrations.Resources.Interface;
using IoTCassandraAPI.Migrations.Resources.Tools;

namespace IoTCassandraAPI.Migrations.Scripts
{
    public class CreatePumpTable : BaseMigration, IMigrationPart
    {
        public MigrationRegister GiveMigrationScript()
        {
            try
            {
                string query = @"CREATE TABLE IF NOT EXISTS pump (
                                    greenhouse TEXT,
                                    id TEXT,
                                    value BOOLEAN,
                                    register_date TIMESTAMP,
                                    PRIMARY KEY(greenhouse)
                                );
                                
                                CREATE INDEX IF NOT EXISTS idx_pump_id ON pump (id);
                                CREATE INDEX IF NOT EXISTS idx_pump_date ON pump (register_date);";

                return CreateMigrationRegister<CreatePumpTable>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
