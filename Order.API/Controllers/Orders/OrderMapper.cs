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

		public OrderDTO OrderToOrderDTO(Domain.Orders.Order order)
		{
			var OrderItemList = new List<OrderItem>(Database.OrderItems.Where(item => item.OrderID == order.ID));

			var dto = new OrderDTO(order.ID, orderItemMapper.OrderItemListToOrderItemDTOList(OrderItemList), order.TotalPrice, order.CustomerID);
			return dto;
		}

		public List<OrderDTO> orderListToOrderDTOList(List<Domain.Orders.Order> orders)
		{
			var dtoList = new List<OrderDTO>();
			foreach (var order in orders)
			{
				dtoList.Add(OrderToOrderDTO(order));
			}
			return dtoList;
		}




	}
}
