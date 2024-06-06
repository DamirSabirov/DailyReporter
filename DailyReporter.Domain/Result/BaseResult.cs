using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Domain.Result
{
	public class BaseResult
	{
		public bool IsSuccess => ErrorMessage == null;

		public string ErrorMessage { get; set; }

		public int? ErrorCode { get; set; }
	}

	public class BaseResult<T> : BaseResult
	{

		public BaseResult()
		{

		}

		public BaseResult(string errorMessage, int errorCode, T data)
		{
			ErrorMessage = errorMessage;
			ErrorCode = errorCode;
			Data = data;
		}

		public T Data { get; set; }
	}

}


