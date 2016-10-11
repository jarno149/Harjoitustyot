using MVCHarjoitustyö.ObjectModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCHarjoitustyö
{
    public class DatabaseInitializer
    {
        public static DbModelBuilder InitModelbuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<UserRole>().Property(r => r.Id).HasColumnName("id");
            modelBuilder.Entity<UserRole>().Property(r => r.Name).HasColumnName("name");

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("id");
            modelBuilder.Entity<User>().Property(u => u.FirstName).HasColumnName("firstname");
            modelBuilder.Entity<User>().Property(u => u.Lastname).HasColumnName("lastname");
            modelBuilder.Entity<User>().Property(u => u.UserName).HasColumnName("username");
            modelBuilder.Entity<User>().Property(u => u.PassWord).HasColumnName("password");

            modelBuilder.Entity<Note>().ToTable("Notes");
            modelBuilder.Entity<Note>().Property(s => s.Id).HasColumnName("id");
            modelBuilder.Entity<Note>().Property(s => s.Content).HasColumnName("content");
            modelBuilder.Entity<Note>().Property(s => s.ImageContent).HasColumnName("imagecontent");
            modelBuilder.Entity<Note>().Property(s => s.CreationTimeString).HasColumnName("creationtime");

            modelBuilder.Entity<User>()
                .HasMany(u => u.Notes)
                .WithMany(n => n.Users)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("NoteId");
                    m.ToTable("UserNotesMappings");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                    m.ToTable("UserRolesMappings");
                });

            return modelBuilder;
        }
    }
}