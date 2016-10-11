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
    public class NoteRepository : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

        public NoteRepository() : base(new SQLiteConnection { ConnectionString = ConfigurationManager.ConnectionStrings["sqliteConnection"].ConnectionString }, true)
        {
            Database.SetInitializer<NoteRepository>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().HasOptional(s => s.Users);
            modelBuilder.Entity<User>().HasKey(s => s.NoteId);
            modelBuilder.Entity<User>().HasOptional(s => s.Notes);
            modelBuilder.Entity<Note>().HasKey(s => s.UserId);

            modelBuilder.Entity<Note>().ToTable("Notes");
            modelBuilder.Entity<Note>().Property(s => s.Id).HasColumnName("id");
            modelBuilder.Entity<Note>().Property(s => s.UserId).HasColumnName("userid");
            modelBuilder.Entity<Note>().Property(s => s.Content).HasColumnName("content");
            modelBuilder.Entity<Note>().Property(s => s.ImageContent).HasColumnName("imagecontent");

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().Property(s => s.Id).HasColumnName("id");
            modelBuilder.Entity<User>().Property(s => s.NoteId).HasColumnName("noteid");
            modelBuilder.Entity<User>().Property(s => s.FirstName).HasColumnName("firstname");
            modelBuilder.Entity<User>().Property(s => s.Lastname).HasColumnName("lastname");

            modelBuilder.Entity<Note>().HasMany<User>(s => s.Users).WithMany(s => s.Notes).Map(m => { m.MapLeftKey("noterefid"); m.MapRightKey("userrefid"); m.ToTable("NoteUsers"); })
        }

        public static IEnumerable<Note> GetAll()
        {
            using (NoteRepository nr = new NoteRepository())
            {
                return nr.Notes.ToList();
            }
        }

        public static void testi()
        {
            Note n = new Note();
            n.Content = "Content!";
            byte[] bytes = new byte[50];
            Random r = new Random();
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)r.Next(0, 255);
            }
            n.ImageContent = bytes;
            n.Users = new List<User>();
            n.Users.Add(new User { FirstName = "Seppo", Lastname = "JeeJee" });
            n.Users.ElementAt(0).Notes = new List<Note>();
            n.Users.ElementAt(0).Notes.Add(n);
            Store(n);
        }

        public static void Store(Note n)
        {
            using (NoteRepository nr = new NoteRepository())
            {
                nr.Notes.Add(n);
            }
        }
    }
}