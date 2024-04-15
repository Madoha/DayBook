using Asp.Versioning;
using DayBook.Domain.Dto.Role;
using DayBook.Domain.Dto.UserRole;
using DayBook.Domain.Entity;
using DayBook.Domain.Interfaces.Services;
using DayBook.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DayBook.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// Create role
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// <h3>Request to create a role:</h3>
    /// 
    ///     POST 
    ///     {
    ///         "name": "User",
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">If the role successfully created</response>
    /// <response code="400">If the role creating failed</response>
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Create([FromBody] CreateRoleDto dto)
    {
        var response = await _roleService.CreateRoleAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    /// <summary>
    /// Deleting a role by id
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// <h3>Request to delete a role:</h3>
    /// 
    ///     DELETE
    ///     {
    ///         "id": 1
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">If the role is deleted</response>
    /// <response code="400">If the role is not deleted</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Delete(long id)
    {
        var response = await _roleService.DeleteRoleAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    /// <summary>
    /// Update the role with basic properties
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// <h3>Request to update a role:</h3>
    /// 
    ///     PUT
    ///     {
    ///         "id": 1,
    ///         "name": "Admin"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">If role updated</response>
    /// <response code="400">If role not updated</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Update([FromBody] RoleDto dto)
    {
        var response = await _roleService.UpdateRoleAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    /// <summary>
    /// Define a role for a user
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// <h3>Request to define a role for a user:</h3>
    /// 
    ///     POST 
    ///     {
    ///         "login": "UserName",
    ///         "role": "RoleName"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">If the role is successfully assigned to the user</response>
    /// <response code="400">If role determination for a user fails</response>
    [HttpPost("add-role-for-user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> AddRoleForUser([FromBody] UserRoleDto dto)
    {
        var response = await _roleService.AddRoleForUserAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    /// <summary>
    /// Deleting a role for user
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// <h3>Request to delete a role for user:</h3>
    /// 
    ///     DELETE
    ///     {
    ///         "Login": "UserName",
    ///         "Role": "User"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">If the role is deleted for user</response>
    /// <response code="400">If the role is not deleted for user</response>
    [HttpDelete("delete-role-for-user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> DeleteRoleForUser(DeleteUserRoleDto dto)
    {
        var response = await _roleService.DeleteRoleForUserAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    /// <summary>
    /// Update a role for user
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// <h3>Request to update a role for user:</h3>
    /// 
    ///     PUT
    ///     {
    ///         "Login": "UserName",
    ///         "RoleId": "1"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">If the role is updated for user</response>
    /// <response code="400">If the role is not updated for user</response>
    [HttpPut("update-role-for-user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> UpdateRoleForUser([FromBody] UpdateUserRoleDto dto)
    {
        var response = await _roleService.UpdateRoleForUserAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}
