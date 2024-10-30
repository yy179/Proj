using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstractions.Services
{
    public interface IMessageService
    {
        Task<MessageEntity> GetByIdAsync(int id);
        Task AddAsync(MessageEntity message);
        Task<List<MessageEntity>> GetMessagesSentByVolunteerAsync(int volunteerId);
        Task<List<MessageEntity>> GetMessagesReceivedByVolunteerAsync(int volunteerId);
        Task DeleteAsync(int id);
    }
}