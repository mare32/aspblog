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
    public class VoteTypeConfiguration : IEntityTypeConfiguration<VoteType>
    {
        public void Configure(EntityTypeBuilder<VoteType> builder)
        {

            builder.Property(x => x.Type).IsRequired().HasMaxLength(20);

            builder.HasIndex(x => x.Type).IsUnique();

            builder.HasMany(x => x.Votes).WithOne(x => x.VoteType).HasForeignKey(x => x.TypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
