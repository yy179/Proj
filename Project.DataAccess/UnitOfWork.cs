using Project.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext _context;

        public UnitOfWork(ProjectDbContext context,
                          IOrganizationRepository organizationRepository,
                          IVolunteerRepository volunteerRepository,
                          IRequestRepository requestRepository,
                          IMilitaryUnitRepository militaryUnitRepository,
                          IContactPersonRepository contactPersonRepository,
                          IMessageRepository messageRepository,
                          IMilitaryUnitContactPersonRepository militaryUnitContactPersonRepository,
                          IUserRepository userRepository,
                          IVolunteerOrganizationRepository volunteerOrganizationRepository)
        {
            _context = context;
            OrganizationRepository = organizationRepository;
            VolunteerRepository = volunteerRepository;
            RequestRepository = requestRepository;
            MilitaryUnitRepository = militaryUnitRepository;
            ContactPersonRepository = contactPersonRepository;
            MessageRepository = messageRepository;
            MilitaryUnitContactPersonRepository = militaryUnitContactPersonRepository;
            UserRepository = userRepository;
            VolunteerOrganizationRepository = volunteerOrganizationRepository;
        }

        public IOrganizationRepository OrganizationRepository { get; }
        public IVolunteerRepository VolunteerRepository { get; }
        public IRequestRepository RequestRepository { get; }
        public IMilitaryUnitRepository MilitaryUnitRepository { get; }
        public IContactPersonRepository ContactPersonRepository { get; }
        public IMessageRepository MessageRepository { get; }
        public IMilitaryUnitContactPersonRepository MilitaryUnitContactPersonRepository { get; }
        public IUserRepository UserRepository { get; }
        public IVolunteerOrganizationRepository VolunteerOrganizationRepository { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
