using ExamProject.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.Core.Configurations
{
    public class DesignationConfiguration : IEntityTypeConfiguration<Designation>
    {
        public void Configure(EntityTypeBuilder<Designation> builder)
        {
            builder.HasIndex(x => x.Name)
                .IsUnique();
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);
        }
    }
}
