using AutoMapper;
using DailyReporter.Domain.Dto.User;
using DailyReporter.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Application.Mapping
{
	public class UserMapping : Profile
	{
		public UserMapping()
		{
			CreateMap<User, UserDto>().ReverseMap();
		}
	}
}
