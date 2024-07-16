using DailyReporter.Application.Services;
using DailyReporter.Domain.Dto;
using DailyReporter.Domain.Dto.User;
using DailyReporter.Domain.Entity;
using DailyReporter.Domain.Interfaces.Services;
using DailyReporter.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace DailyReporter.Api.Controllers
{
	[ApiController]
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<ActionResult<BaseResult<UserDto>>> Register([FromBody]RegisterUserDto dto)
		{
			var response = _authService.Register(dto);
			if (response.IsCompletedSuccessfully)
			{
				return Ok(response);
			}
			return BadRequest(response);
		}

		[HttpPost("login")]
		public async Task<ActionResult<BaseResult<TokenDto>>> Login([FromBody] RegisterUserDto dto)
		{

		}
	}
}
