using IoTCassandraAPI.Migrations.Resources.DTO;

namespace IoTCassandraAPI.Migrations.Resources.Interface
{
    internal interface IMigrationPart
    {
        MigrationRegister GiveMigrationScript();
    }
}