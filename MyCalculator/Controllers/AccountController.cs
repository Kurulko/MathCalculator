using MyCalculator.Models;
using MyCalculator.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MyCalculator.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<User>  UserManager { get; set; }
        public SignInManager<User> SignInManager { get; set; }

        public AccountController(UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        IActionResult DefaultRedirect()
            => RedirectToAction("Index", "Home");

        [HttpGet]
        public IActionResult Register()
        {
            if (User != null)
            {
                string userId = UserManager.GetUserId(User);
                ViewBag.UserId = userId;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Email = model.Email;
                user.UserName = model.Name;
                user.ThemeColor = Color.Black;

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, model.RememberMe);
                    return DefaultRedirect();
                }
                else
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }

            if (User != null)
            {
                string userId = UserManager.GetUserId(User);
                ViewBag.UserId = userId;
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (User != null)
            {
                string userId = UserManager.GetUserId(User);
                ViewBag.UserId = userId;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string name = model.Name;
                var result = await SignInManager.PasswordSignInAsync(name,
                    model.Password, model.RememberMe, false);
                if (result.Succeeded)
                    return DefaultRedirect();
                else
                    ModelState.AddModelError(string.Empty, "Wrong email");
            }

            if (User != null)
            {
                string userId = UserManager.GetUserId(User);
                ViewBag.UserId = userId;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
