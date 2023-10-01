using ChillsRestaurant.Models;
using ChillsRestaurant.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChillsRestaurant.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ChillsRestaurantDBContext _dbContext;

        public ManagerController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ChillsRestaurantDBContext context)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._dbContext = context;
        }

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
        private bool InputContainsAdminWord(string input)
        {
            return !string.IsNullOrEmpty(input) && (input.Contains("admin", StringComparison.OrdinalIgnoreCase) || input.Contains("ADMIN", StringComparison.OrdinalIgnoreCase));
        }

    //-----------------------------------------------
    //              Accounts Management
    //-------------------------------------------------

        [HttpGet]
        public IActionResult AccountsManagement()
        {
            AccountsManagementModel model = new AccountsManagementModel();
            model.Users = _userManager.Users.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            //Se pasan los avatars disponibles para la foto de perfil
            ViewBag.ProfileAvatars = GetProfileAvatars();
            //Se pasan los roles disponibles
            ViewBag.Roles = _roleManager.Roles;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(ManagerCreateAccount user)
        {
            //Se pasan los avatars disponibles para la foto de perfil
            ViewBag.ProfileAvatars = GetProfileAvatars();
            //Se pasan los roles disponibles
            ViewBag.Roles = _roleManager.Roles;

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
                    Role = user.Role,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false
                };

                IdentityResult result = await _userManager.CreateAsync(applicationUser, user.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(applicationUser, user.Role);
                    TempData["CreateAccountSuccess"] = "Account Created";
                    return RedirectToAction("AccountsManagement");
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

        [HttpGet]
        public async Task<IActionResult> MoreInfo(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("AccountManagement");
        }

    }
}
