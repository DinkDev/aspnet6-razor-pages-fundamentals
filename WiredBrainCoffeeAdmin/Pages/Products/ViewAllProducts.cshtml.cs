namespace WiredBrainCoffeeAdmin.Pages.Products
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Data;

    public class ViewAllProductsModel : PageModel
    {
        private readonly IProductRepository _repository;

        public ViewAllProductsModel(IProductRepository repository)
        {
            _repository = repository;
        }

        public List<Product> Products { get; private set; }

        public void OnGet()
        {
            Products = _repository.GetAll();
        }
    }
}
