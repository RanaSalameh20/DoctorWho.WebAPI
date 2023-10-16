using DoctorWho.Web.Models;
using FluentValidation;

namespace DoctorWho.Web.Validators
{
    public class EnemyValidator : AbstractValidator<EnemyDto>
    {
        public EnemyValidator() {

            RuleFor(e => e.EnemyName).NotEmpty().MaximumLength(100);
            RuleFor( e=> e.Description).NotEmpty().MaximumLength(200);
        }
    }
}
