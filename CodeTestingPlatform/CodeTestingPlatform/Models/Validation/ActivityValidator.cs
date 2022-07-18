using CodeTestingPlatform.DatabaseEntities.Local;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models.Validation {
    public class ActivityValidator : AbstractValidator<Activity> {
        public ActivityValidator() {
            //RuleFor(s => s.StartDate)
            //    .LessThanOrEqualTo(e => e.EndDate)
            //    .WithMessage("Start date must be before end date");
            RuleFor(e => e.EndDate)
                .GreaterThanOrEqualTo(s => s.StartDate)
                .WithMessage("End date must be after start date");
        }
    }
}