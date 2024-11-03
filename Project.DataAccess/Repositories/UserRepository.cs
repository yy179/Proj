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
    public class UserRepository : IUserRepository
    {
        private readonly ProjectDbContext _context;

        public UserRepository(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<UserEntity> GetByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.Email == email);
        }
        public async Task<IEnumerable<UserEntity>> GetByRoleAsync(string role)
        {
            return await _context.Users
                .Where(user => user.Role == role)
                .ToListAsync();
        }
        public async Task AddAsync(UserEntity user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserEntity user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
