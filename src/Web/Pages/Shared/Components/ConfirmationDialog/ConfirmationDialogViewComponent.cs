using Microsoft.AspNetCore.Mvc;

namespace ASE3040.Web.Pages.Shared.Components.ConfirmationDialog;

public class ConfirmationDialogViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(ConfirmationDialogViewModel model)
    {
        return View(model);
    }
}