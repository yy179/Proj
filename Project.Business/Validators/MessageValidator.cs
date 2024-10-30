using FluentValidation;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Validators
{
    public class MessageValidator : AbstractValidator<MessageEntity>
    {
        public MessageValidator()
        {
            RuleFor(v => v.Text)
                .NotEmpty().WithMessage("Text is required.")
                .Length(1, 1000).WithMessage("Text must be between 1 and 1000 characters.");

            RuleFor(v => v.SentDate).NotEmpty().WithMessage("Send date is required");
        }
    }
}
