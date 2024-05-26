using System.Data.SqlTypes;

namespace API.Models
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public string ProductNumber { get; set; } = string.Empty;
        public decimal StandardCost { get; set; } = 0;
        public decimal ListPrice { get; set; } = 0;
        public DateTime SellStartDate { get; set; } = DateTime.MinValue; 
    }
}