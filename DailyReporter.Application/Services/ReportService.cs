using AutoMapper;
using DailyReporter.Application.Resources;
using DailyReporter.Domain.Dto.Report;
using DailyReporter.Domain.Entity;
using DailyReporter.Domain.Enum;
using DailyReporter.Domain.Interfaces.Repositories;
using DailyReporter.Domain.Interfaces.Services;
using DailyReporter.Domain.Interfaces.Validations;
using DailyReporter.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Application.Services
{
	public class ReportService : IReportService
	{
		private readonly IBaseRepository<Report> _reportRepository;
		private readonly IBaseRepository<User> _userRepository;
		private readonly ILogger _logger;
		private readonly IReportValidator _reportValidator;
		private readonly IMapper _mapper;

		public ReportService(IBaseRepository<Report> reportRepository,  ILogger logger, IBaseRepository<User> userRepository, IReportValidator reportValidator, IMapper mapper)
		{
			_reportRepository = reportRepository;
			_logger = logger;
			_userRepository = userRepository;
			_reportValidator = reportValidator;
			_mapper = mapper;
		}

		public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto)
		{
			try
			{
				var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.UserId);
				var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
				var result = _reportValidator.CreateValidator(report, user);
				if(!result.IsSuccess)
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
				return new BaseResult<ReportDto>()
				{
					Data = _mapper.Map<ReportDto>(report)
				};

			}
			catch (Exception ex)
			{
				_logger.Error(ex, ex.Message);
				return new BaseResult<ReportDto>()
				{
					ErrorMessage = "Внутренняя ошибка сервера",
					ErrorCode = (int)ErrorCodes.InternalServerError
				};
			}
		}

		public async Task<BaseResult<ReportDto>> DeleteReportAsync(long id)
		{
			try
			{
				var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
				var result = _reportValidator.ValidateOnNull(report);
				if (!result.IsSuccess)
				{
					return new BaseResult<ReportDto>()
					{
						ErrorMessage = result.ErrorMessage,
						ErrorCode = result.ErrorCode
					};
				}
				await _reportRepository.RemoveAsync(report);
				return new BaseResult<ReportDto>()
				{
					Data = _mapper.Map<ReportDto>(report)
				};

			}
			catch (Exception ex)
			{
				_logger.Error(ex, ex.Message);
				return new BaseResult<ReportDto>()
				{
					ErrorMessage = "Внутренняя ошибка сервера",
					ErrorCode = (int)ErrorCodes.InternalServerError
				};
			}
		}

		public async Task<BaseResult<ReportDto>> GetReportByIdAsync(long id)
		{
			ReportDto report;
			try
			{
				report = await _reportRepository.GetAll()
					.Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
					.FirstOrDefaultAsync(x => x.Id == id);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, ex.Message);
				return new BaseResult<ReportDto>()
				{
					ErrorMessage = "Внутренняя ошибка сервера",
					ErrorCode = (int)ErrorCodes.InternalServerError
				};
			}

			if (report == null)
			{
				_logger.Warning($"Отчёт c {id} не найден", id);

				return new BaseResult<ReportDto>()
				{
					ErrorMessage = "Запись не найдена",
					ErrorCode = (int)ErrorCodes.ReportsNotFound
				};
			}

			return new BaseResult<ReportDto>()
			{
				Data = report,
			};
		}

		public async Task<CollectionResult<ReportDto>> GetReportsAsync(long userId)
		{
			ReportDto[] reports;

			try
			{
				reports = await _reportRepository.GetAll()
					.Where(x => x.UserId == userId)
					.Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
					.ToArrayAsync();
			}
			catch (Exception ex)
			{
				_logger.Error(ex, ex.Message);
				return new CollectionResult<ReportDto>()
				{
					ErrorMessage = "Внутренняя ошибка сервера",
					ErrorCode = (int)ErrorCodes.InternalServerError
				};
			}

			if (!reports.Any())
			{
				_logger.Warning("Отчёты не найдены", reports.Length);

				return new CollectionResult<ReportDto>()
				{
					ErrorMessage = "Записи не найдены",
					ErrorCode = (int)ErrorCodes.ReportsNotFound
				};
			}

			return new CollectionResult<ReportDto>()
			{
				Data = reports,
				Count = reports.Length
			};

		}

		public async Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto)
		{
			try
			{
				var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
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

				await _reportRepository.UpdateAsync(report);
				return new BaseResult<ReportDto>()
				{
					Data = _mapper.Map<ReportDto>(report)
				};
			}
			catch (Exception ex)
			{
				_logger.Error(ex, ex.Message);
				return new BaseResult<ReportDto>()
				{
					ErrorMessage = "Внутренняя ошибка сервера",
					ErrorCode = (int)ErrorCodes.InternalServerError
				};
			}
		}
	}
}
