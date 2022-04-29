using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCalculator.Models;
using MyCalculator.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MyCalculator.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        public CalculatorContext Db { get; set; }
        public UserManager<User> UserManager { get; set; }

        public HistoryController(CalculatorContext context,
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
            List<ExpressionAndSolution> expressions = Db.ExpressionAndSolution.Where(e => e.UserId == userId).ToList();
            return View(expressions);
        }

        [HttpGet]
        public IActionResult Solution(int id)
        {
            string userId = UserManager.GetUserId(User);
            ViewBag.UserId = userId;
            ExpressionAndSolution expression = Db.ExpressionAndSolution
                .Include(e => e.Solutions)
                .FirstOrDefault(e => e.Id == id && e.UserId == userId);
            if (expression != null)
                return View(expression);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            string userId = UserManager.GetUserId(User);
            ExpressionAndSolution expression = Db.ExpressionAndSolution
                .Include(e => e.Errors)
                .Include(e => e.Warnings)
                .Include(e => e.Solutions)
                .FirstOrDefault(e => e.Id == id && e.UserId == userId);
            if (expression != null)
            {
                Db.ExpressionAndSolution.Remove(expression);
                Db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
