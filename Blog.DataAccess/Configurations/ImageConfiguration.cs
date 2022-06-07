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
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(x => x.Src).IsRequired();
            builder.Property(x => x.Alt).IsRequired();

            builder.HasMany(x => x.BlogPostImages).WithOne( x => x.Image).HasForeignKey( x => x.ImageId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
