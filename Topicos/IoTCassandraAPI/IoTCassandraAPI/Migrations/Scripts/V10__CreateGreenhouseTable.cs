using IoTCassandraAPI.Migrations.Resources.DTO;
using IoTCassandraAPI.Migrations.Resources.Interface;
using IoTCassandraAPI.Migrations.Resources.Tools;

namespace IoTCassandraAPI.Migrations.Scripts
{
    public class CreateGreenhouseTable : BaseMigration, IMigrationPart
    {
        public MigrationRegister GiveMigrationScript()
        {
            try
            {
                string query = @"CREATE TABLE IF NOT EXISTS greenhouse_list (
                    greenhouse TEXT,
                    id TEXT,
                    PRIMARY KEY(id)
                );

                CREATE INDEX IF NOT EXISTS idx_greenhouse_list_greenhouse ON greenhouse_list (greenhouse);";

                return CreateMigrationRegister<CreateGreenhouseTable>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
