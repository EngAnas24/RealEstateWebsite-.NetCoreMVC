using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;
using System.Configuration;

namespace RealEstateWebsite.Extensions
{
    public static class CustomAuthExtension
    {
        public static void AddCustomAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
         .AddCookie()
         .AddGoogle(options =>
         {
             IConfigurationSection googleAuthNSection = configuration.GetSection("Authentication:Google");
             options.ClientId = googleAuthNSection["ClientId"];
             options.ClientSecret = googleAuthNSection["ClientSecret"];
             options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Important
         })
         .AddFacebook(options =>
         {
             IConfigurationSection facebookAuthNSection = configuration.GetSection("Authentication:Facebook");
             options.AppId = facebookAuthNSection["AppId"];
             options.AppSecret = facebookAuthNSection["AppSecret"];
             options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Important
         });
        }
    }
}
