using IoTCassandraAPI.Migrations.Resources.DTO;
using IoTCassandraAPI.Migrations.Resources.Interface;
using IoTCassandraAPI.Migrations.Resources.Tools;

namespace IoTCassandraAPI.Migrations.Scripts
{
    public class CreateSoilHumidityTable : BaseMigration, IMigrationPart
    {
        public MigrationRegister GiveMigrationScript()
        {
            try
            {
                string query = @"CREATE TABLE IF NOT EXISTS soil_humidity (
                                    greenhouse TEXT,
                                    id TEXT,
                                    value DOUBLE,
                                    register_date TIMESTAMP,
                                    PRIMARY KEY((register_date), id)
                                );";

                return CreateMigrationRegister<CreateSoilHumidityTable>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
