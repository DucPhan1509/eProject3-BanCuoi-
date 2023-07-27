using eProject3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace eProject3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly C2108L_Nhom6Context _context;

        public HomeController(ILogger<HomeController> logger, C2108L_Nhom6Context context)
        {
            _logger = logger;
            _context = context;
        }


        //[Route("index.html")]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> IndexAsync()
        {
            
            ViewBag.ProviderId = new SelectList(_context.Providers, "ProviderId", "ProviderName");

            var c2108L_Nhom6Context = _context.DichVus.Include(d => d.Cat).Include(d => d.Provider);
            return View(await c2108L_Nhom6Context.ToListAsync());
        }

        public IActionResult Support()
        {
            return View();
        }
        public IActionResult UserAccount()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("about.html")]
        public IActionResult About()
        {
            return View();
        }
        
        [Route("login-signup.html")]
        public IActionResult LoginSignup()
        {
            return View();
        }

        


        //[Route("plans.html")]
        //public IActionResult Plans()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}