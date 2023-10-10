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

        #region Arrays con valores

        /// <summary>
        /// Usa el nombre del item para buscar las imagenes asociadas
        /// </summary>
        /// <param name="itemName">El nombre del item para buscar las imagenes asociadas</param>
        /// <returns>Un array con todas las imagenes de ese item</returns>
        public string[] GetMenuItemsImages(string itemName)
        {
            switch (itemName)
            {
                case "Burgers":
                    return new string[] { "bbqbuerger.png", "Hambuegervagena.png", "Hambuerger.png" };

                case "Pasta":
                    return new string[] { "alfredo.png", "lasagna.png", "spagetti.png" };
                case "Desserts":
                    return new string[] { "cheescake.png", "chocolate.png" };
                default:
                    return new string[] { };
            }
        }

        /// <summary>
        /// Obtener todos los avatars disponible para la imagen de perfil
        /// </summary>
        /// <returns>Un array con todas las imagens </returns>
        public string[] GetProfileAvatars()
        {
            string[] profileAvatars = new string[]
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

        /// <summary>
        /// Obtener todas las opciones para el status de una cuenta
        /// </summary>
        /// <returns>Un array con todos los estados posibles</returns>
        public string[] GetAccountStatus()
        {
            string[] status = new string[] { "Enable","Disable" };

            return status;
        }

        #endregion

        #region Validaciones
        /// <summary>
        /// Validacion si los numeros de telefono son iguales
        /// </summary>
        /// <param name="primary">Numero primario</param>
        /// <param name="secondary">Numero secundario</param>
        /// <returns>Cierto si son iguales, Falso si no lo son</returns>
        private bool PhoneNumbersEquals(string primary, string secondary)
        {
            return primary == secondary;
        }

        #endregion


        //------------------------------------------------------------
        //              Accounts Management
        //-----------------------------------------------------------

        #region AccountsManagement Vista Principal

        /// <summary>
        /// Devuelve la vista principal junto con todos los usuarios que estan registrados
        /// </summary>
        [HttpGet]
        public IActionResult AccountsManagement()
        {
            AccountsManagementModel model = new AccountsManagementModel();
            model.Users = _userManager.Users.ToList(); //Lista de todos los usuarios
            return View(model);
        }

        #endregion


        #region Creacion de cuentas siendo manager

        /// <summary>
        /// Devuelve el form vacio para poder crear la cuenta 
        /// </summary>
        [HttpGet]
        public IActionResult CreateAccount()
        {
            //Se pasan los avatars disponibles para la foto de perfil
            ViewBag.ProfileAvatars = GetProfileAvatars();
            //Se pasan los roles disponibles
            ViewBag.Roles = _roleManager.Roles;

            return View();
        }

        /// <summary>
        /// Metodo asincrono para crear la cuenta
        /// </summary>
        /// <param name="user">Modelo que contiene los datos ingresados en los inputs</param>
        [HttpPost]
        public async Task<IActionResult> CreateAccount(ManagerCreateAccount user)
        {   
            //Si el modelo no es valido se devuelve la vista y se muestran los errores
            if (!ModelState.IsValid) 
            {
                //Se pasan los avatars disponibles para la foto de perfil y los roles
                ViewBag.ProfileAvatars = GetProfileAvatars();
                ViewBag.Roles = _roleManager.Roles;

                return View(user);
            }

            //Validar si los numeros de telefonos son iguales
            if (PhoneNumbersEquals(user.PrimaryPhoneNumber, user.SecondaryPhoneNumber ?? "")) //?? significa si es null el valor es blanco
            {
                ModelState.AddModelError(string.Empty, "The secondary phone number cannot be the same as the primary phone number.");
                return View(user);
            }

            //Asignar valores a los campos que se permiten null
            user.SecondaryPhoneNumber ??= "Unregistered";
            user.Address ??= "Unregistered";
            user.Photo ??= "avatar-default.png";

            //Se crea la cuenta
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

            //Resultado del api de identity
            IdentityResult result = await _userManager.CreateAsync(applicationUser, user.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(applicationUser, user.Role); //Se anade la cuenta al rol asignado
                TempData["CreateAccountSuccess"] = "Account Created"; //Mensaje de confirmacion

                return RedirectToAction("AccountsManagement");
            }
            else
            {
                //Se pasan los errores si ocurrio un error creando la cuenta
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            //Si se llega aqui rapido es que hay un problema
            return View(user); 
        }

        #endregion


        #region Ver informacion de la cuenta siendo manager

        /// <summary>
        /// Accion para obtener la informacion de una cuenta
        /// </summary>
        /// <param name="username">Nombre de usuario para buscar la cuenta</param>
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

        #endregion


        #region Editar cuentas siendo manager

        /// <summary>
        /// Metodo que devuelve un form con la informacion del usuario seleccionado para editar
        /// </summary>
        /// <param name="username">Nombre de usuario para editar</param>
        [HttpGet]
        public async Task<IActionResult> EditAccounts(string username)
        {
            //Se pasan los avatars disponibles para la foto de perfil
            ViewBag.ProfileAvatars = GetProfileAvatars();
            //Se pasan los roles disponibles
            ViewBag.Roles = _roleManager.Roles;
            //Se pasan los estados de cuentas disponibles
            ViewBag.AccountStatus = GetAccountStatus();

            var user = await _userManager.FindByNameAsync(username); //Se busca el usuario

            if (user != null)
            {
                //Verificacion de que la cuenta no sea manager1 (cuenta intocable)
                if (user.UserName == "manager1")
                {
                    TempData["EditAccountError"] = "This account cannot be edited";
                    return RedirectToAction("AccountsManagement");
                }

                //Si Secondary Phonenumber es unregistered se cambia a 000-000-0000
                user.SecondaryPhoneNumber = user.SecondaryPhoneNumber == "Unregistered" ? "000-000-0000" : user.SecondaryPhoneNumber;

                //Se crea el modelo que se va a pasar a la vista con los valores llenos
                //Los campos con ?? = "" significa si son null el valor va a ser en ""
                ManagerEditAccountModel model = new ManagerEditAccountModel
                {
                    Name = user.Name,
                    Username = user.UserName ?? "",
                    Email = user.Email ?? "",
                    Address = user.Address,
                    Photo = user.Photo,
                    PrimaryPhoneNumber = user.PhoneNumber ?? "" ,
                    SecondaryPhoneNumber = user.SecondaryPhoneNumber,
                    NewPassword = "",
                    NewRole = user.Role,
                    AccountStatus = user.AccountStatus,
                    Id = user.Id
                };

                return View(model); //Se muestra la vista con el form lleno
            }

            return RedirectToAction("AccountsManagement"); //Si hay un error se vuelve al inicio
        }

        /// <summary>
        /// Metodo asincrono para actualizar la informacion
        /// </summary>
        /// <param name="model">Modelo que tiene los datos ingresados en los inputs</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditAccounts(ManagerEditAccountModel model)
        {
            //Se pasan los avatars disponibles para la foto de perfil
            ViewBag.ProfileAvatars = GetProfileAvatars();
            //Se pasan los roles disponibles
            ViewBag.Roles = _roleManager.Roles;
            //Se pasan los estados de cuentas disponibles
            ViewBag.AccountStatus = GetAccountStatus();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id); //Buscar el user por el id

                if (user != null)
                {
                    //Verificar si el password cambio
                    if (!string.IsNullOrEmpty(model.NewPassword))
                    {
                        //Resultado si se cambia el password
                        var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword ?? "", model.NewPassword);

                        if (!changePasswordResult.Succeeded)
                        {
                            //En caso de que no se pueda actualizar el password se muestra el error
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
                        var newRole = await _roleManager.FindByNameAsync(model.NewRole); //Se busca si el rol existe

                        if (newRole != null)
                        {
                            await _userManager.RemoveFromRoleAsync(user, user.Role); //Se elimina del rol que estaba
                            await _userManager.AddToRoleAsync(user, model.NewRole); //Se anade al nuevo rol
                            user.Role = model.NewRole; //Se actualiza el rol
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Role not found");
                            return View(model);
                        }
                    }

                    //Si el segundo numero es 000-000-000 se marca como unregistered
                    model.SecondaryPhoneNumber = model.SecondaryPhoneNumber == "000-000-0000" ? "Unregistered" : model.SecondaryPhoneNumber;

                    //Se actualizan los valores 
                    user.Name = model.Name;
                    user.UserName = model.Username;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.Photo = model.Photo;
                    user.PhoneNumber = model.PrimaryPhoneNumber;
                    user.SecondaryPhoneNumber = model.SecondaryPhoneNumber;
                    user.Role = model.NewRole;
                    user.AccountStatus = model.AccountStatus;

                    //Resultado del api actualizando el user
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        TempData["EditAccountSucess"] = "Account Updated"; //Mensaje de confirmacion
                        return RedirectToAction("AccountsManagement");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            //Todos los erros si el api no puede actualizar el user
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found"); //Error si el user no se encuentra
                }
            }

            return View(model);
        }

        #endregion


        #region Elminar cuentas

        /// <summary>
        /// Metodo asincrono para borrar la cuenta indicada por el username
        /// </summary>
        /// <param name="username">Nombre de usuario que se va a borrar</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteAccount(string username)
        {
            var user = await _userManager.FindByNameAsync(username); //Se busca el usuario

            if (user != null)
            {
                //En caso de que el username sea manager1 no se puede borrar (cuenta intocable)
                if (user.UserName == "manager1")
                {
                    TempData["DeleteError"] = "This account cannot be deleted";
                    return RedirectToAction("AccountsManagement");
                }

                _dbContext.Users.Remove(user); //Se elimina de la base de datos

                await _dbContext.SaveChangesAsync(); //Actualizar la base de datos

                TempData["DeleteSuccess"] = "Account deleted"; //Mensaje de confirmacion

                return RedirectToAction("AccountsManagement");
            }

            TempData["DeleteError"] = "An error occurred while deleting the account"; //Mensaje de error

            return RedirectToAction("AccountsManagement");
        }
        #endregion


        //-------------------------------------------------
        //              Menu Items Actions
        //--------------------------------------------------

        public void PassFoodImagesToView()
        {
            //Se pasan las categorias disponibles a la vista
            ViewBag.ItemsCategory = _dbContext.MenuItems.Select(x => x.Category).Distinct().ToList();

            //Se pasan las diferentes imagenes a la vista
            ViewBag.BurgersImages = GetMenuItemsImages("Burgers");
            ViewBag.PastasImages = GetMenuItemsImages("Pasta");
            ViewBag.DessertsImages = GetMenuItemsImages("Desserts");
        }

        #region Crear Menu Items

        /// <summary>
        /// Devuelve el formulario para crear platos para el menu
        /// </summary>
        [HttpGet]
        public IActionResult CreateMenuItem()
        {
            //Se pasan todas las categorias y las imagenes de las categorias
            PassFoodImagesToView();

            return View();
        }

        /// <summary>
        /// Metodo asincrono para crear los platos para el menu
        /// </summary>
        /// <param name="model">Contiene los valores que se ingresaron en el form</param>

        [HttpPost]
        public async Task<IActionResult> CreateMenuItem(CreateMenuItem model)
        {
            //Se pasan todas las categorias y las imagenes de las categorias
            PassFoodImagesToView();

            if (ModelState.IsValid)
            {
                var menuItem = new MenuItem
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Category = model.Category,
                    Photo = model.Photo
                };

                _dbContext.MenuItems.Add(menuItem);

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index", "Home");

            }

            return View(model);
        }
        #endregion
    }
}
