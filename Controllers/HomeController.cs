using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //HttpContext.Session.SetString("Test", "Session Value");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ID")))
            {
                return RedirectToAction("UserLogin", "Login");
            }
           return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("ID", "");
            return RedirectToAction("UserLogin", "Login");
        }
        public IActionResult About()
        {
            // ViewBag.sessionv = HttpContext.Session.GetString("Test");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ID")))
            {
                return RedirectToAction("UserLogin", "Login");
            }
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ID")))
            {
                return RedirectToAction("UserLogin", "Login");
            }
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
