using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

            // Seed data for Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Admin" },
                new Role { Id = 2, RoleName = "Manager" },
                new Role { Id = 3, RoleName = "Staff" }
            );

            // Seed data for Admins
            //string plaintextPassword = "admin"; // Original password
            //string hashedPassword = HashPassword(plaintextPassword); // Encrypt password using SHA-512

            modelBuilder.Entity<Admin>().HasData(
                new Admin { Id = 1, RoleId = 1, Username = "admin", Password = "1", Email = "admin@example.com", FullName = "Admin User", Phone = "123456789", Address = "Admin Address", Status = true }
            );

            // Seed data for Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Category 1", Status = true },
                new Category { Id = 2, CategoryName = "Category 2", Status = true }
            );

            // Seed data for Contacts
            modelBuilder.Entity<Contact>().HasData(
                new Contact { Id = 1, Phone = "123456789", Email = "contact1@example.com", Facebook = "facebook1", Instagram = "instagram1", Tiktok = "tiktok1", Shoppee = "shoppee1" },
                new Contact { Id = 2, Phone = "987654321", Email = "contact2@example.com", Facebook = "facebook2", Instagram = "instagram2", Tiktok = "tiktok2", Shoppee = "shoppee2" }
            );

            // Seed data for Discounts
            modelBuilder.Entity<Discount>().HasData(
                new Discount { Id = 1, Percent = 10.0f, ExpiredDate = DateTime.Now.AddMonths(1), Status = true },
                new Discount { Id = 2, Percent = 20.0f, ExpiredDate = DateTime.Now.AddMonths(2), Status = true }
            );

            // Seed data for Imgs
            modelBuilder.Entity<Kind>().HasData(
               new Kind { Id = 1, ProductId = 1, ColorName = "Red", Image = "image1.jpg", Quantity = 5, Status = true },
               new Kind { Id = 2, ProductId = 1, ColorName = "Blue", Image = "image2.jpg", Quantity = 5, Status = true }
            );

            // Seed data for Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, AdminId = 1, DiscountId = 1, CategoryId = 1, ProductName = "Product 1", ProductDescription = "Description for Product 1", ProductPrice = 100.00, StockQuantity = 10, CreateDate = DateTime.Now, ModifiedDate = DateTime.Now, Status = true }
            );

            // Seed data for Orders
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, UserId = 1, AdminId = 1, OrderRequirement = "Requirement 1", OrderCode = "ORD001", PaymentMethod = "Credit Card", CreateDate = DateTime.Now, TotalPrice = 100.00, Status = "Confirmed" }
            );

            // Seed data for OrderDetails
            modelBuilder.Entity<OrderDetails>().HasData(
                new OrderDetails { Id = 1, KindId = 1, OrderId = 1, OrderQuantity = 1, OrderPrice = 100.00 }
            );

            // Seed data for UserInfos
            modelBuilder.Entity<UserInfo>().HasData(
                new UserInfo { Id = 1, UserName = "user1", Email = "user1@example.com", Phone = "123456789", Province = "Province 1", Wards = "Wards 1", Address = "Address 1" }
            );
        }

        /*private string HashPassword(string password)
        {
            using (var sha512 = new System.Security.Cryptography.SHA512Managed())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha512.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }*/


    }
}
