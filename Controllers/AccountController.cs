using ChillsRestaurant.Models;
using ChillsRestaurant.Models.EditModels;
using ChillsRestaurant.Models.ViewModels;
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

    //----------------------------------------------------
    //              Login Actions
    //----------------------------------------------------

        /// <summary>
        /// Devuelve el formulario para hacer login
        /// </summary>
        /// <param name="returnUrl">Url para manejar los logout</param>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login();
            login.ReturnUrl = returnUrl;
            return View(login);
        }

        /// <summary>
        /// Metodo que se encarga de realizar la autenticacion
        /// </summary>
        /// <param name="model">Contiene los valores ingresados en los inputs del form</param>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            //Si el modelo no es valido se devuelve el form con los errores
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Se busca el usuario por el nombre de usuario
            var user = await _userManager.FindByNameAsync(model.UserName);

            //Si no se encuentra se devuelve la vista con el error de no encontrado
            if (user == null)
            {
                ModelState.AddModelError(nameof(model.UserName), "Login Failed: User not found");
                return View(model);
            }

            //Si no tiene el email confirmado se devuelve la vista con el error del email
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(nameof(model.UserName), "Login Failed: Your email is not confirmed.");
                TempData["UserEmail"] = user.Email;
                return View(model);
            }

            //Si la cuenta no esta habilitada se devuelve la vista con el error del estatus de la cuentas
            if (user.AccountStatus != "enable")
            {
                ModelState.AddModelError(string.Empty, "Your account has been disabled");
                return View(model);
            }

            //Se cierra sesion en modo de prevencion si hay una sesion activa
            await _signInManager.SignOutAsync();

            //El Api intenta autenticar el usuario usando el username y el password
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "/"); //Si es exitoso se redirige a la vista del menu
            } 
            else if (result.IsLockedOut) 
            {
                user.AccountStatus = "disabled";
                await _userManager.UpdateAsync(user);
                ModelState.AddModelError(nameof(model.UserName), "Login Failed: Account is locked out");
                return View(model);
            }
            else
            {
                ModelState.AddModelError(nameof(model.UserName), "Login Failed: Invalida Username or Password");
                return View(model);
            }
        }

        /// <summary>
        /// Metodo que solo se encarga de cerrar la sesion
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); //En este caso se devuelve el login 
        }

    //--------------------------------------------------------
    //          Sign Up Actions
    //--------------------------------------------------------

        /// <summary>
        /// Devuelve el formulario para crear la cuenta
        /// </summary>
        [HttpGet]
        public IActionResult SignUp() 
        {
            //Se pasan todas las fotos que se pueden usar de avatar
            ViewBag.ProfileAvatars = GetProfileAvatars();
            return View(); 
        }

        /// <summary>
        /// Metodo asincrono para crear la cuenta
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        public async Task<IActionResult> SignUp(User model)
        {
            //Se pasan las imagenes de los avatars para el perfil
            ViewBag.ProfileAvatars = GetProfileAvatars();

            //Si el modelo no es valido se devuelve el formulario
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (PhoneNumbersEquals(model.PrimaryPhoneNumber, model.SecondaryPhoneNumber ?? "")) //?? significa si es null el valor es blanco
            {
                ModelState.AddModelError(string.Empty, "The secondary phone number cannot be the same as the primary phone number.");
                return View(model);
            }

            //Asignar valores a los campos que se permiten null
            model.SecondaryPhoneNumber ??= "Unregistered";
            model.Address ??= "Unregistered";
            model.Photo ??= "avatar-default.png";

            //Se crea la cuenta
            ApplicationUser applicationUser = new ApplicationUser
            {
                Name = model.Name,
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PrimaryPhoneNumber,
                SecondaryPhoneNumber = model.SecondaryPhoneNumber,
                Address = model.Address,
                Photo = model.Photo,
                Role = "Client",
                AccountStatus = "disable",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false
            };

            //Resultado del api de identity tratando de crear la cuenta
            IdentityResult result = await _userManager.CreateAsync(applicationUser, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(applicationUser, "Client");
                return View("ConfirmAccount", applicationUser);
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
            return View(model);
        }
        
    //--------------------------------------------------------------------
    //              ConfirmAccount Actions
    //--------------------------------------------------------------------

        /// <summary>
        /// Devuelve la vista con el form para confirmar el email y el primary phone number
        /// </summary>
        /// <param name="model">La cuenta que se va a verificar</param>
        [HttpGet]
        public IActionResult ConfirmAccount(ApplicationUser model)
        {
            return View(model);
        }

        /// <summary>
        /// Metodo asyncrono para marcar en la base de datos la confirmacion de email y phonenumber
        /// </summary>
        /// <param name="model">Informacion del usuario</param>
        [HttpPost]
        public async Task<IActionResult> ConfirmAccountPost(ApplicationUser model)
        {
            //Verificacion de los datos enviados en el form
            if (model != null && !string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.PhoneNumber))
            {
                var user = await _userManager.FindByEmailAsync(model.Email); //Se busca el user en la base de datos

                if (user != null)
                {
                    //Se confirman los valores en caso de que no esten confirmados
                    if (!user.EmailConfirmed && !user.PhoneNumberConfirmed)
                    {
                        user.EmailConfirmed = true;
                        user.PhoneNumberConfirmed = true;
                        user.AccountStatus = "enable";
                        await _userManager.UpdateAsync(user);
                        TempData["SignUpConfirm"] = "Account Created";
                        return RedirectToAction("Login", new { returnUrl = "/" }); //Se devuelve el login
                    }
                }
            }

            return RedirectToAction("SignUp",model); //Si llega aqui es porque ocurrio un error
        }

        /// <summary>
        /// Devuelve un link que se muestra cuando se intenta el login y la cuenta no esta confirmada
        /// </summary>
        /// <param name="email">El email de el usuario que quiere confirmarse</param>
        [HttpGet]
        public async Task<IActionResult> ConfirmAccountLink(string email)
        {
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email); //Se busca el user

                if (user != null)
                {
                    return View("ConfirmAccount", user);
                }
            }

            return RedirectToAction("Login", new { returnUrl = "/" }); //Se devuelve el login
        }

    //--------------------------------------------------------------
    //                 Account Profile Actions
    //--------------------------------------------------------------

        /// <summary>
        /// Devuelve una vista con la informacion de la cuenta del usuario
        /// </summary>
        /// <param name="username">Nombre del usuario que quiere ver su cuenta</param>
        [HttpGet]
        public async Task<IActionResult> AccountProfile(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Vista con un formulario lleno con la informacion del usuario
        /// </summary>
        /// <param name="username">Nombre de usuario que se va a editar</param>
        [HttpGet]
        public async Task<IActionResult> EditAccount(string username)
        {
            //Se pasan los avatars para la foto de perfil
            ViewBag.ProfileAvatars = GetProfileAvatars();

            var user = await _userManager.FindByNameAsync(username); //Se busca el usuario

            if (user != null)
            {

                //Si Secondary Phonenumber es unregistered se cambia a 000-000-0000
                user.SecondaryPhoneNumber = user.SecondaryPhoneNumber == "Unregistered" ? "000-000-0000" : user.SecondaryPhoneNumber;

                //Se crea el modelo que se va a pasar a la vista con los valores llenos
                //Los campos con ?? = "" significa si son null el valor va a ser en ""
                AccountEditAccountModel model = new AccountEditAccountModel()
                {
                    Name = user.Name,
                    Username = user.UserName??"",
                    Email = user.Email ?? "",
                    Address = user.Address,
                    Photo = user.Photo,
                    PrimaryPhoneNumber = user.PhoneNumber ?? "",
                    SecondaryPhoneNumber = user.SecondaryPhoneNumber,
                    NewPassword = "",
                    Id = user.Id
                };

                return View(model); //Se muestra la vista con el form lleno
            }

            return RedirectToAction("AccountProfile");
        }

        /// <summary>
        /// Metodo asincrono para editar la informacion
        /// </summary>
        /// <param name="model">Los valores de los inputs</param>
        [HttpPost]
        public async Task<IActionResult> EditAccount(AccountEditAccountModel model)
        {
            //Se pasan los avatars para la foto de perfil
            ViewBag.ProfileAvatars = GetProfileAvatars();

            //Si el modelo no es valido se devuelve la vista con los errores
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Se busca el usuario
            var user = await _userManager.FindByIdAsync(model.Id);

            //Si el usurio no se encuentra se devuelve el error de que no se encontro
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(model);
            }

            //Verificar si el password cambio
            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                //Intento de cambiar el password
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword ?? "", model.NewPassword);

                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
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

            //Resultado si se puede actualizar el user
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["EditAccountSucess"] = "Account Updated";
                return RedirectToAction("AccountProfile", new { username = user.UserName });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model); //Se llega aqui es que hay un error
        }

    }
}
