using Project.Abstractions.Repositories;

namespace Project.DataAccess
{
    public interface IUnitOfWork
    {
        IOrganizationRepository OrganizationRepository { get; }
        IVolunteerRepository VolunteerRepository { get; }
        IRequestRepository RequestRepository { get; }
        IMilitaryUnitRepository MilitaryUnitRepository { get; }
        IContactPersonRepository ContactPersonRepository { get; }
        IMessageRepository MessageRepository { get; }
        IMilitaryUnitContactPersonRepository MilitaryUnitContactPersonRepository { get; }
        IUserRepository UserRepository { get; }
        IVolunteerOrganizationRepository VolunteerOrganizationRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}