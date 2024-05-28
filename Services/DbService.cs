using System.Data;
using API.Factories;
using API.Models;
using Dapper;

namespace API.Services
{
    public class DbService
    {
        private readonly ConnectionFactory _connectionFactory;

        public DbService(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> CreateNewCustomer(Customer customer)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.ExecuteAsync(
                    "dbo.CreateNewCustomer",
                    new
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PasswordHash = customer.PasswordHash,
                        PasswordSalt = customer.PasswordSalt
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        public async Task<int> CreateNewProduct(Product product)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.ExecuteAsync(
                    "dbo.CreateNewProduct",
                    new
                    {
                        Name = product.Name,
                        ProductNumber = product.ProductNumber,
                        StandardCost = product.StandardCost,
                        ListPrice = product.ListPrice,
                        SellStartDate = product.SellStartDate
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        public async Task<int> CreateNewProductCategory(ProductCategory productCategory)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.ExecuteAsync(
                    "dbo.CreateNewProductCategory",
                    new
                    {
                        Name = productCategory.Name
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        public async Task<int> CreateNewProductDescription(ProductDescription productDescription)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.ExecuteAsync(
                    "dbo.CreateNewProductDescription",
                    new
                    {
                        Description = productDescription.Description
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        public async Task<int> CreateNewSalesOrderDetail(SalesOrderDetail salesOrderDetail)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.ExecuteAsync(
                    "dbo.CreateNewSalesOrderDetail",
                    new
                    {
                        SalesOrderID = salesOrderDetail.SalesOrderID,
                        OrderQty = salesOrderDetail.OrderQty,
                        ProductID = salesOrderDetail.ProductID,
                        UnitPrice = salesOrderDetail.UnitPrice
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        public async Task<int> CreateNewSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.ExecuteAsync(
                    "dbo.CreateNewSalesOrderHeader",
                    new
                    {
                        DueDate = salesOrderHeader.DueDate,
                        CustomerID = salesOrderHeader.CustomerID,
                        ShipMethod = salesOrderHeader.ShipMethod
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        public async Task UpdateSalesOrderStatus(int salesOrderID, int status)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                // Using Dapper's dynamic parameters
                var parameters = new DynamicParameters();
                parameters.Add("@SalesOrderID", salesOrderID);
                parameters.Add("@Status", status);

                await connection.ExecuteAsync(
                    "dbo.UpdateSalesOrderStatus",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );                
            }
        }
    }
}