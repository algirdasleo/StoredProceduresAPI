namespace API.Models
{
    public class SalesOrderHeader
    {
        public DateTime DueDate { get; set; } = DateTime.MinValue;
        public int CustomerID { get; set; } = 0;
        public string ShipMethod { get; set; } = string.Empty;
    }
}