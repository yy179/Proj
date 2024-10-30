using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstractions.Repositories
{
    public interface IRequestRepository
    {
        Task<IEnumerable<RequestEntity>> GetAllAsync();
        Task<RequestEntity> GetByIdAsync(int requestId);
        Task<IEnumerable<RequestEntity>> GetByStatusAsync(bool IsActive);
        Task AddAsync(RequestEntity request);
        Task UpdateAsync(RequestEntity request);
        Task DeleteAsync(int requestId);
    }
}
