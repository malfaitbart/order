using Order.API.Controllers.Items;
using Order.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Controllers.Orders
{
	public class OrderItemMapper
	{
		public OrderItemDTO OrderItemToOrderItemDTO(OrderItem orderItem)
		{
			var dto = new OrderItemDTO(orderItem.ItemID, orderItem.ItemName, orderItem.ItemPrice, orderItem.Amount, orderItem.ShippingDate, orderItem.OrderID);
			return dto;
		} 

		public List<OrderItemDTO> OrderItemListToOrderItemDTOList(List<OrderItem> orderItems)
		{
			var dtoList = new List<OrderItemDTO>();
			foreach (var item in orderItems)
			{
				dtoList.Add(OrderItemToOrderItemDTO(item));
			}
			return dtoList;
		}

		public List<Order_Create> OrderItemDTOListToOrderItemList(List<OrderDTO_Create> orderDTO_Create)
		{
			var orderitemlist = new List<Order_Create>();
			foreach (var orderdto_create in orderDTO_Create)
			{
				orderitemlist.Add(new Order_Create(orderdto_create.ItemID, orderdto_create.ItemAmount));
			}
			return orderitemlist;
		}
	}
}
