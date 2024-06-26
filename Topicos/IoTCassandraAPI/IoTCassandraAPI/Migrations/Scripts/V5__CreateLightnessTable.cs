﻿using IoTCassandraAPI.Migrations.Resources.DTO;
using IoTCassandraAPI.Migrations.Resources.Interface;
using IoTCassandraAPI.Migrations.Resources.Tools;

namespace IoTCassandraAPI.Migrations.Scripts
{
    public class CreateLightnessTable : BaseMigration, IMigrationPart
    {
        public MigrationRegister GiveMigrationScript()
        {
            try
            {
                string query = @"CREATE TABLE IF NOT EXISTS lightness (
                                    greenhouse TEXT,
                                    id TEXT,
                                    value DOUBLE,
                                    register_date TIMESTAMP,
                                    PRIMARY KEY((register_date), id)
                                );
                                
                                CREATE INDEX IF NOT EXISTS idx_lightness_greenhouse ON lightness (greenhouse);";

                return CreateMigrationRegister<CreateLightnessTable>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
