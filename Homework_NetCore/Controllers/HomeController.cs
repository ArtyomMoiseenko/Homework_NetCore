using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Homework_NetCore.Models.db;

namespace Homework_NetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly CompanyContext db;

        public HomeController(CompanyContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        public IActionResult Details()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
