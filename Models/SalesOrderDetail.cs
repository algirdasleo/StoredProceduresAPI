namespace API.Models
{
    public class SalesOrderDetail
    {
        public int SalesOrderID { get; set; } = 0;
        public int OrderQty { get; set; } = 0;
        public int ProductID { get; set; } = 0;
        public decimal UnitPrice { get; set; } = 0;
        public decimal? UnitPriceDiscount { get; set; } = 0;
    }
}