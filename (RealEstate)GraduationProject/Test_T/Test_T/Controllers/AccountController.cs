using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Signin(string provider)
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(ExternalLoginCallback), "Account")
            };

            return Challenge(authenticationProperties, provider);
        }

        public async Task<IActionResult> ExternalLoginCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
            {
                return Redirect("https://localhost:44384/Identity/Account/Login");
            }
            // Extract user information from the claims
            var claims = authenticateResult.Principal.Identities.First().Claims.Select(c => new
            {
                c.Type,
                c.Value
            });
            var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return Redirect("https://localhost:44384/Identity/Account/Login");
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new IdentityUser { UserName = email, Email = email };
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    // Directly confirm the email
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                    var confirmResult = await _userManager.ConfirmEmailAsync(user, code);

                    if (confirmResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        var claim = new Claim("User", "User");
                        await _userManager.AddClaimAsync(user, claim);
                    }
                    else
                    {
                        // Handle email confirmation failure
                        return BadRequest();
                    }
                }
                else
                {
                    // Handle user creation failure
                    return NotFound();
                }
            }
            else
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return RedirectToPage("/Index");
        }


        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect(returnUrl ?? "/");
        }


    }
}
