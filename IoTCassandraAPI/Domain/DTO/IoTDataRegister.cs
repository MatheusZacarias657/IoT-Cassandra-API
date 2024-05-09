namespace IoTCassandraAPI.Domain.DTO
{
    public class IoTDataRegister<T>
    {
        public T Value { get; set; }
        public string Greenhouse { get; set; }
    }
}
