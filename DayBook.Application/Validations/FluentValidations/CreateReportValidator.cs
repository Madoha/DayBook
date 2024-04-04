using DayBook.Domain.Dto.Report;
using FluentValidation;

namespace DayBook.Domain.Interfaces.Validations.FluentValidations;

public class CreateReportValidator : AbstractValidator<CreateReportDto>
{
    public CreateReportValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(200);
        RuleFor(c => c.Description).NotEmpty().MaximumLength(1000);
    }
}
