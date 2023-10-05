using ChillsRestaurant.Models;
using ChillsRestaurant.Models.EditModels;
using ChillsRestaurant.Models.ViewModels.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public List<string> GetAccountStatus()
        {
            List<string> status = new List<string>
            {
                "enable","disable"
            };

            return status;
        }
        private bool InputContainsAdminWord(string input)
        {
            return !string.IsNullOrEmpty(input) && (input.Contains("admin", StringComparison.OrdinalIgnoreCase) || input.Contains("ADMIN", StringComparison.OrdinalIgnoreCase));
        }

        public void PassSelectInputValuesToView()
        {
            //Se pasan los avatars disponibles para la foto de perfil
            ViewBag.ProfileAvatars = GetProfileAvatars();
            //Se pasan los roles disponibles
            ViewBag.Roles = _roleManager.Roles;
            //Se pasan los estados de cuentas disponibles
            ViewBag.AccountStatus = GetAccountStatus();
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
            PassSelectInputValuesToView();

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
                    AccountStatus = "enable",
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

            return RedirectToAction("AccountsManagement");
        }

        [HttpGet]
        public async Task<IActionResult> EditAccounts(string username)
        {
            PassSelectInputValuesToView();

            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                if (user.UserName == "manager1")
                {
                    TempData["EditAccountError"] = "This account cannot be edited";
                    return RedirectToAction("AccountsManagement");
                }

                if (user.SecondaryPhoneNumber == "Unregistered")
                {
                    user.SecondaryPhoneNumber = "000-000-0000";
                }

                ManagerEditAccountModel model = new ManagerEditAccountModel
                {
                    Name = user.Name,
                    Username = user.UserName,
                    Email = user.Email,
                    Address = user.Address,
                    Photo = user.Photo,
                    PrimaryPhoneNumber = user.PhoneNumber,
                    SecondaryPhoneNumber = user.SecondaryPhoneNumber,
                    NewPassword = "",
                    NewRole = user.Role,
                    AccountStatus = user.AccountStatus,
                    Id = user.Id
                };

                return View(model);
            }

            return RedirectToAction("AccountsManagement");
        }

        [HttpPost]
        public async Task<IActionResult> EditAccounts(ManagerEditAccountModel model)
        {
            PassSelectInputValuesToView();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {

                    //Verificar si el password cambio
                    if (!string.IsNullOrEmpty(model.NewPassword))
                    {
                        var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                        if (!changePasswordResult.Succeeded)
                        {
                            foreach (var error in changePasswordResult.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View(model);
                        }
                    }

                    //Verificar si el rol cambio
                    var roleChanged = user.Role != model.NewRole;
                    if (roleChanged)
                    {
                        var newRole = await _roleManager.FindByNameAsync(model.NewRole);

                        //En caso de que el rol deje de existir
                        if (newRole != null)
                        {
                            await _userManager.RemoveFromRoleAsync(user, user.Role);
                            await _userManager.AddToRoleAsync(user, model.NewRole);
                            user.Role = model.NewRole;
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Role not found");
                            return View(model);
                        }
                    }

                    if (model.SecondaryPhoneNumber == "000-000-0000")
                    {
                        model.SecondaryPhoneNumber = "Unregistered";
                    }

                    user.Name = model.Name;
                    user.UserName = model.Username;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.Photo = model.Photo;
                    user.PhoneNumber = model.PrimaryPhoneNumber;
                    user.SecondaryPhoneNumber = model.SecondaryPhoneNumber;
                    user.Role = model.NewRole;
                    user.AccountStatus = model.AccountStatus;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        TempData["EditAccountSucess"] = "Account Updated";
                        return RedirectToAction("AccountsManagement");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteAccount(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                if (user.UserName == "manager1")
                {
                    TempData["DeleteError"] = "This account cannot be deleted";
                    return RedirectToAction("AccountsManagement");
                }


                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();

                TempData["DeleteSuccess"] = "Account deleted";
                return RedirectToAction("AccountsManagement");
            }

            TempData["DeleteError"] = "An error occurred while deleting the account";
            return RedirectToAction("AccountsManagement");
        }

    }
}
