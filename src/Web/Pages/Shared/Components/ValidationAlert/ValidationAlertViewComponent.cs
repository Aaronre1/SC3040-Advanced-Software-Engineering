using Microsoft.AspNetCore.Mvc;

namespace ASE3040.Web.Pages.Shared.Components.ValidationAlert;

public class ValidationAlertViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}