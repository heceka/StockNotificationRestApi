using StockNotificationRestApi.Core.Utilities.Results.Abstracts;

namespace StockNotificationRestApi.Core.Utilities.Results.Concretes
{
	public class ErrorDataResult<T> : DataResult<T>
	{
		public ErrorDataResult() : base(default, false) { }

		public ErrorDataResult(string message) : base(default, false, message) { }

		public ErrorDataResult(T data) : base(data, false) { }

		public ErrorDataResult(T data, string message) : base(data, false, message) { }
	}
}
