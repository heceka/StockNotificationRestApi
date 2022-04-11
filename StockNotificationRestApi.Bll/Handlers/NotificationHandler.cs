using StockNotificationRestApi.Bll.Services.Abstracts;
using StockNotificationRestApi.Core.Utilities.Results.Abstracts;
using StockNotificationRestApi.Entities.DTOs;

namespace StockNotificationRestApi.Bll.Handlers
{
	public class NotificationHandler
	{
		#region DEFINES
		private readonly IStockNotificationService _service;
		#endregion

		#region CONSTRUCTOR
		public NotificationHandler(IStockNotificationService service)
		{
			_service = service;
		}
		#endregion

		public void SendNotification(StockNotificationTrigger model)
		{
			IDataResult _service.GetAllByProductId(model.ProductId);
		}
	}
}
