using budgettracker.common;
using budgettracker.data;
using budgettracker.data.Models;
using budgettracker.business.Api;
using budgettracker.business.Api.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using budgettracker.web.Models;

namespace budgettracker.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            UserRequestApiContract user = new UserRequestApiContract {
                Email = "kirkpatrickian56@gmail.com",
                UserName = "kirkpatrickian56",
                Password = "Saline!54"
            };
            ApiResponse userResponse = AuthenticationApi.Register(user, HttpContext.RequestServices);
            Console.WriteLine(JsonConvert.SerializeObject(userResponse));
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
