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
        public DbSet<User> Users { get; set; }

        public UserRepository() : base(new SQLiteConnection { ConnectionString = ConfigurationManager.ConnectionStrings["sqliteConnection"].ConnectionString }, true)
        {
            Database.SetInitializer<UserRepository>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder = DatabaseInitializer.InitModelbuilder(modelBuilder);
        }

        public ICollection<User> Search(string query)
        {
            query = query.ToLower();
            return Users.Where(u => u.FirstName.ToLower().Contains(query) || u.LastName.ToLower().Contains(query) || u.UserName.ToLower().Contains(query)).ToList();
        }

        public User GetById(long id)
        {
            return Users.Where(u => u.Id.Equals(id)).FirstOrDefault();
        }

        public ICollection<User> GetByRoleId(long roleId)
        {
            return Users.Where(u => u.RoleIdsString.Contains("-"+roleId+"-")).ToList();
        }

        public ICollection<User> GetAll()
        {
            return Users.ToList();
        }

        public User GetByUsername(string username)
        {
            return Users.Where(x => x.UserName == username).FirstOrDefault();
        }

        public void Update(User user)
        {
            var u = Users.Where(x => x.Id == user.Id).FirstOrDefault();
            u = user;
            this.SaveChanges();
        }

        public void Store(User user)
        {
            Users.Add(user);
            this.SaveChanges();
        }

        public bool IsValid(string username, string password)
        {
            var user = Users.Where(x => x.UserName == username && x.PassWord == password).FirstOrDefault();
            if (user == null)
                return false;
            return true;
        }
    }
}