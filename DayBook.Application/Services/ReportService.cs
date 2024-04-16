using AutoMapper;
using DayBook.Application.Resources;
using DayBook.Domain.Dto.Report;
using DayBook.Domain.Entity;
using DayBook.Domain.Enum;
using DayBook.Domain.Interfaces.Repositories;
using DayBook.Domain.Interfaces.Services;
using DayBook.Domain.Interfaces.Validations;
using DayBook.Domain.Result;
using DayBook.Domain.Settings;
using DayBook.Producer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

namespace DayBook.Application.Services;

public class ReportService : IReportService
{
    private readonly IBaseRepository<Report> _reportRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly ILogger _logger;
    private readonly IReportValidator _reportValidator;
    private readonly IMapper _mapper;
    private readonly IMessageProducer _messageProducer;
    private readonly IOptions<RabbitMqSettings> _rabbitMqOptions;
    public ReportService(IBaseRepository<Report> reportRepository, 
        ILogger logger,
        IBaseRepository<User> userRepository,
        IReportValidator reportValidator,
        IMapper mapper,
        IMessageProducer messageProducer,
        IOptions<RabbitMqSettings> rabbitMqOptions)
    {
        _reportRepository = reportRepository;
        _logger = logger;
        _userRepository = userRepository;
        _reportValidator = reportValidator;
        _mapper = mapper;
        _messageProducer = messageProducer;
        _rabbitMqOptions = rabbitMqOptions;
    }

    /// <inheritdoc/>
    public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto)
    {
        var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Id == dto.UserId);
        var report = await _reportRepository.GetAll().FirstOrDefaultAsync(r => r.Name == dto.Name && r.UserId == dto.UserId);
        var result = _reportValidator.CreateValidator(report, user);

        if (!result.IsSuccess)
        {
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = result.ErrorMessage,
                ErrorCode = result.ErrorCode
            };
        }

        report = new Report()
        {
            Name = dto.Name,
            Description = dto.Description,
            UserId = dto.UserId
        };

        await _reportRepository.CreateAsync(report);
        await _reportRepository.SaveChangesAsync();

        _messageProducer.SendMessage(report, _rabbitMqOptions.Value.RoutingKey, _rabbitMqOptions.Value.ExchangeName);
        return new BaseResult<ReportDto>()
        {
            Data = _mapper.Map<ReportDto>(report),
        };
    }

    /// <inheritdoc/>
    public async Task<BaseResult<ReportDto>> DeleteReportByIdAsync(long id)
    {
        var report = await _reportRepository.GetAll().FirstOrDefaultAsync(r => r.Id == id);
        var result = _reportValidator.ValidateOnNull(report);

        if (!result.IsSuccess)
        {
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = result.ErrorMessage,
                ErrorCode = result.ErrorCode
            };
        }

        _reportRepository.Remove(report);
        await _reportRepository.SaveChangesAsync();
        return new BaseResult<ReportDto>()
        {
            Data = _mapper.Map<ReportDto>(report)
        };
    }

    /// <inheritdoc/>
    public async Task<BaseResult<ReportDto>> GetReportByIdAsync(long id)
    {
        ReportDto? report;
        try
        {
            report = _reportRepository.GetAll()
                .AsEnumerable()
                .Select(r => new ReportDto(r.Id, r.Name, r.Description, r.CreatedAt.ToLongDateString()))
                .FirstOrDefault(r => r.Id == id);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }

        if (report == null)
        {
            _logger.Warning($"Report with {id} not found", id);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.ReportNotFound,
                ErrorCode = (int)ErrorCodes.ReportNotFound
            };
        }

        return new BaseResult<ReportDto>()
        {
            Data = report
        };
    }

    /// <inheritdoc/>
    public async Task<CollectionResult<ReportDto>> GetReportsAsync(long userId)
    {
        ReportDto[] reports;
        try
        {
            reports = await _reportRepository.GetAll()
                .Where(r => r.UserId == userId)
                .Select(r => new ReportDto(r.Id, r.Name, r.Description, r.CreatedAt.ToLongDateString()))
                .ToArrayAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new CollectionResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }

        if (!reports.Any())
        {
            _logger.Warning(ErrorMessage.ReportsNotFound, reports.Length);
            return new CollectionResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.ReportsNotFound,
                ErrorCode = (int)ErrorCodes.ReportsNotFound
            };
        }

        return new CollectionResult<ReportDto>()
        {
            Data = reports,
            Count = reports.Length
        };
    }

    /// <inheritdoc/>
    public async Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto)
    {
        var report = await _reportRepository.GetAll().FirstOrDefaultAsync(r => r.Id == dto.Id);
        var result = _reportValidator.ValidateOnNull(report);

        if (!result.IsSuccess)
        {
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = result.ErrorMessage,
                ErrorCode = result.ErrorCode
            };
        }

        report.Name = dto.Name;
        report.Description = dto.Description;

        var updatedReport = _reportRepository.Update(report);
        await _reportRepository.SaveChangesAsync();
        return new BaseResult<ReportDto>()
        {
            Data = _mapper.Map<ReportDto>(updatedReport)
        };
    }
}
