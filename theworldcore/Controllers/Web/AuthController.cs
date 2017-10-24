using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using theworldcore.Model;
using theworldcore.ViewModels;

namespace theworldcore.Controllers.Web
{
    public class AuthController: Controller
    {
        private readonly SignInManager<WorldUser> signInManager;

        public AuthController(SignInManager<WorldUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                RedirectToAction("Trips", "App");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                                            login.Username, login.Password, true, false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await signInManager.SignOutAsync();
            }

            return RedirectToAction("Trips", "App");
        }
    }
}
