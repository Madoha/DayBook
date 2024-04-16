using DayBook.Domain.Dto;
using DayBook.Domain.Interfaces.Services;
using DayBook.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DayBook.Api.Controllers;

[Route("api/[controller]")]
//[Authorize]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;
    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    public async Task<ActionResult<BaseResult<TokenDto>>> RefreshToken([FromBody] TokenDto tokenDto)
    {
        var response = await _tokenService.RefreshToken(tokenDto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}
