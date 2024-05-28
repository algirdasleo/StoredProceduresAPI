using API.Services;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class APIController : ControllerBase
    {
        private readonly DbService _dbService;

        public APIController(DbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("CreateNewCustomer")]
        public async Task<IActionResult> CreateNewCustomer(Customer customer)
        {
            var result = await _dbService.CreateNewCustomer(customer);
            return Ok(result);
        }

        [HttpPost("CreateNewProduct")]
        public async Task<IActionResult> CreateNewProduct(Product product)
        {
            var result = await _dbService.CreateNewProduct(product);
            return Ok(result);
        }

        [HttpPost("CreateNewProductCategory")]
        public async Task<IActionResult> CreateNewProductCategory(ProductCategory productCategory)
        {
            var result = await _dbService.CreateNewProductCategory(productCategory);
            return Ok(result);
        }

        [HttpPost("CreateNewProductDescription")]
        public async Task<IActionResult> CreateNewProductDescription(ProductDescription productDescription)
        {
            var result = await _dbService.CreateNewProductDescription(productDescription);
            return Ok(result);
        }

        [HttpPost("CreateNewSalesOrderDetail")]
        public async Task<IActionResult> CreateNewSalesOrderDetail(SalesOrderDetail salesOrderDetail)
        {
            var result = await _dbService.CreateNewSalesOrderDetail(salesOrderDetail);
            return Ok(result);
        }

        [HttpPost("CreateNewSalesOrderHeader")]
        public async Task<IActionResult> CreateNewSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            var result = await _dbService.CreateNewSalesOrderHeader(salesOrderHeader);
            return Ok(result);
        }

        [HttpPut("{salesOrderID}/{statusID}")]
        public async Task UpdateSalesOrderStatus(int salesOrderID, int statusID)
        {
            await _dbService.UpdateSalesOrderStatus(salesOrderID, statusID);
        }
    }
}