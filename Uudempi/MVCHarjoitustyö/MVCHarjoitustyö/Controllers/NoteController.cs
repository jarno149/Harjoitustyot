using MVCHarjoitustyö.Models;
using MVCHarjoitustyö.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MVCHarjoitustyö.Controllers
{
    public class NoteController : Controller
    {
        // GET: Note
        [Authorize]
        public ActionResult Index()
        {
            string username = User.Identity.GetUserName();
            long userId = 0;
            using (UserRepository repo = new UserRepository())
            {
                var user = repo.GetByUsername(username);
                userId = user.Id;
            }

            NoteViewModel vm = new NoteViewModel();
            using (NoteRepository repo = new NoteRepository())
            {
                vm.notes = repo.GetByUserId(userId).ToList();
            }
            return View(vm);
        }
        
        [Authorize]
        public ActionResult AddNote(string imgData, string[] userIds)
        {
            if(imgData != null)
            {
                string datatype = imgData.Split(';')[0];
                if(datatype.ToLower().Equals("data:image/png"))
                {
                    int startP = imgData.IndexOf("base64,") + "base64,".Length;
                    string b64 = imgData.Substring(startP);
                    var bytes = Convert.FromBase64String(b64);
                    using (NoteRepository repo = new NoteRepository())
                    {
                        string userIdString = "";
                        foreach (var str in userIds)
                        {
                            userIdString += "-" + str;
                        }
                        userIdString += "-";
                        repo.Store(new ObjectModels.Note { ImageContent = bytes, UserIdString = userIds != null ? userIdString : null });
                    }
                    return RedirectToAction("index", "userrole");
                }
            }
            return View();
        }
    }
}