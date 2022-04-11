#region USINGS
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StockNotificationRestApi.Bll.Handlers.Abstracts;
using StockNotificationRestApi.Bll.Services.Abstracts;
using StockNotificationRestApi.Core.Resources;
using StockNotificationRestApi.Core.Utilities.Results.Abstracts;
using StockNotificationRestApi.Core.Utilities.Results.Concretes;
using StockNotificationRestApi.Entities.Concretes;
using StockNotificationRestApi.Entities.Configs;
using StockNotificationRestApi.Entities.DTOs;
using StockNotificationRestApi.Entities.Enums;
#endregion

namespace StockNotificationRestApi.Bll.Handlers.Concretes
{
	public class NotificationHandler : INotificationHandler
	{
		#region DEFINES
		private readonly IStockNotificationService _service;
		private readonly ILogger<NotificationHandler> _log;
		private readonly NotificationApiConfig _config;
		private readonly HttpClient _client;
		#endregion

		#region CONSTRUCTOR
		public NotificationHandler(IStockNotificationService service, IConfiguration config, ILogger<NotificationHandler> log,
			IHttpClientFactory factory)
		{
			_service = service;
			_config = config.GetSection("NotificationApiConfig").Get<NotificationApiConfig>();
			_log = log;

			_client = factory.CreateClient();
			_client.DefaultRequestHeaders.Add("Accept", "application/json;charset=utf-8");
			_client.DefaultRequestHeaders.Add("User-Agent", "StockNotificationRestService-1.0");
		}
		#endregion

		public async Task<IDataResult<List<StockNotification>>> SendNotification(StockNotificationTrigger product)
		{
			#region RULES
			if( String.IsNullOrEmpty(product.ProductId) )
			{
				return new ErrorDataResult<List<StockNotification>>(StaticMessages.ProductIdNullOrEmpty);
			}
			else if( String.IsNullOrEmpty(product.ProductName) )
			{
				return new ErrorDataResult<List<StockNotification>>(StaticMessages.ProductNameNullOrEmpty);
			}
			#endregion

			IDataResult<List<StockNotification>> dataResult = await _service.GetAllByProductId(product.ProductId);
			if( !dataResult.Success )
			{
				return new ErrorDataResult<List<StockNotification>>(StaticMessages.StockNotificationNotFound);
			}

			#region EMAIL
			Task tEmail = Task.Run(() => TaskSendStockNotificationByType(dataResult.Data, product.ProductName,
				StockNotificationType.Email));
			#endregion

			#region MOBILE PUSH
			Task tMobilePush = Task.Run(() => TaskSendStockNotificationByType(dataResult.Data, product.ProductName,
				StockNotificationType.MobilePush));
			#endregion

			Task.WaitAll(new Task[] { tEmail, tMobilePush });
			return new SuccessDataResult<List<StockNotification>>(dataResult.Data);
		}

		#region HELPERS

		#region REQUEST CLIENT
		private async Task<bool> RequestClient(SendStockNotification model, string requestUri)
		{
			string content = JsonSerializer.Serialize(model);
			using StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
			using HttpResponseMessage responseMessage = await _client.PostAsync(requestUri, stringContent);
			if( responseMessage.IsSuccessStatusCode )
			{
				_log.LogInformation("Client post successful. {RequestUri}", requestUri);
			}
			else
			{
				_log.LogWarning("Client post failed. {Content} *-* {RequestUri} *-* {StatusCode} *-* {Reason}", content,
					requestUri, (int)responseMessage.StatusCode, responseMessage.ReasonPhrase);
			}
			return responseMessage.IsSuccessStatusCode;
		}
		#endregion

		#region TASK SEND STOCK NOTIFICATION BY TYPE
		private async Task TaskSendStockNotificationByType(List<StockNotification> stockNotifications, string productName,
			StockNotificationType stockNotificationType)
		{
			for( int i = 0; i < stockNotifications.Count; i++ )
			{
				int tryMaxCount = 3;
				bool result = false;
				do
				{
					StockNotification stockNotification = stockNotifications[i];
					try
					{
						result = await RequestClient(new SendStockNotification
						{
							ProductId = stockNotification.ProductId,
							ProductName = productName,
							UserId = stockNotification.UserId
						}, stockNotificationType == StockNotificationType.Email ? _config.Email : _config.Mobile);
						tryMaxCount--;
					}
					catch( Exception ex )
					{
						_log.LogError(ex, "{StockNotificationType} Notification error. {Product} *-* {User}",
							stockNotificationType.ToString(), stockNotification.ProductId, stockNotification.UserId);
					}
					finally
					{
						tryMaxCount--;
					}
				} while( !result && tryMaxCount > 0 );
				if( !result )
				{
					_log.LogWarning("StockNotificationType Notification ( {id} ) could not send",
						stockNotificationType.ToString(), stockNotifications[i].Id);
				}
			}
		}
		#endregion

		#endregion
	}
}
