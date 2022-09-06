using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Logging;
using MyProject.Data;
using MyProject.Models;
using MyProject.View_Models;
using System.Diagnostics;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        private AppDBContext Context { get; }
        private readonly IWebHostEnvironment WebHostEnvironment;

        private readonly INotyfService _notyf;

        public HomeController(AppDBContext _context, IWebHostEnvironment webHostEnvironment, INotyfService _notyf)
        {
            this._notyf = _notyf;
            WebHostEnvironment = webHostEnvironment;
            this.Context = _context;
        }


        
        private INotyfService? notyf;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile(int id)
        {
            
            var data = Context.Addresses.Find(id);
            return View(data);
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

        [Authorize]
        [HttpPost]
        public IActionResult Index(AddressVM views)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(views.Password);
            string stringFile = upload(views);
            var data = new Address
            {
                Name = views.Name,
                Email = views.Email,
                Password = passwordHash,
                HomeTown = views.HomeTown,
                Phone = views.Phone,
                Gender = views.Gender,
                Image = stringFile

            };
            this.Context.Addresses.Add(data);
            this.Context.SaveChanges();
            _notyf.Success("Inserted Successfully");
            return RedirectToAction("ViewPage");
        }
        [Authorize]
        public IActionResult Delete(int? id)
        {
            var data = Context.Addresses.Find(id);
            _ = Context.Addresses.Remove(data);
            Context.SaveChanges();
            _notyf.Success("Deleted Successfully");
            return RedirectToAction("ViewPage");

        }

        public IActionResult Update(int? id)
        {
            var data = Context.Addresses.Find(id);
             return View(data);
        }


        [HttpPost]
        public IActionResult UpdateData(AddressVM views)
        {
            string stringFile = upload(views);
            if (views.Image != null)
            {
                var data = Context.Addresses.Find(views.Id);
                string delDir = Path.Combine(WebHostEnvironment.WebRootPath, "image", data.Image);
                FileInfo f1 = new FileInfo(delDir);
                if (f1.Exists)
                {
                    System.IO.File.Delete(delDir);
                    f1.Delete();
                }
                data.Name = views.Name;
                data.Email = views.Email;
                data.HomeTown = views.HomeTown;
                data.Phone = views.Phone;
                data.Gender = views.Gender;
                data.Image = stringFile;
                Context.SaveChanges();
                _notyf.Success("Updated Successfully");
                return RedirectToAction("ViewPage");
            }
            else
            {
                var data = Context.Addresses.Find(views.Id);
                data.Name = views.Name;
                data.Email = views.Email;
                data.HomeTown = views.HomeTown;
                data.Phone = views.Phone;
                data.Gender = views.Gender;
                Context.SaveChanges();
                _notyf.Success("Updated Successfully");
                return RedirectToAction("ViewPage");

            }
        }
        private string upload(AddressVM s)
        {
            string fileName = "";
            if (s.Image != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "image");
                fileName = Guid.NewGuid().ToString() + "-" + s.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    s.Image.CopyTo(fileStream);
                }
            }
            return fileName;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}