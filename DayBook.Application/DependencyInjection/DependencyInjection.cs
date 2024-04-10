using DayBook.Application.Mapping;
using DayBook.Application.Services;
using DayBook.Application.Validations;
using DayBook.Application.Validations.FluentValidations;
using DayBook.Domain.Dto.Report;
using DayBook.Domain.Interfaces.Services;
using DayBook.Domain.Interfaces.Validations;
using DayBook.Domain.Interfaces.Validations.FluentValidations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DayBook.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(ReportMapping));

        InitServices(service);
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IReportValidator, ReportValidator>();
        services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
        services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
    }
}
