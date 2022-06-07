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
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.Property(x => x.PostId).IsRequired(false);
            builder.Property(x => x.CommentId).IsRequired(false);
            builder.Property(x => x.VotedAt).HasDefaultValueSql("GETDATE()");


        }
    }
}
