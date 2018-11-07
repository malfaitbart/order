using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
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
		public ActionResult<List<OrderDTO>> GetAll([FromQuery]uint? ShippingDay)
		{
			var orderList = new List<Domain.Orders.Order>();
			if (!ShippingDay.HasValue)
			{
				orderList = orderService.GetAll();
			}
			else
			{
				orderList = orderService.GetOrdersWithItemGroupsShipping(ShippingDay.Value);
			}
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

		[Authorize(Roles = "Customer, Admin")]
		[HttpPost]
		public ActionResult CreateOrder([FromBody]List<IncomingOrderItemGroupDTO> incomingItemGroupDTO)
		{
			var incomingItemGroup = orderItemMapper.ToItemGroup(incomingItemGroupDTO);

			var createdOrderID = orderService.CreateOrder(GetUserIDFromHeader(), incomingItemGroup);
			return CreatedAtRoute("GetOrder", new { id = createdOrderID }, orderMapper.ToDTO(orderService.GetByID(createdOrderID)));
		}

		[Authorize(Roles = "Customer, Admin")]
		[HttpPost("ReOrder/{id}")]
		public ActionResult ReOrder(int id)
		{
			var createdOrderID = orderService.ReOrder(id, GetUserIDFromHeader());
			return CreatedAtRoute("GetOrder", new { id = createdOrderID }, orderMapper.ToDTO(orderService.GetByID(createdOrderID)));
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