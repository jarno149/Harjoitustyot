using MVCHarjoitustyö.ObjectModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;

namespace MVCHarjoitustyö.Repositories
{
    public class UserRepository : DbContext
    {
        public DbSet<User> Users;

        public UserRepository() : base(new SQLiteConnection { ConnectionString = ConfigurationManager.ConnectionStrings["sqliteConnection"].ConnectionString }, true)
        {
            Database.SetInitializer<UserRepository>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder = DatabaseInitializer.InitModelbuilder(modelBuilder);
        }

        public ICollection<User> GetAll()
        {
            return Users.Include(u => u.Notes).Include(u => u.Roles).ToList();
        }

        public void Store(User user)
        {
            Users.Add(user);
            this.SaveChanges();
        }
    }
}