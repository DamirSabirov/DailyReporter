using DailyReporter.Domain.Dto.User;
using DailyReporter.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Domain.Interfaces.Services
{
	/// <summary>
	/// Сервис, предназначенный для авторизации и регистрации пользователей.
	/// </summary>
	public interface IAuthService
	{
		/// <summary>
		/// Регистрация.
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<UserDto>> Register(RegisterUserDto dto);
		/// <summary>
		/// Авторизация.
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<UserDto>> Login(RegisterUserDto dto);
	}
}
