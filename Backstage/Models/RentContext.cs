using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Backstage.Models
{
    public partial class RentContext : DbContext
    {
        public RentContext()
        {
        }

        public RentContext(DbContextOptions<RentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administer> Administers { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<BranchStore> BranchStores { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<DeliveryOption> DeliveryOptions { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<SignWay> SignWays { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=bs-2021-hsz-summer.database.windows.net;Database=FirstGroup;persist security info=True;user id=bs;password=3U7hzk5f8Bzm;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Administer>(entity =>
            {
                entity.ToTable("Administer");

                entity.Property(e => e.AdministerId).HasColumnName("AdministerID");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("帳號");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("密碼");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blog");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.BlogContent).IsRequired();

                entity.Property(e => e.BlogTitle).IsRequired();

                entity.Property(e => e.MainImgTitle).IsRequired();

                entity.Property(e => e.MainImgUrl).IsRequired();

                entity.Property(e => e.PostDate).HasColumnType("datetime");

                entity.Property(e => e.Preview).IsRequired();
            });

            modelBuilder.Entity<BranchStore>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.ProductId });

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasComment("顧客");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(8)
                    .HasColumnName("ProductID")
                    .HasComment("商品");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carts_Members");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carts_Products");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId)
                    .HasMaxLength(3)
                    .HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(20);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_Member");
            });

            modelBuilder.Entity<DeliveryOption>(entity =>
            {
                entity.HasKey(e => e.DeliverId);

                entity.Property(e => e.DeliverId).HasColumnName("DeliverID");

                entity.Property(e => e.Description).HasMaxLength(150);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .HasComment("帳號");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasComment("不活躍、凍結、封號");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasComment("地址(可能可以就近推薦)");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasComment("生日");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("信箱");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("姓名");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(50)
                    .HasComment("密碼");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasComment("手機");

                entity.Property(e => e.ProfilePhotoUrl)
                    .HasMaxLength(100)
                    .HasComment("頭像的來源");

                entity.Property(e => e.SignWayId)
                    .HasColumnName("SignWayID")
                    .HasComment("登入方式");

                entity.HasOne(d => d.SignWay)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.SignWayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Members_SignWay");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasComment("流水號");

                entity.Property(e => e.DeliverId)
                    .HasColumnName("DeliverID")
                    .HasComment("運送方式");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasComment("顧客");

                entity.Property(e => e.OrderDate).HasComment("日期");

                entity.Property(e => e.OrderStatusId)
                    .HasColumnName("OrderStatusID")
                    .HasComment("0作廢，1待付款，2付款中，3已付款");

                entity.Property(e => e.StoreId)
                    .HasColumnName("StoreID")
                    .HasComment("自取門市");

                entity.HasOne(d => d.Deliver)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_DeliveryOptions");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Member");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_BranchStores");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(8)
                    .HasColumnName("ProductID");

                entity.Property(e => e.DailyRate)
                    .HasColumnType("money")
                    .HasComment("單日租費");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasComment("租借到期日");

                entity.Property(e => e.GoodsStatus).HasComment("0已歸還，1待出貨，2已出貨，3已到貨，4已取貨");

                entity.Property(e => e.Notify).HasComment("0是未通知 1是已通知");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasComment("租借起始日");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("money")
                    .HasComment("總價");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Products");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .HasMaxLength(8)
                    .HasColumnName("ProductID")
                    .HasComment("");

                entity.Property(e => e.DailyRate)
                    .HasColumnType("money")
                    .HasComment("單日租費");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasComment("描述");

                entity.Property(e => e.Discontinuation).HasComment("停用否(可用下架日來判斷)");

                entity.Property(e => e.LaunchDate)
                    .HasColumnType("datetime")
                    .HasComment("上架日期");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasComment("更新時間");

                entity.Property(e => e.WithdrawalDate)
                    .HasColumnType("datetime")
                    .HasComment("下架日期");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.ImageId })
                    .HasName("PK_ProductImages_1");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(8)
                    .HasColumnName("ProductID");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductImages_Products");
            });

            modelBuilder.Entity<SignWay>(entity =>
            {
                entity.ToTable("SignWay");

                entity.Property(e => e.SignWayId).HasColumnName("SignWayID");

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.SubCategoryId });

                entity.ToTable("SubCategory");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(3)
                    .HasColumnName("CategoryID");

                entity.Property(e => e.SubCategoryId)
                    .HasMaxLength(2)
                    .HasColumnName("SubCategoryID");

                entity.Property(e => e.SubCategoryName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCategory_Categories");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
