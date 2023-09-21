using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DataBin.Models;
using DataBin.Areas.Identity.Data;

namespace DataBin.Data
{
    public class DataBinContext : IdentityDbContext<DataBinUser>
    {
        public DataBinContext (DbContextOptions<DataBinContext> options)
            : base(options)
        {
        }

        public DbSet<DataBin.Models.Post> Post { get; set; } = default!;
        public DbSet<DataBin.Models.Language> Language { get; set; }
        public DbSet<DataBin.Models.Comment>? Comment { get; set; }
        public DbSet<DataBin.Models.Topic>? Topic { get; set; }
        public DbSet<DataBin.Models.PostTopic>? PostTopic { get; set; }
        public DbSet<DataBin.Models.Star>? Star { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Post>()
                .HasOne<Language>(p => p.Language)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.LanguageId);

            builder.Entity<Star>()
                .HasOne<Post>(s => s.Post)
                .WithMany(s => s.Stars)
                .HasForeignKey(s => s.PostId);

            builder.Entity<PostTopic>()
                .HasOne<Post>(p => p.Post)
                .WithMany(p => p.PostTopics)
                .HasForeignKey(p => p.PostId);

            builder.Entity<PostTopic>()
                .HasOne<Topic>(t => t.Topic)
                .WithMany(t => t.PostTopics)
                .HasForeignKey(t => t.TopicId);

            builder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.PostId);
        }
    }
}
