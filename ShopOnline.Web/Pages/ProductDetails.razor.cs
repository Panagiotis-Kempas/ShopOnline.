using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public partial class ProductDetails
    {
        [Inject]
        public IProductService productService { get; set; }

        [Parameter]
        public int Id { get; set; }


        public string ErrorMessage { get; set; }
        public ProductDto Product { get; set; }
        protected async override Task OnInitializedAsync()
        {
            try
            {
                Product = await productService.GetProduct(Id);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }
    }
}
