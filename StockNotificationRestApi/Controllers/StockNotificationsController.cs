using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockNotificationRestApi.Bll.Handlers;
using StockNotificationRestApi.Bll.Services.Abstracts;
using StockNotificationRestApi.Core.Utilities.Results.Abstracts;
using StockNotificationRestApi.Entities.DTOs;

namespace StockNotificationRestApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StockNotificationsController : ControllerBase
	{
		#region DEFINES
		private readonly ILogger<StockNotificationsController> _logger;
		private readonly IStockNotificationService _service;
		private readonly NotificationHandler _notificationHandler;
		#endregion

		#region CONSTRUCTOR
		public StockNotificationsController(ILogger<StockNotificationsController> logger, IStockNotificationService service,
			NotificationHandler notificationHandler)
		{
			_logger = logger;
			_service = service;
			_notificationHandler = notificationHandler;
		}
		#endregion

		#region ADD
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] StockNotificationDto model)
		{
			if( model != null )
			{
				try
				{
					IResult result = await _service.Add(model);
					if( result.Success )
					{
						return Ok();
					}
					return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
				}
				catch( Exception ex )
				{
					_logger.LogError(ex, $"ProductId: {model.ProductId} - UserId: {model.UserId}");
					return StatusCode(StatusCodes.Status500InternalServerError);
				}
			}

			return BadRequest();
		}
		#endregion

		#region REMOVE
		[HttpPost]
		public async Task<IActionResult> Remove([FromBody] StockNotificationDto model)
		{
			if( model != null )
			{
				try
				{
					IResult result = await _service.Remove(model);
					if( result.Success )
					{
						return Ok();
					}
					return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
				}
				catch( Exception ex )
				{
					_logger.LogError(ex, $"ProductId: {model.ProductId} - UserId: {model.UserId}");
					return StatusCode(StatusCodes.Status500InternalServerError);
				}
			}

			return BadRequest();
		}
		#endregion

		#region TRIGGER
		[HttpPost]
		public async Task<IActionResult> Trigger(StockNotificationTrigger model)
		{
			if( model != null )
			{
				try
				{
					_notificationHandler.SendNotification(model); 
					return Ok();
				}
				catch( Exception ex )
				{
					_logger.LogError(ex, $"ProductId: {model.ProductId}");
					return StatusCode(StatusCodes.Status500InternalServerError);
				}
			}

			return BadRequest();
		}
		#endregion
	}
}
