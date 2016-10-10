using MVCHarjoitusTyö.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHarjoitusTyö.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Contact> Contacts { get; set; }
    }

    public class AddViewModel
    {

    }

    public class EditViewModel
    {
        public Contact SelectedContact { get; set; }
    }
}