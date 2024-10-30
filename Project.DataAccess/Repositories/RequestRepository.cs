using Microsoft.EntityFrameworkCore;
using Project.Abstractions.Repositories;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccess.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ProjectDbContext _context;

        public RequestRepository(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<RequestEntity> GetByIdAsync(int requestId)
        {
            return await _context.Requests.FindAsync(requestId);
        }

        public async Task<IEnumerable<RequestEntity>> GetAllAsync()
        {
            return await _context.Requests.ToListAsync();
        }
        public async Task<IEnumerable<RequestEntity>> GetByStatusAsync(bool IsActive)
        {
            return await _context.Requests
                .Where(req => req.IsActive == IsActive)
                .ToListAsync();
        }
        public async Task AddAsync(RequestEntity request)
        {
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RequestEntity request)
        {
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int requestId)
        {
            var request = await _context.Requests.FindAsync(requestId);
            if (request != null)
            {
                _context.Requests.Remove(request);
                await _context.SaveChangesAsync();
            }
        }
    }
}
