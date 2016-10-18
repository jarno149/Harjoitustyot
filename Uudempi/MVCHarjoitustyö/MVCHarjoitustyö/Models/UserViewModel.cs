using MVCHarjoitustyö.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHarjoitustyö.Models
{
    public class AddUserViewModel
    {
        public List<UserRole> Roles { get; set; }
    }
}