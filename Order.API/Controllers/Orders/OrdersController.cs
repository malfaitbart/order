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
			return Ok(orderMapper.orderListToOrderDTOList(orderList));
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
			return Ok(orderMapper.OrderToOrderDTO(order));
		}

		[Authorize(Roles ="Customer, Admin")]
		[HttpPost]
		public ActionResult CreateOrder([FromBody]List<OrderDTO_Create> orderDTO_Create)
		{
			string username = GetUserNameFromHeader();

			var orderitemlist = new List<OrderItem>();
			foreach (var orderdtocreate in orderDTO_Create)
			{
				var itemtobeordered = itemService.GetByID(orderdtocreate.ItemID);
				if (itemtobeordered == null)
				{
					var errorid = Guid.NewGuid();
					logger.LogError(errorid + $" The requested itemID {orderdtocreate.ItemID} does not exist. Order is cancelled.");
					return BadRequest(errorid + $" The requested itemID {orderdtocreate.ItemID} does not exist. Order is cancelled.");
				}
				orderitemlist.Add(new OrderItem(itemtobeordered, orderdtocreate.ItemAmount));
			}

			try
			{
			var createdOrder = orderService.CreateOrder(userService.GetUserID(username), orderitemlist);

			return CreatedAtRoute("GetOrder", new { id = createdOrder}, orderMapper.OrderToOrderDTO(orderService.GetByID(createdOrder)));
			}
			catch (Exception ex)
			{
				var errorid = Guid.NewGuid();
				logger.LogError(errorid + " " + ex.Message);
				return BadRequest(errorid + " " + ex.Message);
			}


		}

		private string GetUserNameFromHeader()
		{
			var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
			var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
			var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
			var username = credentials[0];
			return username;
		}
	}
}