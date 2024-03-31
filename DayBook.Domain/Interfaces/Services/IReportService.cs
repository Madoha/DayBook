using DayBook.Domain.Dto.Report;
using DayBook.Domain.Result;

namespace DayBook.Domain.Interfaces.Services;

/// <summary>
/// Service responsible for working with the domain part of the report (Report)
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Retrieving all user reports
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<CollectionResult<ReportDto>> GetReportsAsync(long userId);

    /// <summary>
    /// Retrieving report by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> GetReportByIdAsync(long id);

    /// <summary>
    /// Creating report with base parameters
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto);

    /// <summary>
    /// Deleting report by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> DeleteReportByIdAsync(long id);

    /// <summary>
    /// Update report
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto);
}
