using budgettracker.common;
using budgettracker.data;
using budgettracker.data.Models;
using budgettracker.business.Api;
using budgettracker.business.Api.Contracts;
using budgettracker.business.Api.Contracts.AuthenticationApi;
using budgettracker.business.Api.Contracts.Requests;
using budgettracker.business.Api.Contracts.Responses;
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
            UserRequestApiContract userValues = new UserRequestApiContract {
                Email = "kirkp1ia@cmich.edu",
                UserName = "kirkp1ia",
                Password = "password!1234"
            };
            ApiRequest request = new ApiRequest()
            {
                Action = "REG_USER",
                ArgumentsDict = new Dictionary<string, object>() {
                    { "user-values", userValues }
                }
            };
            ApiResponse userResponse = AuthenticationApi.Invoke(request, HttpContext.RequestServices);
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
