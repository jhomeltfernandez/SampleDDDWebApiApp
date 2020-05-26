using SampleDDDWebApiApp.DataAccess.DataContext;
using SampleDDDWebApiApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleDDDWebApiApp.DataAccess.Seeder
{
    public static class DataSeeder
    {
        public static void SeedData(AppDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            context.Users.Add(new User { UserName = "User1" });
            context.Users.Add(new User { UserName = "User2" });
            context.Users.Add(new User { UserName = "User3" });
            context.SaveChanges();

            context.Currencies.Add(new Currency { Name = "EUR", Ratio = 0.91707m });
            context.Currencies.Add(new Currency { Name = "USD", Ratio = 1.0904m });
            context.SaveChanges();
        }

    }
}
