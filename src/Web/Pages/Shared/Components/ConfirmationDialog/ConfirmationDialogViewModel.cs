namespace ASE3040.Web.Pages.Shared.Components.ConfirmationDialog;

public class ConfirmationDialogViewModel
{
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? Message { get; set; }
    public string PageHandler { get; set; } = default!;
    public string Method { get; set; } = default!;
    public IDictionary<string, string> RouteValues { get; set; } = new Dictionary<string, string>();
}