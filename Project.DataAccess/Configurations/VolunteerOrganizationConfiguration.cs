using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Entities;

namespace Project.DataAccess.Configurations
{
    public class VolunteerOrganizationConfiguration : IEntityTypeConfiguration<VolunteerOrganizationEntity>
    {
        public void Configure(EntityTypeBuilder<VolunteerOrganizationEntity> builder)
        {
            builder.HasKey(x => new { x.VolunteerId, x.OrganizationId });
            builder
                .HasOne(x => x.Volunteer)
                .WithMany(x => x.VolunteerOrganizations)
                .HasForeignKey(x => x.VolunteerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(x => x.Organization)
                .WithMany(x => x.VolunteerOrganizations)
                .HasForeignKey(x => x.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(vo => vo.JoinedDate).IsRequired();
        }
    }
}
