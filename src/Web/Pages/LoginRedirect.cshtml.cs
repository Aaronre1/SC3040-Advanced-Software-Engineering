using ASE3040.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages;

[Authorize]
public class LoginRedirectModel : PageModel
{
    private readonly ILogger<LoginRedirectModel> _logger;
    private readonly IUser _user;

    public LoginRedirectModel(ILogger<LoginRedirectModel> logger, IUser user)
    {
        _logger = logger;
        _user = user;
    }
    
    public IActionResult OnGetAsync()
    {
        _logger.LogInformation("{UserName} logged in", _user.UserName);
        return RedirectToPage("/Index");
    }
}