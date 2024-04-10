using DayBook.Domain.Dto;
using DayBook.Domain.Dto.User;
using DayBook.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DayBook.Domain.Interfaces.Services;

public interface IAuthService
{
    /// <summary>
    /// Service defined for authorization/registration
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<UserDto>> Register(RegisterUserDto dto);

    /// <summary>
    /// Service defined for authorization
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<TokenDto>> Login(LoginUserDto dto);
}
