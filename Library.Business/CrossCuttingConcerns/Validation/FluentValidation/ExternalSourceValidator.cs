﻿using FluentValidation;
using Library.Entities.Concrete;

namespace Library.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class ExternalSourceValidator : AbstractValidator<ExternalSource>
    {
        public ExternalSourceValidator()
        {
            RuleFor(x => string.IsNullOrWhiteSpace(x.Name)).NotEqual(true);
            RuleFor(x => x.Name).Length(3, 100);
        }
    }
}