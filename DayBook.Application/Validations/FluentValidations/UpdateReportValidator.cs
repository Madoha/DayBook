using DayBook.Domain.Dto.Report;
using FluentValidation;

namespace DayBook.Application.Validations.FluentValidations;

public class UpdateReportValidator : AbstractValidator<UpdateReportDto>
{
    public UpdateReportValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty().MaximumLength(200);
        RuleFor(c => c.Description).NotEmpty().MaximumLength(1000);
    }
}
