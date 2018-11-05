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
		public int CreateOrder(int userID, List<OrderItem> orderItems)
		{
			double totalPrice = 0;
			foreach (var item in orderItems)
			{
				if(Database.Items.FirstOrDefault(find => find.ID == item.ItemID) == null)
				{
					throw new OrderException($"The item with id {item.ItemID} does not exist.");
				}
				totalPrice += item.ItemPrice * item.Amount;
			}

			Domain.Orders.Order order = new Domain.Orders.Order(totalPrice, userID);
			Database.Orders.Add(order);

			foreach (var orderitem in orderItems)
			{
				orderitem.SetOrderID(order.ID);
				Database.OrderItems.Add(orderitem);
			}

			return order.ID;
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
