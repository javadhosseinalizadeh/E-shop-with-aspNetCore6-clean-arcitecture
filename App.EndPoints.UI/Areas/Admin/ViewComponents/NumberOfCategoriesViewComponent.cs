using App.Domain.Core.Contracts.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Admin.ViewComponents
{
    public class NumberOfCategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryAppService _categoryAppService;

        public NumberOfCategoriesViewComponent(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
        {
            var categoies = await _categoryAppService.GetAll(cancellationToken);
            return View ("_CategoriesNumber", categoies.Count);

        }
    }
}
