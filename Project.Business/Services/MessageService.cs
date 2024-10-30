using FluentValidation;
using Project.Abstractions.Services;
using Project.Business.Validators;
using Project.DataAccess;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageEntity> GetByIdAsync(int id)
        {
            return await _unitOfWork.MessageRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(MessageEntity message)
        {
            ValidateMessage(message);
            await _unitOfWork.MessageRepository.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<MessageEntity>> GetMessagesSentByVolunteerAsync(int volunteerId)
        {
            return await _unitOfWork.MessageRepository.GetMessagesSentByVolunteerAsync(volunteerId);
        }

        public async Task<List<MessageEntity>> GetMessagesReceivedByVolunteerAsync(int volunteerId)
        {
            return await _unitOfWork.MessageRepository.GetMessagesReceivedByVolunteerAsync(volunteerId);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.MessageRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        private void ValidateMessage(MessageEntity message)
        {
            var validator = new MessageValidator();
            var result = validator.Validate(message);

            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
        }
    }
}
