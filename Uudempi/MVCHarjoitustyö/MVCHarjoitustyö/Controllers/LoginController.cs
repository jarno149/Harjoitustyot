using MVCHarjoitustyö.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCHarjoitustyö.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login(string username, string password)
        {
            if(ModelState.IsValid)
            {
                using (UserRepository repo = new UserRepository())
                {
                    if(repo.IsValid(username, password))
                    {
                        FormsAuthentication.SetAuthCookie(username, true);
                     //   FormsAuthentication.RedirectFromLoginPage(username, false);
                        //   return RedirectToAction("index", "note");
                        return RedirectToAction("index", "userrole");
                    }
                }
            }
            return RedirectToAction("index");
        }

        public ActionResult LogOff()
        {
            System.Web.HttpContext.Current.Response.Cookies.Clear();
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            return RedirectToAction("index", "login");
        }
    }
}