using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Orders
{
	public class Order_Create
	{
		public int ItemID { get; set; }
		public int ItemAmount { get; set; }

		public Order_Create(int itemID, int itemAmount)
		{
			ItemID = itemID;
			ItemAmount = itemAmount;
		}
	}
}
