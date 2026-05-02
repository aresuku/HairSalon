using Microsoft.EntityFrameworkCore;
using Models;
namespace Data
{
    public static class DbSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Master>().HasData(
                new Master { MasterId = 1, FirstName = "Сатору", LastName = "Судзуки", Experience = "100 Лет", Gender = "М", Description = "по умолчанию 1", Email = "YGGDRASIL@gmail.com" },
                new Master { MasterId = 2, FirstName = "Айнз", LastName = "ОалГоун", Experience = "100 Лет", Gender = "М", Description = "по умолчанию 2", Email = "Overlord@gmail.com" },
                new Master { MasterId = 3, FirstName = "Таня", LastName = "Дёгурешафф", Experience = "100 Лет", Gender = "Ж", Description = "по умолчанию 3", Email = "Degurechaff@gmail.com" }
            );
            modelBuilder.Entity<Service>().HasData(
                new Service { ServiceId = 1, Name = "Стрижка", Price = 1000, Description = "по умолчанию 1" },
                new Service { ServiceId = 2, Name = "Окрашивание", Price = 2000, Description = "по умолчанию 2" },
                new Service { ServiceId = 3, Name = "Восстановление", Price = 3000, Description = "по умолчанию 3" }
            );
            modelBuilder.Entity<Reception>().HasData(
                new Reception { Id = 1, Time = new DateTime(2138, 12, 1, 23, 59, 0), MasterId = 1, ServiceId = 1 },
                new Reception { Id = 2, Time = new DateTime(2138, 12, 2, 23, 59, 0), MasterId = 2, ServiceId = 2 },
                new Reception { Id = 3, Time = new DateTime(2138, 12, 3, 23, 59, 0), MasterId = 3, ServiceId = 3 }
            );
        }
    }
}