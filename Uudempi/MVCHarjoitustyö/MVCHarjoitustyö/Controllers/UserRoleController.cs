using MVCHarjoitustyö.Models;
using MVCHarjoitustyö.ObjectModels;
using MVCHarjoitustyö.Repositories;
using System;
using System.Collections;
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
            UserRoleViewModel vm = new UserRoleViewModel();
            using (UserRoleRepository repo = new UserRoleRepository())
            {
                vm.UserRoles = repo.GetAll();
            }
            return View(vm);
        }

        public ActionResult RoleInfo(UserRole role)
        {
            if (role.Id == 0 && role.Name == null)
                return RedirectToAction("Index");
            RoleInfoViewModel vm = new RoleInfoViewModel();
            using (UserRepository repo = new UserRepository())
            {
                vm.role = role;
                vm.roleUsers = repo.GetByRoleId(role.Id);
            }
            return View(vm);
        }
    }
}