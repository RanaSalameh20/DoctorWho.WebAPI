using DoctorWho.Web.Models;
using FluentValidation;

namespace DoctorWho.Web.Validators
{
    public class CompanionValidator : AbstractValidator<CompanionDto>
    {
        public CompanionValidator()
        {
            RuleFor(c => c.CompanionName).NotEmpty().MaximumLength(100);
            RuleFor(c => c.WhoPlayed).NotEmpty().MaximumLength(100);
        }
    }
}
