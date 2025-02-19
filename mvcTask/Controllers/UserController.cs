using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult HandleLogin(string email, string password)
        {
            string Email = HttpContext.Session.GetString("email");
            string Password = HttpContext.Session.GetString("password");
            if(Email==email && Password == password)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["ErrorMessage"] = "your password and repeat password don't match";
                return RedirectToAction("Login","User");
            }
            
        }
        public IActionResult Profile()
        {
            ViewBag.name= HttpContext.Session.GetString("name");
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.password = HttpContext.Session.GetString("password");
            return View();
        }
    }
}
