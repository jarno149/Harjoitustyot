using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHarjoitustyö.ObjectModels
{
    public class UserRole
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}