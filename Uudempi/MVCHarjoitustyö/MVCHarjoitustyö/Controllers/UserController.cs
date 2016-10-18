using MVCHarjoitustyö.Models;
using MVCHarjoitustyö.ObjectModels;
using MVCHarjoitustyö.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHarjoitustyö.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public string SearchUsers(string query)
        {
            var users = new List<User>();
            if(query != null)
            {
                using (UserRepository repo = new UserRepository())
                {
                    users = repo.Search(query).ToList();
                }
            }
            return JsonConvert.SerializeObject(users.ToArray());
        }

        public string GetUserById(long id)
        {
            using (UserRepository repo = new UserRepository())
            {
                return JsonConvert.SerializeObject(repo.GetById(id));
            }
        }

        public ActionResult AddUser()
        {
            AddUserViewModel vm = new AddUserViewModel();
            using (UserRoleRepository repo = new UserRoleRepository())
            {
                vm.Roles = repo.GetAll().ToList();
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddUser(string firstName, string lastName, string passWord, string[] roles)
        {
            if (firstName == null || lastName == null || User == null || passWord == null)
                return RedirectToAction("Index", "UserRole");
            
            var u = new User { FirstName = firstName, LastName = lastName, PassWord = passWord };
            string userName = "";
            if (firstName.Length > 3)
                userName = firstName.Substring(0, 3);
            else
                userName = firstName;
            if (lastName.Length > 5)
                userName += lastName.Substring(0, 5);
            else
                userName += lastName;
            u.UserName = userName;
            string roleString = "";
            if (roles != null)
            {
                foreach (var s in roles)
                {
                    roleString += "-" + s;
                }
                roleString += "-";
                u.RoleIdsString = roleString;
            }
            using (UserRepository repo = new UserRepository())
            {
                repo.Store(u);
            }

            return RedirectToAction("Index", "UserRole");
        }

        public string AddUserRole(long userId, long roleId)
        {
            using (UserRepository repo = new UserRepository())
            {
                var u = repo.GetById(userId);
                using (UserRoleRepository repo2 = new UserRoleRepository())
                {
                    var r = repo2.GetById(roleId);
                    bool success = u.AddRole(r);
                    repo.Update(u);
                    if (!success)
                        return "";
                    else return JsonConvert.SerializeObject(u);
                }
            }
        }
    }
}