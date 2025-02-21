using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace mvcTask.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string name, string email, string password, string repeat)
        {
            HttpContext.Session.SetString("name", name);
            HttpContext.Session.SetString("email", email);
            HttpContext.Session.SetString("password", password);
            HttpContext.Session.SetString("repeatPassword", repeat);
            if (password == repeat)
            {
                return RedirectToAction("Login");
            }
            else
            {
                TempData["ErrorMessage"] = "your password and repeat password don't match";
                return View();
            }

          
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult HandleLogin(string email, string password, string remember)
        {
            string storedEmail = HttpContext.Session.GetString("email");
            string storedPassword = HttpContext.Session.GetString("password");
            if (storedEmail == email && storedPassword == password)
            {
                if (remember == "yes")
                {
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Append("userEmail", email, options);
                }

                if (email == "admin@gmail.com" && password == "123")
                {
                    return RedirectToAction("Admin", "User");
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Incorrect email or password.";
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult Profile()
        {
            
            ViewBag.name = HttpContext.Session.GetString("name");
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.phone = HttpContext.Session.GetString("phone");
            ViewBag.address = HttpContext.Session.GetString("address");
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult EditProfile()
        {
            ViewBag.name = HttpContext.Session.GetString("name");
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.phone = HttpContext.Session.GetString("phone");
            ViewBag.address = HttpContext.Session.GetString("address");
            return View();
        }
        [HttpPost]
        public IActionResult EditProfile(string name, string email, string phone, string address)
        {
            HttpContext.Session.SetString("name", name);
            HttpContext.Session.SetString("email", email);
            HttpContext.Session.SetString("phone", phone);
            HttpContext.Session.SetString("address", address);

            return RedirectToAction("Profile"); 

        }

    }
}
