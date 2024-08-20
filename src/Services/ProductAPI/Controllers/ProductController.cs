using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models.Dto;
using ProductAPI.Service.IService;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        protected ResponseDto _response;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
            _response = new();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProducts();
                _response.Result = products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            try
            {
                var product = await _productService.GetProductById(productId);
                if (product == null)
                {
                    return NotFound();
                }
                _response.Result = product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductDto productDto)
        {
            try
            {
                if (productDto == null)
                {
                    _logger.LogError("Product object is null.");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid model object.");
                    return BadRequest(ModelState);
                }

                var product = await _productService.CreateProduct(productDto);
                _response.Result = product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

    }
}
