using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using SQLite.CodeFirst;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCHarjoitusTyö.ObjectModels
{
    public class ContactContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public ContactContext() : base(new System.Data.SQLite.SQLiteConnection { ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["sqliteConnection"].ConnectionString }, true)
        {
            Database.SetInitializer<ContactContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasOptional(s => s.Address).WithRequired(a => a.Contact);
            modelBuilder.Entity<Address>().HasKey(x => x.ContactId);

            modelBuilder.Entity<Contact>().ToTable("contacts");
            modelBuilder.Entity<Contact>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Contact>().Property(x => x.FirstName).HasColumnName("firstname");
            modelBuilder.Entity<Contact>().Property(x => x.LastName).HasColumnName("lastname");
            modelBuilder.Entity<Contact>().Property(x => x.PhoneNumber).HasColumnName("phonenumber");
            modelBuilder.Entity<Contact>().Property(x => x.Email).HasColumnName("email");

            modelBuilder.Entity<Address>().ToTable("addresses");
            modelBuilder.Entity<Address>().Property(x => x.ContactId).HasColumnName("contactid");
            modelBuilder.Entity<Address>().Property(x => x.Street).HasColumnName("street");
            modelBuilder.Entity<Address>().Property(x => x.Country).HasColumnName("country");
            modelBuilder.Entity<Address>().Property(x => x.ZIP).HasColumnName("zip");
            modelBuilder.Entity<Address>().Property(x => x.HouseNumber).HasColumnName("housenumber");
            base.OnModelCreating(modelBuilder);
        }
    }
    
    public class Contact
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual Address Address { get; set; }

        public Contact()
        {

        }
        
        public static Contact[] GetAll()
        {
            using (ContactContext cc = new ContactContext())
            {
                var items = cc.Contacts.ToList().ToArray();
                return items;
            }
        }

        public static void Store(Contact c)
        {
            using (ContactContext cc = new ContactContext())
            {
                cc.Contacts.Add(c);
                cc.SaveChanges();
            }
        }

        public static void Remove(Contact c)
        {
            using (ContactContext cc = new ContactContext())
            {
                cc.Contacts.Remove(c);
                cc.SaveChanges();
            }
        }

        public static async System.Threading.Tasks.Task<Contact> GetByIdAsync(string id)
        {
            long lid = long.Parse(id);
            using (ContactContext cc = new ContactContext())
            {
                Contact c = await cc.Contacts.Include(a => a.Address).FirstOrDefaultAsync(x => x.Id == lid);
                return c;
            }
            return null;
        }

        public static void RemoveById(string id)
        {
            long lid = long.Parse(id);
            using (ContactContext cc = new ContactContext())
            {
                Contact c = cc.Contacts.FirstOrDefault(x => x.Id == lid);
                cc.Contacts.Remove(c);
                cc.SaveChanges();
            }
        }
    }

    public class Address
    {
        [ForeignKey("Contact"), Key]
        public long ContactId { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string ZIP { get; set; }
        public string HouseNumber { get; set; }
        public virtual Contact Contact {get;set;}

        public Address()
        {

        }
    }
}