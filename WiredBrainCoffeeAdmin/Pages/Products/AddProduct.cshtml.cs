using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    public class AddProductModel : PageModel
    {
        [BindProperty]
        public Product NewProduct { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

                // save product to database
                var productName = NewProduct.Name;

                // TODO: the following may be appropriate to convert PageModel names to unadorned Page names.
                return RedirectToPage(GetPageName(typeof(ViewAllProductsModel)));
            }

            return Page();
        }

        // TODO: make new PageModel base class with helpers like this
        private string GetPageName(Type pageModelType)
        {
            var pageName = pageModelType.Name.Replace(@"Model", string.Empty);
            if (!string.IsNullOrEmpty(pageName))
            {
                return pageName;
            }

            throw new ArgumentException($"Invalid page model name {pageModelType.Name}", nameof(pageModelType));
        }
    }
}
