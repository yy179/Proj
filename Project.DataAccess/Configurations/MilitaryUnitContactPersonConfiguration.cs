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
    public class MilitaryUnitContactPersonConfiguration : IEntityTypeConfiguration<MilitaryUnitContactPersonEntity>
    {
        public void Configure(EntityTypeBuilder<MilitaryUnitContactPersonEntity> builder)
        {
            builder.HasKey(mucp => new { mucp.MilitaryUnitId, mucp.ContactPersonId });

            builder.HasOne(mucp => mucp.MilitaryUnit)
                .WithMany(mu => mu.MilitaryUnitContactPersons)
                .HasForeignKey(mucp => mucp.MilitaryUnitId);

            builder.HasOne(mucp => mucp.ContactPerson)
                .WithMany(cp => cp.MilitaryUnitContactPersons)
                .HasForeignKey(mucp => mucp.ContactPersonId);
        }
    }
}
