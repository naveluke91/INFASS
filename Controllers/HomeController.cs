using System.Diagnostics;
using INFASS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

namespace INFASS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public static List<User> UserList = new List<User>();


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
 
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string Username, string Password, string ConfirmPassword)
        {
            User user = new User();
            string generatedQuery = user.Registration("Users", new[] { "Username", "Password", "ConfirmPassword" }, new[] { Username, Password, ConfirmPassword });

            // I-add ang user sa atong listahan para ma-save siya temporarily
            UserList.Add(user);

            return Json(new { queryMessage = generatedQuery });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            foreach (var savedUser in UserList)
            {
                if (savedUser.Username == Username && savedUser.Password == Password)
                {
                    return Json(new { success = true});
                }
            }

            return Json(new { success = false});
        }


    }
}
