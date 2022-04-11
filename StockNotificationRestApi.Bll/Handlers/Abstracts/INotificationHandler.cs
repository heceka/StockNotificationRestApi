#region USINGS
using System.Collections.Generic;
using System.Threading.Tasks;
using StockNotificationRestApi.Core.Utilities.Results.Abstracts;
using StockNotificationRestApi.Entities.Concretes;
using StockNotificationRestApi.Entities.DTOs; 
#endregion

namespace StockNotificationRestApi.Bll.Handlers.Abstracts
{
	public interface INotificationHandler
	{
		Task<IDataResult<List<StockNotification>>> SendNotification(StockNotificationTrigger product);
	}
}
