using AutoMapper;
using DayBook.Domain.Dto.Report;
using DayBook.Domain.Entity;

namespace DayBook.Application.Mapping;

public class ReportMapping : Profile
{
    public ReportMapping()
    {
        CreateMap<Report, ReportDto>().ReverseMap();
    }
}
