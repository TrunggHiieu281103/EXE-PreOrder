using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistence.Contexts
{
    public partial class EXE_PreOrderContext : DbContext
    {
        public EXE_PreOrderContext()
        {
        }

        public EXE_PreOrderContext(DbContextOptions<EXE_PreOrderContext> options)
            : base(options)
        {
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(local);Database=EXE_PreOrder;Uid=sa;Pwd=12345;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brands>(entity =>
            {
                entity.ToTable("brands");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("category_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<OrderProducts>(entity =>
            {
                entity.ToTable("order_products");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.DepositPrice)
                    .HasColumnName("deposit_price")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.DepositPrice)
                    .HasColumnName("deposit_price")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsPreorder).HasColumnName("is_preorder");

                entity.Property(e => e.ShippingFee)
                    .HasColumnName("shipping_fee")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.TotalPrice)
                    .HasColumnName("total_price")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.UserAddress).HasColumnName("user_address");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.ToTable("payments");

                entity.HasIndex(e => e.PaymentCode)
                    .HasName("UQ__payments__7234C6E3E91A2373")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PaymentCode)
                    .IsRequired()
                    .HasColumnName("payment_code")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentStatus)
                    .IsRequired()
                    .HasColumnName("payment_status")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentType)
                    .IsRequired()
                    .HasColumnName("payment_type")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ProductAssets>(entity =>
            {
                entity.ToTable("product_assets");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MediaKey)
                    .IsRequired()
                    .HasColumnName("media_key")
                    .HasMaxLength(255);

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.PublicId)
                    .HasColumnName("public_id")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ProductCommentAssets>(entity =>
            {
                entity.ToTable("product_comment_assets");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MediaKey)
                    .IsRequired()
                    .HasColumnName("media_key")
                    .IsUnicode(false);

                entity.Property(e => e.ProductCommentId).HasColumnName("product_comment_id");

                entity.Property(e => e.PublicId)
                    .HasColumnName("public_id")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ProductComments>(entity =>
            {
                entity.ToTable("product_comments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.ProductCode)
                    .HasName("UQ__products__AE1A8CC49C53F1D6")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsPreOrder).HasColumnName("is_pre_order");

                entity.Property(e => e.OpenedAt).HasColumnName("opened_at");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasColumnName("product_code")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDetails).HasColumnName("product_details");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<RefreshTokens>(entity =>
            {
                entity.ToTable("refresh_tokens");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RefreshToken)
                    .IsRequired()
                    .HasColumnName("refresh_token")
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Shippings>(entity =>
            {
                entity.ToTable("shippings");

                entity.HasIndex(e => e.TrackingNumber)
                    .HasName("UQ__shipping__B2C338B7835D7975")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CarrierName)
                    .HasColumnName("carrier_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.DeliveredAt).HasColumnName("delivered_at");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.EstimatedDeliveryAt).HasColumnName("estimated_delivery_at");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ShippedAt).HasColumnName("shipped_at");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('pending')");

                entity.Property(e => e.TrackingNumber)
                    .HasColumnName("tracking_number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<UserAddresses>(entity =>
            {
                entity.ToTable("user_addresses");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressDetail)
                    .IsRequired()
                    .HasColumnName("address_detail")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.District)
                    .HasColumnName("district")
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDefault).HasColumnName("is_default");

                entity.Property(e => e.Province)
                    .HasColumnName("province")
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Ward)
                    .HasColumnName("ward")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.ToTable("user_roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__users__AB6E6164FA6EC6A6")
                    .IsUnique();

                entity.HasIndex(e => e.Phone)
                    .HasName("UQ__users__B43B145F1AD2F825")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AvatarKey)
                    .HasColumnName("avatar_key")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AvatarPublicId)
                    .HasColumnName("avatar_public_id")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsEnableTwoFactor)
                    .HasColumnName("is_enable_two_factor")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsFirstLogin)
                    .HasColumnName("is_first_login")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(datediff(second,'1970-01-01',getutcdate()))");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
