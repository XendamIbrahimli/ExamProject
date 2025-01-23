using ExamProject.Core.Configurations;
using ExamProject.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.DAL.Context
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AgentConfiguration());
        }
    }
}
