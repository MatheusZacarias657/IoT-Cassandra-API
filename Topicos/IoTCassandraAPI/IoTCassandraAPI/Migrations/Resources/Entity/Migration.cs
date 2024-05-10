using System.ComponentModel.DataAnnotations.Schema;

namespace IoTCassandraAPI.Migrations.Resources.Entity
{
    internal class Migration
    {
        [Column("database")]
        public string Database { get; set; }

        [Column("version")]
        public string Version { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("executing_date")]
        public long ExecutingDate { get; set; }
    }
}
