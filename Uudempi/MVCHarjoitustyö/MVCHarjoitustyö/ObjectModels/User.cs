using MVCHarjoitustyö.Repositories;
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
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string RoleIdsString { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }

        public bool AddRole(UserRole role)
        {
            return AddRole(role.Id);
        }

        public bool AddRole(long roleId)
        {
            if(this.RoleIdsString != null && !this.RoleIdsString.Contains("-" + roleId + "-"))
            {
                if (this.RoleIdsString.EndsWith("-"))
                    this.RoleIdsString = this.RoleIdsString.Remove(this.RoleIdsString.Length - 1);
                this.RoleIdsString += "-" + roleId + "-";
                return true;
            }
            else
            {
                this.RoleIdsString = "-" + roleId + "-";
                return true;
            }
            return false;
        }

        public void RemoveRole(UserRole role)
        {
            RemoveRole(role.Id);
        }

        public void RemoveRole(long roleId)
        {
            if (this.RoleIdsString != null)
                this.RoleIdsString = this.RoleIdsString.Replace("-" + roleId + "-", "");
        }

        public UserRole[] GetRoles()
        {
            if (this.RoleIdsString != null)
            {
                string[] rs = this.RoleIdsString.Split('-');
                List<UserRole> roles = new List<UserRole>();
                using (UserRoleRepository repo = new UserRoleRepository())
                {
                    foreach (string splittedPart in rs)
                    {
                        long o = 0;
                        if(long.TryParse(splittedPart, out o))
                        {
                            UserRole r = repo.GetById(o);
                            if (r != null)
                                roles.Add(r);
                        }
                    }
                    return roles.ToArray();
                }
            }
            return null;
        }
    }
}