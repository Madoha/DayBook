﻿using DayBook.Domain.Dto;
using DayBook.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DayBook.Domain.Interfaces.Services;

public interface ITokenService
{
    public string GenerateAccessToken(IEnumerable<Claim> claims);
    public string GenerateRefreshToken();
    public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string accessToken);
    public Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto);
}
