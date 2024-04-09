using IoTCassandraAPI.Migrations.Resources.DTO;
using IoTCassandraAPI.Migrations.Resources.Interface;
using IoTCassandraAPI.Migrations.Resources.Tools;

namespace IoTCassandraAPI.Migrations.Scripts
{
    public class CreateEnergyConsumeTable : BaseMigration, IMigrationPart
    {
        public MigrationRegister GiveMigrationScript()
        {
            try
            {
                string query = @"CREATE TABLE IF NOT EXISTS energy_consume (
                                    greenhouse TEXT,
                                    id TEXT,
                                    value DOUBLE,
                                    register_date TIMESTAMP,
                                    PRIMARY KEY(greenhouse)
                                );
                                
                                CREATE INDEX IF NOT EXISTS idx_energy_id ON energy_consume (id);
                                CREATE INDEX IF NOT EXISTS idx_energy_date ON energy_consume (register_date);";

                return CreateMigrationRegister<CreateEnergyConsumeTable>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
