using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstractions.Services
{
    public interface IRequestService
    {
        Task<RequestEntity> GetByIdAsync(int requestId);
        Task<IEnumerable<RequestEntity>> GetAllAsync();
        Task<IEnumerable<RequestEntity>> GetByStatusAsync(bool isActive);
        Task AddAsync(RequestEntity request);
        Task UpdateAsync(RequestEntity request);
        Task DeleteAsync(int requestId);
    }
}
