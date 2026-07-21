using System.Diagnostics;
using INFASS.Models;
using Microsoft.AspNetCore.Mvc;

namespace INFASS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Simple in-memory user store
        private static List<UserModel> _users = new List<UserModel>();

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

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return Json(new { success = false, message = "Username and Password are required." });
            }

            if (request.Password != request.ConfirmPassword)
            {
                return Json(new { success = false, message = "Passwords do not match." });
            }

            if (_users.Any(u => u.Username == request.Username))
            {
                return Json(new { success = false, message = "Username already exists." });
            }

            _users.Add(new UserModel { Username = request.Username, Password = request.Password });
            return Json(new { success = true, message = "Registration successful! You can now login." });
        }

        [HttpPost]
        public IActionResult LoginUser([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return Json(new { success = false, message = "Username and Password are required." });
            }

            var user = _users.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
            if (user != null)
            {
                return Json(new { success = true, message = "Login successful!" });
            }

            return Json(new { success = false, message = "Invalid username or password." });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
