using CRUD_Operations.Controllers;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Models;

namespace CRUD_Operations.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPermissions>()
                .HasKey(x => new { x.UserId, x.PermissionId });
        }
        public DbSet<Product> Products { get; set; }    
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<User> Users { get; set; } // تعريف الـ DbSet الخاصة بالمستخدمين
        public DbSet<UserAllergy> UserAllergies { get; set; } // تعريف الـ DbSet الخاصة بحساسيات المستخدمين
        public DbSet<Users3> users3s { get; set; }
        public DbSet<UserPermissions> Permissions { get; set; }

    }
}
