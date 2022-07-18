using CodeTestingPlatform.DatabaseEntities.Local;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models.Validation {
    public class SignatureValidator : AbstractValidator<MethodSignature> {
        public SignatureValidator() {
            RuleFor(s => s.MethodName)
                .Matches(@"^((?!\s).)*$") // Validate if MethodName does not contain whitespace.
                .WithMessage("No Spaces allowed"); // If MethodName contains whitespace, error is thrown.
        }
    }
}