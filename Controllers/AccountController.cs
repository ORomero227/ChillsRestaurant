using ChillsRestaurant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Text.RegularExpressions;

namespace ChillsRestaurant.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        #region Login Actions
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login();
            login.ReturnUrl = returnUrl;
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(login.UserName);
                if (user != null)
                {
                    if (user.EmailConfirmed) // Verifica si el correo está confirmado
                    {
                        await _signInManager.SignOutAsync();
                        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, isPersistent: false, lockoutOnFailure: false);

                        if (result.Succeeded)
                        {
                            return Redirect(login.ReturnUrl ?? "/");
                        }
                        else
                        {
                            ModelState.AddModelError(nameof(login.UserName), "Login Failed: Invalid Username or Password");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(login.UserName), "Login Failed: Your email is not confirmed.");
                        TempData["UserEmail"] = user.Email;
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(login.UserName), "Login Failed: User not found");
                }
            }
            return View(login);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region SignUp Actions
        [HttpGet]
        public IActionResult SignUp() { return View(); }

        private bool InputContainsAdminWord(string input)
        {
            return !string.IsNullOrEmpty(input) && (input.Contains("admin", StringComparison.OrdinalIgnoreCase) || input.Contains("ADMIN", StringComparison.OrdinalIgnoreCase));
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                if (InputContainsAdminWord(user.Username))
                {
                    ModelState.AddModelError(string.Empty, "Username entered cant be used");
                    return View(user);
                }

                if (InputContainsAdminWord(user.Password))
                {
                    ModelState.AddModelError(string.Empty, "Email entered cant be used");
                    return View(user);
                }

                if (user.SecondaryPhoneNumber == user.PrimaryPhoneNumber)
                {
                    ModelState.AddModelError(string.Empty, "The secondary phone number cannot be the same as the primary phone number.");
                    return View(user);
                }

                if (user.SecondaryPhoneNumber == null)
                {
                    user.SecondaryPhoneNumber = "Unregistered";
                }

                if (user.Address == null)
                {
                    user.Address = "Unregistered";
                }

                ApplicationUser applicationUser = new ApplicationUser
                {
                    Name = user.Name,
                    UserName = user.Username,
                    Email = user.Email,
                    PhoneNumber = user.PrimaryPhoneNumber,
                    SecondaryPhoneNumber = user.SecondaryPhoneNumber,
                    Address = user.Address,
                    Photo = user.Photo,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false
                };

                IdentityResult result = await _userManager.CreateAsync(applicationUser, user.Password);

                if (result.Succeeded)
                {
                    return View("ConfirmEmailAndPhone", applicationUser);
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(user);
        }
        #endregion

        #region Confirm Email and Phone
        [HttpGet]
        public IActionResult ConfirmEmailAndPhone(ApplicationUser model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmailAndPhonePost(ApplicationUser model)
        {
            if (model != null && !string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.PhoneNumber))
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (!user.EmailConfirmed && !user.PhoneNumberConfirmed)
                    {
                        user.EmailConfirmed = true;
                        user.PhoneNumberConfirmed = true;
                        await _userManager.UpdateAsync(user);
                        TempData["SignUpConfirm"] = "Account Created";
                        return RedirectToAction("Login", new { returnUrl = "/", });
                    }
                }
            }
            return RedirectToAction("SignUp",model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmailAndPhoneLink(string email)
        {
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync (email);
                if (user != null)
                {
                    return View("ConfirmEmailAndPhone",user);
                }
            }

            return RedirectToAction("Login", new { returnUrl = "/" });
        }
        #endregion
    }
}
