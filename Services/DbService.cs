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

        public async Task<(IEnumerable<Product>, string)> GetProductById(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ProductID", id);
                parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

                var products = await connection.QueryAsync<Product>(
                    "dbo.GetProductById",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                var ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return (products, ErrorMessage);
            }
        }

        public async Task<(int, string)> CreateNewCustomer(Customer customer) // Returns a tuple of Rows Affected and ErrorMessage
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FirstName", customer.FirstName);
                parameters.Add("@LastName", customer.LastName);
                parameters.Add("@PasswordHash", customer.PasswordHash);
                parameters.Add("@PasswordSalt", customer.PasswordSalt);
                parameters.Add("@AddressLine1", customer.AddressLine1);
                parameters.Add("@AddressLine2", customer.AddressLine2);
                parameters.Add("@City", customer.City);
                parameters.Add("@StateProvince", customer.StateProvince);
                parameters.Add("@CountryRegion", customer.CountryRegion);
                parameters.Add("@PostalCode", customer.PostalCode);
                parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

                int rowsAffected = await connection.ExecuteAsync(
                    "dbo.CreateNewCustomer",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                string ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return (rowsAffected, ErrorMessage);
            }
        }
        public async Task<(int, string)> CreateNewProduct(Product product)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", product.Name);
                parameters.Add("@ProductNumber", product.ProductNumber);
                parameters.Add("ProductModelID", product.ProductModelId);
                parameters.Add("@ProductCategoryID", product.ProductCategoryId);
                parameters.Add("@ProductDescription", product.ProductDescription);
                parameters.Add("@StandardCost", product.StandardCost);
                parameters.Add("@ListPrice", product.ListPrice);
                parameters.Add("@SellStartDate", product.SellStartDate);
                parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

                int rowsAffected = await connection.ExecuteAsync(
                    "dbo.CreateNewProduct",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                string ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return (rowsAffected, ErrorMessage);
            }
        }
        public async Task<(int, string)> CreateNewProductCategory(ProductCategory productCategory)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", productCategory.Name);
                parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

                int rowsAffected = await connection.ExecuteAsync(
                    "dbo.CreateNewProductCategory",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                string ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return (rowsAffected, ErrorMessage);
            }
        }
        public async Task<(int, string)> CreateNewProductDescription(ProductDescription productDescription)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Description", productDescription.Description);
                parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

                int rowsAffected = await connection.ExecuteAsync(
                    "dbo.CreateNewProductDescription",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                string ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return (rowsAffected, ErrorMessage);
            }
        }
        public async Task<(int, string)> CreateNewSalesOrderDetail(SalesOrderDetail salesOrderDetail)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SalesOrderID", salesOrderDetail.SalesOrderID);
                parameters.Add("@OrderQty", salesOrderDetail.OrderQty);
                parameters.Add("@ProductID", salesOrderDetail.ProductID);
                parameters.Add("@UnitPrice", salesOrderDetail.UnitPrice);
                parameters.Add("@UnitPriceDiscount", salesOrderDetail.UnitPriceDiscount);
                parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

                int rowsAffected = await connection.ExecuteAsync(
                    "dbo.CreateNewSalesOrderDetail",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                string ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return (rowsAffected, ErrorMessage);
            }
        }
        public async Task<(int, string)> CreateNewSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DueDate", salesOrderHeader.DueDate);
                parameters.Add("@CustomerID", salesOrderHeader.CustomerID);
                parameters.Add("ShipToAddressID", salesOrderHeader.ShipToAddressID);
                parameters.Add("@BillToAddressID", salesOrderHeader.BillToAddressID);
                parameters.Add("@ShipMethod", salesOrderHeader.ShipMethod);
                parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

                int rowsAffected = await connection.ExecuteAsync(
                    "dbo.CreateNewSalesOrderHeader",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                string ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return (rowsAffected, ErrorMessage);
            }
        }
        public async Task<string> UpdateSalesOrderStatus(int salesOrderID, int status)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SalesOrderID", salesOrderID);
                parameters.Add("@Status", status);
                parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

                await connection.ExecuteAsync(
                    "dbo.UpdateSalesOrderStatus",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                string ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return ErrorMessage;               
            }
        }
    }
}