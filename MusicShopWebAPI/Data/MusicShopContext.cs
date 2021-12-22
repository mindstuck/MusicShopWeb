using Microsoft.EntityFrameworkCore;
using MusicShopWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShopWebAPI.Data
{
    public class MusicShopContext : DbContext
    {
        public MusicShopContext(DbContextOptions<MusicShopContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCart>().HasKey(s => new { s.OrderId, s.MusicInstrumentId});
        }

        public DbSet<MusicInstrument> Instruments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Warranty> Warranties { get; set; }
    }
}
