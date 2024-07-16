using Asp.Versioning;
using DailyReporter.Domain.Dto.Report;
using DailyReporter.Domain.Interfaces.Services;
using DailyReporter.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyReporter.Api.Controllers
{
	[Authorize]
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class ReportController : ControllerBase
	{
		private readonly IReportService _reportService;

		public ReportController(IReportService reportService)
		{
			_reportService = reportService;
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<ReportDto>>> GetReport(long id)
		{
			var response = _reportService.GetReportByIdAsync(id);
			if (response.IsCompletedSuccessfully)
			{
				return Ok(response);
			}
			return BadRequest(response);
		}

		[HttpGet("reports/{userId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<ReportDto>>> GetUserReports(long userId)
		{
			var response = _reportService.GetReportsAsync(userId);
			if (response.IsCompletedSuccessfully)
			{
				return Ok(response);
			}
			return BadRequest(response);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<ReportDto>>> Delete(long id)
		{
			var response = _reportService.DeleteReportAsync(id);
			if (response.IsCompletedSuccessfully)
			{
				return Ok(response);
			}
			return BadRequest(response);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<ReportDto>>> Create([FromBody]CreateReportDto dto)
		{
			var response = _reportService.CreateReportAsync(dto);
			if (response.IsCompletedSuccessfully)
			{
				return Ok(response);
			}
			return BadRequest(response);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<ReportDto>>> Create([FromBody] UpdateReportDto dto)
		{
			var response = _reportService.UpdateReportAsync(dto);
			if (response.IsCompletedSuccessfully)
			{
				return Ok(response);
			}
			return BadRequest(response);
		}
	}
}
