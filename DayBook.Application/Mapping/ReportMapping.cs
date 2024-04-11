using AutoMapper;
using DayBook.Domain.Dto.Report;
using DayBook.Domain.Entity;

namespace DayBook.Application.Mapping;

public class ReportMapping : Profile
{
    public ReportMapping()
    {
        CreateMap<Report, ReportDto>()
            .ForCtorParam(ctorParamName: "Id", r => r.MapFrom(s => s.Id))
            .ForCtorParam(ctorParamName: "Name", r => r.MapFrom(s => s.Name))
            .ForCtorParam(ctorParamName: "Description", r => r.MapFrom(s => s.Description))
            .ForCtorParam(ctorParamName: "DateCreated", r => r.MapFrom(s => s.CreatedAt))
            .ReverseMap();
    }
}
