using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHarjoitustyö.ObjectModels
{
    public class User
    {
        public long Id { get; set; }
        public long NoteId { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public virtual ICollection<Note> Notes { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + Lastname;
        }
    }
}