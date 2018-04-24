﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Models;
using Microsoft.AspNetCore.Authentication;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        UserDataAccessLayer objUser = new UserDataAccessLayer();

        [HttpGet]
        public IActionResult RegisterUser()
        {

            return View();
        }
        [HttpPost]
        public IActionResult RegisterUser([Bind] UserDetails user)
        {
            if (ModelState.IsValid)
            {
                string RegistrationStatus = objUser.RegisterUser(user);
                if (RegistrationStatus == "Success")
                {
                    ModelState.Clear();
                    TempData["Success"] = "Registration Successfull!";
                    return View();
                }
                else
                {
                    TempData["Fail"] = "This User ID already exists. Registration Failed.";
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin([Bind] UserDetails user)
        {
              ModelState.Remove("FirstName");  
            ModelState.Remove("LastName");  
  
            //if (ModelState.IsValid)  
           // {  
                string LoginStatus = objUser.ValidateLogin(user);  
  
                if (LoginStatus == "Success")  
                {  
                    var claims = new List<Claim>  
                    {  
                        new Claim(ClaimTypes.Name, user.Emplid)  
                    };  
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");  
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);  
  
                    await HttpContext.SignInAsync(principal);  
                    return RedirectToAction("Index", "Home");  
                }  
                else  
                {  
                    TempData["UserLoginFailed"] = "Login Failed.Please enter correct credentials";  
                    return View();  
                }  
           // } else
           // {
              //  return View();
           // } 
                          
        }
    }
}