using FluentValidation;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Validators
{
    public class OrganizationValidator : AbstractValidator<OrganizationEntity>
    {
        public OrganizationValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 18).WithMessage("Name must be between 1 and 18 characters.");

            RuleFor(o => o.Description)
                .MaximumLength(500).WithMessage("Description must be at most 500 characters.");

            RuleFor(o => o.City)
                .MaximumLength(50).WithMessage("City must be at most 50 characters.");
        }
    }
}
