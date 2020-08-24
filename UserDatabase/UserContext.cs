using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UserDatabase.Models;

namespace UserDatabase
{
    public class UserContext : DbContext
    {
        public UserContext()
        {

        }
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: get connection string from environment
            string ctxStr = "Server=localhost\\SQLEXPRESS;Database=cstructure_user_dev;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(ctxStr);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.UserEmail)
                .IsUnique();
        }
        public DbSet<User> User { get; set; }
    }
}
