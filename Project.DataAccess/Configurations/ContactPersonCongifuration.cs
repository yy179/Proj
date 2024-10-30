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
    public class ContactPersonConfiguration : IEntityTypeConfiguration<ContactPersonEntity>
    {
        public void Configure(EntityTypeBuilder<ContactPersonEntity> builder)
        {
            builder.HasKey(cp => cp.Id);

            builder.Property(cp => cp.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cp => cp.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(cp => cp.MilitaryUnitContactPersons)
                .WithOne(mucp => mucp.ContactPerson)
                .HasForeignKey(mucp => mucp.ContactPersonId);
        }
    }
}
