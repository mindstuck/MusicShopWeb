using MusicShopWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShopWebAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MusicShopContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Instruments.Any())
            {
                return;   // DB has been seeded
            }

            IEnumerable<MusicInstrument> instruments = new MusicInstrument[]
            {
                new MusicInstrument {Material = "Metal", Name = "Труба", Price = 400.5m, WarrantySupported = false},
                new MusicInstrument {Material = "Wood", Name = "Гитара", Price = 100, WarrantySupported = true},
                new MusicInstrument {Material = "Wood", Name = "Трамбон", Price = 350.99m, WarrantySupported = false},
                new MusicInstrument {Material = "Wood", Name = "Виолончель", Price = 800m, WarrantySupported = true},
                new MusicInstrument {Material = "Metal", Name = "Кларнет", Price = 340m, WarrantySupported = false},
                new MusicInstrument {Material = "Metal", Name = "Ударная установка", Price = 690.4m, WarrantySupported = false},
                new MusicInstrument {Material = "Metal", Name = "Скрипка", Price = 1000, WarrantySupported = true},
                new MusicInstrument {Material = "Metal", Name = "Флейта металлическая", Price = 599.99m, WarrantySupported = false},
                new MusicInstrument {Material = "Wood", Name = "Ксилофон", Price = 105m, WarrantySupported = true},
                new MusicInstrument {Material = "Wood", Name = "Фортепиано", Price = 2200m, WarrantySupported = false},
                new MusicInstrument {Material = "Wood", Name = "Флейта деревянна", Price = 478m, WarrantySupported = false}
            };

            foreach (MusicInstrument s in instruments)
            {
                context.Instruments.Add(s);
            }

            context.SaveChanges();

            IEnumerable<User> users = new User[]
            {
                new User() { Name = "John", Email = "john@music.com", Password = "12345"},
                new User() { Name = "Mac", Email = "mac@music.com", Password = "12345"},
                new User() { Name = "Lucy", Email = "lucy@music.com", Password = "12345"},
                new User() { Name = "Walter", Email = "walter@music.com", Password = "12345"},
                new User() { Name = "Xanak", Email = "xanax@music.com", Password = "12345"},
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();
        }
    }
}
