using App.Domain.Core.Contracts.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Admin.ViewComponents
{
    public class NumberOfStatusesViewComponent : ViewComponent
    {
        private readonly IOrderStatusAppServcie _statusAppServcie;

        public NumberOfStatusesViewComponent(IOrderStatusAppServcie statusAppServcie)
        {
            _statusAppServcie = statusAppServcie;
        }

        public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
        {
            var statuses = await _statusAppServcie.GetAll(cancellationToken);
            return View("_StatusesNumber", statuses.Count);

        }
    }
}
