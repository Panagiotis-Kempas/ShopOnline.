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

        protected IOrderedEnumerable<IGrouping<int,ProductDto>> GetGroupedProducts()
        {
            return from product in ProductList
                   group product by product.CategoryId into productByCat
                   orderby productByCat.Key
                   select productByCat;

        }

        protected string GetCategoryName(IGrouping<int,ProductDto> productDtos)
        {
            return productDtos.FirstOrDefault(x => x.CategoryId == productDtos.Key).CategoryName;
        }
    }
}
