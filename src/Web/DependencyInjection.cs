using ASE3040.Application.Common.Interfaces;
using ASE3040.Web.Filters;
using ASE3040.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASE3040.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = MicrosoftAccountDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddMicrosoftAccount(options =>
            {
                options.ClientId = configuration["MICROSOFT_PROVIDER_AUTHENTICATION_CLIENT_ID"]!;
                options.ClientSecret = configuration["MICROSOFT_PROVIDER_AUTHENTICATION_SECRET"]!;
                options.SaveTokens = true;
            });

        services.AddRazorPages()
            .AddMvcOptions(options =>
            {
                options.Filters.Add<ExceptionFilters>();
            });

        return services;
    }
}