using MVCHarjoitusTyö.ObjectModels;
using MVCHarjoitusTyö.ObjectModels.DTOObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHarjoitusTyö.Controllers
{
    public class RestController : Controller
    {
        // GET: Rest

        [HttpGet]
        public string GetContacts()
        {
            Contact[] c = MVCHarjoitusTyö.ObjectModels.Contact.GetAll();
            ContactDTO[] cDtos = new ContactDTO[c.Length];
            for (int i = 0; i < c.Length; i++)
            {
                cDtos[i] = new ContactDTO(c[i]);
            }
            return JsonConvert.SerializeObject(cDtos);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<string> GetContact(long id)
        {
            MVCHarjoitusTyö.ObjectModels.Contact c = await MVCHarjoitusTyö.ObjectModels.Contact.GetByIdAsync(id);
            return JsonConvert.SerializeObject(new ContactDTO(c));
        }

        [HttpPut]
        public void AddContact(string firstname, string lastname, string phonenumber, string email, string street, string housenumber, string zip, string country)
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
            MVCHarjoitusTyö.ObjectModels.Contact.Store(c);
        }

        [HttpDelete]
        public void DeleteContact(long id)
        {
            MVCHarjoitusTyö.ObjectModels.Contact.RemoveById(id);
        }

        [HttpPut]
        public void UpdateContact(long id, string firstname, string lastname, string phonenumber, string email, string street, string housenumber, string zip, string country)
        {
            Contact c = new Contact
            {
                Id = id,
                FirstName = firstname,
                LastName = lastname,
                PhoneNumber = phonenumber,
                Email = email,
                Address = new Address
                {
                    ContactId = id,
                    Street = street,
                    HouseNumber = housenumber,
                    ZIP = zip,
                    Country = country
                }
            };
            MVCHarjoitusTyö.ObjectModels.Contact.Update(c);
        }
    }
}