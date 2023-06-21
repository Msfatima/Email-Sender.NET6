using Microsoft.AspNetCore.Mvc;
using Send_Emails_MVC.Models;
using System.Diagnostics;

namespace Send_Emails_MVC.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }
     
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string subject, string email, string message)
        {
            if (ModelState.IsValid)
            {
                await _emailSender.SendEmailAsync(subject, email, message);
            }

            ModelState.AddModelError("CustomError", " Message Sent Successfully! ");

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