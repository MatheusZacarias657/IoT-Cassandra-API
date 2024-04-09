using Cassandra;
using IoTCassandraAPI.Application.Util;
using IoTCassandraAPI.Migrations.Resources.DTO;
using IoTCassandraAPI.Migrations.Resources.Entity;
using IoTCassandraAPI.Migrations.Resources.Interface;
using System.Reflection;
using ISession = Cassandra.ISession;

namespace IoTCassandraAPI.Migrations.Resources.Tools
{
    internal class MigrationHelper
    {
        private static readonly string _migrationTable = "migration_history";

        internal static void CreateMigrationTable(ISession session)
        {
            try
            {
                string query = @$"CREATE TABLE IF NOT EXISTS {_migrationTable} (
                                    database TEXT,
                                    version TEXT,
                                    description TEXT,
                                    executing_date BIGINT,
                                    PRIMARY KEY(database, executing_date)
                                );";

                session.Execute(query);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static List<MigrationRegister> GetMigrations(ISession session, string keyspace)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type[] types = assembly.GetTypes();
                List<IMigrationPart?> migrations = types
                                                    .Where(type => type.GetInterfaces().Any(i => i == typeof(IMigrationPart)))
                                                    .Select(type => (IMigrationPart)Activator.CreateInstance(type))
                                                    .ToList();

                List<MigrationRegister> scripts = new();

                foreach (IMigrationPart migration in migrations)
                {
                    scripts.Add(migration.GiveMigrationScript());
                }

                BubbleSort(scripts);

                string lastMigration = MigrationHelper.FindLastMigration(session, keyspace);

                if (!string.IsNullOrEmpty(lastMigration))
                    scripts = scripts.Where(v => IsVersionAbove(v.Version, lastMigration)).ToList();

                return scripts;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void BubbleSort(List<MigrationRegister> versions)
        {
            for (int i = 0; i < versions.Count - 1; i++)
            {
                for (int j = 0; j < versions.Count - i - 1; j++)
                {
                    if (IsVersionAbove(versions[j].Version, versions[j + 1].Version))
                    {
                        MigrationRegister temp = versions[j];
                        versions[j] = versions[j + 1];
                        versions[j + 1] = temp;
                    }
                }
            }
        }

        private static string FindLastMigration(ISession session, string keyspace)
        {
            try
            {
                string query = @$"SELECT *
                                  FROM {_migrationTable}
                                  WHERE database = '{keyspace}'
                                  ORDER BY executing_date DESC";

                PreparedStatement statement = session.Prepare(query);
                Row row = session.Execute(statement.Bind()).FirstOrDefault();
                string version = string.Empty;

                if (row != null)
                    version = row["version"].ToString();



                return version;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool IsVersionAbove(string version, string targetVersion)
        {
            string[] versionParts = version.Split('.');
            string[] targetParts = targetVersion.Split('.');

            int startVersion = Convert.ToInt32(versionParts[0]);
            int startTarget = Convert.ToInt32(targetParts[0]);

            if (startVersion < startTarget)
                return false;

            if (startVersion > startTarget)
                return true;

            for (int i = 1; i < Math.Max(versionParts.Length, targetParts.Length); i++)
            {
                int versionPart = i < versionParts.Length ? Convert.ToInt32(versionParts[i]) : 0;
                int targetPart = i < targetParts.Length ? Convert.ToInt32(targetParts[i]) : 0;

                if (versionPart == targetPart)
                {
                    continue;
                }
                else if (versionPart < targetPart)
                {
                    return false;
                }
                else if (versionPart > targetPart)
                {
                    return true;
                }
            }

            return false;
        }

        internal static void ExecuteMigration(ISession session, string query)
        {
            try
            {
                string[] queries = query.Split(';');

                foreach(string queryPart in queries)
                {
                    string finalQuery = queryPart.Trim();

                    if (string.IsNullOrEmpty(finalQuery))
                        continue;

                    PreparedStatement statement = session.Prepare(finalQuery);
                    session.Execute(statement.Bind());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static void RegisterMigration(ISession session, MigrationRegister register, string keyspace)
        {
            try
            {
                Migration migration = new Migration()
                {
                    Database = keyspace,
                    Version = register.Version,
                    Description = register.Description,
                    ExecutingDate = DateTime.Now.Ticks,
                };

                string query = QueryHelper.GetInsertQuery(migration, _migrationTable);

                PreparedStatement statement = session.Prepare(query);
                session.Execute(statement.Bind());

                return;

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
