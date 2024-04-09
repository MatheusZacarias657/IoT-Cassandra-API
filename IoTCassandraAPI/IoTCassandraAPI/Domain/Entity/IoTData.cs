using System.ComponentModel.DataAnnotations.Schema;

namespace IoTCassandraAPI.Domain.Entity
{
    public class IoTData<T>
    {
        [Column("greenhouse")]
        public string Greenhouse { get; set; }

        [Column("id")]
        public string Id { get; set; }

        [Column("value")]
        public T Value { get; set; }

        [Column("register_date")]
        public DateTime RegisterDate { get; set; }
    }
}
