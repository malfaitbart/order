using Order.Data;
using Order.Domain.Orders;
using System.Collections.Generic;
using System.Linq;

namespace Order.API.Controllers.Orders
{
	public class OrderMapper
	{
		private readonly OrderItemMapper orderItemMapper;

		public OrderMapper(OrderItemMapper orderItemMapper)
		{
			this.orderItemMapper = orderItemMapper;
		}

		public OrderDTO ToDTO(Domain.Orders.Order order)
		{
			return new OrderDTO(order.ID, orderItemMapper.ToItemGroupDTO(order.ItemGroup), order.TotalPrice, order.CustomerID); ;
		}

		public List<OrderDTO> ToDTOList(List<Domain.Orders.Order> orders)
		{
			var dtoList = new List<OrderDTO>();
			foreach (var order in orders)
			{
				dtoList.Add(ToDTO(order));
			}
			return dtoList;
		}
	}
}
