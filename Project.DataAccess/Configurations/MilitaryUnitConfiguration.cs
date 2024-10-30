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
    public class MilitaryUnitConfiguration : IEntityTypeConfiguration<MilitaryUnitEntity>
    {
        public void Configure(EntityTypeBuilder<MilitaryUnitEntity> builder)
        {
            builder.HasKey(mu => mu.Id);

            builder.Property(mu => mu.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(mu => mu.MilitaryUnitContactPersons)
                .WithOne(mucp => mucp.MilitaryUnit)
                .HasForeignKey(mucp => mucp.MilitaryUnitId);

            builder
                .HasMany(mu => mu.Requests)
                .WithOne(r => r.MilitaryUnit)
                .HasForeignKey(r => r.MilitaryUnitId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
