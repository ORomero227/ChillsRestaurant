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
        //Devuelve la vista con el form para hacer login
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login();
            login.ReturnUrl = returnUrl;
            return View(login);
        }

        //Accion para manejar el login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(login.UserName);

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

        //Accion para cerrar sesion
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region SignUp Actions
        //Lista que contiene todos los avatar para el perfil
        public List<string> GetProfileAvatars()
        {
            List<string> profileAvatars = new List<string>
            {
                "avatar-men1.png",
                "avatar-men2.png",
                "avatar-men3.png",
                "avatar-woman1.png",
                "avatar-woman2.png",
                "avatar-woman3.png",
            };

            return profileAvatars;
        }

        //Devuelve la vista con el formulario y los avatars
        [HttpGet]
        public IActionResult SignUp() 
        {
            ViewBag.ProfileAvatars = GetProfileAvatars();
            return View(); 
        }

        //Metodo para verificar si el username o el email contienen la palabra admin
        private bool InputContainsAdminWord(string input)
        {
            return !string.IsNullOrEmpty(input) && (input.Contains("admin", StringComparison.OrdinalIgnoreCase) || input.Contains("ADMIN", StringComparison.OrdinalIgnoreCase));
        }

        //Metodo para crear el usuario
        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {

            ViewBag.ProfileAvatars = GetProfileAvatars();

            if (ModelState.IsValid)
            {
                if (InputContainsAdminWord(user.Username))
                {
                    ModelState.AddModelError(string.Empty, "Username entered cant be used");
                    return View(user);
                }

                if (InputContainsAdminWord(user.Email))
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

                if (user.Photo == null)
                {
                    user.Photo = "avatar-default.png";
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
                    await _userManager.AddToRoleAsync(applicationUser, "Client");
                    return View("ConfirmAccount", applicationUser);
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

        #region Confirm Account

        //Deuvelve la vista con el form para confirmar email y telefono
        [HttpGet]
        public IActionResult ConfirmAccount(ApplicationUser model)
        {
            return View(model);
        }

        //Accion para marcar el email y el telefono como confirmado
        [HttpPost]
        public async Task<IActionResult> ConfirmAccountPost(ApplicationUser model)
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

        //En caso de que la cuenta no este confirmada se devuelve este link
        [HttpGet]
        public async Task<IActionResult> ConfirmAccountLink(string email)
        {
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync (email);
                if (user != null)
                {
                    return View("ConfirmAccount",user);
                }
            }

            return RedirectToAction("Login", new { returnUrl = "/" });
        }
        #endregion
    }
}
