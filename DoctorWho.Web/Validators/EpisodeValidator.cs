using DoctorWho.Web.Models;
using FluentValidation;

namespace DoctorWho.Web.Validators
{
    public class EpisodeValidator : AbstractValidator<EpisodeDto>
    {
        public EpisodeValidator() { 
            
            RuleFor(e => e.Title)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(e => e.AuthorId).NotEmpty();    
            RuleFor(e => e.DoctorId).NotEmpty();
            RuleFor(e => e.SeriesNumber)
                .NotEmpty()
                .Must(s => s.ToString().Length == 10)
                .WithMessage("SeriesNumber must have exactly 10 digits.");
            RuleFor(e => e.EpisodeNumber)
                .NotEmpty()
                .GreaterThan(0);


        }

    }
}
