using Asp.Versioning;
using DayBook.Domain.Dto.Report;
using DayBook.Domain.Interfaces.Services;
using DayBook.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayBook.Api.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/{version:apiVersion}/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;
    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    /// <summary>
    /// Receiving a report with a specific id
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// <h3>Request to receive a report by id:</h3>
    /// 
    ///     GET
    ///     {
    ///         "id": 1
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">If the report is found</response>
    /// <response code="400">If the report is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetReport(long id)
    {
        var response = await _reportService.GetReportByIdAsync(id);

        if(response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Receiving user reports
    /// </summary>
    /// <param name="userId"></param>
    /// <remarks>
    /// <h3>Request to receive all user reports:</h3>
    /// 
    ///     GET
    ///     {
    ///         "id": 1
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">If reports are found</response>
    /// <response code="400">If reports are not found</response>
    [HttpGet("reports/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CollectionResult<ReportDto>>> GetUserReports(long userId)
    {
        var response = await _reportService.GetReportsAsync(userId);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    /// <summary>
    /// Deleting a report by id
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// <h3>Request to delete a report:</h3>
    /// 
    ///     DELETE
    ///     {
    ///         "id": 1
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">If the report is deleted</response>
    /// <response code="400">If the report is not deleted</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> Delete(long id)
    {
        var response = await _reportService.DeleteReportByIdAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    /// <summary>
    /// Create report
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// <h3>Request to create a report:</h3>
    /// 
    ///     POST 
    ///     {
    ///         "name": "Report #1",
    ///         "description": "Test report",
    ///         "userId": 1
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">If the report successfully created</response>
    /// <response code="400">If the report creating failed</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> Create([FromBody] CreateReportDto dto)
    {
        var response = await _reportService.CreateReportAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    /// <summary>
    /// Update the report with basic properties
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// <h3>Request to update a report:</h3>
    /// 
    ///     PUT
    ///     {
    ///         "id": 1,
    ///         "name": "Report #2",
    ///         "description": "test report2"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">If report updated</response>
    /// <response code="400">If report not updated</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> Update([FromBody] UpdateReportDto dto)
    {
        var response = await _reportService.UpdateReportAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}
