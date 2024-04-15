using AutoMapper;
using DayBook.Domain.Dto.Role;
using DayBook.Domain.Entity;

namespace DayBook.Application.Mapping;

public class RoleMapping : Profile
{
    public RoleMapping()
    {
        //CreateMap<Role, RoleDto>()
        //    .ForCtorParam(ctorParamName: "Id", r => r.MapFrom(x => x.Id))
        //    .ForCtorParam(ctorParamName: "Name", r => r.MapFrom(x => x.Name))
        //    .ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
    }
}
