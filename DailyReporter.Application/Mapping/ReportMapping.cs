using AutoMapper;
using DailyReporter.Domain.Dto.Report;
using DailyReporter.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Application.Mapping
{
	public class ReportMapping : Profile
	{
		public ReportMapping()
		{
			CreateMap<Report, ReportDto>().ReverseMap();
		}
	}
}
