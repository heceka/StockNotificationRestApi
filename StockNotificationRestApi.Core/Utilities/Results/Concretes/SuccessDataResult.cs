using StockNotificationRestApi.Core.Utilities.Results.Abstracts;

namespace StockNotificationRestApi.Core.Utilities.Results.Concretes
{
	public class SuccessDataResult<T> : DataResult<T>
	{
		public SuccessDataResult() : base(default, true) { }
		public SuccessDataResult(string message) : base(default, true, message) { }
		public SuccessDataResult(T data) : base(data, true) { }
		public SuccessDataResult(T data, string message) : base(data, true, message) { }
	}
}
