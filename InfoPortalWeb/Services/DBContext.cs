using InfoPortalWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InfoPortalWeb.Services
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FeedbackInfo> Feedbacks { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<FAQ> FAQs { get; set; }

    }
}
