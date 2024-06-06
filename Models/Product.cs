using System.Data.SqlTypes;

namespace API.Models
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public string ProductNumber { get; set; } = string.Empty;
        public int? ProductModelId { get; set; }
        public string? ProductModelName { get; set; } = string.Empty;
        public int? ProductCategoryId { get; set; }
        public string? ProductCategoryName { get; set; } = string.Empty;
        public string? ProductDescription { get; set; } = string.Empty;
        public string? Culture { get; set; } = string.Empty;
        public decimal StandardCost { get; set; } = 0;
        public decimal ListPrice { get; set; } = 0;
        public DateTime SellStartDate { get; set; } = DateTime.MinValue; 
    }
}