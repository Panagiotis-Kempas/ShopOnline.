using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public partial class Products
    {
        [Inject]
        public IProductService productService { get; set; }

        public IEnumerable<ProductDto> ProductList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ProductList = await productService.GetItems();
        }
    }
}
