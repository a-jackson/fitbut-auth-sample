using AspNet.Security.OAuth.Fitbit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace FitbitAuthSample.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet("~/signin")]
        public IActionResult SignIn()
            => Challenge(
                new AuthenticationProperties
                {
                    RedirectUri = "/"
                },
                FitbitAuthenticationDefaults.AuthenticationScheme);

        [HttpGet("~/signout")]
        public IActionResult Signout()
            => SignOut(
                new AuthenticationProperties
                {
                    RedirectUri = "/"
                },
                CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
