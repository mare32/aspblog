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
    public class UseCaseConfiguration : IEntityTypeConfiguration<UseCase>
    {
        public void Configure(EntityTypeBuilder<UseCase> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(50);

            // odlucio sam da ne stavim Name da bude unique u slucaju gde je isto ime al drugaciji opis

            builder.HasMany(x => x.UserUseCases).WithOne(x => x.UseCase).HasForeignKey(x => x.CaseId).OnDelete(DeleteBehavior.Cascade);


        }
    }
}
