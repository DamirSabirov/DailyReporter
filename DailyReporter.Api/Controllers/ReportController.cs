using DailyReporter.Domain.Dto.Report;
using DailyReporter.Domain.Interfaces.Services;
using DailyReporter.Domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyReporter.Api.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
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
	}
}
