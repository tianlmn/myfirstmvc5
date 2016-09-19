using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using mvc5_first.Models;

namespace mvc5_first.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoLogin(UserDetails u, string btnSubmit)
        {
            if (ModelState.IsValid)
            {
                PokemonBusinessLayer bal = new PokemonBusinessLayer();
                var userStatus = bal.GetUserValidity(u);
                bool isAdmin = false;
                if (userStatus == UserStatus.AuthenticatedAdmin)
                {
                    isAdmin = true;
                }
                else if (userStatus == UserStatus.AuthentucatedUser)
                {
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Login");
                }

                FormsAuthentication.SetAuthCookie(u.UserName, false);
                Session["IsAdmin"] = isAdmin;
                return RedirectToAction("GetView", "Poke");
            }
            else
            {
                return View("Login");
            }
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}