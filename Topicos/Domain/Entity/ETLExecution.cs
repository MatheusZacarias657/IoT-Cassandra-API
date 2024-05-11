using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class ETLExecution
    {
        [Column("id")]
        public string Id { get; set; }

        [Column("greenhouse")]
        public string GreenHouse { get; set; }

        [Column("related_tables")]
        public string RelatedTables { get; set; }

        [Column("last_execution")]
        public DateTime? LastExecution { get; set; }
    }
}
