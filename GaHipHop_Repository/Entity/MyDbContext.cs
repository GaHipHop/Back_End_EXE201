using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Numerics;
using System.Security.Cryptography;

namespace GaHipHop_Repository.Entity
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("MyDB");
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Admin" },
                new Role { Id = 2, RoleName = "Manager" }
            );

            modelBuilder.Entity<Admin>().HasData(
                new Admin { Id = 1, RoleId = 1, Username = "admin", Password = "$2a$12$W6Fr2O.JNK5pxRlm36q1DeLI.hDM5AhHe..ZnhFhofXKwSNUrMTA.", Email = "admin@example.com", FullName = "Admin User", Phone = "123456789", Address = "Admin Address", Status = true },
                new Admin { Id = 2, RoleId = 2, Username = "manager", Password = "$2a$12$W6Fr2O.JNK5pxRlm36q1DeLI.hDM5AhHe..ZnhFhofXKwSNUrMTA.", Email = "admin@example.com", FullName = "Manager User", Phone = "123456789", Address = "Manager Address", Status = true }
            );

            modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, CategoryName = "Category 1", Status = true },
                new Category { Id = 2, CategoryName = "Category 2", Status = true }
            );

            modelBuilder.Entity<Contact>().HasData(
                new Contact { Id = 1, Phone = "0855005005", Email = "phatdao@gmail.com", Facebook = "https://www.facebook.com/nguyen.rosie.946", Instagram = "https://www.instagram.com/ga.hiphop?fbclid=IwZXh0bgNhZW0CMTAAAR29xxUdp_tZ0TqVPfLq5zQVl72SEG7E0SpmIJY8ED62ZmmsKdaTZrc51O4_aem_-LACUZgsYTo5T1fMMgEhjg", Tiktok = "https://www.tiktok.com/@ga.hiphop?fbclid=IwZXh0bgNhZW0CMTAAAR18hmJoFZrOpcozoCaEq74luSuL4Y84xGSBJ3bnlgjRlYhB3xUaRy4YE3Y_aem_494gWYY8v90XRrvald7BQA", Shoppee = "https://shopee.vn/ga.hiphop" }
            );

            modelBuilder.Entity<Discount>().HasData(
                new Discount { Id = 1, Percent = 0f, ExpiredDate = DateTime.Now.AddMonths(1), Status = true },
                new Discount { Id = 2, Percent = 10.0f, ExpiredDate = DateTime.Now.AddMonths(1), Status = true },
                new Discount { Id = 3, Percent = 15.0f, ExpiredDate = DateTime.Now.AddMonths(1), Status = true },
                new Discount { Id = 4, Percent = 20.0f, ExpiredDate = DateTime.Now.AddMonths(2), Status = true }
            );

            modelBuilder.Entity<Kind>().Property(k => k.Image).IsRequired(false);
            modelBuilder.Entity<Kind>().HasData(
               new Kind { Id = 1, ProductId = 1, ColorName = "Red", Image = "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", Quantity = 50, Status = true },
               new Kind { Id = 2, ProductId = 1, ColorName = "Blue", Image = "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", Quantity = 50, Status = true },
               new Kind { Id = 3, ProductId = 2, ColorName = "White", Image = "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", Quantity = 500, Status = true },
               new Kind { Id = 4, ProductId = 3, ColorName = "Yellow", Image = "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", Quantity = 150, Status = true },
               new Kind { Id = 5, ProductId = 4, ColorName = "Pink", Image = "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", Quantity = 250, Status = true },
               new Kind { Id = 6, ProductId = 5, ColorName = "Purple", Image = "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", Quantity = 550, Status = true },
               new Kind { Id = 7, ProductId = 6, ColorName = "Orange", Image = "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", Quantity = 500, Status = true },
               new Kind { Id = 8, ProductId = 6, ColorName = "Green", Image = "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", Quantity = 500, Status = true }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, AdminId = 1, DiscountId = 1, CategoryId = 1, ProductName = "Figure", ProductDescription = "Nilou", ProductPrice = 50000.00, StockQuantity = 100, CreateDate = DateTime.Now, ModifiedDate = DateTime.Now, Status = true },
                new Product { Id = 2, AdminId = 1, DiscountId = 1, CategoryId = 1, ProductName = "Figure", ProductDescription = "Shenhe", ProductPrice = 1000000.00, StockQuantity = 500, CreateDate = DateTime.Now, ModifiedDate = DateTime.Now, Status = true },
                new Product { Id = 3, AdminId = 1, DiscountId = 2, CategoryId = 1, ProductName = "Figure", ProductDescription = "Shenhe", ProductPrice = 1000000.00, StockQuantity = 150, CreateDate = DateTime.Now, ModifiedDate = DateTime.Now, Status = true },
                new Product { Id = 4, AdminId = 1, DiscountId = 3, CategoryId = 2, ProductName = "Figure", ProductDescription = "Shenhe", ProductPrice = 1000000.00, StockQuantity = 250, CreateDate = DateTime.Now, ModifiedDate = DateTime.Now, Status = true },
                new Product { Id = 5, AdminId = 1, DiscountId = 4, CategoryId = 2, ProductName = "Figure", ProductDescription = "Shenhe", ProductPrice = 1000000.00, StockQuantity = 550, CreateDate = DateTime.Now, ModifiedDate = DateTime.Now, Status = true },
                new Product { Id = 6, AdminId = 1, DiscountId = 4, CategoryId = 2, ProductName = "Figure", ProductDescription = "Shenhe", ProductPrice = 1000000.00, StockQuantity = 1000, CreateDate = DateTime.Now, ModifiedDate = DateTime.Now, Status = true }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, UserId = 1, AdminId = 1, OrderRequirement = "Requirement 1", OrderCode = "ORD001", PaymentMethod = "Credit Card", CreateDate = DateTime.Now, TotalPrice = 100.00, Status = "Confirmed" }
            );

            modelBuilder.Entity<OrderDetails>().HasData(
                new OrderDetails { Id = 1, KindId = 1, OrderId = 1, OrderQuantity = 1, OrderPrice = 100.00 }
            );

            modelBuilder.Entity<UserInfo>().HasData(
                new UserInfo { Id = 1, UserName = "user1", Email = "user1@example.com", Phone = "123456789", Province = "Province 1", Wards = "Wards 1", Address = "Address 1" }
            );
        }

    }
}
