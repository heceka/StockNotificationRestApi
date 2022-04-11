namespace StockNotificationRestApi.Core.Utilities.Results.Abstracts
{
	public abstract class Result : IResult
	{
		public bool Success { get; }
		public string Message { get; }

		public Result(bool success)
		{
			Success = success;
		}

		public Result(bool success, string message) : this(success)
		{
			Message = message;
		}
	}
}
