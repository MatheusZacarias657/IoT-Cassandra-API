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
                                    value INT,
                                    register_date TIMESTAMP,
                                    PRIMARY KEY((register_date), id)
                                );
                                
                                CREATE INDEX IF NOT EXISTS idx_pump_greenhouse ON pump (greenhouse);";

                return CreateMigrationRegister<CreatePumpTable>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
