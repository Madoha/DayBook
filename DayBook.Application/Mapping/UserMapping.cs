using AutoMapper;
using DayBook.Domain.Dto.User;
using DayBook.Domain.Entity;

namespace DayBook.Application.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}
