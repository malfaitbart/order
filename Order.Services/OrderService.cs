using Order.Data;
using Order.Domain.Items;
using Order.Domain.Orders;
using Order.Domain.Orders.Exceptions;
using Order.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Services
{
	public class OrderService : IOrderService
	{
		private readonly IItemService itemService;

		public OrderService(IItemService itemService)
		{
			this.itemService = itemService;
		}

		public int CreateOrder(int customerID, List<IncomingOrderItemGroup> incomingOrderItems)
		{
			var itemGroup = new List<OrderItemGroup>();

			foreach (var item in incomingOrderItems)
			{
				var itemtobeordered = itemService.GetByID(item.ItemID);
				CheckIfItemExistsInDatabase(item, itemtobeordered);
				itemGroup.Add(new OrderItemGroup(itemtobeordered, item.ItemAmount));
			}
			Domain.Orders.Order order = new Domain.Orders.Order(itemGroup, customerID);
			Database.Orders.Add(order);
			return order.ID;
		}

		private void CheckIfItemExistsInDatabase(IncomingOrderItemGroup item, Item itemtobeordered)
		{
			if (itemtobeordered == null)
			{
				throw new OrderException($"The item with id {item.ItemID} does not exist. Order is cancelled.");
			}
		}

		private double CalculatePrice(double price, int amount)
		{
			return price * amount;
		}

		public List<Domain.Orders.Order> GetAll()
		{
			return Database.Orders;
		}

		public Domain.Orders.Order GetByID(int id)
		{
			return Database.Orders.FirstOrDefault(order => order.ID == id);
		}

		public Tuple<List<Domain.Orders.Order>, double> GetOrdersReport(int customerID)
		{
			var ordersList = new List<Domain.Orders.Order>();
			double totalPrice = 0;
			foreach (var order in Database.Orders)
			{
				if (order.CustomerID == customerID)
				{
					ordersList.Add(order);
					totalPrice += order.TotalPrice;
				}
			}
			return new Tuple<List<Domain.Orders.Order>, double>(ordersList, totalPrice);
		}

		public int ReOrder(int orderID, int customerID)
		{
			var originalOrder = Database.Orders.FirstOrDefault(find => find.ID == orderID);
			if (originalOrder == null)
			{
				throw new OrderException("OrderID does not exist. Reorder is cancelled.");
			}
			if (originalOrder.CustomerID != customerID)
			{
				throw new OrderException("This order can not be reordered by you. Reorder is cancelled.");
			}
			var itemGroups = new List<IncomingOrderItemGroup>();
			foreach (var itemgroup in originalOrder.ItemGroups)
			{
				itemGroups.Add(new IncomingOrderItemGroup(itemgroup.ItemID, itemgroup.ItemAmount));
			}
			return CreateOrder(customerID, itemGroups);

		}

		public List<Domain.Orders.Order> GetOrdersWithItemGroupsShipping(uint todayPlusShippingday)
		{
			var orderList = new List<Domain.Orders.Order>();
			foreach (var order in Database.Orders)
			{
				var itemgroup = order.ItemGroups.Any(find => find.ShippingDate == DateTime.Now.Date.AddDays(todayPlusShippingday));
				if (itemgroup)
				{
					orderList.Add(order);
				}
			}
			return orderList;
		}
	}
}
