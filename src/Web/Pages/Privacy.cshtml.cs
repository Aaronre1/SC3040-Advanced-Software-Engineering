using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages;

[Authorize]
public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public string? AccessToken { get; set; }
    public string? Name { get; set; }

    public async Task OnGetAsync()
    {
        var accessToken = await HttpContext.GetTokenAsync(
            MicrosoftAccountDefaults.AuthenticationScheme, "access_token");

        var claims = HttpContext.User;
        
        Name =  HttpContext.User?.Identity?.Name ?? "no name";
        AccessToken = accessToken ?? "not found";
    }
}


