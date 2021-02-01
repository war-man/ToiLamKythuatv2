using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToiLamKythuat.Models;

namespace ToiLamKythuat.Context
{
    public class BlogContext : DbContext
    {
        private static readonly string RowVersion = nameof(RowVersion);
        public BlogContext()
        {

        }
        public BlogContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppGlobal.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(e =>
            {
                e.HasKey(x => x.username);
                e.Property(x => x.username).HasMaxLength(1000);
                e.Property(x => x.password).HasMaxLength(1000);
                e.Property(x => x.hashKey).HasMaxLength(1000);
            });

            builder.Entity<Post>(e =>
            {
                e.HasKey(x => x.id);
                e.Property(x => x.id).UseIdentityColumn();
                e.Property(x => x.title).HasMaxLength(1000);
                e.Property(x => x.summary).HasMaxLength(1000);
                e.Property(x => x.description).HasMaxLength(1000);
                e.Property(x => x.keywords).HasMaxLength(1000);
                e.Property(x => x.detail).HasMaxLength(4000);
                e.HasMany(x => x.categories)
                 .WithMany(x => x.posts)
                 .UsingEntity<CategoryPost>(
                    y => y.HasOne(y => y.category).WithMany().HasForeignKey("categoryCode"),
                    y => y.HasOne(y => y.post).WithMany().HasForeignKey("postId"))
                 .ToTable("categoryPost")
                 .HasKey(y => new { y.postId, y.categoryCode });
                e.HasMany(x => x.tags)
                .WithMany(x => x.posts)
                .UsingEntity<TagPost>(
                    y => y.HasOne(y => y.tag).WithMany().HasForeignKey("tagId"),
                    y => y.HasOne(y => y.post).WithMany().HasForeignKey("postId"))
                .ToTable("tagPost")
                .HasKey(x => new { x.postId, x.tagId });
            });

            builder.Entity<Category>(e =>
            {
                e.HasKey(x => x.code);
                e.Property(x => x.name).HasMaxLength(4000);
            });

            builder.Entity<Tag>(e =>
            {
                e.HasKey(x => x.id);
                e.Property(x => x.tagName).HasMaxLength(1000);
            });
        }
    }
}
