using MVCHarjoitustyö.Models;
using MVCHarjoitustyö.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHarjoitustyö.Controllers
{
    public class UserRoleController : Controller
    {
        // GET: UserRole
        public ActionResult Index()
        {
            UserRoleViewModel mv = new UserRoleViewModel();
            using (UserRoleRepository repo = new UserRoleRepository())
            {
                mv.UserRoles = repo.GetAll();
            }
            return View(mv);
        }
    }
}