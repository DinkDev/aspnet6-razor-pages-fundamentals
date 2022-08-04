using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    using Data;

    public class EditProductModel : PageModel
    {
        private readonly IProductRepository _repository;
        private readonly IWebHostEnvironment _environment;

        public EditProductModel(
            IProductRepository repository,
            IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        [FromRoute]
        public int Id { get; set; }

        [BindProperty]
        public Product EditProduct { get; set; }

        public void OnGet()
        {
            EditProduct = _repository.GetById(Id);
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (EditProduct.Upload is not null)
            {
                EditProduct.ImageFileName = EditProduct.Upload.FileName;

                var file = Path.Combine(
                    _environment.ContentRootPath,
                    @"wwwroot/images/menu",
                    EditProduct.Upload.FileName);

                await using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await EditProduct.Upload.CopyToAsync(fileStream);
                }
            }

            EditProduct.Created = DateTime.Now;
            EditProduct.Id = Id;
            _repository.Update(EditProduct);

            // TODO: the following may be appropriate to convert PageModel names to unadorned Page names.
            return RedirectToPage(GetPageName(typeof(ViewAllProductsModel)));

        }

        public IActionResult OnPostDelete()
        {
            _repository.Delete(Id);

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
