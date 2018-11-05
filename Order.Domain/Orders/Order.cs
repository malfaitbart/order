using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Orders
{
	public class Order
	{
		public int ID { get; private set; }
		public double TotalPrice { get; private set; }
		public int CustomerID { get; private set; }

		private static int CreateID;

		public Order(double totalPrice, int customerID)
		{
			ID = CreateID;
			TotalPrice = totalPrice;
			CustomerID = customerID;

			CreateID++;
		}
	}
}
