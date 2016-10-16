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
            return null;
        }

        public void AddUserRole(long userId, long roleId)
        {
            using (UserRepository repo = new UserRepository())
            {
                var u = repo.GetById(userId);
                using (UserRoleRepository repo2 = new UserRoleRepository())
                {
                    var r = repo2.GetById(roleId);
                    u.AddRole(r);
                    repo.Update(u);
                }
            }
        }
    }
}