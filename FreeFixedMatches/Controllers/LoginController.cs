using FreeFixedMatches.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeFixedMatches.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(User user)
        {
            var adminUser = new User {
                Username = "losko",
                Password = "filip"
            };

            if (user.Username == adminUser.Username && user.Password == adminUser.Password) { 
                Session["Username"] = adminUser.Username;
                return RedirectToAction("Index", "Admin");
            }
            return HttpNotFound();
        }
    }
}