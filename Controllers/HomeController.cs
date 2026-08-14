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
        public static List<User> AccountList = new List<User>();


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
            user.Username = Username;
            user.Password = Password;
            user.ConfirmPassword = ConfirmPassword;
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

        [HttpGet]
        public IActionResult GetAccounts()
        {
            Account account = new Account();
            string query = account.Select("Accounts");
            var accounts = AccountList.Select((a, i) => new { index = i, id = a.Id, username = a.Username, password = a.Password });
            return Json(new { query = query, accounts = accounts });
        }

        [HttpPost]
        public IActionResult InsertAccount(string Username, string Password)
        {
            Account account = new Account();
            string query = account.Insert("Accounts", new[] { "Username", "Password" }, new[] { Username, Password });

            User newAccount = new User();
            newAccount.Id = AccountList.Count + 1;
            newAccount.Username = Username;
            newAccount.Password = Password;
            AccountList.Add(newAccount);

            return Json(new { success = true, query = query });
        }

        [HttpPost]
        public IActionResult DeleteAccount(int index)
        {
            if (index >= 0 && index < AccountList.Count)
            {
                Account account = new Account();
                string query = account.Delete("Accounts", AccountList[index].Id);
                AccountList.RemoveAt(index);
                return Json(new { success = true, query = query });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult UpdateAccount(int index, string newUsername, string newPassword)
        {
            if (index >= 0 && index < AccountList.Count)
            {
                Account account = new Account();
                if (!string.IsNullOrWhiteSpace(newUsername))
                    AccountList[index].Username = newUsername;
                if (!string.IsNullOrWhiteSpace(newPassword))
                    AccountList[index].Password = newPassword;

                string query = account.Update("Accounts", AccountList[index].Id, AccountList[index].Username, AccountList[index].Password);
                return Json(new { success = true, query = query });
            }
            return Json(new { success = false });
        }


    }
}
