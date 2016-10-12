using MVCHarjoitustyö.ObjectModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace MVCHarjoitustyö.Repositories
{
    public class UserRoleRepository : DbContext
    {
        public DbSet<UserRole> Roles { get; set; }

        public UserRoleRepository() : base(new SQLiteConnection { ConnectionString = ConfigurationManager.ConnectionStrings["sqliteConnection"].ConnectionString }, true)
        {
            Database.SetInitializer<UserRoleRepository>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder = DatabaseInitializer.InitModelbuilder(modelBuilder);
        }

        public UserRole GetById(long id)
        {
            return Roles.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<UserRole> GetAll()
        {
            return Roles.ToList();
        }

        public void Store(UserRole userRole)
        {
            Roles.Add(userRole);
            this.SaveChanges();
        }
    }
}