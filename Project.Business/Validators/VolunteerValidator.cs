using FluentValidation;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Validators
{
    public class VolunteerValidator : AbstractValidator<VolunteerEntity>
    {
        public VolunteerValidator()
        {
            RuleFor(v => v.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(1, 100).WithMessage("First name must be between 1 and 100 characters.");

            RuleFor(v => v.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(1, 100).WithMessage("Last name must be between 1 and 100 characters.");

            RuleFor(v => v.City)
                .Length(0, 100).WithMessage("City must be at most 100 characters.");

            RuleFor(v => v.Bio)
                .Length(0, 500).WithMessage("Bio must be at most 500 characters.");
        }
    }
}
