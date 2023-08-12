using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataBin.Models;

namespace DataBin.Data
{
    public class DataBinContext : DbContext
    {
        public DataBinContext (DbContextOptions<DataBinContext> options)
            : base(options)
        {
        }

        public DbSet<DataBin.Models.Post> Post { get; set; } = default!;

        public DbSet<DataBin.Models.Comment>? Comment { get; set; }

        public DbSet<DataBin.Models.Topic>? Topic { get; set; }

        public DbSet<DataBin.Models.PostTopic>? PostTopic { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
