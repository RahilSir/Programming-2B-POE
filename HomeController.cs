using CMCS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace CMCS.Controllers
{
    public class HomeController : Controller // Correct class declaration
    {
       
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context) // Correct constructor
        {
            _context = context; // Initialize the context
       
        }

        [HttpPost]
        public IActionResult SelectRole(string role)
        {
            // Store the selected role in TempData or Session
            TempData["UserRole"] = role;

            // Redirect to a different action based on the selected role
            switch (role)
            {
                case "Lecturer":
                    return RedirectToAction("LecturerDashboard"); // Create this action
                case "AcademicManager":
                    return RedirectToAction("AcademicManagerDashboard"); // Create this action
                case "ProgrammeCoordinator":
                    return RedirectToAction("ProgrammeCoordinatorDashboard"); // Create this action
                default:
                    return RedirectToAction("Index"); // Default action
            }
        }

        public IActionResult LecturerDashboard()
        {
            return View();
        }



        public async Task<IActionResult> AcademicManagerDashboard()
        {
            var claims = await _context.Claims.ToListAsync(); // Fetch claims from the database
            return View(claims);
        }




        public IActionResult ProgrammeCoordinatorDashboard()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            return View();
        }

       // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}







       
























    } 

}

