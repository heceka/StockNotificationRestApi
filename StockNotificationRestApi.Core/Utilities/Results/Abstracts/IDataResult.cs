namespace StockNotificationRestApi.Core.Utilities.Results.Abstracts
{
	public interface IDataResult<T> : IResult
	{
		T Data { get; }
	}
}
