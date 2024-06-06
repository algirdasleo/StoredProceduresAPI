using System.Data;
using API.Factories;
using API.Models;
using Dapper;

namespace API.Services
{
    public class DbService
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly CommandFactory _commandFactory;

        public DbService(ConnectionFactory connectionFactory, CommandFactory commandFactory)
        {
            _connectionFactory = connectionFactory;
            _commandFactory = commandFactory;
        }

        public async Task<(IEnumerable<Product>, string)> GetProductById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProductID", id);
            parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
            var command = _commandFactory.CreateStoredProcedureCommand("dbo.GetProductById", parameters);

            using (var connection = _connectionFactory.CreateConnection())
            {
                var products = await connection.QueryAsync<Product>(command);
                var ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return (products, ErrorMessage);
            }
        }

        public async Task<(int, string)> CreateNewCustomer(Customer customer) // Returns a tuple of Rows Affected and ErrorMessage
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", customer.FirstName);
            parameters.Add("@LastName", customer.LastName);
            parameters.Add("@PasswordHash", customer.PasswordHash);
            parameters.Add("@PasswordSalt", customer.PasswordSalt);
            parameters.Add("@AddressLine1", customer.AddressLine1);
            parameters.Add("@AddressLine2", customer.AddressLine2);
            parameters.Add("@AddressType", customer.AddressType);
            parameters.Add("@City", customer.City);
            parameters.Add("@StateProvince", customer.StateProvince);
            parameters.Add("@CountryRegion", customer.CountryRegion);
            parameters.Add("@PostalCode", customer.PostalCode);
            parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
            var command = _commandFactory.CreateStoredProcedureCommand("dbo.CreateNewCustomer", parameters);

            using (var connection = _connectionFactory.CreateConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(command);
                string ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return (rowsAffected, ErrorMessage);
            }
        }
        public async Task<(int, string)> CreateNewProduct(Product product)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", product.Name);
            parameters.Add("@ProductNumber", product.ProductNumber);
            parameters.Add("@ProductModelID", product.ProductModelId);
            parameters.Add("@ProductModelName", product.ProductModelName);
            parameters.Add("@ProductCategoryID", product.ProductCategoryId);
            parameters.Add("@ProductCategoryName", product.ProductCategoryName);
            parameters.Add("@ProductDescription", product.ProductDescription);
            parameters.Add("@Culture", product.Culture);
            parameters.Add("@StandardCost", product.StandardCost);
            parameters.Add("@ListPrice", product.ListPrice);
            parameters.Add("@SellStartDate", product.SellStartDate);
            parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
            var command = _commandFactory.CreateStoredProcedureCommand("dbo.CreateNewProduct", parameters);

            using (var connection = _connectionFactory.CreateConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(command);
                string ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return (rowsAffected, ErrorMessage);
            }
        }
        public async Task<(int, string)> CreateNewProductCategory(ProductCategory productCategory)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", productCategory.Name);
            parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
            var command = _commandFactory.CreateStoredProcedureCommand("dbo.CreateNewProductCategory", parameters);

            using (var connection = _connectionFactory.CreateConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(command);
                string ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return (rowsAffected, ErrorMessage);
            }
        }
        public async Task<(int, string)> CreateNewProductDescription(ProductDescription productDescription)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@Description", productDescription.Description);
            parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
            var command = _commandFactory.CreateStoredProcedureCommand("dbo.CreateNewProductDescription", parameters);

            using (var connection = _connectionFactory.CreateConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(command);
                string ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return (rowsAffected, ErrorMessage);
            }
        }
        public async Task<(int, string)> CreateNewSalesOrderDetail(SalesOrderDetail salesOrderDetail)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SalesOrderID", salesOrderDetail.SalesOrderID);
            parameters.Add("@OrderQty", salesOrderDetail.OrderQty);
            parameters.Add("@ProductID", salesOrderDetail.ProductID);
            parameters.Add("@UnitPrice", salesOrderDetail.UnitPrice);
            parameters.Add("@UnitPriceDiscount", salesOrderDetail.UnitPriceDiscount);
            parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
            var command = _commandFactory.CreateStoredProcedureCommand("dbo.CreateNewSalesOrderDetail", parameters);

            using (var connection = _connectionFactory.CreateConnection())
            {
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
            var parameters = new DynamicParameters();
            parameters.Add("@DueDate", salesOrderHeader.DueDate);
            parameters.Add("@CustomerID", salesOrderHeader.CustomerID);
            parameters.Add("ShipToAddressID", salesOrderHeader.ShipToAddressID);
            parameters.Add("@BillToAddressID", salesOrderHeader.BillToAddressID);
            parameters.Add("@ShipMethod", salesOrderHeader.ShipMethod);
            parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
            var command = _commandFactory.CreateStoredProcedureCommand("dbo.CreateNewSalesOrderHeader", parameters);

            using (var connection = _connectionFactory.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(command);
                string ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return (rowsAffected, ErrorMessage);
            }
        }
        public async Task<string> UpdateSalesOrderStatus(int salesOrderID, int status)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SalesOrderID", salesOrderID);
            parameters.Add("@Status", status);
            parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
            var command = _commandFactory.CreateStoredProcedureCommand("dbo.UpdateSalesOrderStatus", parameters);

            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(command);
                string ErrorMessage = parameters.Get<string>("@ErrorMessage");
                return ErrorMessage;               
            }
        }
    }
}