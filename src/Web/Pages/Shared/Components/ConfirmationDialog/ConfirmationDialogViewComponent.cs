using Microsoft.AspNetCore.Mvc;

namespace ASE3040.Web.Pages.Shared.Components.ConfirmationDialog;

public class ConfirmationDialogViewComponent : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync(ConfirmationDialogViewModel model)
    {
        return Task.FromResult<IViewComponentResult>(View(model));
    }
}