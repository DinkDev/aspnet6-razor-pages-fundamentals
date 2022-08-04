using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    public class AddProductModel : PageModel
    {
        private readonly IProductRepository _repository;
        private readonly IWebHostEnvironment _environment;

        public AddProductModel(
            IProductRepository repository,
            IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        [BindProperty]
        public Product NewProduct { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (NewProduct.Upload is not null)
            {
                NewProduct.ImageFileName = NewProduct.Upload.FileName;

                var file = Path.Combine(
                    _environment.ContentRootPath,
                    @"wwwroot/images/menu",
                    NewProduct.Upload.FileName);

                await using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await NewProduct.Upload.CopyToAsync(fileStream);
                }
            }

            NewProduct.Created = DateTime.Now;
            _repository.Add(NewProduct);

            // TODO: the following may be appropriate to convert PageModel names to unadorned Page names.
            return RedirectToPage(GetPageName(typeof(ViewAllProductsModel)));

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
