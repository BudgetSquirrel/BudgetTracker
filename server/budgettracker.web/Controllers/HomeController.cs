using budgettracker.common;
using budgettracker.data;
using budgettracker.data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using budgettracker.web.Models;

namespace budgettracker.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            User user = new User {
                Email = "ianmann56@gmail.com",
                UserName = "ianmann56",
                Password = "Saline!54"
            };
            IEnumerable<string> userCreateErrors;
            // UserModel user = new UserModel { UserName = Input.UserName, Email = Input.Email };
            // var result = await _userManager.CreateAsync(user, Input.Password);
            UserStore userStore = new UserStore(HttpContext.RequestServices);
            Console.WriteLine(userStore);
            bool userCreated = userStore.Register(user, out userCreateErrors);
            foreach (string code in userCreateErrors)
            {
                Console.WriteLine("Here: " + code);
            }
            Console.WriteLine(userCreated);
            ViewData["Test"] = "Hello world!";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
