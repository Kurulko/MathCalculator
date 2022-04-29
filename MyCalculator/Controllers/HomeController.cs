using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCalculator.Calculator.Calculate;
using MyCalculator.Models;
using MyCalculator.Models.ViewModels;
using System.Linq;

namespace MyCalculator.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public CalculatorContext Db { get; set; }
        public UserManager<User> UserManager { get; set; }

        public HomeController(CalculatorContext context,
            UserManager<User> userManager)
        {
            Db = context;
            UserManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string userId = UserManager.GetUserId(User);
            ViewBag.UserId = userId;
            return View();
        }
        [HttpPost]
        public IActionResult Index(ExpressionModel model)
        {
            string userId = UserManager.GetUserId(User);
            ViewBag.UserId = userId;
            if (ModelState.IsValid)
            {
                FigureAndOperation fo = new FigureAndOperation(model.Expression);
                var expressionAndSolution = fo.ExpressionAndSolution;
                expressionAndSolution.UserId = userId;
                ViewBag.ExpressionAndSolution = expressionAndSolution;
                if (!expressionAndSolution.Errors.Any() && !expressionAndSolution.Warnings.Any())
                {
                    Db.ExpressionAndSolution.Add(expressionAndSolution);
                    Db.SaveChanges();
                }
                return View(model);
            }
            return View(default(object));
        }

        [HttpGet]
        public IActionResult Rules() 
        {
            string userId = UserManager.GetUserId(User);
            ViewBag.UserId = userId;
            return View();
        }

        [HttpGet]
        public IActionResult ChangeThemeColor(string nowUrl)
        {
            string userId = UserManager.GetUserId(User);
            User user = Db.Users.FirstOrDefault(u => u.Id == userId);
            user.ThemeColor = user.ThemeColor == Color.White ? Color.Black : Color.White;
            Db.SaveChanges();
            //return RedirectToAction(nowUrl ?? "Index");
            return LocalRedirect(nowUrl ?? "/Home/Index");
        }

        [HttpGet]
        public IActionResult ChangeLanguage(string nowUrl)
        {
            string userId = UserManager.GetUserId(User);
            User user = Db.Users.FirstOrDefault(u => u.Id == userId);
            user.Language = user.Language == Language.English ? Language.Russian : Language.English;
            Db.SaveChanges();
            //return RedirectToAction(nowUrl ?? "Index");
            return LocalRedirect(nowUrl ?? "/Home/Index");
        }
    }
}
