using MVCHarjoitusTyö.Models;
using MVCHarjoitusTyö.ObjectModels;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHarjoitusTyö.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            Contact[] contacts = Contact.GetAll();

            IndexViewModel vm = new IndexViewModel()
            {
                Contacts = contacts
            };
            return View(vm);
        }

        public ActionResult Remove(long id)
        {
            Contact.RemoveById(id);
            return RedirectToAction("Index");
        }

        public async System.Threading.Tasks.Task<ActionResult> Edit(long id)
        {
            Contact c = await Contact.GetByIdAsync(id);
            if (c == null)
            {
                return null;
            }
            EditViewModel vm = new EditViewModel
            {
                SelectedContact = c
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Update(string action, string id, string firstname, string lastname, string phonenumber, string email, string street, string housenumber, string zip, string country)
        {
            if (action == "Save")
            {
                Contact c = new Contact
                {
                    Id = long.Parse(id),
                    FirstName = firstname,
                    LastName = lastname,
                    PhoneNumber = phonenumber,
                    Email = email,
                    Address = new Address
                    {
                        ContactId = long.Parse(id),
                        Street = street,
                        HouseNumber = housenumber,
                        ZIP = zip,
                        Country = country
                    }
                };
                Contact.Update(c);
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult Add()
        {
            AddViewModel vm = new AddViewModel();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(string action, string firstname, string lastname, string phonenumber, string email, string street, string housenumber, string zip, string country)
        {
            if(action == "save" && firstname != null && lastname != null)
            {
                Contact c = new Contact
                {
                    FirstName = firstname,
                    LastName = lastname,
                    PhoneNumber = phonenumber,
                    Email = email,
                    Address = new Address
                    {
                        Street = street,
                        HouseNumber = housenumber,
                        ZIP = zip,
                        Country = country
                    }
                };

                Contact.Store(c);
            }
            return RedirectToAction("Index");
        }

    }
}