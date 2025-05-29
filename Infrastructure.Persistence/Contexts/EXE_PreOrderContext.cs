using System;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistence.Contexts
{
    public partial class EXE_PreOrderContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;
        public EXE_PreOrderContext(DbContextOptions<EXE_PreOrderContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }

        public virtual DbSet<Brands> Brands { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<OrderProducts> OrderProducts { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<ProductAssets> ProductAssets { get; set; }
        public virtual DbSet<ProductCommentAssets> ProductCommentAssets { get; set; }
        public virtual DbSet<ProductComments> ProductComments { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<RefreshTokens> RefreshTokens { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Shippings> Shippings { get; set; }
        public virtual DbSet<UserAddresses> UserAddresses { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var currentTimeMillis = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = currentTimeMillis;
                        entry.Entity.UpdatedAt = currentTimeMillis;
                        entry.Entity.Version = 1; // Khởi tạo version = 1
                        entry.Entity.IsActive = true; // Mặc định là true nếu chưa được gán
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = currentTimeMillis;
                        entry.Entity.Version += 1; // Tăng version mỗi lần cập nhật
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USERS
            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Phone);

            // USER_ADDRESSES
            modelBuilder.Entity<UserAddresses>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAddresses)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ROLES
            modelBuilder.Entity<Roles>()
                .Property(r => r.RoleName)
                .IsRequired();

            // USER_ROLES
            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // REFRESH TOKENS
            modelBuilder.Entity<RefreshTokens>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // PRODUCTS
            modelBuilder.Entity<Products>()
                .HasIndex(p => p.ProductCode).IsUnique();

            modelBuilder.Entity<Products>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Products>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId);

            // PRODUCT_ASSETS
            modelBuilder.Entity<ProductAssets>()
                .HasOne(pa => pa.Product)
                .WithMany(p => p.ProductAssets)
                .HasForeignKey(pa => pa.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // PRODUCT_COMMENTS
            modelBuilder.Entity<ProductComments>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductComments)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductComments>()
                .HasOne(pc => pc.Order)
                .WithMany(o => o.ProductComments)
                .HasForeignKey(pc => pc.OrderId);

            modelBuilder.Entity<ProductComments>()
                .HasOne(pc => pc.User)
                .WithMany(u => u.ProductComments)
                .HasForeignKey(pc => pc.UserId);

            // PRODUCT_COMMENT_ASSETS
            modelBuilder.Entity<ProductCommentAssets>()
                .HasOne(pca => pca.ProductComment)
                .WithMany(pc => pc.ProductCommentAssets)
                .HasForeignKey(pca => pca.ProductCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            // ORDERS
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Address)
                .WithMany(ua => ua.Orders)
                .HasForeignKey(o => o.UserAddress);

            // ORDER_PRODUCTS
            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);

            // PAYMENTS
            modelBuilder.Entity<Payments>()
                .HasIndex(p => p.PaymentCode).IsUnique();

            modelBuilder.Entity<Payments>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId);

            // SHIPPINGS
            modelBuilder.Entity<Shippings>()
                .HasIndex(s => s.TrackingNumber).IsUnique();

            modelBuilder.Entity<Shippings>()
                .HasOne(s => s.Order)
                .WithOne(o => o.Shipping)
                .HasForeignKey<Shippings>(s => s.OrderId);
        }


        
    }
}
