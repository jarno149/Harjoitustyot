using MVCHarjoitustyö.Models;
using MVCHarjoitustyö.ObjectModels;
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
            UserRoleViewModel vm = new UserRoleViewModel();
            using (UserRoleRepository repo = new UserRoleRepository())
            {
                vm.UserRoles = repo.GetAll();
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(CreateUserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                CreateUserRoleViewModel vm = new CreateUserRoleViewModel();
                return View(vm);
            }
            else
            {
                new UserRoleRepository().Store(new UserRole { Name = model.Name });
            }
            return RedirectToAction("Index");
        }
    }
}