using MVCHarjoitustyö.Models;
using MVCHarjoitustyö.ObjectModels;
using MVCHarjoitustyö.Repositories;
using Newtonsoft.Json;
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
        [Authorize]
        public ActionResult Index()
        {
            UserRoleViewModel vm = new UserRoleViewModel();
            using (UserRoleRepository repo = new UserRoleRepository())
            {
                vm.UserRoles = repo.GetAll();
            }
            return View(vm);
        }

        [Authorize]
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

        [Authorize]
        public ActionResult RemoveUserRole(long userId, long roleId)
        {
            if (userId < 1  && roleId < 1)
                return RedirectToAction("Index");

            using (UserRepository repo = new UserRepository())
            {
                User u = repo.GetById(userId);
                u.RemoveRole(roleId);
                repo.Update(u);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public string RoleUsers(long roleId)
        {
            using (UserRepository repo = new UserRepository())
            {
                List<User> users = repo.GetByRoleId(roleId).ToList();
                return JsonConvert.SerializeObject(users);
            }
        }
    }
}