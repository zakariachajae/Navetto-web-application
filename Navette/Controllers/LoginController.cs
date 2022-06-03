using Navette.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Navette.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public static List<User> users = new List<User>();

        public ActionResult Login()

        {
            if (users.Count == 0)
            {
                User user1 = new User() { Id = 1, UserName = "admin", Password = "123456789" };
                users.Add(user1);
                User user2 = new User() { Id = 2, UserName = "hamza", Password = "1234567" };
                users.Add(user2);
                User user3 = new User() { Id = 3, UserName = "hamid", Password = "1234" };
                users.Add(user3);
                User user4 = new User() { Id = 4, UserName = "mourad", Password = "123" };
                users.Add(user4);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(int i = 0)
        {
            foreach (var user in users)
            {
                if (Request.Form["login"] == user.UserName && Request.Form["password"] == user.Password && user.Id == 1)
                {
                    Session["user_id"] = user.Id;
                    Session["username"] = user.UserName;
                    Session["mode"] = "admin";
                    return RedirectToAction("Index", "Home");
                }

                if (Request.Form["login"] == user.UserName && Request.Form["password"] == user.Password && user.Id != 1)
                {
                    Session["user_id"] = user.Id;
                    Session["username"] = user.UserName;
                    Session["mode"] = "user";
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.msg = "Compte n'existe pas ";
            return View();


        }
        public ActionResult Deconnexion()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignIn()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SignIn(int id = 0)
        {
            if (Request.Form["password"] == Request.Form["confirmerPassword"])
            {
                string UserName = Request.Form["username"];
                string Password = Request.Form["password"];
                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                {
                    User nve = new User()
                    {
                        Id = users.Count + 1,
                        UserName = UserName,
                        Password = Password
                    };
                    users.Add(nve);
                }
                else
                {
                    ViewBag.error1 = "veuillez saisir vos informations";
                    return View();
                }
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.error = "password n'est pas confirmé";
                return View();
            }


        }
    }
}