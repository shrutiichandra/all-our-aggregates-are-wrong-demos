﻿using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Models;

namespace Warehouse.Data
{
    public class WarehouseContext : DbContext
    {
        public static WarehouseContext Create()
        {
            var db = new WarehouseContext();
            db.Database.EnsureCreated();

            return db;
        }

        private WarehouseContext() { }

        public DbSet<StockItem> StockItems { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\all-our-aggregates-are-wrong;Initial Catalog=Warehouse;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockItem>().HasData(Initial.Data());

            var shoppingCartItemEntity = modelBuilder.Entity<ShoppingCartItem>();
            shoppingCartItemEntity.Property(i => i.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }

        internal static class Initial
        {
            internal static StockItem[] Data()
            {
                return new[]
                {
                    new StockItem()
                    {
                        Id = 1,
                        ProductId = 1,
                        Inventory = 4,
                    },
                    new StockItem()
                    {
                        Id = 2,
                        ProductId = 2,
                        Inventory = 0,
                    }
                };
            }

        }
    }
}
