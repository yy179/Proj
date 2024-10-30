using FluentValidation;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Validators
{
    public class RequestValidator : AbstractValidator<RequestEntity>
    {
        public RequestValidator()
        {
            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(1, 200).WithMessage("Title must be between 1 and 200 characters.");

            RuleFor(v => v.EndDate).NotEmpty().WithMessage("EndDate is required.");

            RuleFor(v => v.Description)
                .Length(0, 1000).WithMessage("Description must be at most 1000 characters.");

            RuleFor(v => v.Importance).NotEmpty().WithMessage("Importance is required.");
        }
    }
}
