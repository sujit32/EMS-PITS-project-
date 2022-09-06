using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using MyProject.Data;
using MyProject.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MyProject.Controllers
{
    public class AdminController : Controller
    {
        private AppDBContext Context { get; }
        private readonly IWebHostEnvironment WebHostEnvironment;

        private readonly INotyfService _notyf;

        public AdminController(AppDBContext _context, IWebHostEnvironment webHostEnvironment, INotyfService _notyf)
        {
            this._notyf = _notyf;
            WebHostEnvironment = webHostEnvironment;
            this.Context = _context;
        }

        private INotyfService? notyf;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(AdminVM admin)
        {
           
            if (ModelState.IsValid)
                {
                    var data = Context.Addresses.Where(e => e.Email == admin.Email).SingleOrDefault();
                    if (data != null)
                    {
                    bool isValid = (data.Email == admin.Email && BCrypt.Net.BCrypt.Verify(admin.Password, data.Password)); ;
                        if (isValid)
                        {
                            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, admin.Email) }, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);
                             HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                             HttpContext.Session.SetString("Username", admin.Email);
                            return RedirectToAction("ViewPage","Home");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Password Incorrect";
                            return View("Index");
                        }

                    }
                    else
                    {
                    TempData["ErrorMessage"] = "Email not Found";
                    return View("Index");
                }
            }
            else
            {
                TempData["errorMessage"] = "Found";
                return View("Index");
            }
            return View("Index");
        }

     

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

    }
}
