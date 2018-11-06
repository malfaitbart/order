using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Data;
using Order.Domain.Orders;
using Order.Services.Interfaces;

namespace Order.API.Controllers.Orders
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
		private readonly IItemService itemService;
		private readonly IOrderService orderService;
		private readonly IUserService userService;
		private readonly OrderMapper orderMapper;
		private readonly OrderItemMapper orderItemMapper;
		private readonly ILogger<OrdersController> logger;

		public OrdersController(IItemService itemService, IOrderService orderService, IUserService userService, OrderMapper orderMapper, OrderItemMapper orderItemMapper, ILogger<OrdersController> logger)
		{
			this.itemService = itemService;
			this.orderService = orderService;
			this.userService = userService;
			this.orderMapper = orderMapper;
			this.orderItemMapper = orderItemMapper;
			this.logger = logger;
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public ActionResult<List<OrderDTO>> GetAll()
		{
			var orderList = orderService.GetAll();
			return Ok(orderMapper.ToDTOList(orderList));
		}

		[Authorize(Roles = "Admin")]
		[HttpGet("{id}", Name = "GetOrder")]
		public ActionResult<OrderDTO> GetByID(int id)
		{
			var order = orderService.GetByID(id);
			if (order == null)
			{
				return NotFound();
			}
			return Ok(orderMapper.ToDTO(order));
		}

		[Authorize(Roles = "Customer, Admin")]
		[HttpGet]
		[Route("OrderReport")]
		public ActionResult<OrderReportDTO> GetOrderReportForCurrentUser()
		{
			var result = orderService.GetOrdersReport(GetUserIDFromHeader());

			return orderMapper.ToOrderReportDTO(result);
		}

		[Authorize(Roles ="Customer, Admin")]
		[HttpPost]
		public ActionResult CreateOrder([FromBody]List<IncomingOrderItemGroupDTO> incomingItemGroupDTO)
		{
			var incomingItemGroup = orderItemMapper.ToItemGroup(incomingItemGroupDTO);

			try
			{
			var createdOrderID = orderService
					.CreateOrder(
						GetUserIDFromHeader(), 
						incomingItemGroup);

			return CreatedAtRoute("GetOrder", new { id = createdOrderID}, orderMapper.ToDTO(orderService.GetByID(createdOrderID)));
			}
			catch (Exception ex)
			{
				var errorid = Guid.NewGuid();
				logger.LogError(errorid + " " + ex.Message);
				return BadRequest(errorid + " " + ex.Message);
			}
		}

		private int GetUserIDFromHeader() //naar auth helper
		{
			var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
			var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
			var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
			var username = credentials[0];
			var customerID = userService.GetUserID(username);
			return customerID;
		}
	}
}