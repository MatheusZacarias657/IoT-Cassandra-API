using IoTCassandraAPI.Migrations.Resources.DTO;
using System.ComponentModel;
using System.Diagnostics;

namespace IoTCassandraAPI.Migrations.Resources.Tools
{
    public class BaseMigration
    {
        protected MigrationRegister CreateMigrationRegister<T>(string migration)
        {
            try
            {
                return new MigrationRegister
                {
                    Migration = migration,
                    Version = CaptureVersion<T>(),
                    Description = CaptureDescrition<T>(),
                };

            }
            catch (Exception)
            {
                throw;
            }
        }

        private string CaptureDescrition<T>()
        {
            try
            {
                return TypeDescriptor.GetClassName(typeof(T)).Split(".")[^1];

            }
            catch (Exception)
            {
                throw;
            }
        }

        private string CaptureVersion<T>()
        {
            try
            {
                StackFrame[] stackFrames = new StackTrace(true).GetFrames();

                var targetStack =
                    (from stackFrame in stackFrames
                     where stackFrame.GetMethod().DeclaringType.Name.Equals(TypeDescriptor.GetClassName(typeof(T)).Split(".")[^1])
                     select stackFrame).ToList().FirstOrDefault();

                string fileName = Path.GetFileName(targetStack.GetFileName());
                string[] versionArray = fileName.Split("_");
                string finalVersion = "";

                foreach (string version in versionArray)
                {
                    string verionNumber = new string(version.Where(c => char.IsDigit(c)).ToArray());

                    if (!string.IsNullOrEmpty(verionNumber))
                        finalVersion += $"{verionNumber}.";

                }

                return finalVersion[..^1];
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
