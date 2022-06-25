using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Extensions;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]      
        public async Task<ActionResult<IEnumerable<ProductDto>>> GeItems()
        {
            try
            {
                var products = await _productRepository.GetItems();
                var productCategories = await _productRepository.GetCategories();

                if(products is null || productCategories is null)
                {
                    return NotFound();
                }
                else
                {
                    var productDtos = products.ConvertToDto(productCategories);
                    return Ok(productDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database.");
            }
            
           
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GeItem(int id)
        {
            try
            {
                var product = await _productRepository.GetItem(id);

                if (product is null)
                {
                    return BadRequest();
                }
                else
                {
                    var category = await _productRepository.GetCategory(product.CategoryId);
                    var productDto = product.ConvertToDto(category);
                    return Ok(productDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database.");
            }


        }
    }
}
