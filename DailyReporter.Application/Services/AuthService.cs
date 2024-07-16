using AutoMapper;
using DailyReporter.Application.Resources;
using DailyReporter.Domain.Dto.Report;
using DailyReporter.Domain.Dto.User;
using DailyReporter.Domain.Entity;
using DailyReporter.Domain.Enum;
using DailyReporter.Domain.Interfaces.Repositories;
using DailyReporter.Domain.Interfaces.Services;
using DailyReporter.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ILogger = Serilog.ILogger;

namespace DailyReporter.Application.Services
{
	public class AuthService : IAuthService
	{
		private readonly IBaseRepository<User> _userRepository;
		private readonly ILogger _logger;
		private readonly IMapper _mapper;
		public AuthService(IBaseRepository<User> userRepository, ILogger logger, IMapper mapper)
		{
			_userRepository = userRepository;
			_logger = logger;
			_mapper = mapper;
		}

		public async Task<BaseResult<UserDto>> Login(RegisterUserDto dto)
		{
			if (dto.Password != dto.PasswordConfirmed)
			{
				return new BaseResult<UserDto>()
				{
					ErrorMessage = "Пароли не совпадают",
					ErrorCode = (int)ErrorCodes.InternalServerError
				};
			}
			try
			{
				var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == dto.Login);
				if (user != null)
				{
					return new BaseResult<UserDto>()
					{
						ErrorMessage = "Пользователь с такими данными уже существует",
						ErrorCode = (int)ErrorCodes.UserAlreadyExists
					};
				}
				var hashUserPassword = HashPassword(dto.Password);
				user = new User()
				{
					Login = dto.Login,
					Password = dto.Password
				};
				await _userRepository.CreateAsync(user);
				return new BaseResult<UserDto>()
				{
					Data = _mapper.Map<UserDto>(user)
				};
			}
			catch (Exception ex)
			{
				_logger.Error(ex, ex.Message);
				return new BaseResult<UserDto>()
				{
					ErrorMessage = "Внутренняя ошибка сервера",
					ErrorCode = (int)ErrorCodes.InternalServerError
				};
			}
		}

		public Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
		{
			throw new NotImplementedException();
		}
		private string HashPassword(string password)
		{
			var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
			return BitConverter.ToString(bytes).ToLower();
		}
	}
}
