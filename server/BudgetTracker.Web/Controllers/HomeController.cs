using BudgetTracker.Common;
using BudgetTracker.Web.Data;
using BudgetTracker.Web.Data.Models;
using BudgetTracker.Business.Api;
using BudgetTracker.Business.Api.Messages;
using BudgetTracker.Business.Api.Messages.AuthenticationApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BudgetTracker.Web.Models;

namespace BudgetTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
