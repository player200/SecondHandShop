namespace SecondHandShop.Web.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SecondHandShop.Data.Models;

    public class SecondHandShopDbContext : IdentityDbContext<User>
    {
        public SecondHandShopDbContext(DbContextOptions<SecondHandShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Advertisement> Advertisements { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Category>()
                .HasMany(c => c.Advertisements)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId);

            builder
                .Entity<Advertisement>()
                .HasMany(a => a.Pictures)
                .WithOne(p => p.Advertisement)
                .HasForeignKey(p => p.AdvertisementId);

            builder
                .Entity<User>()
                .HasMany(u => u.Advertisements)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder
                .Entity<User>()
                .HasMany(u => u.MessagesSended)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId);

            builder
                .Entity<User>()
                .HasMany(u => u.MessagesReceived)
                .WithOne(m => m.Receiver)
                .HasForeignKey(m => m.ReceiverId);

            base.OnModelCreating(builder);
        }
    }
}