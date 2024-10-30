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
    public class RequestConfiguration : IEntityTypeConfiguration<RequestEntity>
    {
        public void Configure(EntityTypeBuilder<RequestEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.Importance).IsRequired();

            builder
                .HasOne(x => x.CompletedByVolunteer)
                .WithMany(x => x.CompletedRequests)
                .HasForeignKey(x => x.CompletedByVolunteerId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(x => x.TakenByVolunteer)
                .WithMany(x => x.ActiveRequests)
                .HasForeignKey(x => x.TakenByVolunteerId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(x => x.OrganizationCompletedBy)
                .WithMany(x => x.CompletedRequests)
                .HasForeignKey(x => x.OrganizationCompletedById)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(x => x.OrganizationTakenBy)
                .WithMany(x => x.ActiveRequests)
                .HasForeignKey(x => x.OrganizationTakenById)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
