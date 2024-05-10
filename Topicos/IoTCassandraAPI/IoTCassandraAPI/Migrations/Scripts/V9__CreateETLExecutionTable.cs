using IoTCassandraAPI.Migrations.Resources.DTO;
using IoTCassandraAPI.Migrations.Resources.Interface;
using IoTCassandraAPI.Migrations.Resources.Tools;

namespace IoTCassandraAPI.Migrations.Scripts
{
    public class CreateETLExecutionTable : BaseMigration, IMigrationPart
    {
        public MigrationRegister GiveMigrationScript()
        {
            try
            {
                string query = @"CREATE TABLE IF NOT EXISTS etl_execution (
                            greenhouse TEXT,
                            id TEXT,
                            related_tables TEXT,
                            last_execution TIMESTAMP,
                            PRIMARY KEY(id)
                        );

                        CREATE INDEX IF NOT EXISTS idx_etl_execution_greenhouse ON etl_execution (greenhouse);";

                return CreateMigrationRegister<CreateETLExecutionTable>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
