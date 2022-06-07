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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Username).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(100);
            builder.Property( x => x.Email).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Active).HasDefaultValue(1);
            builder.Property(x => x.RoleId).HasDefaultValue(2);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedAt).IsRequired(false);
            builder.Property(x => x.DeletedAt).IsRequired(false);
            builder.Property(x => x.DeletedBy).IsRequired(false);

            builder.HasIndex(x => x.Username).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasMany( x => x.Comments).WithOne( x => x.User).HasForeignKey( x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.BlogPosts).WithOne(x => x.Author).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Votes).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.UserUseCases).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
