using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHarjoitustyö.ObjectModels
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<Note> Notes { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + Lastname;
        }
    }
}