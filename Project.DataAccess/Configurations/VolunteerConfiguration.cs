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
    public class VolunteerConfiguration : IEntityTypeConfiguration<VolunteerEntity>
    {
        public void Configure(EntityTypeBuilder<VolunteerEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(v => v.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(v => v.LastName).IsRequired().HasMaxLength(100);
            builder.Property(v => v.City).HasMaxLength(100);
            builder.Property(v => v.Bio).HasMaxLength(500);
            builder
                .HasMany(v => v.VolunteerOrganizations)
                .WithOne(vo => vo.Volunteer)
                .HasForeignKey(vo => vo.VolunteerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasMany(x => x.CompletedRequests)
                .WithOne(x => x.CompletedByVolunteer)
                .HasForeignKey(x => x.CompletedByVolunteerId).OnDelete(DeleteBehavior.Restrict);
            builder
                .HasMany(x => x.ActiveRequests)
                .WithOne(x => x.TakenByVolunteer)
                .HasForeignKey(x => x.TakenByVolunteerId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
