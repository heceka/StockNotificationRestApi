#region USINGS
using System.Collections.Generic;
using System.Threading.Tasks;
using StockNotificationRestApi.Core.Utilities.Results.Abstracts;
using StockNotificationRestApi.Entities.Concretes;
using StockNotificationRestApi.Entities.DTOs; 
#endregion

namespace StockNotificationRestApi.Bll.Services.Abstracts
{
	public interface IStockNotificationService
	{
		Task<IDataResult<StockNotification>> GetByProductIdAndUserId(string productId, string userId);
		Task<IDataResult<List<StockNotification>>> GetAllByProductId(string productId);
		Task<IResult> Add(StockNotificationDto model);
		Task<IResult> Remove(StockNotificationDto model);
		Task<IResult> RemoveAll(List<StockNotification> stockNotifications);
	}
}
