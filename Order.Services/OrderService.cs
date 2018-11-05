using Order.Data;
using Order.Domain.Items;
using Order.Domain.Orders;
using Order.Domain.Orders.Exceptions;
using Order.Domain.Users;
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

		public int CreateOrder(int userID, List<Order_Create> orderCreate)
		{
			double totalPrice = 0;
			List<OrderItem> orderItemsList = new List<OrderItem>();

			foreach (var orderdtocreate in orderCreate)
			{
				var itemtobeordered = itemService.GetByID(orderdtocreate.ItemID);

				CheckIfItemExistsInDatabase(orderdtocreate, itemtobeordered);

				totalPrice += itemtobeordered.Price * orderdtocreate.ItemAmount;

				orderItemsList.Add(new OrderItem(itemtobeordered, orderdtocreate.ItemAmount));
			}

			Domain.Orders.Order order = new Domain.Orders.Order(totalPrice, userID);
			Database.Orders.Add(order);

			SetOrderIDOnTheOrderedItemsAndStoreInDB(orderItemsList, order);

			return order.ID;
		}

		private static void SetOrderIDOnTheOrderedItemsAndStoreInDB(List<OrderItem> orderItemsList, Domain.Orders.Order order)
		{
			foreach (var orderitem in orderItemsList)
			{
				orderitem.SetOrderID(order.ID);
				Database.OrderItems.Add(orderitem);
			}
		}

		private static void CheckIfItemExistsInDatabase(Order_Create orderdtocreate, Item itemtobeordered)
		{
			if (itemtobeordered == null)
			{
				throw new OrderException($"The item with id {orderdtocreate.ItemID} does not exist.");
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
	}
}
