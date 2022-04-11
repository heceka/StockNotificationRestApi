using StockNotificationRestApi.Core.Utilities.Results.Abstracts;

namespace StockNotificationRestApi.Core.Utilities.Results.Concretes
{
	public class SuccessResult : Result
	{
		public SuccessResult() : base(true) { }

		public SuccessResult(string message) : base(true, message) { }
	}
}
