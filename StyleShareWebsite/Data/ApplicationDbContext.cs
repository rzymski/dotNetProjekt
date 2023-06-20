using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Models;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace StyleShareWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #region DbSets

        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<AchievementUser> AchievementsUsers { get; set; }
        public DbSet<ColorSet> ColorSets { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Postable> Postables { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<StylePack> StylePacks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<StyleStylePack> StyleStylePacks { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>().HasKey(a => new { a.Id });
            builder.Entity<Like>().HasOne(a => a.User).WithMany(a => a.Likes).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Like>().HasOne(a => a.Postable).WithMany(a => a.Likes).HasForeignKey(a => a.PostableId).OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
    }
}