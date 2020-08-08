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
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=cstructure_user_dev;Trusted_Connection=True;");
        }
        public DbSet<User> User { get; set; }
    }
}
