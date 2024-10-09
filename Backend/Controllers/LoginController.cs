using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {

        }
        public ActionResult Login(string email, string password)
        {
            return View();
        }

        public ActionResult Register(string email, string password, string name, int phoneNumber)
        {
            return View();
        }
    }
}
