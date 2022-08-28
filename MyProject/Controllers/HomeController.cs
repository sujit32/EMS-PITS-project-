using Microsoft.AspNetCore.Mvc;
using MyProject.Data;
using MyProject.Models;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        private AppDBContext Context { get; }
        public HomeController(AppDBContext _context)
        {
            this.Context = _context;
        }


        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewPage()
        {
            
            var data= Context.Addresses.ToList();

            return View(data);
        }

        public IActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Address address)
        {
            this.Context.Addresses.Add(address);
            this.Context.SaveChanges();

            return RedirectToAction("ViewPage");
        }

        public IActionResult Delete(int? id)
        {
            var data = Context.Addresses.Find(id);
            Context.Addresses.Remove(data);
            Context.SaveChanges();
            return RedirectToAction("ViewPage");
        }

        public IActionResult Update(int? id)
        {
            var data = Context.Addresses.Find(id);
            //data.Name = update.Name;
            //data.Email = update.Email;
            //data.Password = Update().Password
            //Context.SaveChanges();
            //return RedirectToAction("ViewPage");


            return View(data);

        }
        [HttpPost]
        public IActionResult UpdateData(Address address)
        {
            var data = Context.Addresses.Find(address.Id);
            data.Name = address.Name;
            data.Email = address.Email;
            data.Password = address.Password;
            data.ConfirmPassword = address.ConfirmPassword;
            data.Gender = address.Gender;
            Context.SaveChanges();
            return RedirectToAction("ViewPage");


           // return View(data);

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}