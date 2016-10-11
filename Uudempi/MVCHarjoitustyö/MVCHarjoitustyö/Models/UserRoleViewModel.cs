using MVCHarjoitustyö.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHarjoitustyö.Models
{
    public class UserRoleViewModel
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}