using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccess.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<OrganizationEntity>
    {
        public void Configure(EntityTypeBuilder<OrganizationEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(18);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(o => o.City).HasMaxLength(50);
            builder
                .HasMany(x => x.VolunteerOrganizations)
                .WithOne(x => x.Organization)
                .HasForeignKey(x => x.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasMany(x => x.CompletedRequests)
                .WithOne(x => x.OrganizationCompletedBy)
                .HasForeignKey(x => x.OrganizationCompletedById)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasMany(x => x.ActiveRequests)
                .WithOne(x => x.OrganizationTakenBy)
                .HasForeignKey(x => x.OrganizationTakenById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
