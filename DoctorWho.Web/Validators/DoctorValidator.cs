using DoctorWho.Web.Models;
using FluentValidation;

namespace DoctorWho.Web.Validators
{
    public class DoctorValidator : AbstractValidator<DoctorDto>
    {
        public DoctorValidator() { 
            RuleFor(d => d.DoctorName).NotEmpty();
            RuleFor(d => d.DoctorNumber).NotEmpty();
            RuleFor(d => d.LastEpisodeDate).Null().When(d => d.FirstEpisodeDate == null);
            RuleFor(d => d.LastEpisodeDate).GreaterThanOrEqualTo(d => d.FirstEpisodeDate);
        
        }
    }
}
