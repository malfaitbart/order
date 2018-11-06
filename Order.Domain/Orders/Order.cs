﻿using System;
using System.Collections.Generic;

namespace Order.Domain.Orders
{
	public class Order
	{
		public int ID { get; private set; }
		public List<OrderItemGroup> ItemGroup { get; private set; }
		public double TotalPrice { get => CalculateTotalPrice(); }
		public int CustomerID { get; private set; }

		private static int CreateID;

		public Order(List<OrderItemGroup> itemGroup, int customerID)
		{
			ID = CreateID;
			ItemGroup = itemGroup;
			CustomerID = customerID;

			CreateID++;
		}

		private double CalculateTotalPrice()
		{
			double totalPrice = 0;
			foreach (var item in ItemGroup)
			{
				totalPrice += item.ItemPrice * item.Amount;
			}
			return totalPrice;
		}
	}
}
