using Microsoft.AspNetCore.Mvc;
using Order.Data;
using Order.Domain.Orders;
using System;
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
			return new OrderDTO(order.ID, orderItemMapper.ToItemGroupDTO(order.ItemGroups), order.TotalPrice, order.CustomerID); ;
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

		public  OrderReportDTO ToOrderReportDTO(Tuple<List<Domain.Orders.Order>, double> result)
		{
			(var item, var price) = result;
			List<OrderDTO> orderDTOs = ToDTOList(item);


			var reportDTO = new OrderReportDTO(orderDTOs, price);
			return reportDTO;
		}
	}
}
