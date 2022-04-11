using System.Threading.Tasks;
using StockNotificationRestApi.Core.Utilities.Results.Abstracts;
using StockNotificationRestApi.Entities.Concretes;
using StockNotificationRestApi.Entities.DTOs;

namespace StockNotificationRestApi.Bll.Services.Abstracts
{
	public interface IStockNotificationService
	{
		Task<IDataResult<StockNotification>> GetByProductIdAndUserId(string productId, string userId);
		Task<IResult> Add(StockNotificationDto model);
		Task<IResult> Remove(StockNotificationDto model);
	}
}
