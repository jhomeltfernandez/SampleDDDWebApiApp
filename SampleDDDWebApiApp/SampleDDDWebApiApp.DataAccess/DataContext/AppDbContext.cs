using Microsoft.EntityFrameworkCore;
using SampleDDDWebApiApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleDDDWebApiApp.DataAccess.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<UserMoney> UserMoney { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().Property(o => o.Ratio).HasColumnType("decimal(18,6)");
            modelBuilder.Entity<UserMoney>().Property(o => o.Amount).HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
