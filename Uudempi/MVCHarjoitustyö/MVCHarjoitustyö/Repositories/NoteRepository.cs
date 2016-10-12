using MVCHarjoitustyö.ObjectModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;

namespace MVCHarjoitustyö.Repositories
{
    public class NoteRepository : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public NoteRepository() : base(new SQLiteConnection { ConnectionString = ConfigurationManager.ConnectionStrings["sqliteConnection"].ConnectionString }, true)
        {
            Database.SetInitializer<NoteRepository>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder = DatabaseInitializer.InitModelbuilder(modelBuilder);
        }

        public ICollection<Note> GetAll()
        {
            return Notes.ToList();
        }

        public void Store(Note note)
        {
            Notes.Add(note);
            this.SaveChanges();
        }
    }
}