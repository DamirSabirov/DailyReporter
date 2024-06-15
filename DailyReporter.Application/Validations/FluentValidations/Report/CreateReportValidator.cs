using DailyReporter.Domain.Dto.Report;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Application.Validations.FluentValidations.Report
{
    public class CreateReportValidator : AbstractValidator<CreateReportDto>
    {
        public CreateReportValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        }
    }
}
