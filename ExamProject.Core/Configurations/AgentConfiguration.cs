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
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.HasIndex(x => x.Fullname)
                .IsUnique();
            builder.Property(x => x.Fullname)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(x => x.ImageUrl)
                .IsRequired()
                .HasMaxLength(128);
            builder.HasOne(x => x.Designation)
                .WithMany(x => x.Agents)
                .HasForeignKey(x => x.DesignationId);

        }
    }
}
