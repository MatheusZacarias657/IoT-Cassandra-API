using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DTO.ETL
{
    public class DataReference
    {
        public string Table { get; set; }
        public string Value { get; set; }
        public DateTime RegisterDate { get; set; }

        public DataReference(string value, string registerDate, string table)
        {
            RegisterDate = DateTime.Parse(registerDate[..^6]);
            Value = value;
            Table = table;
        }
    }
}
