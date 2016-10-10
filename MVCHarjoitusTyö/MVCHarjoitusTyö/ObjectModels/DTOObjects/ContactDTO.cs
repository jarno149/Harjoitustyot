using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHarjoitusTyö.ObjectModels.DTOObjects
{
    public class ContactDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AddressDTO Address { get; set; }

        public ContactDTO(Contact c)
        {
            this.Id = c.Id;
            this.FirstName = c.FirstName;
            this.LastName = c.LastName;
            this.PhoneNumber = c.PhoneNumber;
            this.Email = c.Email;
            this.Address = new AddressDTO(c.Address);
        }
    }

    public class AddressDTO
    {
        public long ContactId { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string ZIP { get; set; }
        public string HouseNumber { get; set; }

        public AddressDTO(Address a)
        {
            this.ContactId = a.ContactId;
            this.Street = a.Street;
            this.Country = a.Country;
            this.ZIP = a.ZIP;
            this.HouseNumber = a.HouseNumber;
        }
    }
}