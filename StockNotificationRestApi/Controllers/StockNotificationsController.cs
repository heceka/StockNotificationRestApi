#region USINGS
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockNotificationRestApi.Bll.Handlers.Concretes;
using StockNotificationRestApi.Bll.Services.Abstracts;
using StockNotificationRestApi.Core.Utilities.Results.Abstracts;
using StockNotificationRestApi.Entities.Concretes;
using StockNotificationRestApi.Entities.DTOs;
#endregion

namespace StockNotificationRestApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StockNotificationsController : ControllerBase
	{
		#region DEFINES
		private readonly IStockNotificationService _service;
		private readonly ILogger<StockNotificationsController> _log;
		private readonly NotificationHandler _notificationHandler;
		#endregion

		#region CONSTRUCTOR
		public StockNotificationsController(IStockNotificationService service, ILogger<StockNotificationsController> log,
			NotificationHandler notificationHandler)
		{
			_service = service;
			_log = log;
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
					_log.LogError(ex, $"ProductId: {model.ProductId} - UserId: {model.UserId}");
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
					_log.LogError(ex, $"ProductId: {model.ProductId} - UserId: {model.UserId}");
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
					IDataResult<List<StockNotification>> dataResult = await _notificationHandler.SendNotification(model);
					if( dataResult.Success )
					{
						await _service.RemoveAll(dataResult.Data);
						return Ok();
					}
					return StatusCode(StatusCodes.Status500InternalServerError, dataResult.Message);
				}
				catch( Exception ex )
				{
					_log.LogError(ex, $"ProductId: {model.ProductId}");
					return StatusCode(StatusCodes.Status500InternalServerError);
				}
			}

			return BadRequest();
		}
		#endregion
	}
}
