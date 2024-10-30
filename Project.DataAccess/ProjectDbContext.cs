using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Configurations;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccess
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options)
        {
        }
        public DbSet<ContactPersonEntity> ContactPersons { get; set; }
        public DbSet<MilitaryUnitEntity> MilitaryUnits { get; set; }
        public DbSet<OrganizationEntity> Organizations { get; set; }
        public DbSet<RequestEntity> Requests { get; set; }
        public DbSet<VolunteerEntity> Volunteers { get; set; }
        public DbSet<VolunteerOrganizationEntity> VolunteerOrganizations { get; set; }
        public DbSet<MilitaryUnitContactPersonEntity> MilitaryUnitContactPersons { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VolunteerOrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new MilitaryUnitConfiguration());
            modelBuilder.ApplyConfiguration(new ContactPersonConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new RequestConfiguration());
            modelBuilder.ApplyConfiguration(new VolunteerConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new MilitaryUnitContactPersonConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Documents\\LastDb.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }
    }
}
