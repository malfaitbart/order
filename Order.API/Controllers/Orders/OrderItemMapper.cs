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
		public OrderItemGroupDTO ToOrderItemDTO(OrderItemGroup orderItem)
		{
			var dto = new OrderItemGroupDTO(orderItem.ItemID, orderItem.ItemName, orderItem.ItemPrice, orderItem.ItemAmount, orderItem.ShippingDate, orderItem.ItemGroupTotalPrice);
			return dto;
		} 

		public List<OrderItemGroupDTO> ToItemGroupDTO(List<OrderItemGroup> orderItems)
		{
			var dtoList = new List<OrderItemGroupDTO>();
			foreach (var item in orderItems)
			{
				dtoList.Add(ToOrderItemDTO(item));
			}
			return dtoList;
		}

		public List<IncomingOrderItemGroup> ToItemGroup(List<IncomingOrderItemGroupDTO> itemGroupsDTO)
		{
			var itemGroup = new List<IncomingOrderItemGroup>();
			foreach (var incomingOrderItem in itemGroupsDTO)
			{
				itemGroup.Add(new IncomingOrderItemGroup(incomingOrderItem.ItemID, incomingOrderItem.ItemAmount));
			}
			return itemGroup;
		}
	}
}
