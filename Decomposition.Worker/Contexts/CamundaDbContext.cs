using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Decomposition.Worker.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace Decomposition.Worker.Contexts
{
    public class CamundaDbContext : DbContext
    {
        public CamundaDbContext(DbContextOptions<CamundaDbContext> options) : base(options)
        {
        }

        // Define DbSet for the ACT_HI_PROCINST table
        public DbSet<ACT_HI_PROCINST> ACT_HI_PROCINST { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define table mapping for ACT_HI_PROCINST
            modelBuilder.Entity<ACT_HI_PROCINST>()
                .ToTable("ACT_HI_PROCINST", "dbo")
                .HasKey(x => x.ID_);

            modelBuilder.Entity<ACT_HI_PROCINST>()
                .Property(x => x.BUSINESS_KEY_)
                .HasColumnName("BUSINESS_KEY_");
        }
    }

}
