using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Configurations
{
    public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.Property(x => x.BlogPostContent).IsRequired();
            builder.Property(x => x.AuthorId).IsRequired();
            builder.Property(x => x.CoverImage).IsRequired();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedAt).IsRequired(false);
            builder.HasIndex(x => x.Title);

            builder.HasMany( x => x.BlogPostCategories).WithOne( x => x.BlogPost).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany( x => x.Votes).WithOne( x => x.BlogPost).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany( x => x.BlogPostImages).WithOne( x => x.BlogPost).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Comments).WithOne(x => x.BlogPost).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
