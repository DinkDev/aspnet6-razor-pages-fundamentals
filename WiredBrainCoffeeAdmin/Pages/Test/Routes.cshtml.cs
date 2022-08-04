using System.Reflection;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WiredBrainCoffeeAdmin.Pages
{
    public class RoutesModel : PageModel
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public RoutesModel(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public List<RouteInfo> Routes { get; } = new List<RouteInfo>();

        public void OnGet()
        {
            foreach (var x in _actionDescriptorCollectionProvider.ActionDescriptors.Items.OrderBy(i =>
                         i.AttributeRouteInfo?.Template))
            {
                var y = x as CompiledPageActionDescriptor;

                Routes.Add(new RouteInfo
                {
                    Template = x.AttributeRouteInfo?.Template,
                    Page = x.RouteValues?["Page"],
                    ModelType = y?.ModelTypeInfo,
                    PageType = y?.PageTypeInfo,
                });
            }
        }

        public class RouteInfo
        {
            public string Template { get; set; }
            public string Page { get; set; }
            public TypeInfo ModelType { get; set; }
            public TypeInfo PageType { get; set; }
        }
    }
}

