using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnline.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ProductDto>> GetItems()
        {
            try
            {
                var products = await _httpClient.GetAsync("api/Product");
                if (products.IsSuccessStatusCode)
                {
                    if(products.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        Enumerable.Empty<ProductDto>();
                    }
                    return await products.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
                }
                else
                {
                    var message = await products.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            try
            {
                var product =await _httpClient.GetAsync($"api/Product/{id}");
                if (product.IsSuccessStatusCode)
                {
                    if(product.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductDto);
                    }
                    return await product.Content.ReadFromJsonAsync<ProductDto>();
                }
                else
                {
                    var message = await product.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
