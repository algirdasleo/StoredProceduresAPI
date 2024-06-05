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

        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var (products, ErrorMessage) = await _dbService.GetProductById(id);
            if (ErrorMessage != null)
                return BadRequest(ErrorMessage);
            return Ok(products);
        }
        
        [HttpPost("CreateNewCustomer")]
        public async Task<IActionResult> CreateNewCustomer(Customer customer)
        {
            var (rowsAffected, ErrorMessage) = await _dbService.CreateNewCustomer(customer);
            if (ErrorMessage != null)
                return BadRequest(ErrorMessage);
            return Ok(rowsAffected);
        }

        [HttpPost("CreateNewProduct")]
        public async Task<IActionResult> CreateNewProduct(Product product)
        {
            var (rowsAffected, ErrorMessage) = await _dbService.CreateNewProduct(product);
            if (ErrorMessage != null)
                return BadRequest(ErrorMessage);
            return Ok(rowsAffected);
        }

        [HttpPost("CreateNewProductCategory")]
        public async Task<IActionResult> CreateNewProductCategory(ProductCategory productCategory)
        {
            var (rowsAffected, ErrorMessage) = await _dbService.CreateNewProductCategory(productCategory);
            if (ErrorMessage != null)
                return BadRequest(ErrorMessage);
            return Ok(rowsAffected);
        }

        [HttpPost("CreateNewProductDescription")]
        public async Task<IActionResult> CreateNewProductDescription(ProductDescription productDescription)
        {
            var (rowsAffected, ErrorMessage) = await _dbService.CreateNewProductDescription(productDescription);
            if (ErrorMessage != null)
                return BadRequest(ErrorMessage);
            return Ok(rowsAffected);
        }

        [HttpPost("CreateNewSalesOrderDetail")]
        public async Task<IActionResult> CreateNewSalesOrderDetail(SalesOrderDetail salesOrderDetail)
        {
            var (rowsAffected, ErrorMessage) = await _dbService.CreateNewSalesOrderDetail(salesOrderDetail);
            if (ErrorMessage != null)
                return BadRequest(ErrorMessage);
            return Ok(rowsAffected);
        }

        [HttpPost("CreateNewSalesOrderHeader")]
        public async Task<IActionResult> CreateNewSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            var (rowsAffected, ErrorMessage) = await _dbService.CreateNewSalesOrderHeader(salesOrderHeader);
            if (ErrorMessage != null)
                return BadRequest(ErrorMessage);
            return Ok(rowsAffected);
        }

        [HttpPut("{salesOrderID}/{statusID}")]
        public async Task<IActionResult> UpdateSalesOrderStatus(int salesOrderID, int statusID)
        {
            var ErrorMessage = await _dbService.UpdateSalesOrderStatus(salesOrderID, statusID);
            if (ErrorMessage != null)
                return BadRequest(ErrorMessage);
            return Ok();
        }
    }
}